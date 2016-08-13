using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Packets
{
    class Packet
    {
        public enPacket PacketType { get; private set; }
        private MemoryStream packetBuffer;
        private BinaryWriter writer;
        private BinaryReader reader;
        public Packet( enPacket type)
        {
            packetBuffer = new MemoryStream();
            writer = new BinaryWriter(packetBuffer);

            writer.Write((byte)type);
        }

        public static Packet Load(byte[] b)
        {
            return new Packet(b);
        }

        public Packet(byte[] packet)
        {
            packetBuffer = new MemoryStream(packet);
            reader = new BinaryReader(packetBuffer);
            PacketType = (enPacket)reader.ReadByte();
            
        }

        public byte[] ToBytes()
        {
            return packetBuffer.ToArray();
        }
        public void Write(int i)
        {
            writer.Write(i);
        }

        public void Write(bool b)
        {
            writer.Write((byte)(b ? 1 : 0));
        }

        public void Write(Color c)
        {
            writer.Write(c.R);
            writer.Write(c.G);
            writer.Write(c.B);
        }

        public void Write(Point p)
        {
            writer.Write(p.X);
            writer.Write(p.Y);
        }

        public void Write(string s)
        {
            writer.Write((int)s.Length);
            writer.Write(Encoding.UTF8.GetBytes(s));
        }

        public void Write(byte b)
        {
            writer.Write(b);
        }


        public int ReadInt()
        {
            return reader.ReadInt32();
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public bool ReadBool()
        {
            return reader.ReadByte() != 0;
        }

        public Color ReadColor()
        {
            return Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        public string ReadString()
        {
            try
            {
                return Encoding.UTF8.GetString(reader.ReadBytes(reader.ReadInt32()));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public Point ReadPoint()
        {
            return new Point(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
