namespace AoC2021.Logic.BITS.Packets
{
    public class Literal : PacketBase
    {
        public long Value { get; set; }

        public override long Execute()
        {
            return Value;
        }
    }
}