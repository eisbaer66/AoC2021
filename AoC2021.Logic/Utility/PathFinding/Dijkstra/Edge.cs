namespace AoC2021.Logic.Utility.PathFinding.Dijkstra
{
    public class Edge<T>
    {
        public Edge(Node<T> node, int cost)
        {
            Node = node;
            Cost = cost;
        }

        public Node<T> Node { get; init; }
        public int     Cost { get; init; }
    }
}