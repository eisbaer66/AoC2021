using System.Linq;

namespace AoC2021.Logic.BingoGame
{
    public record WinningBoard(Board Board, int WinningNumber)
    {
        public int GetFinalScore()
        {
            return Board.GetUnmarkedNumbers().Sum() * WinningNumber;
        }
    }
}