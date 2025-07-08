namespace Plugin1;

using PluginLib;

[PluginLoad(Dependencies = new string[] { "Plugin2" })]
public class Plugin1 : ICommand
{
    public void Execute()
    {
        Console.Write("World!!");
    }
}
