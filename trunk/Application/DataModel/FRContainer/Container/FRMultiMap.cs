
//////////////////////////////////////////////////////////////////////////
//
// Author: Sun Zhongkui
//
// History:
//  2008-8-3 Create
//
//////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;

// FRD - Encapsulate the built-in container

namespace FRContainer
{
    public class FRMultiMap<TKey, TValue> : FRList<KeyValuePair<TKey, TValue>> where TKey : System.IComparable
    {
        public FRMultiMap()
        {
            m_IsSortAscending = true;
        }

        public FRMultiMap(bool IsAscending)
        {
            m_IsSortAscending = IsAscending;
        }

        public void AddItem(TKey key, TValue value)
        {
            KeyValuePair<TKey, TValue> item = new KeyValuePair<TKey, TValue>(key, value);

            int index = FindPosition(item);
            Insert(index, item);

        }

        private int FindPosition(KeyValuePair<TKey, TValue> item)
        {
            int index = 0;

            // Find the first item in the list greater than [Ascend]
            // or less than [Descend] the inserted item.
            // For Ascend, if we meet the same key, 
            // we insert the new item after the same key.
            // For Descend, if we meet the same key, 
            // we insert the new item after the same key.
            for (int i = 0; i < Count; i++)
            {
                KeyValuePair<TKey, TValue> existItem = this[i];
                int compareRet = item.Key.CompareTo(existItem.Key);
                if (m_IsSortAscending ? compareRet < 0 : compareRet > 0)
                    break;
                else
                    index = i + 1;
            }

            return index;
        }

        public List<TValue> Values
        {
            get
            {
                List<TValue> TheValues = new List<TValue>();

                foreach (KeyValuePair<TKey, TValue> item in this)
                {
                    TheValues.Add(item.Value);
                }

                return TheValues;
            }
        }

        private bool m_IsSortAscending;
    }
}
