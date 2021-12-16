using System.Linq;

namespace AoC2021.Logic.BITS.Packets
{
    public class Minimum : PacketBase
    {
        public override long Execute()
        {
            return SubPackets.Select(p => p.Execute())
                             .Min();
        }
    }
}