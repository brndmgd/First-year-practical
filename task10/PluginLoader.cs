namespace task10;

using System.Reflection;
using System.Linq;
using PluginLib;

public class PluginLoader
{
    public void PluginsLoad(string path)
    {
        if (path == null)
            throw new DirectoryNotFoundException();

        var dllPaths = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

        var allPlugins = dllPaths
        .SelectMany(p => Assembly.LoadFrom(p).GetTypes())
        .Where(t => t.GetCustomAttribute<PluginLoadAttribute>() != null);

        var independantPlugins = allPlugins
        .Where(p => p.GetCustomAttribute<PluginLoadAttribute>()!.Dependencies.Length == 0)
        .ToList();

        List<Type> pluginOrder = new List<Type>();

        while (independantPlugins.Any())
        {
            var curPlugin = independantPlugins.First();
            pluginOrder.Add(curPlugin);
            independantPlugins.RemoveAt(0);
            foreach (var p in allPlugins.Except(pluginOrder))
            {
                var dependencies = p
                .GetCustomAttribute<PluginLoadAttribute>()!
                .Dependencies
                .Except(pluginOrder.Select(d => d.Name));

                if (!dependencies.Any()) independantPlugins.Add(p);
            }
        }

        foreach (var p in pluginOrder)
        {
            var plugin = Activator.CreateInstance(p);
            var method = p.GetMethod("Execute");
            method!.Invoke(plugin, null);
        }
    }
}
