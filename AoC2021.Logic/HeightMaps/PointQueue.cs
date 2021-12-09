using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.HeightMaps
{
    internal class PointQueue
    {
        private readonly Queue<Point>      _queue;
        private readonly HashSet<Position> _positions;

        public PointQueue(HashSet<Position> positions)
        {
            _queue     = new Queue<Point>();
            _positions = positions ?? throw new ArgumentNullException(nameof(positions));
        }

        public PointQueue(IEnumerable<Point> points)
            : this(points.Select(p => p.Position).ToHashSet())
        {
        }

        public int Count => _queue.Count;

        public void Enqueue(Point point)
        {
            if (!_positions.Contains(point.Position))
                return;

            _queue.Enqueue(point);
            _positions.Remove(point.Position);
        }

        public void Enqueue(IEnumerable<Point> points)
        {
            foreach (var potentialBasinPoint in points)
            {
                Enqueue(potentialBasinPoint);
            }
        }

        public Point Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}