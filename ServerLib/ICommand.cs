namespace ServerLib;

public interface ICommand
{
    public void Execute();
    public bool IsCompleted { get; }
}
