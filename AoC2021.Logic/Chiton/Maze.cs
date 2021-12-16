using System;
using System.Collections.Generic;
using System.Linq;
using AoC2021.Logic.Chiton.MapExpanders;
using AoC2021.Logic.Utility.PathFinding.AStar;
//using AoC2021.Logic.Utility.PathFinding.Dijkstra;

namespace AoC2021.Logic.Chiton
{
    public class Maze
    {
        private readonly Dictionary<Coordinate, RiskReading> _dict;

        public Maze(string input, IExpander expander)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var list = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                            .SelectMany((line, y) => line.Select((c, x) => (risk: c - '0', x, y)))
                            .Select(x => new RiskReading(new Coordinate(x.x, x.y), x.risk))
                            .ToArray();

            var initMaxX = list.Select(k => k.Coordinate.X).Max();
            var initMaxY = list.Select(k => k.Coordinate.X).Max();

            _dict = list
                    .SelectMany(x => expander.Expand(x, initMaxX, initMaxY))
                    .ToDictionary(x => x.Coordinate, x => x);


            var maxX = _dict.Keys.Select(k => k.X).Max();
            var maxY = _dict.Keys.Select(k => k.Y).Max();
            MaxCoord = new Coordinate(maxX, maxY);

            foreach (var node in _dict.Values)
            {
                if (node.Coordinate.X > 0)
                    node.Neighbors.Add(_dict[new Coordinate(node.Coordinate.X - 1, node.Coordinate.Y)]);
                if (node.Coordinate.X < maxX)
                    node.Neighbors.Add(_dict[new Coordinate(node.Coordinate.X + 1, node.Coordinate.Y)]);

                if (node.Coordinate.Y > 0)
                    node.Neighbors.Add(_dict[new Coordinate(node.Coordinate.X, node.Coordinate.Y - 1)]);
                if (node.Coordinate.Y < maxY)
                    node.Neighbors.Add(_dict[new Coordinate(node.Coordinate.X, node.Coordinate.Y + 1)]);
            }


            //var currentY      = 0;
            //var stringBuilder = new StringBuilder();
            //foreach (var node in _dict.Keys.OrderBy(c => c.Y)
            //                              .ThenBy(c => c.X)
            //                              .Select(c => _dict[c]))
            //{
            //    if (node.Coordinate.Y != currentY)
            //    {
            //        stringBuilder.AppendLine();
            //        currentY = node.Coordinate.Y;
            //    }

            //    stringBuilder.Append(node.Risk);
            //}

            //var s = stringBuilder.ToString();
        }

        public Coordinate MaxCoord { get; }

        public IList<RiskReading> FindPath(Coordinate start, Coordinate end)
        {
            var dijkstra = new PathFinding<RiskReading, Coordinate>(_dict.Values,
                                                                    (r, target) => new Node<RiskReading>(r, ManhattanDistanceBetween(r.Coordinate, target)),
                                                                    //(r) => new Node<RiskReading>(r),
                                                                    r => r.Coordinate,
                                                                    r => r.Neighbors.Select(n => new Neighbor<Coordinate>(n.Coordinate, n.Risk)));
            var path     = dijkstra.FindPath(start, end);
            return path;
        }

        private int ManhattanDistanceBetween(Coordinate a, Coordinate b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}