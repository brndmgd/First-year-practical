namespace task19tests;

using task19;

public class LongOperationTest
{
    [Fact]
    public void LongOperation_PrintsCorrectInformation()
    {
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        LongOperation.Main();

        for (int i = 0; i < 5; i++)
        {
            for (int j = 1; j < 4; j++)
            {
                Assert.Contains($"Поток {i} вызов {j}", output.ToString());
            }
        }
    }
}
