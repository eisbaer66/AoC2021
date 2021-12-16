namespace AoC2021.Logic.Utility.PathFinding.Dijkstra
{
    public record Neighbor<TKey>(TKey Key, int Cost);
}