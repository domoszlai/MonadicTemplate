using System;

using MonadicTemplate.AST;
using static MonadicTemplate.Prelude;

namespace MonadicTemplate
{
    class Program
    {
        private static Generator<string> GetContextProperty(string property)
        {
            return WithContext(delegate (Context ctx)
            {
                return ctx.GetProperty(property);
            });
        }

        private static Generator<T> SetContextProperty<T>(Generator<T> gen, string property, Generator<string> value)
        {
            return WithContext(delegate (Context ctx)
            {
                return SetContext(gen, ctx.SetProperty(property, value));
            });
        }

        private static Generator<string> GenerateIdentifier(Identifier id)
        {
            return R(id.Name);
        }

        private static Generator<string> GenerateArgumentList(ArgumentList arguments)
        {
            return R("..."); // real code should come here
        }

        private static Generator<string> GenerateBlock(Block block)
        {
            return R("Method body for ") + GetContextProperty("method");
        }

        private static Generator<string> GenerateMethod(Method method)
        {
            var name = GenerateIdentifier(method.Name);
            var arguments = GenerateArgumentList(method.Arguments);
            var body = SetContextProperty(GenerateBlock(method.Body), "method", name);

            return T($"function {name}({arguments}){{{body}}}");
        }

        static void Main(string[] args)
        {
            var method1 = new Method(new Identifier("Fun1"), new ArgumentList(), new Block());
            var method2 = new Method(new Identifier("Fun2"), new ArgumentList(), new Block());

            Generator<string> res = GenerateMethod(method1) + GenerateMethod(method2);

            string str = res.Run(new Context()).Item1;
            Console.WriteLine(str);
        }
    }
}
