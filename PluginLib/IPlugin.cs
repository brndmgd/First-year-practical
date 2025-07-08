namespace PluginLib;

[AttributeUsage(AttributeTargets.Class)]
public class PluginLoadAttribute : Attribute
{
    private string[] _dependencies;
    public string[] Dependencies
    {
        get { return _dependencies; }
        set { _dependencies = value; }
    }

    public PluginLoadAttribute()
    {
        _dependencies = new string[] { };
    }
}

public interface ICommand
{
    void Execute();
}
