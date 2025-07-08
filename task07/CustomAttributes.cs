namespace task07;

[AttributeUsage(AttributeTargets.All)]
public class DisplayNameAttribute : Attribute
{
    public string DisplayName;

    public DisplayNameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}

[AttributeUsage(AttributeTargets.Class)]
public class VersionAttribute : Attribute
{
    public int Major;
    public int Minor;

    public VersionAttribute(int major, int minor)
    {
        Major = major;
        Minor = minor;
    }
}
