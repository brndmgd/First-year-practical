namespace task09;

using System;
using task07;
using System.Reflection;

public class MetadataReader
{
    static void PrintClassInfo(Type c)
    {
        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var attributes = c.GetCustomAttributes();
        var methods = c.GetMethods(flags);
        var constructors = c.GetConstructors(flags);

        Console.WriteLine($"Класс {c.Name}\n");

        if (attributes.Any())
        {
            Console.WriteLine("Атрибуты класса:");
            foreach (var attr in attributes)
            {
                Console.WriteLine($"\t{attr.GetType().Name};");
            }
            Console.WriteLine();
        }

        if (methods.Length != 0)
        {
            Console.WriteLine("Методы класса:");
            foreach (var met in methods)
            {
                var parameters = met.GetParameters();
                Console.Write($"\t{met.Name}: ");
                if (parameters.Length == 0) Console.Write("нет параметров");
                else
                {
                    for (int i = 0; i < parameters.Length - 1; i++)
                    {
                        Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}, ");
                    }
                    Console.Write($"{parameters.Last().ParameterType.Name} {parameters.Last().Name};");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        if (constructors.Length != 0)
        {
            Console.WriteLine("Конструкторы класса:");
            foreach (var con in constructors)
            {
                var parameters = con.GetParameters();
                Console.Write($"\t{con.Name}: ");
                if (parameters.Length == 0) Console.Write("нет параметров");
                else
                {
                    for (int i = 0; i < parameters.Length - 1; i++)
                    {
                        Console.Write($"{parameters[i].ParameterType.Name} {parameters[i].Name}, ");
                    }
                    Console.Write($"{parameters.Last().ParameterType.Name} {parameters.Last().Name};");
                }
                Console.WriteLine();
            }
        }
    }

    public static void Main(string[] args)
    {
        if (args.Length != 1)
            throw new ArgumentOutOfRangeException();

        string dllPath = args[0];
        var library = Assembly.LoadFrom(dllPath);
        var classes = library.GetTypes().Where(t => t.IsClass && !t.Name.StartsWith("<>"));
        foreach (Type c in classes)
        {
            PrintClassInfo(c);
            Console.WriteLine(new string('-', 30));
        }
    }
}