using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watki
{
    internal class Multiply
    {
        public Matrix a {  get; set; }
        public Matrix b {  get; set; }
        public Matrix c { get; set; }
        public Multiply(Matrix a, Matrix b)
        {
            this.a = a;
            this.b = b;
            this.c = new Matrix(a.rows, b.cols);
        }
        
        public TimeSpan MultiplyParallel(int maxThreads) {

            if (a.rows != b.cols)
                return TimeSpan.Zero;

            ParallelOptions opt = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};

            Stopwatch time = new Stopwatch();

            time.Start();
            Parallel.For(0, a.rows, row =>
            {
                for(int col=0; col<b.cols; col++)
                {
                    c.matrix[row, col] = 0;
                    for(int k=0; k<a.cols; k++)
                    {
                        c.matrix[row, col] += a.matrix[row, k] * b.matrix[k, col];
                    }
                }
            });
            time.Stop();

            return time.Elapsed;
        }

        

    }
}
