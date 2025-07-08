namespace Plugin2;

using PluginLib;

[PluginLoad()]
public class Plugin2 : ICommand
{
    public void Execute()
    {
        Console.Write("Hello ");
    }
}
