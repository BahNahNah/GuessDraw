using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets.Types
{
    class ChatPacket : Packet
    {
        public ChatPacket(string messahe) : base(enPacket.ChatMessage)
        {
            Write(messahe);
        }

        public ChatPacket(string sender, string messahe) : base(enPacket.ChatMessage)
        {
            Write(sender);
            Write(messahe);
        }
    }
}
