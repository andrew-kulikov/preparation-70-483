using System.Threading.Tasks;

namespace Common
{
    public abstract class AsyncExample : ExampleBase
    {
        protected AsyncExample(string name, string module) : base(name, module)
        {
        }

        public abstract Task Execute();
    }
}