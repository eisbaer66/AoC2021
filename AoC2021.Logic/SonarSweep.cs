using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic
{
    public class SonarSweep
    {
        private int[] _readings;

        public void Load(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            _readings = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                 .Select(int.Parse)
                 .ToArray();
        }

        public int GetSlope()
        {
            return GetSlope(_readings);
        }

        public int GetSlidingWindowSlope(int windowSize)
        {
            var slidingWindows = GetSlidingWindows(_readings, windowSize)
                .Select(window => window.Sum());

            return GetSlope(slidingWindows);
        }

        private IList<IList<int>> GetSlidingWindows(IReadOnlyList<int> readings, int windowSize)
        {
            IList<IList<int>> slidingWindows = new List<IList<int>>();
            var               windowsCount   = readings.Count - windowSize + 1;
            for (int i = 0; i < windowsCount; i++)
            {
                slidingWindows.Add(new List<int>());

                for (int j = i; j < i + windowSize; j++)
                {
                    slidingWindows[i].Add(readings[j]);
                }
            }

            return slidingWindows;
        }

        private int GetSlope(IEnumerable<int> readings)
        {
            var slope           = 0;
            var previousReading = int.MaxValue;
            foreach (var reading in readings)
            {
                if (previousReading < reading)
                    ++slope;
                previousReading = reading;
            }

            return slope;
        }
    }
}
