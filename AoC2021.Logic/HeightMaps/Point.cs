namespace AoC2021.Logic.HeightMaps
{
    public record Point
    {
        public Point(int value, int x, int y)
        {
            Value    = value;
            Position = new Position(x, y);
        }

        public Position Position { get; set; }
        public int      Value    { get; set; }

        public void Deconstruct(out int value, out int X, out int Y)
        {
            value = this.Value;
            X     = Position.X;
            Y     = Position.Y;
        }
    }
}