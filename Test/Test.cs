using System;
using System.Threading;
using System.Threading.Tasks;
using Event;

namespace Test
{
    class Test
    {
        static Test test = new Test();

        public Test()
        {
            Console.WriteLine($"Register to event bus.");
            EventBus.Default.Register(this);
        }

        ~Test()
        {
            Console.WriteLine($"Unregister from event bus.");
            EventBus.Default.Unregister(this);
        }

        static void Main(string[] args)
        {
            _ = Test.test;

            EventBus.Default.Post(new NAMESPACE_B.Message());

            Task.Run(async delegate
            {
                EventBus.Default.Post(new NAMESPACE_A.Message());
                EventBus.Default.Post(new NAMESPACE_B.Message());
            });

            while (true)
            {
                DispatchQueue.Main.Update();
                DispatchQueue.Global.Update();
                Thread.Sleep(1);
            }

        }


        [Subscribe(ThreadMode.MAIN)]
        private void HandleEvent(NAMESPACE_A.Message message)
        {
            Console.Out.WriteLine("[HandleEvent] message = {0}", message.msg);
        }

        [Subscribe]
        private void HandleEventSync(NAMESPACE_B.Message message)
        {
            Console.Out.WriteLine("[HandleEventSync] message = {0}", message.msg);
        }

        [Subscribe(ThreadMode.BACKGROUND)]
        private void HandleEventAsync(NAMESPACE_A.Message message)
        {
            Console.Out.WriteLine("[HandleEventAsync] message = {0}", message.msg);
        }
    }
}

namespace NAMESPACE_A
{
    class Message
    {
        public string msg = "NAMESPACE_A";
    }
}

namespace NAMESPACE_B
{
    class Message
    {
        public string msg = "NAMESPACE_B";
    }
}

