using PerformanceConsoleApp._Shared;
using System;

namespace PerformanceConsoleApp
{
    public static class ArrayPerformanceTester
    {
        private const int _count = 100000;
        public static void DemoArrayFlattening()
        {
            var height = DataGenerator.GetRandomInt(5, 5);
            var width = DataGenerator.GetRandomInt(5, 5);
            int result;

            int[,] twoDimensional = new int[height, width];
            int[] oneDimensional = new int[width * height];

            //2D

            //Assign values
            twoDimensional[0, 1] = 4;
            twoDimensional[1, 2] = 5;
            twoDimensional[2, 3] = 6;
            twoDimensional[3, 4] = 7;

            //Display array
            for (int i = 0; i < height; i++)
            {
                for (int a = 0; a < width; a++)
                {
                    Console.Write(twoDimensional[i, a]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            //1D

            //Assign values
            oneDimensional[1] = 4;
            oneDimensional[1 * width + 2] = 5;
            oneDimensional[2 * width + 3] = 6;
            oneDimensional[3 * width + 4] = 7;

            //Display array
            for (int i = 0; i < height; i++)
            {
                for (int a = 0; a < width; a++)
                {
                    Console.Write(oneDimensional[i * width + a]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();


            Benchmarker.Benchmark(() =>
            {
                twoDimensional[0, 1] = 4;
                twoDimensional[1, 2] = 5;
                twoDimensional[2, 3] = 6;
                twoDimensional[3, 4] = 7;

                for (int j = 0; j < height; j++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        result = twoDimensional[j, a];
                    }
                }
            }, "2D array (assign & read)", _count);

            Benchmarker.Benchmark(() =>
            {
                oneDimensional[1] = 4;
                oneDimensional[1 * width + 2] = 5;
                oneDimensional[2 * width + 3] = 6;
                oneDimensional[3 * width + 4] = 7;

                for (int j = 0; j < height; j++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        result = oneDimensional[j * width + a];
                    }
                }
            }, "1D array (assign & read)", _count);

            Console.WriteLine();
        }

        public static void DemoJaggedArray()
        {
            int width = 50;
            int height = 50;
            int result;

            // 2D array.
            Benchmarker.Benchmark(() =>
            {
                DataGenerator.Get2DArray(height, width);

            }, "2D array creation", _count);
          

            // Jagged array.
            Benchmarker.Benchmark(() =>
            {
                DataGenerator.GetJaggedArray(height, width);

            }, "Jagged array creation", _count);

            var twoDimensionalArray = DataGenerator.Get2DArray(height, width);
            var jaggedArray = DataGenerator.GetJaggedArray(height, width);

            // 2D array.
            Benchmarker.Benchmark(() =>
            {
                for (int j = 0; j < height; j++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        result = twoDimensionalArray[j, a];
                    }
                }
            }, "2D array element access", _count);

            // Jagged array.
            Benchmarker.Benchmark(() =>
            {
                for (int j = 0; j < height; j++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        result = jaggedArray[a][j];
                    }
                }
            }, "Jagged array element access with low temporal locality", _count);

            // Jagged array.
            Benchmarker.Benchmark(() =>
            {
                for (int j = 0; j < height; j++)
                {
                    for (int a = 0; a < width; a++)
                    {
                        result = jaggedArray[j][a];
                    }
                }
            }, "Jagged array element access with high temporal locality", _count);

            /*
             * Summary: 2D arrays are faster to allocate, but a lot slower to access than jagged arrays.
             * Since element access count is usually a lot greater than array allocation count (1), 
             * using jagged arrays should still result in faster code, but be careful with memory usage 
             * 
             * Jagged array allocation slowdown explained:
             *  - 2D array only needs 1 allocation
             *  - Jagged array needs length * width + 1 allocations
             *  => this means garbage collection will have to do a lot more work when using jagged arrays
             *  
             *  Jagged array element access with imperformant index ordering => See DemoJaggedTemporalLocality explanation
             *  
             * NOTE: jagged arrays don't necessarily have to be allocated at the start, u could do this lazily!
             * 
             */
            Console.WriteLine();
        }

        public static void DemoJaggedTemporalLocality()
        {
            int height = 50;
            int width = 50;
            var jaggedArray = DataGenerator.GetJaggedArray(height, width);
            int result;

            Benchmarker.Benchmark(() =>
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        result = jaggedArray[j][i];
                    }
                }
            }, "Low temporal locality", _count);

            Benchmarker.Benchmark(() =>
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        result = jaggedArray[i][j];
                    }
                }
            }, "High temporal locality", _count);

            Console.WriteLine();
            /*
             * https://stackoverflow.com/questions/763262/how-does-one-write-code-that-best-utilizes-the-cpu-cache-to-improve-performance
             * The reason this is cache inefficient is because modern CPUs will load the cache line with "near" memory addresses 
             * from main memory when you access a single memory address. 
             * We are iterating through the "j"(outer) rows in the array in the inner loop, 
             * so for each trip through the inner loop, 
             * the cache line will cause to be flushed and loaded with a line of addresses that are near to the[j][i] entry
             */
        }

    }
}
