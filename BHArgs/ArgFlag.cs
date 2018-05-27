namespace BeHappy.Args
{
    public class ArgFlag : Arg
    {
        bool _value;
        bool _default;

        public ArgFlag(bool value, string shortDesignation, string longDesignation, string help, string toolTip = "", bool required = false)
            : base(shortDesignation, longDesignation, help, toolTip, required)
        {
            this._value = value;
            this._default = value;
        }
        public void SetValue(bool value)
        {
            this._value = value;
        }
        
        public void Toggle()
        {
            this._value = !this._default;
        }
        public void Restore()
        {
            this._value = this._default;
        }

        protected override void set(ref int i, string[] ps)
        {
            this.Toggle();
        }
        public void Set(bool value)
        {
            this._value = value;
        }

        public static implicit operator bool(ArgFlag argFlag)
        {
            return argFlag._value;
        }
        public override string ToString()
        {
            return _isUsed ? _value.ToString() : _isUsed.ToString();
        }
    }
}