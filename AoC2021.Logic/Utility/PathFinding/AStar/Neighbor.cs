namespace AoC2021.Logic.Utility.PathFinding.AStar
{
    public record Neighbor<TKey>(TKey Key, int Cost);
}