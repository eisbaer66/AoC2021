using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.HydrothermalVentsMapping.LineGenerators
{
    public class HorizontalVerticalAndDiagonalLineGenerator : HorizontalAndVerticalLineGenerator
    {
        public override IEnumerable<Coordinate> From(Coordinate startPoint, Coordinate endPoint)
        {
            var list = base.From(startPoint, endPoint);
            if (list != null)
                return list;

            var startX = Math.Min(startPoint.X, endPoint.X);
            var endX   = Math.Max(startPoint.X, endPoint.X);
            var startY = Math.Min(startPoint.Y, endPoint.Y);
            var endY   = Math.Max(startPoint.Y, endPoint.Y);
            var xs     = Enumerable.Range(startX, endX - startX + 1);
            var ys     = Enumerable.Range(startY, endY - startY + 1);

            if (startPoint.X > endPoint.X)
                xs = xs.Reverse();
            if (startPoint.Y > endPoint.Y)
                ys = ys.Reverse();

            return xs.Zip(ys).Select(c => new Coordinate(c.First, c.Second));
        }
    }
}