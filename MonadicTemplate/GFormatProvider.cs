using System;

namespace MonadicTemplate
{
    class GFormatProvider : IFormatProvider, ICustomFormatter
    {
        private Context ctx;

        public GFormatProvider(Context ctx)
        {
            this.ctx = ctx;
        }

        public object GetFormat(System.Type formatType)
        {
            if (formatType == typeof(ICustomFormatter)) return this;
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null)
                return string.Empty;

            // This is why we need the covariant type variable
            if (arg is IGenerator<object>)
            {
                arg = ((IGenerator<object>)arg).Run(ctx).Item1;
            }

            if(arg is IFormattable)
            {
                return ((IFormattable)arg).ToString(format, formatProvider);
            }
            else
            {
                return arg.ToString();
            }
        }

    }
}
