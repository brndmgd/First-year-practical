namespace task10tests;

using task10;

public class PluginLoaderTest
{
    [Fact]
    public void PluginLoader_ThrowsExceptionDirectoryNotFound()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
        if (slnDirectory == null)
            throw new DirectoryNotFoundException();
        var randomDirectory = Path.Combine(slnDirectory, "SomeDir");
        var pluginLoader = new PluginLoader();

        Assert.Throws<DirectoryNotFoundException>(() => pluginLoader.PluginsLoad(randomDirectory));
    }

    [Fact]
    public void PluginLoader_LoadsPluginsInCorrectOrder()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
        if (slnDirectory == null)
            throw new DirectoryNotFoundException();
        var pluginsDirectory = Path.Combine(slnDirectory, "Plugins");
        var pluginLoader = new PluginLoader();
        var output = new StringWriter();
        Console.SetOut(output);
        string expectedOrder = "Hello World!!!! :)";

        pluginLoader.PluginsLoad(pluginsDirectory);

        Assert.Contains(expectedOrder, output.ToString());
    }
}
