namespace task19;

using ServerLib;
using task17;

public class TestCommand(int id, int expectedCount) : ICommand
{
    public int Counter = 0;

    public void Execute()
    {
        Console.WriteLine($"Поток {id} вызов {++Counter}");
    }

    public bool IsCompleted => Counter == expectedCount - 1;
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

        serverThread.RequestHardStop();
    }
}