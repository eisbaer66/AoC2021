using System.Collections.Generic;

namespace AoC2021.Logic.Utility.PathFinding.Dijkstra
{
    public class Node<T>
    {
        public Node(T item)
        {
            Item      = item;
            Neighbors = new List<Edge<T>>();
        }

        public   T             Item           { get; init; }
        public   List<Edge<T>> Neighbors      { get; init; }
        internal int?          CostToStart    { get; set; } = null;
        internal Node<T>       NearestToStart { get; set; }
        internal bool          Visited        { get; set; }
    }
}