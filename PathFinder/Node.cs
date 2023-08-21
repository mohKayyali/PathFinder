using System;
namespace PathFinder
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        public int Cost { get; }

        public Node Next { get; set; }

        public Node(int y, int x, int cost)
        {
            X = x;
            Y = y;
            Cost = cost;
            Next = null;
        }
    }
}

