using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.HeightMaps
{
    public class HeightMap
    {
        private readonly Point[]                     _points;
        private readonly Dictionary<Position, Point> _lookup;
        private          int                         _maxY;
        private          int                         _maxX;

        public HeightMap(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _points = input.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                           .SelectMany((s, y) =>
                           {
                               _maxY = y;
                               return s.Select((c, x) =>
                               {
                                   _maxX = x;
                                   return new Point(c - '0', x, y);
                               });
                           })
                           .ToArray();
            var lookup = _points.ToList();
            for (var x = -1; x <= _maxX + 1; x++)
            {
                lookup.Add(new Point(9, x, -1));
                lookup.Add(new Point(9, x, _maxY + 1));
            }

            for (var y = 0; y <= _maxY; y++)
            {
                lookup.Add(new Point(9, -1, y));
                lookup.Add(new Point(9, _maxX + 1, y));
            }

            _lookup = lookup.ToDictionary(p => p.Position, p => p);
        }

        public IEnumerable<Point> LowPoints()
        {
            return _points.Where(p => GetNeighbors(p.Position).All(n => _lookup[n].Value > p.Value));
        }

        public IEnumerable<int> FindBasinSizes()
        {
            return LowPoints().Select(lp => FindBasin(lp).Count());
        }

        public IEnumerable<Point> FindBasin(Point lowPoint)
        {
            var queue  = new PointQueue(_points);
            queue.Enqueue(lowPoint);

            while (queue.Count > 0)
            {
                var point = queue.Dequeue();
                var potentialBasinPoints = GetNeighbors(point.Position)
                                           .Where(p => _lookup.ContainsKey(p))
                                           .Select(p => _lookup[p])
                                           .Where(p => p.Value < 9)
                                           .ToArray();

                queue.Enqueue(potentialBasinPoints);

                if (potentialBasinPoints.Length > 0) 
                    yield return point;
            }
        }

        private static IEnumerable<Position> GetNeighbors(Position position)
        {
            var (x, y) = position;

            yield return new Position(x - 1, y);
            yield return new Position(x + 1, y);
            yield return new Position(x, y - 1);
            yield return new Position(x, y + 1);
        }
    }
}