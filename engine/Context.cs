using System.Collections.Immutable;

namespace MonadicTemplate
{
    public class Context
    {
        private ImmutableDictionary<string, Generator<string>> properties;

        public Context()
        {
            properties = ImmutableDictionary<string, Generator<string>>.Empty;
        }

        private Context(ImmutableDictionary<string, Generator<string>> properties)
        {
            this.properties = properties;
        }

        public Generator<string> GetProperty(string property)
        {
            return properties[property];
        }

        public string GetPropertyValue(string property)
        {
            return properties[property].Run(this).Item1;
        }

        public Context SetProperty(string property, Generator<string> value)
        {
            return new Context(properties.Add(property, value));
        }
    }
}
