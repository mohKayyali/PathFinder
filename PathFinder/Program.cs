
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

public class Node
{
    public int X { get; }
    public int Y { get; }
    public int Cost { get; }

    public Node Next { get; set; }

    public Node(int y, int x,int cost)
    {
        X = x;
        Y = y;
        Cost = cost;
        Next = null;
    }
}

public class RTSPathfinding
{
    private int maxLength;
    private Stack<Node> Result ;
    private int minCost;
    private bool PathFound;

    public RTSPathfinding() {
        Result = new Stack<Node>();
        minCost = 1000;
        PathFound = false;
    }

    public void FindPath(int[,] map) {
        
        maxLength = map.GetLength(0);
        
        Node startNode = new Node(maxLength - 1, maxLength - 1, 0);
        
        BuildPath(map, startNode, startNode);

        int depth = 1;

        if (Result.Count == 0)
            Console.WriteLine("No path found");

        while (Result.Count > 0)
        {
            Node current = Result.Pop();
            Console.WriteLine("X=" + current.X+ "   Y=" + current.Y + "   Depth=" + depth++);
        }

        

    }

    private int[,] CopyArray(int[,] map) {

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        int[,] copiedMap = new int[rows, cols];

        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                copiedMap[i, j] = map[i, j];
            }
        }

        return copiedMap;
    }

    private void StorePath(Node node) {

        PathFound = true;
        Result.Clear();
        while (node != null)
        {
            Result.Push(node);
            node = node.Next;
        }
    }


    private void BuildPath(int[,] map, Node node, Node startNode)
    {
        if (PathFound || node.Cost >= minCost)
            return;

        if (node.Y == 0 && node.X == 0)
        {
            minCost = node.Cost;

            StorePath(startNode);
            return;
        }
        var copiedMap = CopyArray(map);

        if (node.X - 1 >=0 && map[node.Y, node.X - 1] == 0)
        {
            node.Next = new Node(node.Y, node.X - 1, node.Cost + 1);
            
            copiedMap[node.Y, node.X] = 1;
            
            BuildPath(copiedMap, node.Next, startNode);
        }
        if (node.Y - 1 >= 0 && map[node.Y -1, node.X ] == 0)
        {
            node.Next = new Node(node.Y -1, node.X, node.Cost + 1);
           
            copiedMap[node.Y, node.X] = 1;
            
            BuildPath(copiedMap, node.Next, startNode);
        }

        if (node.X + 1 < maxLength && map[node.Y, node.X + 1] == 0)
        {
            node.Next = new Node(node.Y, node.X + 1, node.Cost + 1);
            
            copiedMap[node.Y, node.X] = 1;
            BuildPath(copiedMap, node.Next, startNode);
        }
        if (node.Y + 1 < maxLength && map[node.Y + 1, node.X] == 0)
        {
            node.Next = new Node(node.Y + 1, node.X, node.Cost + 1);
            
            copiedMap[node.Y, node.X] = 1;
            BuildPath(copiedMap, node.Next, startNode);
        }

    }
}

    class Program
    {

    static int[,] CreateMap(int dimension) {

        int[,] array = new int[dimension, dimension];
        Random random = new Random(); // Create a Random object

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                if (random.NextDouble() < 0.2) // Use the Random object to generate random numbers
                {
                    array[i, j] = 1;
                }
                else
                {
                    array[i, j] = 0;
                }
            }
        }
        return array;

    }
    

    static void Main(string[] args)
    {

        int dimension = 64;


        int[,] array = CreateMap(dimension);

        array[0, 0] = 0;
        array[dimension - 1, dimension - 1] = 0;

        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                Console.Write(array[i, j]);
            }
            Console.WriteLine();
        }

        // Starting point is (0,0)
        // End point is (max lengeth -1 ,max lengeth -1)

        RTSPathfinding pf = new RTSPathfinding();

        pf.FindPath(array);

    }
}