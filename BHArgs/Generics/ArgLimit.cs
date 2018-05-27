using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BeHappy.Args.Generics
{
    public class ArgLimit<T> : Arg where T : IConvertible, IComparable<T>
    {
        T _minimum;
        T _maximum;
        T _value;
        T _default;
        public ArgLimit(T value,T minimum, T maximum, string shortDesignation, string longDesignation, string help, string toolTip = "", bool required = false)
            : base(shortDesignation, longDesignation, help, toolTip, required)
        {
            this._minimum = minimum;
            this._maximum = maximum;
            this._value = value;
            this._default = value;
        }
        public bool SetValue(T value)
        {
            int com1 = value.CompareTo(_minimum);
            int com2 = value.CompareTo(_maximum);
            if (com1 * com2 > 0)
                return false;
            this._value = value;
            return true;
        }

        public void Restore()
        {
            this._value = this._default;
        }

        protected override void set(ref int i, string[] ps)
        {
            ++i;
            if (i < ps.Length)
            {
                try
                {
                    T value= (T)Convert.ChangeType(ps[i], typeof(T));

                    int com1 = value.CompareTo(_minimum);
                    int com2 = value.CompareTo(_maximum);
                    if (com1 * com2 > 0)
                        throw new OverflowException();
                    else
                        this._value = value;
                }
                catch
                {
                    string text = string.Format("wrong value for {0}/{1} argument: {2} ", this._shortDesignation, this._longDesignation, ps[i]);
                    if (Common.HasConsole())
                    {
                        Console.Error.WriteLine(text);
                        Environment.Exit(1);
                    }
                    else
                    {
                        int num = (int)MessageBox.Show(text, "Error");
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                string text = string.Format("there is no value for {0}/{1} argument", this._shortDesignation, this._longDesignation);
                if (Common.HasConsole())
                {
                    Console.Error.WriteLine(text);
                    Environment.Exit(1);
                }
                else
                {
                    MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                }
            }
        }

        public static implicit operator T(ArgLimit<T> arg)
        {
            return arg._value;
        }

        public static implicit operator bool(ArgLimit<T> arg)
        {
            return arg._isUsed;
        }

        public override string ToString()
        {
            return _isUsed ? String.Format("{0} is on [{1}; {2}]", _value.ToString(), _minimum.ToString(), _maximum.ToString()) : _isUsed.ToString();
        }
    }
}
