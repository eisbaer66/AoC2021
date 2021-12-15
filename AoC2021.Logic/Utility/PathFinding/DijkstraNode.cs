using System.Collections.Generic;

namespace AoC2021.Logic.Utility.PathFinding
{
    public class DijkstraNode<T>
    {
        public DijkstraNode(T item, int cost)
        {
            this.Item      = item;
            this.Cost      = cost;
            this.Neighbors = new List<DijkstraNode<T>>();
        }

        public   T             Item           { get; init; }
        public   List<DijkstraNode<T>> Neighbors      { get; init; }
        public   int           Cost           { get; init; }
        internal int?          CostToStart    { get; set; } = null;
        internal DijkstraNode<T>       NearestToStart { get; set; }
        internal bool          Visited        { get; set; }
    }
}