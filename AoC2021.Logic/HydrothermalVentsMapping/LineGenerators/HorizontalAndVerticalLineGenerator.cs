using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.HydrothermalVentsMapping.LineGenerators
{
    public class HorizontalAndVerticalLineGenerator : ILineGenerator
    {
        public virtual IEnumerable<Coordinate> From(Coordinate startPoint, Coordinate endPoint)
        {
            if (startPoint.X == endPoint.X)
            {
                return Range(startPoint.Y, endPoint.Y)
                    .Select(y => new Coordinate(startPoint.X, y));
            }

            if (startPoint.Y == endPoint.Y)
            {
                return Range(startPoint.X, endPoint.X)
                    .Select(x => new Coordinate(x, startPoint.Y));
            }

            return null;
        }

        protected IEnumerable<int> Range(int start, int end)
        {
            var min   = Math.Min(start, end);
            var max   = Math.Max(start, end);
            var range = Enumerable.Range(min, max - min + 1);

            return start > end ? range.Reverse() : range;
        }
    }
}