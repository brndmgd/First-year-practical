namespace task17;

using ServerLib;

public class HardStop : ICommand
{
    private ServerThread _thread;

    public HardStop(ServerThread thread) => _thread = thread;

    public bool IsCompleted { get; } = true;

    public void Execute()
    {
        if (!_thread.isCurrentThread())
            throw new WrongThreadException("Неверный поток выполнения команды");

        _thread.RequestHardStop();
    }
}

public class SoftStop : ICommand
{
    private ServerThread _thread;

    public SoftStop(ServerThread thread) => _thread = thread;

    public bool IsCompleted { get; } = true;

    public void Execute()
    {
        if (!_thread.isCurrentThread())
            throw new WrongThreadException("Неверный поток выполнения команды");

        _thread.RequestSoftStop();
    }
}

public class WrongThreadException : Exception
{
    public WrongThreadException(string message) : base(message) { }
}