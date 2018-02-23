using System;

namespace MonadicTemplate
{
    public static class Prelude
    {
        public static Generator<T> R<T>(T val)
        {
            return Generator<T>.Return(val);
        }

        public static Generator<string> T(FormattableString formattable)
        {
            return Generator<string>.Template(formattable);
        }

        public static Generator<T> WithContext<T>(GContextFun<T> gfun)
        {
            return Generator<T>.WithContext(gfun);
        }

        public static Generator<T> SetContext<T>(Generator<T> gen, Context ctx)
        {
            return Generator<T>.SetContext(gen, ctx);
        }
    }
}
