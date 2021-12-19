using System;

namespace AoC2021.Logic.BeaconScanner
{
    public class SensorConfigurationBuilder
    {
        public Matrix Rotation { get; set; }
        public int?   X        { get; set; }
        public int?   Y        { get; set; }
        public int?   Z        { get; set; }

        public SensorConfiguration ToConfiguration()
        {
            if (!X.HasValue)
                throw new InvalidOperationException("Build was not completed");
            if (!Y.HasValue)
                throw new InvalidOperationException("Build was not completed");
            if (!Z.HasValue)
                throw new InvalidOperationException("Build was not completed");

            return new SensorConfiguration
                   {
                       Rotation = Rotation,
                       X        = X.Value,
                       Y        = Y.Value,
                       Z        = Z.Value,
                   };
        }
    }
}