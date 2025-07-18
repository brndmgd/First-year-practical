namespace task18;

using ServerLib;

public class RoundRobinScheduler : IScheduler
{
    private Queue<ICommand> _commands = new Queue<ICommand>();

    public void Add(ICommand cmd) => _commands.Enqueue(cmd);

    public bool HasCommand() => _commands.Any();

    public ICommand Select()
    {
        if (_commands.Count == 0)
            throw new InvalidOperationException("В очереди нет команд");

        var cmd = _commands.Dequeue();
        if (!cmd.IsCompleted)
            _commands.Enqueue(cmd);

        return cmd;
    }
}