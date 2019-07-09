using System.Collections.Generic;

namespace Laba2.MyCollections
{
    interface IMyCollection<T> : IList<T>
    {
        void Sort(IComparer<T> comparer);
    }
}
