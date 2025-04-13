using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watki
{
    internal class ThreadData()
    {
        public Matrix a { get; set; }
        public Matrix b { get; set; }
        public Matrix c { get; set; }
        public int startRow { get; set; }
        public int endRow { get; set; }
    }

    internal class Multiply
    {
        public Matrix a {  get; set; }
        public Matrix b { get; set; }

        //public Matrix resultThreads { get; set; }

        public Multiply(Matrix a, Matrix b)
        {
            this.a = a;
            this.b = b;
            //this.resultThreads = new Matrix(a.rows, b.cols);
        }
        
        public Matrix MultiplyParallel(int maxThreads) {

            if (a.rows != b.cols)
                return null;

            Matrix c = new Matrix(a.rows, b.cols);

            ParallelOptions opt = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};

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


            return c;
        }

        public Matrix MultiplyThread(int maxThreads)
        {
            if (a.rows != b.cols)
                return null;

            Matrix c = new Matrix(a.rows, b.cols);

            int rowsPerThread = a.rows / maxThreads;

            Thread[] threads = new Thread[maxThreads];

            for (int i=0; i<maxThreads; i++)
            {
                threads[i] = new Thread(MultiplyRows);
            }


            for (int i = 0; i < maxThreads; i++)
            {
                int end;
                var tmp = i; 
                int start = tmp * rowsPerThread;
                if (tmp == maxThreads - 1)
                    end = a.rows;
                else
                    end = start + rowsPerThread;
                
                ThreadData data = new ThreadData
                {
                    startRow =start,
                    endRow = end,
                    a = a,
                    b = b,
                    c = c
                };
                threads[i].Start(data);

            }

            for (int i = 0; i < maxThreads; i++)
            {
                threads[i].Join();
            }

            return c;
        }

        static void MultiplyRows(object threadData)
        {
            ThreadData data = (ThreadData)threadData;

            for (int row = data.startRow; row < data.endRow; row++)
            {
                for (int col = 0; col < data.b.cols; col++)
                {
                    data.c.matrix[row, col] = 0;
                    for (int k = 0; k < data.a.cols; k++)
                    {
                        data.c.matrix[row, col] += data.a.matrix[row, k] * data.b.matrix[k, col];
                    }
                }
            };

        }

    }
}
