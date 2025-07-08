namespace Plugin3;

using PluginLib;

[PluginLoad(Dependencies = new string[] { "Plugin2", "Plugin1" })]
public class Plugin3 : ICommand
{
    public void Execute()
    {
        Console.WriteLine("!! :)");
    }
}
