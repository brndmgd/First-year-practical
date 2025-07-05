namespace CommandRunner;

using System;
using System.Diagnostics;
using System.Reflection;
using CommandLib;

public class ConsoleApp
{
    public static void Main()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
        if (slnDirectory == null)
            throw new DirectoryNotFoundException("Папка с решением не найдена");

        var dllPath = Directory.GetFiles(slnDirectory, "FileSystemCommands.dll", SearchOption.AllDirectories).First();
        if (dllPath == null)
            throw new FileNotFoundException("Файл с библиотекой не найден");

        var commands = Assembly.LoadFrom(Path.GetFullPath(dllPath));

        var appDir = Path.Combine(Path.GetTempPath(), "AppDir");
        Directory.CreateDirectory(appDir);
        File.WriteAllText(Path.Combine(appDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(appDir, "test2.txt"), "World");

        var directorySizeClass = commands.GetType("FileSystemCommands.DirectorySizeCommand");
        if (directorySizeClass == null)
            throw new TypeLoadException("Команда DirectorySizeCommand не найдена");

        var directorySizeMethod = directorySizeClass.GetMethod("Execute");
        var directorySizeObject = Activator.CreateInstance(directorySizeClass, appDir);
        directorySizeMethod!.Invoke(directorySizeObject, null);

        Directory.Delete(appDir, true);

        Directory.CreateDirectory(appDir);
        File.WriteAllText(Path.Combine(appDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(appDir, "file2.log"), "Log");

        var findFilesClass = commands.GetType("FileSystemCommands.FindFilesCommand");
        if (findFilesClass == null)
            throw new TypeLoadException("Команда FindFilesCommand не найдена");

        var findFilesMethod = findFilesClass.GetMethod("Execute");
        var findFilesObject = Activator.CreateInstance(findFilesClass, appDir, "*.txt");
        findFilesMethod!.Invoke(findFilesObject, null);

        Directory.Delete(appDir, true);
    }
}
