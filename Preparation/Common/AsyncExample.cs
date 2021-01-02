using System.Threading.Tasks;

namespace Common
{
    public abstract class AsyncExample : Example
    {
        protected AsyncExample(string name, string module) : base(name, module)
        {
        }

        public override void Execute()
        {
            ExecuteAsync().GetAwaiter().GetResult();
        }

        public abstract Task ExecuteAsync();
    }
}