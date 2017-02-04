using System.Threading;

namespace App1
{
    public class TimerTaskPerform
    {
        private Timer _tmr;

        private Timer _pingTmr;

        public void StartTask(TimerTaskEventHandler task)
        {
            var timerDelegate = new TimerCallback(task);
            var timer = new Timer(timerDelegate, this, 0, 1000);
            _tmr = timer;
        }

        public void EndTask(TimerTaskEndEventhandler task)
        {
            task();
            _tmr.Dispose();
        }

        public void StartPingTask(TimerPingTaskEventHandler task)
        {
            var timerDelegate = new TimerCallback(task);
            var timer = new Timer(timerDelegate, this, 0, 500);
            _pingTmr = timer;
        }

        public void EndPingTask()
        {
            _pingTmr.Dispose();
        }

        //任务委托
        public delegate void TimerTaskEventHandler(object state);

        public delegate void TimerTaskEndEventhandler();

        //Ping委托
        public delegate void TimerPingTaskEventHandler(object state);
    }
}