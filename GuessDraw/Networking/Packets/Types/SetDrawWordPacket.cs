using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class SetDrawWordPacket : Packet
    {
        public SetDrawWordPacket(string word) : base(enPacket.WordToDraw)
        {
            Write(word);
        }
    }
}
