using GuessDraw.Controls;
using GuessDraw.Networking;
using GuessDraw.Networking.Client;
using GuessDraw.Networking.Packets;
using GuessDraw.Networking.Packets.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessDraw.Forms
{
    public partial class frmMain : Form
    {
        GDClient client = null;
        bool connected = false;

        string Username = string.Empty;
        int ID = 0;
        Dictionary<int, ListViewItemClient> Players = new Dictionary<int, ListViewItemClient>();

        public frmMain(string connectionName, string _IP, int _port)
        {
            InitializeComponent();

            client = new GDClient();
            client.ClientDataRetrieve += Client_ClientDataRetrieve;
            
          
            if (!client.Connect(_IP, _port))
            {
                MessageBox.Show("Failed to connect.");
                DialogResult = DialogResult.Abort;
                return;
            }
            Username = connectionName;
            client.StartRetrieve();
            client.Send(new ConnectionPacket(connectionName));

            this.Text += string.Format(" {0}", connectionName);
        }

        private void Client_ClientDataRetrieve(GDClient sender, byte[] data)
        {
            Packet p = Packet.Load(data);

            if (!connected)
            {
                if(p.PacketType != enPacket.ConnectionSuccess)
                {
                    MessageBox.Show("Failed to connect.");
                    DialogResult = DialogResult.Cancel;
                    return;
                }
                else
                {
                    connected = true;
                    ID = p.ReadInt();
                    return;
                }
            }
            switch (p.PacketType)
            {
                case enPacket.PlayerConnection:
                    string name = p.ReadString();
                    int _id = p.ReadInt();
                    AddPlayer(name, _id);
                    UpdateSingleStatus(_id, (PlayerStatus)p.ReadByte());
                    break;

                case enPacket.DrawDot:
                    Point po = p.ReadPoint();
                    drawCanvas1.DrawDot(po, false);
                    break;

                case enPacket.SetColor:
                    Color c = p.ReadColor();
                    drawCanvas1.DrawingBrush = new SolidBrush(c);
                    brushPreview1.SetColor(c);
                    break;

                case enPacket.ClearCanvas:
                    drawCanvas1.ClearDraw();
                    break;

                case enPacket.SetSize:
                    int s = p.ReadInt();
                    brushPreview1.SetSize(s);
                    drawCanvas1.DrawingSize =s;
                    break;

                case enPacket.ChatMessage:
                    HandleMessage(p.ReadString(), p.ReadString());
                    break;

                case enPacket.SetDrawStatus:
                    SetDrawStatus(p.ReadBool());
                    break;

                case enPacket.Disconnect:
                    RemovePlayer(p.ReadInt());
                    break;

                case enPacket.UpdateStatus:
                   _id = p.ReadInt();
                    PlayerStatus newStatus = (PlayerStatus)p.ReadByte();
                    if (_id == -1)
                        UpdateAllStatus(newStatus);
                    else
                        UpdateSingleStatus(_id, newStatus);
                    break;

                case enPacket.WordInfo:
                    SetDrawStatus(false);
                    SetWordInfo(p.ReadInt());
                    break;

                case enPacket.WordToDraw:
                    SetDrawStatus(true);
                    SetWord(p.ReadString());
                    break;

                case enPacket.UpdateTimer:
                    UpdateTimer(p.ReadInt());
                    break;

                case enPacket.WinAnnounce:
                    MessageBox.Show(p.ReadString());
                    break;
            }
        }

        private void SetDrawStatus(bool canDraw)
        {
            if(this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    gbDrawing.Enabled = canDraw;
                    drawCanvas1.CanDraw = canDraw;
                });
            }
            else
            {
                gbDrawing.Enabled = canDraw;
                drawCanvas1.CanDraw = canDraw;
            }
        }

        private void SetWord(string word)
        {
            if(lblDrawWord.InvokeRequired)
            {
                lblDrawWord.Invoke((MethodInvoker)delegate ()
                {
                    lblDrawWord.Text = word;
                });
            }
            else
            {
                lblDrawWord.Text = word;
            }

            MessageBox.Show("You are the drawer. Draw the word in the top left!");
        }

        private void SetWordInfo(int len)
        {
            if (lblDrawWord.InvokeRequired)
            {
                lblDrawWord.Invoke((MethodInvoker)delegate ()
                {
                    lblDrawWord.Text = string.Concat(Enumerable.Repeat("_ ", len).ToArray());
                });
            }
            else
            {
                lblDrawWord.Text = string.Concat(Enumerable.Repeat("_ ", len).ToArray());
            }
        }

        private void UpdateTimer(int val)
        {
            pbTimer.Invoke((MethodInvoker)delegate ()
            {
                if(val <= pbTimer.Maximum && val != 0)
                {
                    pbTimer.Value = val;
                }
            });
        }

        private void AddPlayer(string name, int id)
        {
            lvPlayerList.Invoke((MethodInvoker)delegate ()
            {
                ListViewItemClient c = new ListViewItemClient(id, name);
                c.SubItems.Add("...");
                Players.Add(id, c);
                lvPlayerList.Items.Add(c);
            });
        }

        private void UpdateAllStatus(PlayerStatus s)
        {
            foreach (var i in Players)
                UpdateSingleStatus(i.Key, s);
        }

        private void UpdateSingleStatus(int id, PlayerStatus status)
        {
            if (!Players.ContainsKey(id))
                return;

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    Players[id].SubItems[1].Text = status.ToString();
                });
            }
            else
            {
                Players[id].SubItems[1].Text = status.ToString();
            }
        }

        private void RemovePlayer(int id)
        {
            if (Players.ContainsKey(id))
            {
                lvPlayerList.Invoke((MethodInvoker)delegate ()
                {
                    lvPlayerList.Items.Remove(Players[id]);
                });
                Players.Remove(id);
            }
        }

        private void tbSetBrushSize_Scroll(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                if(cd.ShowDialog() == DialogResult.OK)
                {
                    brushPreview1.SetColor(cd.Color);
                    drawCanvas1.DrawingBrush = new SolidBrush(cd.Color);
                    client.Send(new SetColorPacket(cd.Color));
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            drawCanvas1.ClearDraw();
            client.Send(new ClearCanvasPacket());
        }

        private void drawCanvas1_OnDotDraw(Color c, Point p)
        {
            client.Send(new DrawDotPacket(p));
        }

        private void btnSetSize_Click(object sender, EventArgs e)
        {
            using (frmSetSize sz = new frmSetSize(brushPreview1.BrushColor, brushPreview1.BrushSize))
            {
                if (sz.ShowDialog() == DialogResult.OK)
                {
                    brushPreview1.SetSize(sz.SetSize);
                    drawCanvas1.DrawingSize = sz.SetSize;
                    client.Send(new SetSizePacket(sz.SetSize));
                }
            }
                
        }

        private void HandleMessage(string s, string m)
        {
            if (lbChatLog.InvokeRequired)
            {
                lbChatLog.Invoke((MethodInvoker)delegate ()
                {
                    lbChatLog.Items.Add(string.Format("<{0}> {1}", s, m));
                    lbChatLog.SelectedIndex = lbChatLog.Items.Count - 1;
                });
            }
            else
            {
                lbChatLog.Items.Add(string.Format("<{0}> {1}", s, m));
                lbChatLog.SelectedIndex = lbChatLog.Items.Count - 1;
            }
        }

        private void tbChat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            string tm = tbChat.Text;

            if (string.IsNullOrWhiteSpace(tm))
                return;

            tbChat.Text = string.Empty;
            client.Send(new ChatPacket(tm));
        }
    }
}
