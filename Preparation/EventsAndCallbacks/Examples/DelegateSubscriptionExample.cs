using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Common;

namespace EventsAndCallbacks.Examples
{
    /// <summary>
    ///     If we try to subtract lambda from multicast delegate, it will not work (different lambdas compile to dofferent
    ///     classess)
    ///     As I got, MulticastDelegate class is redundant
    ///     BeginInvoke and EndInvoke does not work on .net core
    /// </summary>
    public class DelegateSubscriptionExample : Example
    {
        private readonly Delegate _actionDelegate;
        private readonly Delegate _multicastDelegate;
        private Action _action;
        private Action _longAction;

        private Func<string, string> _func;


        public DelegateSubscriptionExample() : base("Example delegate subscription", "1.4")
        {
            _actionDelegate = Delegate.CreateDelegate(
                typeof(Action),
                this,
                typeof(DelegateSubscriptionExample).GetMethod(nameof(Dorova),
                    BindingFlags.Instance | BindingFlags.NonPublic)!);

            _multicastDelegate = Delegate.Combine(_actionDelegate, _actionDelegate, _actionDelegate);
        }

        public override void Execute()
        {
            ExecuteAction();
            Console.WriteLine("\n\n");
            ExecuteFunc();
            Console.WriteLine("\n\n");
            _actionDelegate.DynamicInvoke();
            Console.WriteLine("\n\n");
            _multicastDelegate.DynamicInvoke();
            Console.WriteLine("\n\n");

            _longAction = new Action(LongTask);

            var ticks = new List<int>();
            var i = 0;
            var result = _longAction.BeginInvoke((ticksList) => Console.WriteLine($"AsyncCallback result: {ticksList}"), ticks);

            do
            {
                Console.WriteLine($"Completed: {result.IsCompleted}. Synchronously: {result.CompletedSynchronously}");
                Thread.Sleep(100);
            } while (result.IsCompleted);

            _longAction.EndInvoke(result);
        }

        private void LongTask()
        {
            Console.WriteLine("Starting long task...");

            Thread.Sleep(1000);

            Console.WriteLine("Completing long task...");
        }


        private void Dorova()
        {
            Console.WriteLine("Dorova");
        }

        private void SinSobaki()
        {
            Console.WriteLine("Sin Sobaki");
        }

        public void ExecuteAction()
        {
            _action += Dorova;
            _action += SinSobaki;

            _action.Invoke();

            _action -= Dorova;
            _action -= SinSobaki;

            _action?.Invoke();
        }

        public void ExecuteFunc()
        {
            _func += s =>
            {
                Console.WriteLine("Invoking dorova");
                return $"Dorova {s}";
            };
            _func += s => $"{s} Sin Sobaki";

            var result = _func.Invoke("TI");
            Console.WriteLine(result);
        }
    }
}