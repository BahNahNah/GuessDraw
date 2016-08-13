using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class DisconnectPacket : Packet
    {
        public DisconnectPacket(int id) : base(enPacket.Disconnect)
        {
            Write(id);
        }
    }
}
