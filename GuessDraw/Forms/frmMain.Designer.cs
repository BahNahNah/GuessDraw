namespace GuessDraw.Forms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbChatLog = new System.Windows.Forms.ListBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.lvPlayerList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbDrawing = new System.Windows.Forms.GroupBox();
            this.btnSetSize = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblDrawWord = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbTimer = new System.Windows.Forms.ProgressBar();
            this.brushPreview1 = new GuessDraw.Controls.BrushPreview();
            this.drawCanvas1 = new GuessDraw.Controls.DrawCanvas();
            this.panel1.SuspendLayout();
            this.gbDrawing.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.drawCanvas1);
            this.panel1.Location = new System.Drawing.Point(222, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(331, 498);
            this.panel1.TabIndex = 0;
            // 
            // lbChatLog
            // 
            this.lbChatLog.FormattingEnabled = true;
            this.lbChatLog.Location = new System.Drawing.Point(559, 15);
            this.lbChatLog.Name = "lbChatLog";
            this.lbChatLog.Size = new System.Drawing.Size(268, 472);
            this.lbChatLog.TabIndex = 1;
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(559, 493);
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(268, 20);
            this.tbChat.TabIndex = 2;
            this.tbChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbChat_KeyDown);
            // 
            // lvPlayerList
            // 
            this.lvPlayerList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader3});
            this.lvPlayerList.Location = new System.Drawing.Point(1, 132);
            this.lvPlayerList.Name = "lvPlayerList";
            this.lvPlayerList.Size = new System.Drawing.Size(215, 143);
            this.lvPlayerList.TabIndex = 3;
            this.lvPlayerList.UseCompatibleStateImageBehavior = false;
            this.lvPlayerList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 66;
            // 
            // gbDrawing
            // 
            this.gbDrawing.Controls.Add(this.btnSetSize);
            this.gbDrawing.Controls.Add(this.btnClear);
            this.gbDrawing.Controls.Add(this.button1);
            this.gbDrawing.Controls.Add(this.brushPreview1);
            this.gbDrawing.Location = new System.Drawing.Point(1, 281);
            this.gbDrawing.Name = "gbDrawing";
            this.gbDrawing.Size = new System.Drawing.Size(215, 169);
            this.gbDrawing.TabIndex = 4;
            this.gbDrawing.TabStop = false;
            this.gbDrawing.Text = "Drawing";
            // 
            // btnSetSize
            // 
            this.btnSetSize.Location = new System.Drawing.Point(6, 79);
            this.btnSetSize.Name = "btnSetSize";
            this.btnSetSize.Size = new System.Drawing.Size(195, 26);
            this.btnSetSize.TabIndex = 6;
            this.btnSetSize.Text = "Set Size";
            this.btnSetSize.UseVisualStyleBackColor = true;
            this.btnSetSize.Click += new System.EventHandler(this.btnSetSize_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 140);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(195, 23);
            this.btnClear.TabIndex = 5;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Set Colour";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblDrawWord
            // 
            this.lblDrawWord.AutoSize = true;
            this.lblDrawWord.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrawWord.Location = new System.Drawing.Point(6, 16);
            this.lblDrawWord.Name = "lblDrawWord";
            this.lblDrawWord.Size = new System.Drawing.Size(104, 30);
            this.lblDrawWord.TabIndex = 5;
            this.lblDrawWord.Text = "<NONE>";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDrawWord);
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 62);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Word";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pbTimer);
            this.groupBox2.Location = new System.Drawing.Point(1, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 55);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Timer";
            // 
            // pbTimer
            // 
            this.pbTimer.Location = new System.Drawing.Point(6, 19);
            this.pbTimer.Maximum = 60;
            this.pbTimer.Name = "pbTimer";
            this.pbTimer.Size = new System.Drawing.Size(203, 23);
            this.pbTimer.TabIndex = 0;
            // 
            // brushPreview1
            // 
            this.brushPreview1.Location = new System.Drawing.Point(76, 23);
            this.brushPreview1.Name = "brushPreview1";
            this.brushPreview1.Size = new System.Drawing.Size(50, 50);
            this.brushPreview1.TabIndex = 1;
            this.brushPreview1.Text = "brushPreview";
            // 
            // drawCanvas1
            // 
            this.drawCanvas1.BackColor = System.Drawing.Color.White;
            this.drawCanvas1.CanDraw = true;
            this.drawCanvas1.DrawingSize = 10;
            this.drawCanvas1.Location = new System.Drawing.Point(3, 3);
            this.drawCanvas1.Name = "drawCanvas1";
            this.drawCanvas1.Size = new System.Drawing.Size(325, 492);
            this.drawCanvas1.TabIndex = 0;
            this.drawCanvas1.Text = "drawCanvas1";
            this.drawCanvas1.OnDotDraw += new GuessDraw.Controls.OnDotDrawDelegate(this.drawCanvas1_OnDotDraw);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 519);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbDrawing);
            this.Controls.Add(this.lvPlayerList);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.lbChatLog);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMain";
            this.Text = "GuessDraw";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.gbDrawing.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.DrawCanvas drawCanvas1;
        private System.Windows.Forms.ListBox lbChatLog;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.ListView lvPlayerList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox gbDrawing;
        private System.Windows.Forms.Button button1;
        private Controls.BrushPreview brushPreview1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSetSize;
        private System.Windows.Forms.Label lblDrawWord;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar pbTimer;
    }
}