namespace task11;

using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public class ClassCompiler
{
    public ICalculator Compile()
    {
        string calculator = @"public class Calculator : task11.ICalculator
{
    public int Add(int a, int b) => a + b;
    public int Minus(int a, int b) => a - b;
    public int Mul(int a, int b) => a * b;
    public int Div(int a, int b) => a / b ;
}";

        var syntaxTree = CSharpSyntaxTree.ParseText(calculator);

        var baseReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        var interfaceReference = MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location);

        CSharpCompilation compilation = CSharpCompilation.Create(
            "Calculator.dll",
            new[] { syntaxTree },
            new[] { baseReference, interfaceReference },
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        Assembly loadedAssembly;
        using (var ms = new MemoryStream())
        {
            var emitResult = compilation.Emit(ms);

            if (!emitResult.Success)
            {
                var compileErrors = emitResult.Diagnostics
                .Where(d => d.Severity == DiagnosticSeverity.Error)
                .Select(d => $"{d.GetMessage()}");
                string formattedErrors = string.Join("\n", compileErrors);

                throw new Exception($"Compilation failed:\n{formattedErrors}");
            }

            ms.Seek(0, SeekOrigin.Begin);
            loadedAssembly = Assembly.Load(ms.ToArray());
        }

        Type? calculatorType = loadedAssembly.GetType("Calculator");
        if (calculatorType == null)
        {
            throw new TypeLoadException("Класс Calculator не найден");
        }
        ICalculator calculatorInstance = (ICalculator)Activator.CreateInstance(calculatorType)!;

        return calculatorInstance;
    }
}
