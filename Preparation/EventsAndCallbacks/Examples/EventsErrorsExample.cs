using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace EventsAndCallbacks.Examples
{
    /// <summary>
    ///     Delegate always throws on first error and stops to execute next actions
    /// </summary>
    public class EventsErrorsExample : Example
    {
        private Action _action;
        private event Action Event;


        public EventsErrorsExample() : base("Error handling in events and multicast deleagates", "1.4")
        {
        }

        public override void Execute()
        {
            ExecuteAction();
            Console.WriteLine("\n\n");
            ExecuteEvent();
        }


        private void Dorova()
        {
            Console.WriteLine("Dorova");
            throw new Exception("First exception");
        }

        private void SinSobaki()
        {
            Console.WriteLine("Sin Sobaki");
            throw new Exception("Second exception");
        }
        private void Dorova(object sender, AlarmEventArgs e)
        {
            Console.WriteLine($"Dorova {e.Time}");
            throw new Exception("First exception");
        }

        private void SinSobaki(object sender, AlarmEventArgs e)
        {
            Console.WriteLine($"Sin Sobaki {e.Time}");
            throw new Exception("Second exception");
        }

        public void ExecuteAction()
        {
            var alarm = new DelegateAlarm();

            alarm.OnAlarmRaised += Dorova;
            alarm.OnAlarmRaised += SinSobaki;

            try
            {
                alarm.RaiseAlarm();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Thrown exceptions: {e.InnerExceptions.Count}");

                foreach (var (i, exception) in e.InnerExceptions.Enumerate())
                    Console.WriteLine($"Exception #{i}: {exception.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Simple exception thrown: {e.Message}");
            }
        }

        public void ExecuteEvent()
        {
            var alarm = new EventualAlarm();

            alarm.OnAlarmRaised += Dorova;
            alarm.OnAlarmRaised += SinSobaki;

            try
            {
                alarm.RaiseAlarm();
            }
            catch (AggregateException e)
            {
                Console.WriteLine($"Thrown exceptions: {e.InnerExceptions.Count}");

                foreach (var (i, exception) in e.InnerExceptions.Enumerate())
                    Console.WriteLine($"Exception #{i}: {exception.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Simple exception thrown: {e.Message}");
            }
        }
    }

    public class DelegateAlarm
    {
        public event Action OnAlarmRaised;

        public void RaiseAlarm()
        {
            OnAlarmRaised?.Invoke();
        }
    }


    public class AlarmEventArgs : EventArgs
    {
        public DateTime Time { get; set; }
    }

    public class EventualAlarm
    {
        public event EventHandler<AlarmEventArgs> OnAlarmRaised;

        public void RaiseAlarm()
        {
            if (OnAlarmRaised == null) return;

            var exceptions = new List<Exception>();

            foreach (var action in OnAlarmRaised.GetInvocationList())
            {
                try
                {
                    action.DynamicInvoke(this, new AlarmEventArgs {Time = DateTime.Now});
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Any()) throw new AggregateException(exceptions);
        }
    }
}