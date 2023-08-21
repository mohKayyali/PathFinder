using System;
namespace PathFinder
{
    public class PathFinder
    {
        
        private Stack<Node> Result;
        
        private bool PathFound { get; set; }

        private Node StartNode { get; set; }

        public PathFinder()
        {
            Result = new Stack<Node>();
            
            PathFound = false;
            StartNode = new Node(Utils.DIMENTIONS - 1, Utils.DIMENTIONS - 1, 0);
        }

        public void FindPath(bool[,] map)
        {

            Walk(map, StartNode);
            PrintResultPath(map);

        }

        private void PrintResultPath(bool[,] map)
        {
            if (Result.Count == 0)
            {
                Console.WriteLine("No path found");
                return;
            }

            Console.WriteLine("Solution path:");

            var charMap= Utils.ToCharArray(map);

            while (Result.Count > 0)
            {
                Node current = Result.Pop();
                Console.WriteLine("X=" +  current.X + "   Y=" + current.Y + "   Depth=" + current.Cost);
                charMap[current.Y, current.X] = '$';
            }
            Console.WriteLine("Solution path in map:");
            Utils.PrintMap(charMap);


        }

        

        private bool[,] CopyArray(bool[,] map)
        {

            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            bool[,] copiedMap = new bool[rows, cols];



            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    copiedMap[i, j] = map[i, j];
                }
            }

            return copiedMap;
        }

        private void StorePath()
        {
           
            Result.Clear();
            PathFound = true;

            while (StartNode != null)
            {
                Result.Push(StartNode);
                StartNode = StartNode.Next;
            }
        }

        public double CalculateDistance(int x1, int y1, int x2=0, int y2=0)
        {
            int deltaX = x1 - x2;
            int deltaY = y1 - y2;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }


        private void Walk(bool[,] map, Node node)
        {
            if (PathFound )
                return;

            if (node.Y == 0 && node.X == 0)
            {
                StorePath();
                map[node.Y, node.X] = true;
                return;
            }
            var copiedMap = CopyArray(map);
            copiedMap[node.Y, node.X] = true;

            Dictionary<Node, double> distencesToDistincation = new Dictionary<Node, double>();


            if (node.X - 1 >= 0 && map[node.Y, node.X - 1] == false)
            {
                Node left = new Node(node.Y, node.X - 1, node.Cost + 1);
                double distance = CalculateDistance(left.Y, left.X); //+ CalculateDistance(left.Y, left.X,Utils.DIMENTIONS-1, Utils.DIMENTIONS - 1);
                distencesToDistincation.Add(left, distance);
            }

            if (node.Y - 1 >= 0 && map[node.Y - 1, node.X] == false)
            {
                Node up = new Node(node.Y - 1, node.X, node.Cost + 1);
                double distance = CalculateDistance(up.Y, up.X);//+ CalculateDistance(up.Y, up.X, Utils.DIMENTIONS - 1, Utils.DIMENTIONS - 1); ;
                distencesToDistincation.Add(up, distance);
            }

            if (node.X + 1 < Utils.DIMENTIONS && map[node.Y, node.X + 1] == false)
            {
                Node right = new Node(node.Y, node.X + 1, node.Cost + 1)  ;
                double distance = CalculateDistance(right.Y, right.X);//+ CalculateDistance(right.Y, right.X, Utils.DIMENTIONS - 1, Utils.DIMENTIONS - 1); ;
                distencesToDistincation.Add(right, distance);
            }
            if (node.Y + 1 < Utils.DIMENTIONS && map[node.Y + 1, node.X] == false)
            {
                Node down = new Node(node.Y + 1, node.X, node.Cost + 1);
                double distance = CalculateDistance(down.Y, down.X);//+ CalculateDistance(down.Y, down.X, Utils.DIMENTIONS - 1, Utils.DIMENTIONS - 1); ;
                distencesToDistincation.Add(down, distance);
            }
            
            if (distencesToDistincation.Count > 0) {
                
                distencesToDistincation.OrderBy(dist => dist.Value);

                foreach (var distance in distencesToDistincation) {
                    
                    node.Next = distance.Key;
                    Walk(copiedMap, node.Next);

                }
                
            }

        }
    }
}

