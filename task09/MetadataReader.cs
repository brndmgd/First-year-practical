namespace task09;
using System;
using task07;
using System.Reflection;

public class MetadataReader
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
            throw new ArgumentOutOfRangeException();

        string dllPath = args[0];
        var library = Assembly.LoadFrom(dllPath);
        var classes = library.GetTypes().Where(t => t.IsClass);
        foreach (Type c in classes)
        {
            c.PrintTypeInfo();
            Console.WriteLine("----------------");
        }
    }
}