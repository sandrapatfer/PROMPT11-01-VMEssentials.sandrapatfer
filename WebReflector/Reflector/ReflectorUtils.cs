using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflector.Reflector
{
    public static class ReflectorUtils
    {
        public static List<U> ConvertAll<T, U>(this SortedDictionary<string, T> dic, Converter<T, U> conv)
        {
            var list = new List<U>();
            foreach (var key in dic.Keys)
            {
                list.Add(conv(dic[key]));
            }
            return list;
        }
    }
}
