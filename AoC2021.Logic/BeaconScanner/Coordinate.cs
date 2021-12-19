using System;

namespace AoC2021.Logic.BeaconScanner
{
    public record Coordinate(int X, int Y, int Z)
    {
        public int ManhattanDistanceTo(Coordinate other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y) + Math.Abs(Z - other.Z);
        }
    }
}