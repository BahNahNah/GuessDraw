using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuessDraw.Networking.Utils
{
    public delegate void OnDataRetrieveDelegate(byte[] data);
    public delegate void OnDisconnectDelegate(PacketManager sender);
    public class PacketManager
    {
        public event OnDataRetrieveDelegate OnDataRetrieve;
        public event OnDisconnectDelegate OnDisconnect;
        public Socket NetworkSocket { get; private set; }
        public int CurrentPacketLength { get; private set;}

        private Queue<byte[]> SendQueue = new Queue<byte[]>();
        private object _lock = new object();
        private AsyncCallback RetrieveCallback;
        private byte[] PacketBuffer = new byte[1024];
        private MemoryStream PacketConstructor = new MemoryStream();
        //private const byte MagicNumber = 0x55;

        public PacketManager(Socket _s)
        {
            CurrentPacketLength = 0;
            NetworkSocket = _s;
        }

        public void StartRetrieve()
        {
            if (RetrieveCallback != null)
                return;

            RetrieveCallback = new AsyncCallback(HandleReceve);
            NetworkSocket.BeginReceive(PacketBuffer, 0, PacketBuffer.Length, SocketFlags.None, RetrieveCallback, null);
        }


        public void Send(byte[] packet)
        {
            using(MemoryStream ms = new MemoryStream(packet.Length + 4 + 1))
            using (BinaryWriter br = new BinaryWriter(ms))
            {
                //br.Write(MagicNumber);
                br.Write(packet.Length);
                br.Write(packet);
                lock(_lock)
                {
                    SendQueue.Enqueue(ms.ToArray());
                    ThreadPool.QueueUserWorkItem(HandleSend);
                }
            }
        }

        private void HandleSend(object state)
        {
            if (SendQueue.Count == 0)
                return;

            byte[] payload;
            lock(_lock)
            {
                payload = SendQueue.Dequeue();
            }

            NetworkSocket.Send(payload);
        }

        private void HandleReceve(IAsyncResult AR)
        {
            int packetSize = 0;
            try
            {
                packetSize = NetworkSocket.EndReceive(AR);
            }
            catch
            {
                OnDisconnect?.Invoke(this);
                return;
            }
            using (MemoryStream ms = new MemoryStream(PacketBuffer, 0, packetSize))
            using(BinaryReader br = new BinaryReader(ms))
            {
                
                while (ms.Position != ms.Length)
                {
                   /* if (br.ReadByte() != MagicNumber)
                    {
                        NetworkSocket.BeginReceive(PacketBuffer, 0, PacketBuffer.Length, SocketFlags.None, RetrieveCallback, null);
                        return;
                    }*/
                    int len = br.ReadInt32();

                    if (CurrentPacketLength == 0)
                    {
                        
                        if(len <= 0)
                        {
                            NetworkSocket.BeginReceive(PacketBuffer, 0, PacketBuffer.Length, SocketFlags.None, RetrieveCallback, null);
                            return;
                        }

                        int cur = (int)(ms.Length - ms.Position);
                        if (cur >= len)
                        {
                            OnDataRetrieve?.Invoke(br.ReadBytes(len));
                        }
                        else
                        {
                            CurrentPacketLength = len;
                            PacketConstructor.Write(br.ReadBytes(cur), 0, cur);
                        }
                    }
                    else
                    {
                        int cur = (int)(ms.Length - ms.Position);
                        int neededpackets = (int)(CurrentPacketLength - PacketConstructor.Length);
                        if(cur >= neededpackets)
                        {
                            PacketConstructor.Write(br.ReadBytes(neededpackets), 0, neededpackets);
                            OnDataRetrieve?.Invoke(PacketConstructor.ToArray());
                            PacketConstructor = new MemoryStream();
                        }
                        else
                        {
                            PacketConstructor.Write(br.ReadBytes(cur), 0, cur);
                        }
                    }
                }
            }

            NetworkSocket.BeginReceive(PacketBuffer, 0, PacketBuffer.Length, SocketFlags.None, RetrieveCallback, null);
        }


    }
}
