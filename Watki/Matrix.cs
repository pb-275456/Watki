using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watki
{
    internal class Matrix
    {
        public int[,] matrix { get; set; }
        public int rows { get; set; }
        public int cols { get; set; }

        public Matrix(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            matrix = new int[rows, cols];
        }

        public void generateMatrix(int seed)
        {
            Random random = new Random(seed);
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    matrix[row, col] = random.Next(1,10);
                }
            }
        }

        public override string ToString() {
            string str = "";
            for(int row = 0; row < rows; row++)
            {
                for(int col = 0;col < cols; col++)
                {
                    str += $"{matrix[row, col]}   ";
                }
                str += "\n";
            }

            return str;
        }
    }
}

