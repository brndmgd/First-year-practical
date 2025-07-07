namespace FileSystemCommands;

using task07;
using CommandLib;

[DisplayName("Команда размер каталога")]
[Version(1, 2)]
public class DirectorySizeCommand : ICommand
{
    private string _path;

    public DirectorySizeCommand(string path)
    {
        _path = path;
    }

    [DisplayName("Исполнение команды")]
    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException("Папка не найдена");

        long size;
        var directory = new DirectoryInfo(_path);
        size = directory.GetFiles().Sum(f => f.Length);
        Console.WriteLine($"Размер каталога {size} байт");
    }
}

[DisplayName("Команда поиска файлов")]
[Version(2, 4)]
public class FindFilesCommand : ICommand
{
    private string _path;
    private string _mask;

    public FindFilesCommand(string path, string mask)
    {
        _path = path;
        _mask = mask;
    }

    [DisplayName("Исполнение команды")]
    public void Execute()
    {
        if (!Directory.Exists(_path))
            throw new DirectoryNotFoundException("Папка не найдена");

        var directory = new DirectoryInfo(_path);
        var files = directory.GetFiles(_mask);
        foreach (var f in files)
        {
            Console.WriteLine(f.Name);
        }
    }
}
