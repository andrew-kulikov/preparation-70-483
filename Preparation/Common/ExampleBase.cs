namespace Common
{
    public abstract class ExampleBase
    {
        protected ExampleBase(string name, string module)
        {
            Name = name;
            Module = module;
        }

        public string Name { get; }
        public string Module { get; }

        public override string ToString()
        {
            return $"Module {Module}. Example '{Name}'";
        }
    }
}