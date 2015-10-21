namespace MonadicTemplate.AST
{
    public class Identifier
    {
        public Identifier(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
