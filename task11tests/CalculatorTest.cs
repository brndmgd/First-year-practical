namespace task11tests;

using task11;

public class CalculatorTest
{
    [Fact]
    public void Calculator_ReturnsCorrectSum()
    {
        var compiler = new ClassCompiler();
        var calculator = compiler.Compile();

        int result = calculator.Add(1, 3);

        Assert.Equal(4, result);
    }

    [Fact]
    public void Calculator_ReturnsCorrectDifference()
    {
        var compiler = new ClassCompiler();
        var calculator = compiler.Compile();

        int result = calculator.Minus(5, 6);

        Assert.Equal(-1, result);
    }

    [Fact]
    public void Calculator_ReturnsCorrectProduct()
    {
        var compiler = new ClassCompiler();
        var calculator = compiler.Compile();

        int result = calculator.Mul(3, 7);

        Assert.Equal(21, result);
    }

    [Fact]
    public void Calculator_ReturnsCorrectQuotient()
    {
        var compiler = new ClassCompiler();
        var calculator = compiler.Compile();

        int result = calculator.Div(12, 4);

        Assert.Equal(3, result);
    }
}
