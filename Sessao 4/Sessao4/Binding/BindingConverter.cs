using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Binding
{
    public interface IBindingConverter
    {
        bool TryConvertFrom(string value, out object convertedValue);
    }

    public class PointConverter : IBindingConverter
    {
        public bool TryConvertFrom(string value, out object convertedValue)
        {
            string[] parts = value.Split(new char[] { ',' });
            if (parts.Length == 2)
            {
                int x = Convert.ToInt32(parts[0]);
                int y = Convert.ToInt32(parts[1]);
                convertedValue = new Point(x, y);
                return true;
            }
            else
            {
                convertedValue = null;
                return false;
            }
        }
    }
}
