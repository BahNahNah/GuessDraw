using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class WordInfoPacket : Packet
    {
        public WordInfoPacket(string word) : base(enPacket.WordInfo)
        {
            Write((int)word.Length);
        }
    }
}
