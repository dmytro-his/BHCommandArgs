using System.Globalization;
using System.Threading;
using System.Windows;

namespace BeHappy.Args
{
    public abstract class Arg
    {
        protected string _shortDesignation;
        protected string _longDesignation;
        protected string _toolTip;
        protected string _help;
        protected bool _required;
        protected bool _isUsed;
        public string ShortDesignation { get => _shortDesignation; }
        public string LongDesignation { get => _longDesignation; }
        public string ToolTip { get => _toolTip; }
        public string Help { get => _help; }
        public bool IsRequired { get => _required; }
        public bool IsUsed{ get => _isUsed; }
        protected Arg(string shortDesignation, string longDesignation, string help, string toolTip, bool required)
        {
            this._shortDesignation = shortDesignation;
            this._longDesignation = longDesignation;
            this._toolTip = toolTip;
            this._help = help;
            this._required = required;
        }

        public bool Check(ref int i, string[] ps)
        {
            bool flag = false;
            if (i < ps.Length)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU", false);
                Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = ".";
                string p = ps[i];
                if (p[0] == '-' || p[0] == '/')
                {
                    string lower = p.Substring(1).ToLower();
                    if (lower == this._shortDesignation.ToLower() || lower == this._longDesignation.ToLower())
                    {
                        this.set(ref i, ps);
                        flag = true;
                        _isUsed = true;
                    }
                }
            }
            return flag;
        }
        protected abstract void set(ref int i, string[] ps);
        
        
    }
}
    