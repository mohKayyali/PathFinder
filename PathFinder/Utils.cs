using System;
namespace PathFinder
{
	public static class Utils
	{
        public const int DIMENTIONS = 64;

        public static bool[,] CreateMap()
        {

            bool[,] array = new bool[DIMENTIONS, DIMENTIONS];
            Random random = new Random();

            for (int i = 0; i < DIMENTIONS; i++)
            {
                for (int j = 0; j < DIMENTIONS; j++)
                {
                    if (random.NextDouble() < 0.2)
                    {
                        array[i, j] = true;
                    }
                    else
                    {
                        array[i, j] = false;
                    }
                }
            }
            array[DIMENTIONS - 1, DIMENTIONS - 1] = false;
            array[0, 0] = false;

            return array;

        }

        public static void PrintMap<V>(V[,] array)
        {
            
            for (int i = 0; i < DIMENTIONS; i++)
            {
                for (int j = 0; j < DIMENTIONS; j++)
                {
                    if (array[i, j] is bool)
                    {
                        bool node = (bool)(object)array[i, j];
                        Console.Write(node ? '1':'0');
                    }
                    else
                    Console.Write(array[i, j] );
                }
                Console.WriteLine();
            }
        }

        public static Char[,] ToCharArray(bool[,] map)
        {

            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            char[,] copiedMap = new char[rows, cols];



            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copiedMap[i, j] = map[i, j] ? '1' : '0';
                }
            }

            return copiedMap;
        }
    }
}

