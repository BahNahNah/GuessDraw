using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class UpdatePlayerStatusPacket : Packet
    {
        public UpdatePlayerStatusPacket(int id, PlayerStatus status) : base(enPacket.UpdateStatus)
        {
            Write(id);
            Write((byte)status);
        }
    }
}
