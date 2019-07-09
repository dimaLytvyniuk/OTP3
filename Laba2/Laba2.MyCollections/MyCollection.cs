using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Laba2.MyCollections
{
    public class MyCollection<T> : IMyCollection<T>
    {
        // Dynamic array
        private T[] array;
        // Current size of array
        private int size;

        // Gets the number of elements contained in the collection
        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public MyCollection()
        {
            size = 20;
            Count = 0;
            array = new T[size];
            List<int> list = new List<int>();
        }

        // Gets or sets the element at the specified index.
        public T this[int index]

        {
            get
            {
                if (IsIndexNotInRange(index))
                    throw new ArgumentOutOfRangeException();

                return array[index];
            }
            set
            {
                if (IsReadOnly)
                    throw new NotSupportedException();

                if (IsIndexNotInRange(index))
                    throw new ArgumentOutOfRangeException();

                array[index] = value;
            }
        }

        // Adds an object to the end of the collection
        public void Add(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            if (size == Count)
                ExpandArray();

            array[Count] = item;
            Count++;
        }

        // Removes all elements from collection
        public void Clear()
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            Count = 0;
            size = 20;
            array = new T[size];
        }

        // Determines whether an element is in the collection
        public bool Contains(T item)
        {
            foreach(var element in array)
            {
                if (element.Equals(item))
                    return true;
            }
            return false;
        }

        // Copies the entire collection to a compatible one-dimensional
        // array, starting at the specified index of the target array.
        public void CopyTo(T[] destinationArray, int arrayIndex)
        {
            if (destinationArray == null)
                throw new ArgumentNullException();
            if (arrayIndex < 0 || arrayIndex > destinationArray.Length)
                throw new ArgumentOutOfRangeException();
            if ((destinationArray.Length - arrayIndex) < Count)
                throw new ArgumentException();

            Array.Copy(array, 0, destinationArray, arrayIndex, Count);
        }

        // Returns an enumerator that iterates through the collection
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return array[i];
            }
        }

        // Searches for the specified object and returns the zero-based index of the first
        // occurrence within the entire colllection.
        public int IndexOf(T item)
        {
            for (var i = 0; i < Count; i++)
            {
                if (array[i].Equals(item))
                    return i;
            }

            return -1;
        }

        // Inserts an element into the collection at the specified index.
        public void Insert(int index, T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (IsIndexNotInRange(index))
                throw new ArgumentOutOfRangeException();

            if (size == Count)
                ExpandArray();

            Array.Copy(array, index, array, index + 1, Count - index);
            array[index] = item;
            Count++;
        }

        // Removes the first occurrence of a specific object from the collection
        public bool Remove(T item)
        {
            if (IsReadOnly)
                throw new NotSupportedException();

            for (var i = 0; i < Count; i++)
            {
                if (array[i].Equals(item))
                {
                    Array.Copy(array, i + 1, array, i , Count - i - 1);
                    Count--;
                    return true;
                }
            }
            return false;
        }

        // Removes the element at the specified index of the
        public void RemoveAt(int index)
        {
            if (IsReadOnly)
                throw new NotSupportedException();
            if (IsIndexNotInRange(index))
                throw new ArgumentOutOfRangeException();

            Array.Copy(array, index + 1, array, index, Count - index - 1);
            Count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // Sorts the elements in the entire collection using the specified comparer
        public void Sort(IComparer<T> comparer)
        {
            if (comparer == null)
                throw new InvalidOperationException();

            try
            {
                Array.Sort(array, comparer);
            }
            catch
            {
                throw new ArgumentException();
            }
        }

        private void ExpandArray()
        {
            size = (int)(size * 1.3);
            Array.Resize(ref array, size);
        }

        private bool IsIndexNotInRange(int index)
        {
            return (index < 0 || index > (Count - 1));
        }
    }
}
