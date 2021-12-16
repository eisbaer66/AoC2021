namespace AoC2021.Logic.BITS.Packets
{
    public class EqualTo : PacketBase
    {
        public override long Execute()
        {
            return SubPackets[0].Execute() == SubPackets[1].Execute() ? 1 : 0;
        }
    }
}