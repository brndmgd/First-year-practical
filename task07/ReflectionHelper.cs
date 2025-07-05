namespace task07;

using System.Reflection;

public static class ReflectionHelper
{
    public static void PrintTypeInfo(this Type type)
    {
        var classDisplayName = type.GetCustomAttribute<DisplayNameAttribute>();
        var version = type.GetCustomAttribute<VersionAttribute>();
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

        if (classDisplayName != null)
            Console.WriteLine($"Класс: {classDisplayName.DisplayName}");

        if (version != null)
            Console.WriteLine($"Версия: {version.Major}.{version.Minor}");

        Console.WriteLine("Методы");
        foreach (var m in methods)
        {
            Console.Write($"Метод {m.Name}");
            var methodDisplayName = m.GetCustomAttribute<DisplayNameAttribute>();
            if (methodDisplayName != null)
                Console.WriteLine($": {methodDisplayName.DisplayName}");
        }

        Console.WriteLine("Свойства");
        foreach (var p in properties)
        {
            Console.Write($"Свойство {p.Name}");
            var propertyDisplayName = p.GetCustomAttribute<DisplayNameAttribute>();
            if (propertyDisplayName != null)
                Console.WriteLine($": {propertyDisplayName.DisplayName}");
        }
    }
}
