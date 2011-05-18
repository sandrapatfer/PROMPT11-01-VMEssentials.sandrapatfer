using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5
{
    public static class Utils
    {
        public static IEnumerable<T> RemoveRepeated<T>(this IEnumerable<T> seq)
        {
            var l = new List<T>();
            foreach (var e in seq)
            {
                if (l.IndexOf(e) == -1)
                {
                    l.Add(e);
                    yield return e;
                }
            }
        }

        public static SortEnumerable<T> OrderBy<T, U>(this IEnumerable<T> seq, Func<T, U> key)
            where U : IComparable<U>
        {
            return new SortEnumerable<T>() { SeqToSort = seq, SortCriteria = ((t1, t2) => key(t1).CompareTo(key(t2)))};
        }

        public static IEnumerable<T> ThenBy<T, U>(this SortEnumerable<T> seq, Func<T, U> key)
            where U : IComparable<U>
        {
            return new SortEnumerable<T>()
            {
                SeqToSort = seq,
                SortCriteria = ((t1, t2) =>
                {
                    int prev = seq.SortCriteria(t1, t2);
                    if (prev != 0)
                        return prev;
                    else
                        return key(t1).CompareTo(key(t2));
                })
            };
        }

        public class SortEnumerable<T> : IEnumerable<T>
        {
            public IEnumerable<T> SeqToSort { get; set; }
            public Comparison<T> SortCriteria { get; set; }

            #region IEnumerable<T> Members

            public IEnumerator<T> GetEnumerator()
            {
                List<T> list = SeqToSort.ToList();
                list.Sort(SortCriteria);
                return list.GetEnumerator();
            }

            #endregion

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }

    }

}
