namespace task14tests;

using Xunit;
using task14;

public class UnitTest1
{
    static Func<double, double> X = (double x) => x;
    static Func<double, double> SIN = (double x) => Math.Sin(x);

    [Fact]
    public void DefiniteIntegral_CorrectResult_X()
    {
        double result = DefiniteIntegral.Solve(-1, 1, X, 1e-4, 2);

        Assert.Equal(0, result, 1e-4);
    }

    [Fact]
    public void DefiniteIntegral_CorrectResult_SIN()
    {
        double result = DefiniteIntegral.Solve(-1, 1, SIN, 1e-5, 8);

        Assert.Equal(0, result, 1e-4);
    }

    [Fact]
    public void DefiniteIntegral_CorrectResult_X_NonZero()
    {
        double result = DefiniteIntegral.Solve(0, 5, X, 1e-6, 8);

        Assert.Equal(12.5, result, 1e-5);
    }
}
