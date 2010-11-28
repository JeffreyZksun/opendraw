
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2007-10-22 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

// FRD - Encapsulate the built-in container

namespace FRContainer
{
    public class FRList<T> : List<T>
    {
        public bool Empty()
        {
            return (Count == 0);
        }

        public T Back()
        {
            return this[Count - 1];
        }

        public T PopBack()
        {
            T last = this[Count - 1];
            Remove(last);

            return last;
        }

        // RISK
        public bool Exists(T TItem)
        {
            foreach(T t in this)
            {
                // RISK - Maybe for some class, it can't judge whether 
                // the two objects are equal or not.
                if (TItem.Equals(t)) return true;
            }

            return false;
        }

        public void RemoveAll()
        {
            while(!Empty())
            {
                PopBack();
            }
        }
    } 
}
