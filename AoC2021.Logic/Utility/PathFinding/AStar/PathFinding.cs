using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.Utility.PathFinding.AStar
{
    public class PathFinding<T, TKey>
    {
        private readonly T[]                                  _items;
        private readonly Func<T, TKey, Node<T>>               _getNode;
        private readonly Func<T, TKey>                        _getKey;
        private readonly Func<T, IEnumerable<Neighbor<TKey>>> _getNeighbors;

        public PathFinding(IEnumerable<T> items, Func<T, TKey, Node<T>> getNode, Func<T, TKey> getKey, Func<T, IEnumerable<Neighbor<TKey>>> getNeighborKeys)
        {
            _items        = items?.ToArray() ?? throw new ArgumentNullException(nameof(items));
            _getNode      = getNode          ?? throw new ArgumentNullException(nameof(getNode));
            _getKey       = getKey           ?? throw new ArgumentNullException(nameof(getKey));
            _getNeighbors = getNeighborKeys  ?? throw new ArgumentNullException(nameof(getNeighborKeys));
        }

        public IList<T> FindPath(TKey start, TKey end)
        {
            var dict = GenerateNodes(end);

            var startNode = dict[start];
            var endNode   = dict[end];

            FindPath(startNode, endNode);

            //var count = dict.Values.Count(n => n.Visited);

            var path = BuildPath(endNode);

            return path;
        }

        private Dictionary<TKey, Node<T>> GenerateNodes(TKey end)
        {
            var dict = _items
                       .Select(i => _getNode(i, end))
                       .ToDictionary(n => _getKey(n.Item));
            foreach (var node in dict.Values)
            {
                node.Neighbors.AddRange(_getNeighbors(node.Item).Select(x => new Edge<T>(dict[x.Key], x.Cost)));
            }

            return dict;
        }

        private static void FindPath(Node<T> startNode, Node<T> endNode)
        {
            startNode.CostToStart = 0;

            var openNodes = new PriorityQueue<Node<T>, int>();
            openNodes.Enqueue(startNode, 0);

            do
            {
                var node = openNodes.Dequeue();
                if (node.Visited)
                    continue;

                foreach (var neighbor in node.Neighbors.OrderBy(n => n.Cost))
                {
                    if (neighbor.Node.Visited)
                        continue;

                    var costToStart = (node.CostToStart ?? 0) + neighbor.Cost;
                    if (neighbor.Node.CostToStart != null && costToStart >= neighbor.Node.CostToStart)
                        continue;

                    neighbor.Node.CostToStart    = costToStart;
                    neighbor.Node.NearestToStart = node;
                    openNodes.Enqueue(neighbor.Node, costToStart + neighbor.Node.Heuristic);
                }

                node.Visited = true;
                if (node == endNode)
                    break;
            } while (openNodes.Count > 0);
        }

        private static List<T> BuildPath(Node<T> node)
        {
            var path = new List<T> { node.Item };
            BuildPath(path, node);
            path.Reverse();
            return path;
        }

        private static void BuildPath(ICollection<T> path, Node<T> node)
        {
            while (true)
            {
                var nextNode = node.NearestToStart;
                if (nextNode == null) return;

                path.Add(nextNode.Item);
                node = nextNode;
            }
        }
    }
}