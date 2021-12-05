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
                var minY = Math.Min(startPoint.Y, endPoint.Y);
                var maxY = Math.Max(startPoint.Y, endPoint.Y);
                return Enumerable.Range(minY, maxY - minY + 1)
                                 .Select(y => new Coordinate(startPoint.X, y));
            }

            if (startPoint.Y == endPoint.Y)
            {
                var minX = Math.Min(startPoint.X, endPoint.X);
                var maxX = Math.Max(startPoint.X, endPoint.X);
                return Enumerable.Range(minX, maxX - minX + 1)
                                 .Select(x => new Coordinate(x, startPoint.Y));
            }

            return null;
        }
    }
}