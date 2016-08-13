using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class PlayerConnectionPacket : Packet
    {
        public PlayerConnectionPacket(string name, int id, PlayerStatus status) : base(enPacket.PlayerConnection)
        {
            Write(name);
            Write(id);
            Write((byte)status);
        }

    }
}
