namespace task05;

using System;
using System.Reflection;
using System.Collections.Generic;

public class ClassAnalyzer
{
    private Type _type;

    public ClassAnalyzer(Type type)
    {
        _type = type;
    }

    public IEnumerable<string> GetPublicMethods()
    => _type
    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
    .Select(m => m.Name);

    public IEnumerable<string> GetMethodParams(string methodname)
    {
        var method = _type.GetMethod(methodname);
        if (method == null)
        {
            return new List<string>();
        }

        return method.GetParameters().Select(p => p.Name!);
    }

    public IEnumerable<string> GetAllFields()
    => _type
    .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
    .Select(f => f.Name);

    public IEnumerable<string> GetProperties()
    => _type
    .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
    .Select(p => p.Name);

    public bool HasAttribute<T>() where T : Attribute
    => _type.GetCustomAttribute(typeof(T)) != null;
}
