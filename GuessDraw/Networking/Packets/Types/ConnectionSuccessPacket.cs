using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class ConnectionSuccessPacket : Packet
    {
        public ConnectionSuccessPacket(int id) : base(enPacket.ConnectionSuccess)
        {
            Write(id);
        }
    }
}
