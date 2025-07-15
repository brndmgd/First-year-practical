namespace task13tests;

using task13;
using Xunit;

public class JsonSerializerTest
{

    Student student = new Student
    {
        FirstName = "Egor",
        LastName = "Poleshchuk",
        BirthDate = new DateTime(2006, 03, 06),
        Grades = new List<Subject>
        {
            new Subject { Name = "Math", Grade = 5},
            new Subject { Name = "Programming", Grade = 4}
        }
    };

    Student nullStudent = new Student
    {
        FirstName = "Ivan",
        LastName = null,
        BirthDate = new DateTime(2004, 12, 31)
    };

    [Fact]
    public void Serializer_WritesJSONCorrectly()
    {
        var serializer = new Serializer("dd.MM.yyyy");
        var directory = Path.Combine(Path.GetTempPath(), "testDir");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "student.json");

        serializer.Serialize(student, path);
        string json = File.ReadAllText(path);
        string expectedJson = @"{
  ""FirstName"": ""Egor"",
  ""LastName"": ""Poleshchuk"",
  ""BirthDate"": ""06.03.2006"",
  ""Grades"": [
    {
      ""Name"": ""Math"",
      ""Grade"": 5
    },
    {
      ""Name"": ""Programming"",
      ""Grade"": 4
    }
  ]
}";
        Assert.Equal(expectedJson, json);
        Directory.Delete(directory, true);
    }

    [Fact]
    public void Serializer_IgnoresNull()
    {
        var serializer = new Serializer("dd.MM.yyyy");
        var directory = Path.Combine(Path.GetTempPath(), "testDir");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "student.json");

        serializer.Serialize(nullStudent, path);
        string json = File.ReadAllText(path);
        string expectedJson = @"{
  ""FirstName"": ""Ivan"",
  ""BirthDate"": ""31.12.2004""
}";

        Assert.Equal(expectedJson, json);
        Directory.Delete(directory, true);
    }

    [Fact]
    public void Serializer_ReadsJSONCorrectly()
    {
        var serializer = new Serializer("dd.MM.yyyy");
        var directory = Path.Combine(Path.GetTempPath(), "testDir");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "student.json");
        serializer.Serialize(student, path);

        Student resultStudent = serializer.Deserialize<Student>(path);

        Assert.Equal(student.FirstName, resultStudent.FirstName);
        Assert.Equal(student.LastName, resultStudent.LastName);
        Assert.Equal(student.BirthDate, resultStudent.BirthDate);
        Assert.Equal(student.Grades![0].Name, resultStudent.Grades![0].Name);
        Assert.Equal(student.Grades[0].Grade, resultStudent.Grades[0].Grade);
        Assert.Equal(student.Grades[1].Name, resultStudent.Grades[1].Name);
        Assert.Equal(student.Grades[1].Grade, resultStudent.Grades[1].Grade);
        Directory.Delete(directory, true);
    }

    [Fact]
    public void Serializer_ReadsNullCorrectly()
    {
        var serializer = new Serializer("dd.MM.yyyy");
        var directory = Path.Combine(Path.GetTempPath(), "testDir");
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, "student.json");
        serializer.Serialize(nullStudent, path);

        Student resultStudent = serializer.Deserialize<Student>(path);

        Assert.Equal(nullStudent.FirstName, resultStudent.FirstName);
        Assert.Equal(nullStudent.BirthDate, resultStudent.BirthDate);
    }
}
