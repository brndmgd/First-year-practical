namespace task07;

using System.Reflection;

public static class ReflectionHelper
{
    public static void PrintTypeInfo(this Type type)
    {
        var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        var classDisplayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();
        var methods = type.GetMethods(flags);
        var properties = type.GetProperties(flags);

        Console.Write($"Класс {type.Name}");
        if (classDisplayName != null)
            Console.Write($": {classDisplayName.DisplayName}");
        Console.WriteLine();

        if (version != null)
            Console.WriteLine($"Версия: {version.Major}.{version.Minor}");
        Console.WriteLine();

        if (methods.Length != 0)
        {
            Console.WriteLine("Методы");
            foreach (var m in methods)
            {
                Console.Write($"Метод {m.Name}");
                var methodDisplayName = m.GetCustomAttribute<DisplayNameAttribute>();
                if (methodDisplayName != null)
                    Console.Write($": {methodDisplayName.DisplayName}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        if (properties.Length != 0)
        {
            Console.WriteLine("Свойства");
            foreach (var p in properties)
            {
                Console.Write($"Свойство {p.Name}");
                var propertyDisplayName = p.GetCustomAttribute<DisplayNameAttribute>();
                if (propertyDisplayName != null)
                    Console.Write($": {propertyDisplayName.DisplayName}");
                Console.WriteLine();
            }
        }
    }
}
