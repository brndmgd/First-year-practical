namespace task17;

using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Threading;
using ServerLib;
using task18;

public class ServerThread
{
    private Thread _thread;
    private bool _softStopRequested = false;
    private bool _hardStopRequested = false;
    private BlockingCollection<ICommand> _commands = new BlockingCollection<ICommand>();
    private IScheduler _scheduler = new RoundRobinScheduler();

    public ServerThread()
    {
        _thread = new Thread(() => ExecuteCommands());
    }

    public void Start()
    {
        if (_thread.ThreadState == ThreadState.Running) return;
        _thread.Start();
    }

    public void AddCommand(ICommand command)
    {
        _commands.Add(command);
    }

    public void ExecuteCommands()
    {
        while (!_hardStopRequested)
        {
            if (_softStopRequested && _commands.Count == 0)
                break;

            if (_scheduler.HasCommand())
            {
                var cmd = _scheduler.Select();
                try
                {
                    cmd.Execute();
                    continue;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex, cmd);
                }
            }

            if (_commands.TryTake(out var curCommand, 100))
            {
                if (curCommand.IsCompleted)
                {
                    try
                    {
                        curCommand.Execute();
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex, curCommand);
                    }
                }
                else _scheduler.Add(curCommand);
            }
        }
    }

    public void RequestHardStop() => _hardStopRequested = true;
    public void RequestSoftStop() => _softStopRequested = true;
    public bool isCurrentThread() => _thread == Thread.CurrentThread;
}


public static class ExceptionHandler
{
    public static void Handle(Exception exception, ICommand command)
    {
        Console.WriteLine($"Команда {command.GetType().Name} не была выполнена: {exception.Message}");
    }
}