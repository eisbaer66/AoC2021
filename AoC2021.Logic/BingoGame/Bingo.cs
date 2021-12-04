using System;
using System.Linq;

namespace AoC2021.Logic.BingoGame
{
    public class Bingo
    {
        private readonly int[]   _selectedNumbers;
        private readonly Board[] _boards;

        public Bingo(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentNullException(nameof(input));

            var lines = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
                throw new ArgumentException("input contains to little lines");

            _selectedNumbers = lines[0]
                               .Split(',', StringSplitOptions.RemoveEmptyEntries)
                               .Select(int.Parse)
                               .ToArray();

            _boards = lines.Skip(1)
                           .Select((line, i) => (line, i))
                           .GroupBy(x => x.i / 5)
                           .Select(g => new Board(g.Select(x => x.line)))
                           .ToArray();
        }

        public WinningBoard FindFirstWinningBoard()
        {
            foreach (var selectedNumber in _selectedNumbers)
            {
                foreach (var board in _boards)
                {
                    board.Mark(selectedNumber);
                    if (!board.IsFinished())
                        continue;
                    
                    return new WinningBoard(board, selectedNumber);
                }
            }

            throw new InvalidOperationException("could not find a winning board");
        }

        public WinningBoard FindLastWinningBoard()
        {
            var finishedBoards = new bool[_boards.Length];
            foreach (var selectedNumber in _selectedNumbers)
            {
                for (var i = 0; i < _boards.Length; i++)
                {
                    if (finishedBoards[i])
                        continue;

                    var board = _boards[i];

                    board.Mark(selectedNumber);
                    if (!board.IsFinished())
                        continue;

                    finishedBoards[i] = true;

                    if (finishedBoards.Any(b => !b))
                        continue;

                    return new WinningBoard(board, selectedNumber);
                }
            }

            throw new InvalidOperationException("not all boards win");
        }
    }
}