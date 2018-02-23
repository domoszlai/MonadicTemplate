using System;
using System.Text;

namespace MonadicTemplate
{
    public delegate ICovariantTuple<T, Context> GFun<T>(Context ctx);
    public delegate Generator<T> GContextFun<T>(Context ctx);

    public class Generator<T> : IGenerator<T>
    {
        private readonly GFun<T> f;

        private Generator(GFun<T> f)
        {
            this.f = f;
        }

        private static Generator<T> Wrap<T>(GFun<T> f)
        {
            return new Generator<T>(f);
        }

        public ICovariantTuple<T, Context> Run(Context ctx)
        {
            return f(ctx);
        }

        public static Generator<T> Return(T val)
        {
            return Generator<T>.Wrap(delegate (Context ctx)
            {
                return CovariantTuple<T, Context>.Create(val, ctx);
            });
        }

        public static Generator<String> Template(FormattableString formattable)
        {
            return Wrap(delegate (Context ctx)
            {
                var provider = new GFormatProvider(ctx);
                var res = formattable.ToString(provider);
                return CovariantTuple<String, Context>.Create(res, ctx);
            });
        }

        public static Generator<T> WithContext(GContextFun<T> gfun)
        {
            return Generator<T>.Wrap(delegate (Context ctx)
            {
                return gfun(ctx).Run(ctx);
            });
        }

        public static Generator<T> SetContext(Generator<T> gen, Context localctx)
        {
            return Generator<T>.Wrap(delegate (Context ctx)
            {
                var ret = gen.Run(localctx);
                return CovariantTuple<T, Context>.Create(ret.Item1, ctx);
            });
        }

        public static Generator<string> operator +(Generator<T> a, Generator<T> b)
        {
            return Wrap(delegate (Context ctx)
            {
                StringBuilder o = new StringBuilder();

                var Res1 = a.f(ctx);
                o.Append(Res1.Item1);

                var Res2 = b.f(Res1.Item2);
                o.Append(Res2.Item1);

                return CovariantTuple<String, Context>.Create(o.ToString(), Res2.Item2);
            });
        }

    }
}
