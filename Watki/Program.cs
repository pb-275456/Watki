using System.Diagnostics;

namespace Watki
{
    internal class Program
    {
    public static void Tests(string filename, int runs)
    {
        int[] matrixSizes = {100, 200, 300};
        int[] threads = { 1, 2, 4, 8, 16 };

        using (StreamWriter writer = new StreamWriter(filename))
        {
            // Write header including average time column
            writer.WriteLine("MatrixSize,Threads,AverageTimeMicroseconds");
            
            foreach (int size in matrixSizes)
            {
                Matrix a = new Matrix(size, size);
                Matrix b = new Matrix(size, size);
                a.generateMatrix(seed: 5);
                b.generateMatrix(seed: 6);
                Multiply m = new Multiply(a, b);

                foreach (int thread in threads)
                {
                    long totalTime = 0; // To accumulate time for average calculation
                    
                    for (int run = 1; run <= runs; run++)
                    {
                        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                        Matrix c = m.MultiplyThread(thread);
                        stopwatch.Stop();

                        long elapsedMicroseconds = stopwatch.ElapsedTicks * 1000000 / System.Diagnostics.Stopwatch.Frequency;
                        totalTime += elapsedMicroseconds;

                        
                        long currentAverage = totalTime / run;

                        //Console.WriteLine($"Size: {size} | Threads: {thread} | Run: {run} | Time: {elapsedMicroseconds} μs | Avg: {currentAverage} μs");
                    }

                    long finalAverage = totalTime / runs;
                    writer.WriteLine($"{size},{thread},{finalAverage}");
                    Console.WriteLine($"Final Average for Size {size}, Threads {thread}: {finalAverage} μs");
                }
            }
        }
    }
        static void Main(string[] args)
        {
            Tests("test5.csv", 20);
            
        }
    }
}
