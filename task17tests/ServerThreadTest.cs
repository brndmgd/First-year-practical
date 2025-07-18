namespace task17tests;

using task17;

public class TestCommand : ICommand
{
    private Action _action;

    public TestCommand(Action action) => _action = action;

    public void Execute() => _action();
}

public class ServerThreadTest
{
    [Fact]
    public void HardStop_TerminatesImmediately()
    {
        ServerThread serverThread = new ServerThread();
        bool command1 = false;
        bool command2 = false;

        serverThread.Start();
        serverThread.AddCommand(new TestCommand(() => command1 = true));
        serverThread.AddCommand(new HardStop(serverThread));
        serverThread.AddCommand(new TestCommand(() => command2 = true));

        Thread.Sleep(100);

        Assert.True(command1);
        Assert.False(command2);
    }

    [Fact]
    public void SoftStop_ShouldWaitAllCommandsExecution()
    {
        ServerThread serverThread = new ServerThread();
        bool command1 = false;
        bool command2 = false;

        serverThread.Start();
        serverThread.AddCommand(new TestCommand(() => command1 = true));
        serverThread.AddCommand(new SoftStop(serverThread));
        serverThread.AddCommand(new TestCommand(() => command2 = true));

        Thread.Sleep(100);

        Assert.True(command1);
        Assert.True(command2);
    }

    [Fact]
    public void HardStop_ThrowsException_OnWrongThread()
    {
        ServerThread serverThread = new ServerThread();
        serverThread.Start();

        HardStop hardStop = new HardStop(serverThread);

        Assert.Throws<WrongThreadException>(() => hardStop.Execute());
    }

    [Fact]
    public void SoftStop_ThrowsException_OnWrongThread()
    {
        ServerThread serverThread = new ServerThread();
        serverThread.Start();

        SoftStop softStop = new SoftStop(serverThread);

        Assert.Throws<WrongThreadException>(() => softStop.Execute());
    }
}
