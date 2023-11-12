using System;
using System.Diagnostics;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.ImageSharp;
using OxyPlot.Series;

double Function(double x)
{
    // Функции
    /*return Math.Sqrt(x * x - 2 * x + 7);*/
    /*return (x * x) * Math.Sqrt(4 - (x * x));*/
    return Math.Pow(Math.Sin(x), 2) + Math.Pow(Math.Cos(x), 2) + Math.Sin(2 * x);
}

double a = 0.0; // Начальный предел интегрирования
double b = 100.0; // Конечный предел интегрирования
int n = 3000; // Количество прямоугольников


// Вычисление интеграла без использования параллелизма
DateTime start = DateTime.Now;
double resultSerial = LeftTriangleMethod.CalculateIntegral(Function, a, b, n);
var timeDiffnoParallel = (DateTime.Now - start).TotalMilliseconds;
Console.WriteLine($"Результат без параллелизма: {resultSerial}");
Console.WriteLine($"Время выполнения без параллелизма: {timeDiffnoParallel} мс");

// Вычисление интеграла с использованием параллелизма
int minThreads = 2;
int maxThreads = Environment.ProcessorCount;

double[] accelerations = new double[maxThreads-minThreads+1];
int counter = 0;

for (int numThreads = minThreads; numThreads < maxThreads; numThreads++)
{

    start = DateTime.Now;
    double resultParallel = LeftTriangleMethod.ParallelCalculateIntegral(Function, a, b, n, numThreads);
    var timeDiffwithParallel = (DateTime.Now - start).TotalMilliseconds;
    Console.WriteLine($"Результат с параллелизмом ({numThreads} потоков): {resultParallel}");
    Console.WriteLine($"Время выполнения с параллелизмом: {timeDiffwithParallel} мс");

    // Вычисление и вывод ускорения
    double acceleration = (double)timeDiffnoParallel / (double)timeDiffwithParallel;
    Console.WriteLine($"Ускорение: {acceleration:F2}x");
    accelerations[counter] = acceleration;
    counter++;
}

LineSeries lineSeries = new LineSeries();
counter = 0;
for (int i = 2; i < accelerations.Length; i++)
{
    lineSeries.Points.Add(new DataPoint(i, accelerations[counter]));
    counter++;
}

var model = new PlotModel { Title = "Ускорения многопоточной программы", Background = OxyColor.FromRgb(255, 255, 255) };
model.Series.Add(lineSeries);
PngExporter.Export(model, "plot.png", 1280, 720);

public static class LeftTriangleMethod
{
    public static double CalculateIntegral(Func<double, double> func, double a, double b, int n)
    {
        double h = (b - a) / n;
        double sum = 0.0;

        for (int i = 0; i < n; i++)
        {
            double x = a + i * h;
            sum += func(x);
        }

        return h * sum;
    }

    public static double ParallelCalculateIntegral(Func<double, double> func, double a, double b, int n, int numThreads)
    {
        double h = (b - a) / n;
        double[] partialSums = new double[numThreads];
        int chunkSize = n / numThreads;

        Parallel.For(0, numThreads, threadNum =>
        {
            int startIndex = threadNum * chunkSize;
            // Если не делится нацело берём конечную точку
            int endIndex = (threadNum == numThreads - 1) ? n : startIndex + chunkSize;

            double localSum = 0.0;

            for (int i = startIndex; i < endIndex; i++)
                localSum += func(a + i * h);

            partialSums[threadNum] = localSum;
        });

        return h * partialSums.Sum();
    }
}