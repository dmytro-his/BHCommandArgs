using System;
using System.Windows;

namespace BeHappy.Args
{
    [Obsolete("Class is deprecated, please use Arg<string> from Generics instead.)")]
    public class ArgString : Arg
    {
        string _value;
        string _default;
        public ArgString(string value, string shortDesignation, string longDesignation, string help, string toolTip="", bool required=false)
            : base(shortDesignation, longDesignation,  help, toolTip, required)
        {
            this._value = value;
            this._default = value;
        }
        public void SetValue(string value)
        {
            this._value = value;
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
                this._value = ps[i];
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
        
        public static implicit operator string(ArgString argString)
        {
            return argString._value;
        }


        public static implicit operator bool(ArgString argString)
        {
            return argString._isUsed;
        }

        public override string ToString()
        {
            return _isUsed ? _value : _isUsed.ToString();
        }
    }
}
