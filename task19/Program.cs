namespace task19;

using ServerLib;
using task17;

public class TestCommand(int id, int expectedCount) : ICommand
{
    public int Counter = 0;

    public bool IsCompleted => Counter == expectedCount;

    public void Execute()
    {
        Console.WriteLine($"Поток {id} вызов {++Counter}");
    }
}

public class LongOperation
{
    public static void Main()
    {
        ServerThread serverThread = new ServerThread();

        for (int i = 0; i < 5; i++)
        {
            serverThread.AddCommand(new TestCommand(i, 3));
        }

        serverThread.Start();
        Thread.Sleep(500);
    }
}