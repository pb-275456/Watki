using System.Diagnostics;

namespace Watki
{
    internal class Program
    {
        public static void Tests(string filename, int runs)
        {
            int[] matrixSizes = { 100 };
            int[] threads = { 1, 2};

            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("MatrixSize,Threads,Run,TimeMs");
                foreach (int size in matrixSizes)
                {
                    Matrix a = new Matrix(size, size);
                    Matrix b = new Matrix(size, size);
                    a.generateMatrix(seed: 1);
                    b.generateMatrix(seed: 2);
                    Multiply m = new Multiply(a, b);

                    foreach (int thread in threads)
                    {
                        for (int run = 1; run <= runs; run++)
                        {
                          
                            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                            Matrix c = m.MultiplyThread(thread);
                            stopwatch.Stop();

                            long elapsed = stopwatch.ElapsedMilliseconds;

                            writer.WriteLine($"{size},{thread},{run},{elapsed}");
                            Console.WriteLine($"Size: {size} | Threads: {thread} | Run: {run} | Time: {elapsed} ms");
                        }
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            Tests("test.csv", 3);
            
        }
    }
}
