namespace AoC2021.Logic.BeaconScanner
{
    public class SensorConfiguration
    {
        public Matrix Rotation { get; init; }
        public int    X        { get; init; }
        public int    Y        { get; init; }
        public int    Z        { get; init; }

        public SensorConfiguration Reverse()
        {
            var coordinate      = new Coordinate(X, Y, Z);
            var reverseRotation = Rotation.Flip();
            return new SensorConfiguration
                   {
                       Rotation = reverseRotation,
                       X        = reverseRotation.X.Apply(coordinate) * -1,
                       Y        = reverseRotation.Y.Apply(coordinate) * -1,
                       Z        = reverseRotation.Z.Apply(coordinate) * -1,
                   };
        }
    }
}