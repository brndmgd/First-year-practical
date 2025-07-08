namespace task09tests;

using System.Reflection;
using task09;

public class UnitTest1
{
    [Fact]
    public void MetadataReader_ThrowsExceptionEmptyArgs()
    {
        string[] emptyArgs = new string[] { };

        Assert.Throws<ArgumentOutOfRangeException>(() => MetadataReader.Main(emptyArgs));
    }

    [Fact]
    public void MetadataReader_IncludesDirectorySizeCommand()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
        if (slnDirectory == null)
            throw new DirectoryNotFoundException();
        var dllPath = Directory.GetFiles(slnDirectory, "FileSystemCommands.dll", SearchOption.AllDirectories).First();
        var output = new StringWriter();
        Console.SetOut(output);
        
        string[] args = new string[] { dllPath };
        MetadataReader.Main(args);

        Assert.Contains("Класс DirectorySizeCommand", output.ToString());
        Assert.Contains("DisplayNameAttribute", output.ToString());
        Assert.Contains("VersionAttribute", output.ToString());
        Assert.Contains("Execute: нет параметров", output.ToString());
        Assert.Contains(".ctor: String path", output.ToString());
    }

    [Fact]
    public void MetadataReader_IncludesFindFilesCommand()
    {
        var slnDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName;
        if (slnDirectory == null)
            throw new DirectoryNotFoundException();
        var dllPath = Directory.GetFiles(slnDirectory, "FileSystemCommands.dll", SearchOption.AllDirectories).First();
        var output = new StringWriter();
        Console.SetOut(output);

        string[] args = new string[] { dllPath };
        MetadataReader.Main(args);

        Assert.Contains("Класс FindFilesCommand", output.ToString());
        Assert.Contains("DisplayNameAttribute", output.ToString());
        Assert.Contains("VersionAttribute", output.ToString());
        Assert.Contains("Execute: нет параметров", output.ToString());
        Assert.Contains(".ctor: String path, String mask", output.ToString());
    }
}
