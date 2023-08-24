using Luna.Core.Dispatch;
using Luna.Core.Event;

Console.WriteLine("Hello, World!");
_ = T.Test.test;

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
