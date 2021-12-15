using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.Utility.PathFinding
{
    public class Dijkstra<T, TKey>
    {
        private readonly T[]                        _items;
        private readonly Func<T, DijkstraNode<T>>           _getNode;
        private readonly Func<T, TKey>              _getKey;
        private readonly Func<T, IEnumerable<TKey>> _getNeighborKeys;

        public Dijkstra(IEnumerable<T> items, Func<T, DijkstraNode<T>> getNode, Func<T, TKey> getKey, Func<T, IEnumerable<TKey>> getNeighborKeys)
        {
            _items           = items?.ToArray() ?? throw new ArgumentNullException(nameof(items));
            _getNode         = getNode          ?? throw new ArgumentNullException(nameof(getNode));
            _getKey          = getKey           ?? throw new ArgumentNullException(nameof(getKey));
            _getNeighborKeys = getNeighborKeys  ?? throw new ArgumentNullException(nameof(getNeighborKeys));
        }

        public IList<T> FindPath(TKey start, TKey end)
        {
            var dict = GenerateNodes();

            var startNode = dict[start];
            var endNode   = dict[end];

            FindPath(startNode, endNode);

            var path = BuildPath(endNode);

            return path;
        }

        private Dictionary<TKey, DijkstraNode<T>> GenerateNodes()
        {
            var dict = _items
                       .Select(_getNode)
                       .ToDictionary(n => _getKey(n.Item));
            foreach (var node in dict.Values)
            {
                node.Neighbors.AddRange(_getNeighborKeys(node.Item).Select(n => dict[n]));
            }

            return dict;
        }

        private static void FindPath(DijkstraNode<T> startNode, DijkstraNode<T> endNode)
        {
            startNode.CostToStart = 0;

            var openNodes = new PriorityQueue<DijkstraNode<T>, int>();
            openNodes.Enqueue(startNode, 0);

            do
            {
                var node = openNodes.Dequeue();
                if (node.Visited)
                    continue;
                
                foreach (var neighbor in node.Neighbors.OrderBy(n => n.Cost))
                {
                    if (neighbor.Visited)
                        continue;

                    var costToStart = (node.CostToStart ?? 0) + neighbor.Cost;
                    if (neighbor.CostToStart != null && costToStart >= neighbor.CostToStart)
                        continue;

                    neighbor.CostToStart    = costToStart;
                    neighbor.NearestToStart = node;
                    openNodes.Enqueue(neighbor, costToStart);
                }

                node.Visited = true;
                if (node == endNode)
                    break;
            } while (openNodes.Count > 0);
        }

        private List<T> BuildPath(DijkstraNode<T> node)
        {
            var path = new List<T>();
            path.Add(node.Item);
            BuildPath(path, node);
            path.Reverse();
            return path;
        }

        private void BuildPath(List<T> path, DijkstraNode<T> node)
        {
            var nextNode = node.NearestToStart;
            if (nextNode == null)
                return;

            path.Add(nextNode.Item);
            BuildPath(path, nextNode);
        }
    }
}