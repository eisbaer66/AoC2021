namespace AoC2021.Logic.BeaconScanner
{
    public class MatrixRow
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public int Apply(Coordinate coordinate)
        {
            return coordinate.X * X + coordinate.Y * Y + coordinate.Z * Z;
        }
    }
}