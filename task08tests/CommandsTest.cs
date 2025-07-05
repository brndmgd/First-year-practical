namespace task08tests;

using FileSystemCommands;
using CommandRunner;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var output = new StringWriter();
        Console.SetOut(output);

        var command = new DirectorySizeCommand(testDir);
        command.Execute(); // Проверяем, что не возникает исключений

        Assert.Contains("Размер каталога 10 байт", output.ToString());
        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var output = new StringWriter();
        Console.SetOut(output);

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute(); // Должен найти 1 файл

        Assert.Contains("file1.txt", output.ToString());
        Assert.DoesNotContain("file2.log", output.ToString());
        Directory.Delete(testDir, true);
    }

    [Fact]
    public void ConsoleApp_WorksCorrect()
    {
        var output = new StringWriter();
        Console.SetOut(output);

        ConsoleApp.Main();

        Assert.Contains("Размер каталога 10 байт", output.ToString());
        Assert.Contains("file1.txt", output.ToString());
        Assert.DoesNotContain("file2.log", output.ToString());
    }
}
