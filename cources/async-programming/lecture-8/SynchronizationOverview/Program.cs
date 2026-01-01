namespace SynchronizationOverview;

public static class Program
{
    private static int Counter { get; set; }
    
    public static void Main()
    {
        var thread1 = new Thread(IncrementCounter);
        var thread2 = new Thread(IncrementCounter);
        
        thread1.Start();
        thread2.Start();
        
        thread1.Join();
        thread2.Join();

        Console.WriteLine(Counter);
    }

    private static void IncrementCounter()
    {
        for (var i = 0; i < 100000; i++)
        {
            var currentCounterValue = Counter;
            var updatedCounterValue = currentCounterValue + 1;
            Counter = updatedCounterValue;
        }
    }
}