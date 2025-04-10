namespace Watki
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix a = new Matrix(500, 500);
            a.generateMatrix(1);

            //Console.WriteLine(a.ToString());

            Matrix b = new Matrix(500, 500);
            b.generateMatrix(2);
            //Console.WriteLine(b.ToString());
            Multiply m = new Multiply(a, b);
            var result = m.MultiplyParallel(1);
            Console.WriteLine(result);

            result = m.MultiplyParallel(8);
            Console.WriteLine(result);

        }
    }
}
