namespace task18tests;

using ServerLib;
using task17;

public class LongCommand : ICommand
{
    public int Count = 0;
    private int _expectedCount;
    private Action _action;

    public LongCommand(Action action, int expectedCount)
    {
        _action = action;
        _expectedCount = expectedCount;
    }

    public bool IsCompleted => Count == _expectedCount;

    public void Execute()
    {
        if (IsCompleted) _action();
        else Count++;
    }
}

public class ShortCommand : ICommand
{
    public bool IsCompleted { get; } = true;
    private Action _action;

    public ShortCommand(Action action) => _action = action;

    public void Execute() => _action();
}

public class SchedulerTest
{
    [Fact]
    public void Scheduler_ExecutesShortCommands()
    {
        ServerThread serverThread = new ServerThread();
        bool command1 = false;
        bool command2 = false;

        serverThread.AddCommand(new ShortCommand(() => command1 = true));
        serverThread.AddCommand(new ShortCommand(() => command2 = true));
        serverThread.Start();

        Thread.Sleep(200);

        Assert.True(command1);
        Assert.True(command2);
    }

    [Fact]
    public void Scheduler_ExecutesLongCommands()
    {
        ServerThread serverThread = new ServerThread();
        bool command1 = false;
        bool command2 = false;
        LongCommand longCommand1 = new LongCommand(() => command1 = true, 5);
        LongCommand longCommand2 = new LongCommand(() => command2 = true, 10);

        serverThread.AddCommand(longCommand1);
        serverThread.AddCommand(longCommand2);
        serverThread.Start();

        Thread.Sleep(200);

        Assert.Equal(5, longCommand1.Count);
        Assert.True(command1);
        Assert.Equal(10, longCommand2.Count);
        Assert.True(command2);
    }

    [Fact]
    public void Scheduler_ExecutesBoth()
    {
       ServerThread serverThread = new ServerThread();
        bool command1 = false;
        bool command2 = false;
        LongCommand longCommand = new LongCommand(() => command1 = true, 5);
        ShortCommand shortCommand = new ShortCommand(() => command2 = true);

        serverThread.AddCommand(longCommand);
        serverThread.AddCommand(shortCommand);
        serverThread.Start();

        Thread.Sleep(200);

        Assert.Equal(5, longCommand.Count);
        Assert.True(command1);
        Assert.True(command2);
    }
}
