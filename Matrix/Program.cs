// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

Console.WriteLine("Hello, World!");


static void Multiply(double [,] A, double [,] B, double [,] C, long size)
{
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    for (long i = 0; i < size; i++)
    {
        for(long j = 0; j < size; j++)
        {
            double si = 0.0;

            for(long k = 0; k < size; k++)
            {
                si += A[i, k] * B[k, j];
            }
            C[i, j] = si;
        }
    }
    stopwatch.Stop();
    Console.WriteLine($"Milliseconds without parallel method: {stopwatch.ElapsedMilliseconds}");

    PrintMatrix(C, size);
}

static void PrintMatrix(double [,] A, long size)
{
    for(long i = 0; i < size; i++)
    {
        for (long j = 0; j < size; j++)
        {
            Console.Write($"{A[i, j]} ");
        }
        Console.WriteLine();
    }
    Console.WriteLine();
}

static void ParallelMultiply(double[,] A, double[,] B, double[,] C, long size)
{
    Stopwatch stopwatch2 = new Stopwatch();
    stopwatch2.Start();

    Parallel.For(0, size, i =>
    {

        for (long j = 0; j < size; j++)
        {
            double si = 0.0;

            for (long k = 0; k < size; k++)
            {
                si += A[i, k] * B[k, j];
            }
            C[i, j] = si;
        }
    });

    stopwatch2.Stop();
    Console.WriteLine($"Milliseconds with parallel method: {stopwatch2.ElapsedMilliseconds}");

    PrintMatrix(C, size);
}

long size = 3;

double [,] A = new double[size, size];
double [,] B = new double[size, size];  
double [,] C = new double[size, size];


A[0, 0] = 14.0; A[0, 1] = 9.0; A[0, 2] = 3.0;
A[1, 0] = 2.0; A[1, 1] = 11.0; A[1, 2] = 15.0;
A[2, 0] = 0.0; A[2, 1] = 12.0; A[2, 2] = 17.0;

B[0, 0] = 12.0; B[0, 1] = 25.0; B[0, 2] = 5.0;
B[1, 0] = 9.0; B[1, 1] = 10.0; B[1, 2] = 0.0;
B[2, 0] = 8.0; B[2, 1] = 5.0; B[2, 2] = 1.0;


Multiply(A, B, C, size);
ParallelMultiply(A, B, C, size);

