using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PhysicsTestbed
{
	public class ManagedList<T> : IEnumerable<T>
	{
		protected List<T> objects = new List<T>();
		protected List<T> deadObjects = new List<T>();
		protected List<T> newObjects = new List<T>();
		protected int numActiveEnumerators = 0;

		class ManagedListEnumerator : IEnumerator<T>, IEnumerator
		{
			ManagedList<T> daddy;
			IEnumerator<T> listEnumerator;

			public ManagedListEnumerator(ManagedList<T> daddy)
			{
				this.daddy = daddy;
				daddy.IncrementNumEnumerators();
				listEnumerator = daddy.objects.GetEnumerator();
			}

			public bool MoveNext()
			{
				if (!listEnumerator.MoveNext()) return false;
				while (daddy.deadObjects.Contains(listEnumerator.Current))
				{
					if (!listEnumerator.MoveNext()) return false;
				}

				return true;
			}

			public void Reset()
			{
				listEnumerator.Reset();
			}

			public T Current
			{
				get
				{
					return listEnumerator.Current;
				}
			}

			Object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}

			public void Dispose()
			{
				daddy.DecrementNumEnumerators();
			}
		}

		public int Count
		{
			get
			{
				return objects.Count - deadObjects.Count;
			}
		}

		public void Add(T o)
		{
			if (numActiveEnumerators == 0)
			{
				objects.Add(o);
			}
			else
			{
				newObjects.Add(o);
			}
		}

		public void Remove(T o)
		{
			//System.Diagnostics.Trace.Assert(objects.Contains(o), "Attempting to remove object " + o + ", but it is not in the list");
			if (numActiveEnumerators == 0)
			{
				objects.Remove(o);
			}
			else
			{
				deadObjects.Add(o);
			}
		}

		protected void Clean()
		{
			foreach (T o in deadObjects)
			{
				objects.Remove(o);
			}
			foreach (T o in newObjects)
			{
				objects.Add(o);
			}
			deadObjects.Clear();
			newObjects.Clear();
		}

		public void Sort()
		{
			objects.Sort();
		}

		public T this[int index]
		{
			get
			{
				if (index > Count)
					throw new System.ArgumentOutOfRangeException("index", index, "Index out of bounds");

				IEnumerator<T> enumerator = GetEnumerator();
				for(int i = 0; i <= index; i++)
				{
					enumerator.MoveNext();
				}
				return enumerator.Current;
			}
			set
			{
				if (index > Count)
					throw new System.ArgumentOutOfRangeException("index", index, "Index out of bounds");

				IEnumerator enumerator = GetEnumerator();
				int j = 0;
				for(int i = 0; i <= index; i++)
				{
					while(deadObjects.Contains(objects[j]))
						j++;
					j++;
				}
				objects[j] = value;
			}
		}

		public int IndexOf(T o)
		{
			int index = 0;
			foreach (T test in this)
			{
				if (test.Equals(o))
					return index;
				index++;
			}
			return -1;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new ManagedListEnumerator(this);
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new Exception("BZZT");
		}

		protected void IncrementNumEnumerators()
		{
			numActiveEnumerators++;
		}

		protected void DecrementNumEnumerators()
		{
			numActiveEnumerators--;

			System.Diagnostics.Trace.Assert(numActiveEnumerators >= 0);

			if (numActiveEnumerators == 0)
			{
				Clean();
			}
		}

		public bool Contains(T o)
		{
			return ((objects.Contains(o) || newObjects.Contains(o)) && !deadObjects.Contains(o));
		}
	}
}
