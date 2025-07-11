namespace task14;

using System;
using System.Threading;

//
// Вычисление определенного интеграла
//
public static class DefiniteIntegral
{
    private static int usingResource = 0;
    private static double curArea = 0;

    static Barrier? barrier;
    //
    // a, b - границы отрезка, на котором происходит вычисление опредленного интеграла
    // function - функция, для которой вычисляется определнный интеграл
    // step - размер одного шага разбиения
    // threadsNumber - число потоков, которые используются для вычислений
    //
    public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
    {
        double resultArea = 0;
        barrier = new Barrier(threadsnumber + 1, (b) => resultArea = curArea);
        double partition = (b - a) / threadsnumber;
        for (int i = 0; i < threadsnumber; i++)
        {
            double curStart = a + partition * i;
            double curEnd = a + partition * (i + 1);
            Thread myThread = new Thread(() => Area(curStart, curEnd, function, step));
            myThread.Start();
        }

        barrier.SignalAndWait();
        return resultArea;
    }

    static void Area(double a, double b, Func<double, double> function, double step)
    {
        double area = 0;
        double segment = b - a;
        int steps = (int)(segment / step);
        for (int i = 0; i < steps; i++)
        {
            area += step * (function(a + step * i) + function(a + step * (i + 1))) / 2;
        }

        bool hasWrote = false;
        while (hasWrote == false)
        {
            hasWrote = WriteArea(area);
        }

        barrier!.SignalAndWait();
    }

    static bool WriteArea(double area)
    {
        if (0 == Interlocked.Exchange(ref usingResource, 1))
        {
            curArea += area;

            Interlocked.Exchange(ref usingResource, 0);
            return true;
        }
        else
        {
            return false;
        }
    }
}