using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PhysicsTestbed
{
    public class BodyRepulse
    {
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "CCD time offset (pure visual)", Min = 0.01, Max = 0.99)]
        public static double ccdTimeOffset = 0.01;
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "0.01x CCD time offset", Min = -0.01, Max = 0.01)]
        public static double ccdTimeOffset001x = -0.01;
        [Controllable(Type = ControllableAttribute.ControllerType.Slider, Caption = "coefficient of elasticity", Min = 0.0, Max = 1.0)]
        public static double coefficientElasticity = 1.0; // TODO: make this coeffetient dependent on material types of both interacting bodies - use 2d-table for this

        public static double CCDTimeOffset { get{ return ccdTimeOffset + ccdTimeOffset001x; } }

        private const double epsilon = 0.00001;

        private bool CollideSweptSegments(LineSegment first, LineSegment second, ref Vector2 intersection)
        {
            double Ua, Ub;
            // Equations to determine whether lines intersect
            Ua = ((second.end.X - second.start.X) * (first.start.Y - second.start.Y) - (second.end.Y - second.start.Y) * (first.start.X - second.start.X)) /
                ((second.end.Y - second.start.Y) * (first.end.X - first.start.X) - (second.end.X - second.start.X) * (first.end.Y - first.start.Y));
            Ub = ((first.end.X - first.start.X) * (first.start.Y - second.start.Y) - (first.end.Y - first.start.Y) * (first.start.X - second.start.X)) /
                ((second.end.Y - second.start.Y) * (first.end.X - first.start.X) - (second.end.X - second.start.X) * (first.end.Y - first.start.Y));
            if (Ua >= 0.0f && Ua <= 1.0f && Ub >= 0.0f && Ub <= 1.0f)
            {
                intersection.X = first.start.X + Ua*(first.end.X - first.start.X);
                intersection.Y = first.start.Y + Ua*(first.end.Y - first.start.Y);
                return true;
            }
            return false;
        }

        private void CheckFrozenEdge(
            Vector2 origin, Vector2 neighbor,
            Vector2 pos, Vector2 posNext, Vector2 velocity, ref List<CollisionSubframeBuffer> collisionBuffer,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            Vector2 intersection = new Vector2();
            if (CollideSweptSegments(new LineSegment(origin, neighbor), new LineSegment(pos, posNext), ref intersection))
            {
                double particleOffset = (intersection - pos).Length();
                if (particleOffset > epsilon) // to prevent slipping of start point to just reflected edge // TODO: check if this condition is really usefull. may be (timeOffset > 0.0) is a sufficient condition
                {
                    // reflect velocity relative edge
                    Vector2 reflectSurface = neighbor - origin;
                    Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
                    if (reflectNormal.Dot(velocity) < 0) reflectNormal = -reflectNormal;
                    Vector2 newVelocity = velocity - (1.0 + coefficientElasticity) * reflectNormal * (reflectNormal.Dot(velocity) / reflectNormal.LengthSq());

                    subframeToAdd.vParticle = newVelocity;
                    subframeToAdd.vEdgeStart = new Vector2(0.0, 0.0);
                    subframeToAdd.vEdgeEnd = new Vector2(0.0, 0.0);
                    subframeToAdd.timeCoefficient = particleOffset / velocity.Length();
                    collisionBuffer.Add(subframeToAdd);
                }
            }
        }

        private CollisionSubframeBuffer GenerateContact_Reflection(
            Particle particle, Particle origin, Particle neighbor, Particle.CCDDebugInfo ccd, double ccdCollisionTime, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            double alpha = ccd.coordinateOfPointOnEdge;
            Vector2 velocityEdgeCollisionPoint = origin.v + (neighbor.v - origin.v) * alpha;
            Vector2 velocityPointRelativeEdge = particle.v - velocityEdgeCollisionPoint;

            // compute velocity reflection relativly moving edge
            Vector2 reflectSurface = ccd.edge.end - ccd.edge.start;
            Vector2 reflectNormal = new Vector2(-reflectSurface.Y, reflectSurface.X);
            if (reflectNormal.Dot(velocityPointRelativeEdge) < 0) reflectNormal = -reflectNormal;

            Vector2 newVelocity = 
                velocityPointRelativeEdge - (1.0 + coefficientElasticity) * reflectNormal * (reflectNormal.Dot(velocityPointRelativeEdge) / reflectNormal.LengthSq());
            if (ccdCollisionTime <= 0.0) Testbed.PostMessage(System.Drawing.Color.Red, "timeCoefficient = 0"); // Zero-Distance not allowed // DEBUG
            double newTimeCoefficient = timeCoefficientPrediction * ccdCollisionTime;
            newTimeCoefficient -= epsilon / newVelocity.Length(); // try to prevent Zero-Distances // HACK // TODO: check Length() > epsilon
            if (newTimeCoefficient < 0.0) newTimeCoefficient = 0.0; // don't move particle toward edge - just reflect velocity
            newVelocity += velocityEdgeCollisionPoint; // newVelocity should be in global coordinates

            subframeToAdd.vParticle = newVelocity;
            subframeToAdd.timeCoefficient = newTimeCoefficient;
            return subframeToAdd;
        }

        private CollisionSubframeBuffer GenerateContact_ImpulseConservation(
            Particle particle, Particle origin, Particle neighbor, Particle.CCDDebugInfo ccd, double ccdCollisionTime, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            double alpha = ccd.coordinateOfPointOnEdge;
            Vector2 edge = neighbor.x - origin.x;
            double edgeLengthSq = edge.LengthSq();
            if (edgeLengthSq < epsilon) // don't collide with too short edges // TODO: figure out how to solve this case
                return null;

            //  1. find mass of EdgeCollisionPoint:
            //    double massEdgeCollisionPoint = origin.mass + neighbor.mass; // ??? // mass of virtual point // alternative:
            //                                                                                                      m = origin.mass,                    alpha = 0
            //                                                                                                      m = origin.mass + neighbor.mass,    alpha = neighbor.mass/(origin.mass + neighbor.mass)
            //                                                                                                      m = neighbor.mass,                  alpha = 1
            //
            //  2. use rule of impact for 2 bodies - it defines normal components of velocity of EdgeCollisionPoint (v2) and collisionParticle (v1). tangent components of velocities have no changes.
            //          v1new = v1 - m2*(v1-v2)*(1+k)/(m1+m2)
            //          v2new = v2 + m1*(v1-v2)*(1+k)/(m1+m2)
            //
            //      k is a coefficient of elasticity, it belongs to [0..1]
            //      note that system lose some kinetic energy: dT = (0.5*(1-k^2)*m1*m2*(v1-v2)^2)/(m1+m2)
            //
            //  3. find new origin.v and new neighbor.v (origin.v' and neighbor.v') from found velocity of EdgeCollisionPoint
            //          // Rule of distribution for the EdgeCollisionPoint velocity on origin and neighbor
            //          velocityEdgeCollisionPoint' = origin.v' + (neighbor.v' - origin.v') * ccd.coordinateOfPointOnEdge				// linear velocity distribution on edge
            //          massEdgeCollisionPoint * velocityEdgeCollisionPoint' = origin.mass * origin.v' + neighbor.mass * neighbor.v'	// impulse of virtual point = sum of impulses of edge-vertex points
            //																															//		to distribute velocity from virtual points to edge vertices with impulse conservation

            double alphaCenterOfMass = neighbor.mass / (origin.mass + neighbor.mass);
            double betaLeft = alpha / alphaCenterOfMass;
            double betaRight = (alpha - alphaCenterOfMass) / (1.0 - alphaCenterOfMass);

            /**/
            double massEdgeCollisionPoint = origin.mass + neighbor.mass;        // simple mass approach
            /*/
            double massEdgeCollisionPoint = alpha < alphaCenterOfMass ?       // complex mass approach
                origin.mass * (1.0 - betaLeft) + (origin.mass + neighbor.mass) * betaLeft :
                (origin.mass + neighbor.mass) * (1.0 - betaRight ) + neighbor.mass * betaRight;
            /**/

            Vector2 velocityEdgeCollisionPoint = origin.v + (neighbor.v - origin.v) * alpha;
            Vector2 velocityEdgeCollisionPoint_Tangent = (velocityEdgeCollisionPoint.Dot(edge) / edgeLengthSq) * edge;
            Vector2 velocityEdgeCollisionPoint_Normal = velocityEdgeCollisionPoint - velocityEdgeCollisionPoint_Tangent;
            Vector2 velocityParticle_Tangent = (particle.v.Dot(edge) / edgeLengthSq) * edge;
            Vector2 velocityParticle_Normal = particle.v - velocityParticle_Tangent;

            Vector2 newVelocityECP_Tangent = velocityEdgeCollisionPoint_Tangent; // it means that we have no perticle-edge friction // TODO: implement some friction model
            Vector2 newVelocityECP_Normal = velocityEdgeCollisionPoint_Normal +
                ((1.0 + coefficientElasticity) * particle.mass / (particle.mass + massEdgeCollisionPoint)) * (velocityParticle_Normal - velocityEdgeCollisionPoint_Normal);
            Vector2 newVelocityECP = newVelocityECP_Tangent + newVelocityECP_Normal;

            Vector2 newVelocityParticle_Tangent = velocityParticle_Tangent; // it means that we have no perticle-edge friction // TODO: implement some friction model
            Vector2 newVelocityParticle_Normal = velocityParticle_Normal -
                ((1.0 + coefficientElasticity) * massEdgeCollisionPoint / (particle.mass + massEdgeCollisionPoint)) * (velocityParticle_Normal - velocityEdgeCollisionPoint_Normal);
            Vector2 newVelocityParticle = newVelocityParticle_Tangent + newVelocityParticle_Normal;

            if (ccdCollisionTime <= 0.0) Testbed.PostMessage(System.Drawing.Color.Red, "timeCoefficient = 0"); // Zero-Distance not allowed // DEBUG
            double newTimeCoefficient = timeCoefficientPrediction * ccdCollisionTime;
            newTimeCoefficient -= epsilon / (newVelocityParticle - newVelocityECP).Length(); // try to prevent Zero-Distances // HACK // TODO: check Length() > epsilon
            if (newTimeCoefficient < 0.0) newTimeCoefficient = 0.0; // don't move particle toward edge - just reflect velocity

            Vector2 newVelocityOrigin = alpha < alphaCenterOfMass ?
                newVelocityECP * (1.0 - betaLeft) + (origin.v + newVelocityECP - velocityEdgeCollisionPoint) * betaLeft :
                (origin.v + newVelocityECP - velocityEdgeCollisionPoint) * (1.0 - betaRight) + origin.v * betaRight;
            Vector2 newVelocityNeighbor = alpha < alphaCenterOfMass ?
                neighbor.v * (1.0 - betaLeft) + (neighbor.v + newVelocityECP - velocityEdgeCollisionPoint) * betaLeft :
                (neighbor.v + newVelocityECP - velocityEdgeCollisionPoint) * (1.0 - betaRight) + newVelocityECP * betaRight;

            subframeToAdd.vParticle = newVelocityParticle;
            subframeToAdd.vEdgeStart = newVelocityOrigin;
            subframeToAdd.vEdgeEnd = newVelocityNeighbor;
            subframeToAdd.timeCoefficient = newTimeCoefficient;
            return subframeToAdd;
        }

        private void CheckParticleEdge_D2D(
            ref List<Particle.CCDDebugInfo> ccds,
            Particle particle,
            Particle origin, Particle neighbor,
            ref List<CollisionSubframeBuffer> collisionBuffer, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            // TODO: avoid code coping (see below)
            Vector2 pos = particle.x;
            Vector2 velocity = particle.v;
            Vector2 posNext = pos + velocity * timeCoefficientPrediction;

            // swept collision for body
            LineSegment edge = new LineSegment(origin.x, neighbor.x);
            LineSegment edgeNext = new LineSegment(edge.start + origin.v * timeCoefficientPrediction, edge.end + neighbor.v * timeCoefficientPrediction);

            EdgePointCCDSolver.SolverInput solverInput = new EdgePointCCDSolver.SolverInput(edge, edgeNext, pos, posNext);
            double? ccdCollisionTime = EdgePointCCDSolver.Solve(solverInput);

            if (ccdCollisionTime != null)
            {
                Particle.CCDDebugInfo ccd = GenerateDebugInfo(solverInput, ccdCollisionTime.Value);
                CollisionSubframeBuffer contact = // TODO: use the Rule of conservative impulse to handle this case. Simple reflection rule is not effective here.
                    GenerateContact_ImpulseConservation(
                    // GenerateContact_Reflection(
                        particle, origin, neighbor, ccd, ccdCollisionTime.Value, timeCoefficientPrediction, subframeToAdd
                    );
                if (contact != null)
                {
                    collisionBuffer.Add(contact);
                }
                ccds.Add(ccd);

                if (LsmBody.pauseOnBodyBodyCollision)
                    Testbed.Paused = true;
            }
        }

        Particle.CCDDebugInfo GenerateDebugInfo(EdgePointCCDSolver.SolverInput solverInput, double time) // DEBUG
        {
            Particle.CCDDebugInfo ccd = new Particle.CCDDebugInfo(
                solverInput.currentPoint + (solverInput.nextPoint - solverInput.currentPoint) * time,
                new LineSegment(
                    solverInput.currentEdge.start + (solverInput.nextEdge.start - solverInput.currentEdge.start) * time,
                    solverInput.currentEdge.end + (solverInput.nextEdge.end - solverInput.currentEdge.end) * time
                )
            );
            return ccd;
        }

        private void CheckParticleEdge_D2F(
            ref List<Particle.CCDDebugInfo> ccds,
            Particle particle,
            Particle origin, Particle neighbor,
            ref List<CollisionSubframeBuffer> collisionBuffer, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            // TODO: avoid code coping
            Vector2 pos = particle.x;
            Vector2 velocity = particle.v;
            Vector2 posNext = pos + velocity * timeCoefficientPrediction;

            // simple collision for frozen body
            CheckFrozenEdge(origin.goal, neighbor.goal, pos, posNext, velocity, ref collisionBuffer, subframeToAdd); // current edge position
        }

        private CollisionSubframeBuffer GenerateContact_ImpulseConservation_F2D(
            Particle particle, Particle origin, Particle neighbor, Particle.CCDDebugInfo ccd, double ccdCollisionTime, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            double alpha = ccd.coordinateOfPointOnEdge;
            Vector2 edge = neighbor.x - origin.x;
            double edgeLengthSq = edge.LengthSq();
            if (edgeLengthSq < epsilon)
                return null;

            double alphaCenterOfMass = neighbor.mass / (origin.mass + neighbor.mass);
            double betaLeft = alpha / alphaCenterOfMass;
            double betaRight = (alpha - alphaCenterOfMass) / (1.0 - alphaCenterOfMass);

            /**/
            double massEdgeCollisionPoint = origin.mass + neighbor.mass;    // simple mass approach
            /*/
            double massEdgeCollisionPoint = alpha < alphaCenterOfMass ?     // complex mass approach
                origin.mass * (1.0 - betaLeft) + (origin.mass + neighbor.mass) * betaLeft :
                (origin.mass + neighbor.mass) * (1.0 - betaRight ) + neighbor.mass * betaRight;
            /**/

            Vector2 velocityEdgeCollisionPoint = origin.v + (neighbor.v - origin.v) * alpha;
            Vector2 velocityEdgeCollisionPoint_Tangent = (velocityEdgeCollisionPoint.Dot(edge) / edgeLengthSq) * edge;
            Vector2 velocityEdgeCollisionPoint_Normal = velocityEdgeCollisionPoint - velocityEdgeCollisionPoint_Tangent;
            Vector2 velocityParticle_Tangent = Vector2.ZERO;
            Vector2 velocityParticle_Normal = Vector2.ZERO;
            // particle.mass = infinity
            // particle.v = newVelocityParticle = Vector2.ZERO

            Vector2 newVelocityECP_Tangent = velocityEdgeCollisionPoint_Tangent; // it means that we have no particle-edge friction // TODO: implement some friction model
            Vector2 newVelocityECP_Normal = -(1.0 + coefficientElasticity) * velocityEdgeCollisionPoint_Normal;
            Vector2 newVelocityECP = newVelocityECP_Tangent + newVelocityECP_Normal;

            Vector2 newVelocityParticle = Vector2.ZERO;

            if (ccdCollisionTime <= 0.0) Testbed.PostMessage(System.Drawing.Color.Red, "timeCoefficient = 0"); // Zero-Distance not allowed // DEBUG
            double newTimeCoefficient = timeCoefficientPrediction * ccdCollisionTime;
            newTimeCoefficient -= epsilon / (newVelocityParticle - newVelocityECP).Length(); // try to prevent Zero-Distances // HACK // TODO: check Length() > epsilon
            if (newTimeCoefficient < 0.0) newTimeCoefficient = 0.0; // don't move particle toward edge - just reflect velocity

            Vector2 newVelocityOrigin = alpha < alphaCenterOfMass ?
                newVelocityECP * (1.0 - betaLeft) + (origin.v + newVelocityECP - velocityEdgeCollisionPoint) * betaLeft :
                (origin.v + newVelocityECP - velocityEdgeCollisionPoint) * (1.0 - betaRight) + origin.v * betaRight;
            Vector2 newVelocityNeighbor = alpha < alphaCenterOfMass ?
                neighbor.v * (1.0 - betaLeft) + (neighbor.v + newVelocityECP - velocityEdgeCollisionPoint) * betaLeft :
                (neighbor.v + newVelocityECP - velocityEdgeCollisionPoint) * (1.0 - betaRight) + newVelocityECP * betaRight;

            subframeToAdd.vParticle = newVelocityParticle;
            subframeToAdd.vEdgeStart = newVelocityOrigin;
            subframeToAdd.vEdgeEnd = newVelocityNeighbor;
            subframeToAdd.timeCoefficient = newTimeCoefficient;
            return subframeToAdd;
        }

        private void CheckParticleEdge_F2D(
            ref List<Particle.CCDDebugInfo> ccds,
            Particle particle,
            Particle origin, Particle neighbor,
            ref List<CollisionSubframeBuffer> collisionBuffer, double timeCoefficientPrediction,
            CollisionSubframeBuffer subframeToAdd
        )
        {
            // swept collision for body
            LineSegment edge = new LineSegment(origin.x, neighbor.x);
            LineSegment edgeNext = new LineSegment(edge.start + origin.v * timeCoefficientPrediction, edge.end + neighbor.v * timeCoefficientPrediction);

            EdgePointCCDSolver.SolverInput solverInput = new EdgePointCCDSolver.SolverInput(edge, edgeNext, particle.x, particle.x);
            double? ccdCollisionTime = EdgePointCCDSolver.Solve(solverInput);

            if (ccdCollisionTime != null)
            { 
                Particle.CCDDebugInfo ccd = GenerateDebugInfo(solverInput, ccdCollisionTime.Value);
                CollisionSubframeBuffer contact =
                    GenerateContact_ImpulseConservation_F2D(
                        particle, origin, neighbor, ccd, ccdCollisionTime.Value, timeCoefficientPrediction,
                        subframeToAdd
                    );
                if (contact != null)
                {
                    collisionBuffer.Add(contact);
                }
                ccds.Add(ccd);

                if (LsmBody.pauseOnBodyBodyCollision)
                    Testbed.Paused = true;
            }
        }

        public void ApplyImpulse(
            LsmBody applyBody, Particle applyParticle, LsmBody otherBody, // HACK // TODO: try to don't use such information for collisions or formilize this ussage
            double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer // HACK // TODO: remove ref List<>
        )
        {
            Debug.Assert(!applyBody.Equals(otherBody)); // don't allow self-collision here
            Debug.Assert(!applyBody.Frozen && !otherBody.Frozen);

            // iterate all possible edges of body and test them with current subframed point
            foreach (Particle bodyt in otherBody.particles)
            {
                if (bodyt.xPos != null)
                {
                    CheckParticleEdge_D2D(
                        ref applyParticle.ccdDebugInfos, applyParticle, bodyt, bodyt.xPos, 
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(bodyt, bodyt.xPos), bodyt.v, bodyt.xPos.v, 0.0)
                    );
                }
                if (bodyt.yPos != null)
                {
                    CheckParticleEdge_D2D(
                        ref applyParticle.ccdDebugInfos, applyParticle, bodyt, bodyt.yPos, 
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(bodyt, bodyt.yPos), bodyt.v, bodyt.yPos.v, 0.0)
                    );
                }
            }
        }

        public void ApplyImpulse_DynamicToFrozen(LsmBody applyBody, Particle applyParticle, LsmBody otherBody, double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer)
        {
            Debug.Assert(!applyBody.Equals(otherBody)); // don't allow self-collision here
            Debug.Assert(!applyBody.Frozen && otherBody.Frozen);

            foreach (Particle bodyt in otherBody.particles)
            {
                if (bodyt.xPos != null)
                {
                    CheckParticleEdge_D2F(
                        ref applyParticle.ccdDebugInfos, applyParticle, bodyt, bodyt.xPos,
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(bodyt, bodyt.xPos), bodyt.v, bodyt.xPos.v, 0.0)
                    );
                }
                if (bodyt.yPos != null)
                {
                    CheckParticleEdge_D2F(
                        ref applyParticle.ccdDebugInfos, applyParticle, bodyt, bodyt.yPos,
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(bodyt, bodyt.yPos), bodyt.v, bodyt.yPos.v, 0.0)
                    );
                }
            }
        }

        public void ApplyImpulse_FrozenToDynamic(LsmBody otherBody, Particle otherParticle, LsmBody applyBody, double timeCoefficientPrediction, ref List<CollisionSubframeBuffer> collisionBuffer)
        {
            Debug.Assert(!applyBody.Equals(otherBody)); // don't allow self-collision here
            Debug.Assert(applyBody.Frozen && !otherBody.Frozen);

            foreach (Particle applyParticle in applyBody.particles)
            {
                if (otherParticle.xPos != null)
                {
                    CheckParticleEdge_F2D(
                        ref applyParticle.ccdDebugInfos, applyParticle, otherParticle, otherParticle.xPos,
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(otherParticle, otherParticle.xPos), otherParticle.v, otherParticle.xPos.v, 0.0)
                    );
                }
                if (otherParticle.yPos != null)
                {
                    CheckParticleEdge_F2D(
                        ref applyParticle.ccdDebugInfos, applyParticle, otherParticle, otherParticle.yPos,
                        ref collisionBuffer, timeCoefficientPrediction,
                        new CollisionSubframeBuffer(applyParticle, applyParticle.v, new Edge(otherParticle, otherParticle.yPos), otherParticle.v, otherParticle.yPos.v, 0.0)
                    );
                }
            }
        }
    }
}
