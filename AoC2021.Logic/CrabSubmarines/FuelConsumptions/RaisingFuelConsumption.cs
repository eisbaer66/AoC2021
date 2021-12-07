using System;

namespace AoC2021.Logic.CrabSubmarines.FuelConsumptions
{
    public class RaisingFuelConsumption : IFuelConsumption
    {
        public int Between(int a, int b)
        {
            var distance = Math.Abs(a - b);
            return distance * (distance + 1) / 2; //sum of first n numbers: n * (n+1) / 2
            // 1+2+3+4+5 = 5*6/2 = 15
        }
    }
}