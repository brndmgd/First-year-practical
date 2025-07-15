namespace task14;

using System;
using System.Threading;

public static class DefiniteIntegral
{
    private static int usingResource = 0;
    private static double Area = 0;

    static Barrier? barrier;
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        barrier = new Barrier(threadsnumber + 1);
        double partition = (b - a) / threadsnumber;
        for (int i = 0; i < threadsnumber; i++)
        {
            double curStart = a + partition * i;
            double curEnd = a + partition * (i + 1);
            Thread myThread = new Thread(() => FindArea(curStart, curEnd, function, step));
            myThread.Start();
        }

        barrier.SignalAndWait();
        return Area;
    }

    static void FindArea(double a, double b, Func<double, double> function, double step)
    {
        double curArea = 0;
        double segment = b - a;
        int steps = (int)(segment / step);
        for (int i = 0; i < steps; i++)
        {
            curArea += step * (function(a + step * i) + function(a + step * (i + 1))) / 2;
        }

        bool hasWrote = false;
        while (hasWrote == false)
        {
            hasWrote = WriteArea(curArea);
        }

        barrier!.SignalAndWait();
    }

    static bool WriteArea(double curArea)
    {
        if (0 == Interlocked.Exchange(ref usingResource, 1))
        {
            Area += curArea;

            Interlocked.Exchange(ref usingResource, 0);
            return true;
        }
        else
        {
            return false;
        }
    }
}