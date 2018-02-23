namespace MonadicTemplate.AST
{
    public class Method
    {
        public Method(Identifier name, ArgumentList arguments, Block body)
        {
            this.Name = name;
            this.Arguments = arguments;
            this.Body = body;
        }

        public Identifier Name { get; set; }

        public ArgumentList Arguments { get; set; }

        public Block Body { get; set; }
    }
}
