using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021.Logic.BingoGame
{
    public class Board
    {
        private readonly int[][]                         _numbers;
        private readonly Dictionary<int, (int x, int y)> _numberLookup;
        private readonly bool[][]                        _hits;

        public Board(IEnumerable<string> lines)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            _numbers = lines.Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                                .Select(int.Parse)
                                                .ToArray())
                            .ToArray();
            _numberLookup = _numbers
                            .SelectMany((numbers, y) => numbers.Select((number, x) => (number, x, y)))
                            .ToDictionary(x => x.number, x => (x.x, x.y));

            var boardHeight = _numbers.Length;
            var boardWidth  = _numbers[0].Length;
            _hits = new bool[boardHeight][];
            for (var i = 0; i < boardHeight; i++)
            {
                _hits[i] = new bool[boardWidth];
            }
        }

        public void Mark(int selectedNumber)
        {
            if (!_numberLookup.ContainsKey(selectedNumber))
                return;

            var (x, y)  = _numberLookup[selectedNumber];
            _hits[y][x] = true;
        }
        
        public bool IsFinished()
        {
            for (var i = 0; i < 5; i++)
            {
                if (_hits[i].All(hit => hit))
                    return true;
                if (_hits.Select(x => x[i]).All(hit => hit))
                    return true;
            }

            return false;
        }

        public IEnumerable<int> GetUnmarkedNumbers()
        {
            return _hits.SelectMany((hits, y) => hits.Select((hit, x) => new
                                                                         {
                                                                             hit,
                                                                             number = _numbers[y][x]
                                                                         }))
                        .Where(x => !x.hit)
                        .Select(x => x.number);
        }
    }
}