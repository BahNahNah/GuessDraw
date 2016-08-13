using GuessDraw.Controls;
using GuessDraw.Networking;
using GuessDraw.Networking.Packets;
using GuessDraw.Networking.Packets.Types;
using GuessDraw.Networking.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Forms
{
    public partial class frmServer : Form
    {
        GDServer server;
        List<ConnectedClient> Clients = new List<ConnectedClient>();

        string[] DefaultWordList = new string[] { "meme", "doge", "pepe", "dat boi", "harambe" };
        string[] LoadedWords = null;

        int drawSize = 10;
        int currentTime = 0;
        int id = 0;
        ConnectedClient nextPlayer = null;
        string CurrentGuessWord = string.Empty;
        string LastGuessWord = string.Empty;
        Color drawCol = Color.Black;

        Random r = new Random();
        
        public frmServer(int port)
        {
            InitializeComponent();
            LoadedWords = DefaultWordList;
            server = new GDServer();
            server.OnClientConnect += Server_OnClientConnect;
            server.Start(port);
        }

        private void Server_OnClientConnect(ConnectedClient client)
        {
            client.ClientDataRetrieve += Client_ClientDataRetrieve;
            client.StartRetrieve();
        }

        private void Client_ClientDisconnect(ConnectedClient sender)
        {
            RemovePlayer(sender);
            Clients.Remove(sender);

            SendToAll(new DisconnectPacket(sender.ID), null);

            if (sender.Status == PlayerStatus.Drawing)
                SelectNextPlayer();
        }

        private void Client_ClientDataRetrieve(ConnectedClient sender, byte[] data)
        {
            if(!sender.Connected)
            {
                if (data[0] != (byte)enPacket.ConnectionRequest)
                    return;

                sender.Name = Packet.Load(data).ReadString();
               

                int newID = id++;
                sender.ID = newID;

                AddPlayer(sender);

                sender.Send(new ConnectionSuccessPacket(newID));

                SendToAll(new PlayerConnectionPacket(sender.Name, newID, sender.Status), sender);
                SendToAll(new UpdatePlayerStatusPacket(sender.ID, sender.Status), null);

                Clients.Add(sender);
                sender.Status = PlayerStatus.Idle;
                sender.Connected = true;
                foreach (ConnectedClient c in Clients)
                    sender.Send(new PlayerConnectionPacket(c.Name, c.ID, sender.Status));

                
                sender.Send(new SetSizePacket(drawSize));
                sender.Send(new SetColorPacket(drawCol));
               

                sender.ClientDisconnect += Client_ClientDisconnect;
                return;
            }

            Packet p = Packet.Load(data);
            switch (p.PacketType)
            {
                case enPacket.ClearCanvas:
                case enPacket.DrawDot:
                    if(sender.Status == PlayerStatus.Drawing)
                        SendToAll(p, sender);
                    break;

                case enPacket.SetColor:
                    SendToAll(p, sender);
                    drawCol = p.ReadColor();
                    break;

                case enPacket.SetSize:
                    SendToAll(p, sender);
                    drawSize = p.ReadInt();
                    break;

                case enPacket.ChatMessage:
                    string msg = p.ReadString();
                    if(msg == "/ff" && sender.Status == PlayerStatus.Drawing)
                    {
                        SelectNextPlayer();
                        SendToAll(new ChatPacket("[Server]", string.Format("{0} has ended their turn.", sender.Name)), null);
                        break;
                    }
                    if (sender.Status == Networking.PlayerStatus.Guessing)
                    {
                        if (msg.ToLower() == CurrentGuessWord.Replace(" ", ""))
                        {
                            UpdateStatus(sender.ID, PlayerStatus.Guessed);
                            if (nextPlayer == null)
                                nextPlayer = sender;
                            CheckIfAllComplete();
                        }
                        else
                        {
                            SendToAll(new ChatPacket(sender.Name, msg), null);
                        }

                    }
                    break;
            }
        }

        private void CheckIfAllComplete()
        {
            SelectNextPlayer();
            return;
            bool shouldFinish = true;
            lock(Clients)
            {
                foreach (ConnectedClient c in Clients)
                {
                    if (c.Status == PlayerStatus.Guessing)
                    {
                        shouldFinish = false;
                        break;
                    }
                }
            }
            if(shouldFinish)
            {
                SelectNextPlayer();
            }
        }

        private void RemovePlayer(ConnectedClient c)
        {
            lvName.Invoke((MethodInvoker)delegate ()
            {
                lvName.Items.Remove(c.ListViewItem);
            });
        }
        private void AddPlayer(ConnectedClient c)
        {
            lvName.Invoke((MethodInvoker)delegate ()
            {
                ListViewItemConnectedClient i = new ListViewItemConnectedClient(c);
                i.SubItems.Add(c.ID.ToString());
                i.SubItems.Add(c.Status.ToString());
                lvName.Items.Add(i);
            });
        }

        private void UpdateAllStatus(PlayerStatus s)
        {
            foreach (ConnectedClient c in Clients)
                c.Status = s;
            SendToAll(new UpdatePlayerStatusPacket(-1, s), null);
        }

        private void UpdateStatus(int i, PlayerStatus s)
        {
            ConnectedClient c = Clients.FirstOrDefault(x => x.ID == i);
            lvName.Invoke((MethodInvoker)delegate ()
            {
                c.ListViewItem.SubItems[2].Text = s.ToString();
            });
           
            c.Status = s;
            SendToAll(new UpdatePlayerStatusPacket(i, s), null);
        }

        private void SendToAll(Packet p, ConnectedClient dontSend)
        {
            foreach(ConnectedClient c in Clients)
            {
                if (c == dontSend)
                    continue;
                if(c.Connected && c.Status != PlayerStatus.Idle)
                    c.Send(p);
            }
        }
        private void frmServer_Load(object sender, EventArgs e)
        {

        }
        private void SelectNextPlayer()
        {
            if(!string.IsNullOrWhiteSpace(CurrentGuessWord))
                SendToAll(new WinAnnouncePacket(string.Format("The word was {0} and {1} won", CurrentGuessWord, nextPlayer == null ? "Nobody" : nextPlayer.Name)), null);

            if(nextPlayer == null && Clients.Count > 0)
                nextPlayer = Clients[r.Next(Clients.Count)];
            if (nextPlayer == null)
                return;
            UpdateAllStatus(PlayerStatus.Guessing);
            UpdateStatus(nextPlayer.ID, PlayerStatus.Drawing);

            UpdateDrawWord();
            nextPlayer = null;
        }

        private void btnSelectPlayer_Click(object sender, EventArgs e)
        {
            SelectNextPlayer();
        }

        private string GetNewWord()
        {
            string ret = "derp";
            if (LoadedWords == null || LoadedWords.Length == 0)
                return ret;
            do
            {
                CurrentGuessWord = LoadedWords[r.Next(LoadedWords.Length)];
            } while (LastGuessWord == CurrentGuessWord);
            LastGuessWord = CurrentGuessWord;
            return CurrentGuessWord;
        }

        private void UpdateDrawWord()
        {
            SendToAll(new ClearCanvasPacket(), null);
            GetNewWord();
            var drawWord = new SetDrawWordPacket(CurrentGuessWord);
            var info = new WordInfoPacket(CurrentGuessWord);

            foreach(ConnectedClient c in Clients)
            {
                if (c.Status == PlayerStatus.Drawing)
                    c.Send(drawWord);
                else
                    c.Send(info);
            }
            currentTime = 60;
            SendToAll(new UpdateTimerPacket(currentTime), null);
        }

        private void btnLoadWords_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Word List|*.txt";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;
                path = ofd.FileName;
            }

            if(File.Exists(path))
            {
                List<string> loaded = new List<string>();
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = null;
                    while((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        loaded.Add(line.ToLower());
                    }

                    LoadedWords = loaded.ToArray();
                    lblNumberOfWords.Text = LoadedWords.Length.ToString();
                }
            }

            if(LoadedWords.Length == 0)
            {
                LoadedWords = DefaultWordList;
                MessageBox.Show("No valid words found. Default word ist was loaded.");
            }
        }

        private void tmrDrawTimer_Tick(object sender, EventArgs e)
        {
            if (currentTime != 0)
            {
                currentTime -= (tmrDrawTimer.Interval / 1000);
                if (currentTime < 0)
                    currentTime = 0;
                SendToAll(new UpdateTimerPacket(currentTime), null);
                if (currentTime == 0)
                    SelectNextPlayer();
            }
        }
    }
}
