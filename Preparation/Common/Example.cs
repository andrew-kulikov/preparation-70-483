namespace Common
{
    public abstract class Example : ExampleBase
    {
        protected Example(string name, string module) : base(name, module)
        {
        }

        public abstract void Execute();
    }
}