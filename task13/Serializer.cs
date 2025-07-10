namespace task13;

using System.Text.Json;
using System.Text.Json.Serialization;

public class Serializer
{
    private JsonSerializerOptions _options = JsonSerializerOptions.Default;

    public Serializer(string format)
    {
        _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        _options.Converters.Add(new DateTimeConverter(format));
    }

    public void Serialize(object obj, string path)
    {
        string jsonString = JsonSerializer.Serialize(obj, _options);
        File.WriteAllText(path, jsonString);
    }

    public T Deserialize<T>(string path)
    {
        string json = File.ReadAllText(path);
        T? deserializedObject = JsonSerializer.Deserialize<T>(json, _options);
        if (deserializedObject == null)
            throw new FileNotFoundException($"Файл {path} не найден");

        return deserializedObject;
    }
}