using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PathFinder
{

    class Program
    {

        static void Main(string[] args)
        {
            bool[,] array = Utils.CreateMap();
            Console.WriteLine("Original map:");
            Utils.PrintMap(array);
            PathFinder pathfinder = new PathFinder();
            pathfinder.FindPath(array);

        }

        
    }
}