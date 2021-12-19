using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.BeaconScanner
{
    public class Matrix
    {
        public MatrixRow X { get; set; }
        public MatrixRow Y { get; set; }
        public MatrixRow Z { get; set; }

        public Matrix Flip()
        {
            return new Matrix
                   {
                       X = new MatrixRow { X = X.X, Y = Y.X, Z = Z.X },
                       Y = new MatrixRow { X = X.Y, Y = Y.Y, Z = Z.Y },
                       Z = new MatrixRow { X = X.Z, Y = Y.Z, Z = Z.Z },
                   };
        }

        public static IEnumerable<Matrix> GenerateAllRotations()
        {
            //TODO get rid of nonsensical combinations

            var potentialAxisOrder = new (Coordinate, Coordinate, Coordinate)[]
                                     {
                                         (new(1, 0, 0),
                                          new(0, 1, 0),
                                          new(0, 0, 1)),
                                         (new(1, 0, 0),
                                          new(0, 0, 1),
                                          new(0, 1, 0)),

                                         (new(0, 1, 0),
                                          new(1, 0, 0),
                                          new(0, 0, 1)),
                                         (new(0, 1, 0),
                                          new(0, 0, 1),
                                          new(1, 0, 0)),

                                         (new(0, 0, 1),
                                          new(0, 1, 0),
                                          new(1, 0, 0)),
                                         (new(0, 0, 1),
                                          new(1, 0, 0),
                                          new(0, 1, 0)),
                                     };
            var potentialAxisFactors = new Coordinate[]
                                       {
                                           new(1, 1, 1),
                                           new(1, 1, -1),
                                           new(1, -1, 1),
                                           new(-1, 1, 1),
                                           new(1, -1, -1),
                                           new(-1, 1, -1),
                                           new(-1, -1, 1),
                                           new(-1, -1, -1),
                                       };
            return potentialAxisOrder
                   .SelectMany(order => potentialAxisFactors.Select(factors => new Matrix
                                                                               {
                                                                                   X = new MatrixRow
                                                                                       {
                                                                                           X = order.Item1.X * factors.X,
                                                                                           Y = order.Item1.Y * factors.Y,
                                                                                           Z = order.Item1.Z * factors.Z
                                                                                       },
                                                                                   Y = new MatrixRow
                                                                                       {
                                                                                           X = order.Item2.X * factors.X,
                                                                                           Y = order.Item2.Y * factors.Y,
                                                                                           Z = order.Item2.Z * factors.Z
                                                                                       },
                                                                                   Z = new MatrixRow
                                                                                       {
                                                                                           X = order.Item3.X * factors.X,
                                                                                           Y = order.Item3.Y * factors.Y,
                                                                                           Z = order.Item3.Z * factors.Z
                                                                                       }
                                                                               }));
        }
    }
}