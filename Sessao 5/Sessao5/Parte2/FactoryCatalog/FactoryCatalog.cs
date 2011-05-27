using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FactoryCatalog
{
    public class FactoryCatalog
    {
//        public List<FactoryMap<I, T>> BindingList { get; set; }
        Hashtable BindingTable;
        public FactoryCatalog()
        {
            BindingTable = new Hashtable();
        }
        public FactoryMap Bind<I, T>()
        {
            FactoryMap map = new FactoryMap() {};
            map.Bind<I, T>();
//           BindingList.Add(map);
            Type t = typeof(T);
            BindingTable.Add(t, map);
            return map;
        }

        public I CreateInstance<I>()
        {
            Type i = typeof(I);
            FactoryMap map = (FactoryMap)BindingTable[i];
            return map.CreateInstance();
        }
    }

    public class FactoryMap
    {
        Func<I> constructor;
        public void Bind<I, T>()
        {
            constructor = (() => { return new T(); });
        }
        public I CreateInstance<I>()
        {
            return constructor;
        }
    }
}
