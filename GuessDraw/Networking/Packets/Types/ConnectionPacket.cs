using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class ConnectionPacket : Packet
    {
        public ConnectionPacket(string name) : base(enPacket.ConnectionRequest)
        {
            Write(name);
        }
    }
}
