namespace AoC2021.Logic.SyntaxScoring
{
    internal record SyntaxError(char? IllegalChar, char[] UnmatchedChunks);
}