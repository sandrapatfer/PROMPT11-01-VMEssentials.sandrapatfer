using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sessao5
{
    public struct RationalNumber : IComparable<RationalNumber>, IEquatable<RationalNumber>
    {
        private readonly int _numerator, _denominator;

        public RationalNumber(int numerator, int denominator)
        {
            if (denominator == 0) throw new InvalidRationalException();
            _numerator = numerator;
            _denominator = denominator;
        }

        public static RationalNumber operator +(RationalNumber n1, RationalNumber n2)
        {
            int nn, nd;
            if (n1._denominator == n2._denominator)
            {
                nn = n1._numerator + n2._numerator;
                nd = n1._denominator;
            }
            else
            {
                nn = n1._numerator * n2._denominator + n2._numerator * n1._denominator;
                nd = n1._denominator * n2._denominator;
            }
            return new RationalNumber(nn, nd);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(_numerator);
            sb.Append('/');
            sb.Append(_denominator);
            return sb.ToString();
        }



        #region IComparable Members

        public int CompareTo(object obj)
        {
            RationalNumber r2 = (RationalNumber)obj;
            int i1 = (int)(_numerator / _denominator);
            int i2 = (int)(r2._numerator / r2._denominator);
            return i1.CompareTo(i2);
        }

        #endregion

        #region IComparable<RationalNumber> Members

        public int CompareTo(RationalNumber r2)
        {
            int i1 = (int)(_numerator / _denominator);
            int i2 = (int)(r2._numerator / r2._denominator);
            return i1.CompareTo(i2);
        }
        
        #endregion

        #region IEquatable<RationalNumber> Members

        public bool Equals(RationalNumber other)
        {
            return CompareTo(other) == 0;
        }

        #endregion

    }

    public class InvalidRationalException : Exception
    {}
}
