/*
 * IPWorks WebSockets 2020 .NET Edition - Demo Application
 * Copyright (c) 2020 /n software inc. - All rights reserved. - www.nsoftware.com
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using nsoftware.IPWorksWS;
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Linq;

namespace nsoftware.Demos
{
    /// <summary>
    /// Summary description for echoclientDemo.
    /// </summary>
    public class websocketclientDemo : System.Windows.Forms.Form
    {
        private nsoftware.IPWorksWS.Websocketclient websocketclient1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label lNotice;
        internal System.Windows.Forms.Button bSend;
        internal System.Windows.Forms.TextBox tbEcho;
        internal System.Windows.Forms.Label lServer;
        internal System.Windows.Forms.Button bConnect;
        internal System.Windows.Forms.TextBox tbServer;
        internal Button button1;
        internal Button button2;
        private Timer timer1;
        internal TextBox textBox1;
        internal Label label2;
        internal Label label3;
        private System.ComponentModel.IContainer components;

        public websocketclientDemo()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(websocketclientDemo));
            this.websocketclient1 = new nsoftware.IPWorksWS.Websocketclient(this.components);
            this.Label1 = new System.Windows.Forms.Label();
            this.lNotice = new System.Windows.Forms.Label();
            this.bSend = new System.Windows.Forms.Button();
            this.tbEcho = new System.Windows.Forms.TextBox();
            this.lServer = new System.Windows.Forms.Label();
            this.bConnect = new System.Windows.Forms.Button();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // websocketclient1
            // 
            this.websocketclient1.About = "IPWorks WebSockets 2020 [Build 7525]";
            this.websocketclient1.Firewall.Port = 1080;
            this.websocketclient1.OnConnected += new nsoftware.IPWorksWS.Websocketclient.OnConnectedHandler(this.websocketclient1_OnConnected);
            this.websocketclient1.OnDataIn += new nsoftware.IPWorksWS.Websocketclient.OnDataInHandler(this.websocketclient1_OnDataIn);
            this.websocketclient1.OnDisconnected += new nsoftware.IPWorksWS.Websocketclient.OnDisconnectedHandler(this.websocketclient1_OnDisconnected);
            this.websocketclient1.OnSSLServerAuthentication += new nsoftware.IPWorksWS.Websocketclient.OnSSLServerAuthenticationHandler(this.websocketclient1_OnSSLServerAuthentication);
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(8, 104);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(144, 16);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "Data received from server:";
            // 
            // lNotice
            // 
            this.lNotice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lNotice.ForeColor = System.Drawing.SystemColors.Highlight;
            this.lNotice.Location = new System.Drawing.Point(8, 5);
            this.lNotice.Name = "lNotice";
            this.lNotice.Size = new System.Drawing.Size(1231, 45);
            this.lNotice.TabIndex = 17;
            this.lNotice.Text = resources.GetString("lNotice.Text");
            // 
            // bSend
            // 
            this.bSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSend.Location = new System.Drawing.Point(1143, 82);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(75, 23);
            this.bSend.TabIndex = 22;
            this.bSend.Text = "Retake";
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tbEcho
            // 
            this.tbEcho.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbEcho.Location = new System.Drawing.Point(11, 123);
            this.tbEcho.Multiline = true;
            this.tbEcho.Name = "tbEcho";
            this.tbEcho.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbEcho.Size = new System.Drawing.Size(1101, 760);
            this.tbEcho.TabIndex = 21;
            // 
            // lServer
            // 
            this.lServer.Location = new System.Drawing.Point(12, 57);
            this.lServer.Name = "lServer";
            this.lServer.Size = new System.Drawing.Size(80, 23);
            this.lServer.TabIndex = 15;
            this.lServer.Text = "Echo Server:";
            // 
            // bConnect
            // 
            this.bConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bConnect.Location = new System.Drawing.Point(1143, 53);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 23;
            this.bConnect.Tag = "disconnected";
            this.bConnect.Text = "Connect";
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // tbServer
            // 
            this.tbServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbServer.Location = new System.Drawing.Point(143, 55);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(523, 20);
            this.tbServer.TabIndex = 19;
            this.tbServer.Text = "ws://localhost:8801";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1143, 121);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "Take 72 ";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1143, 295);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Clear";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(143, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(523, 20);
            this.textBox1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 23);
            this.label2.TabIndex = 28;
            this.label2.Text = "Sequence Numbers:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(672, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "(Example: all or 12,34,56)";
            // 
            // websocketclientDemo
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1247, 892);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.lNotice);
            this.Controls.Add(this.bSend);
            this.Controls.Add(this.tbEcho);
            this.Controls.Add(this.lServer);
            this.Controls.Add(this.bConnect);
            this.Controls.Add(this.tbServer);
            this.Name = "websocketclientDemo";
            this.Text = "WebSocketClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new websocketclientDemo());
        }



        private void websocketclient1_OnConnected(object sender, nsoftware.IPWorksWS.WebsocketclientConnectedEventArgs e)
        {
            bConnect.Enabled = true;
            tbEcho.AppendText(e.StatusCode == 0 ? "Connected.\r\n" : "Failed to connect. Reason: " + e.Description + ".\r\n");
        }

        private void websocketclient1_OnDataIn(object sender, nsoftware.IPWorksWS.WebsocketclientDataInEventArgs e)
        {
            tbEcho.AppendText(e.Text.Replace("\r\n", "").Replace("\n", "")+"\r\n" + "\r\n");
        }

        private void websocketclient1_OnDisconnected(object sender, nsoftware.IPWorksWS.WebsocketclientDisconnectedEventArgs e)
        {
            bConnect.Enabled = true;
            bConnect.Text = "Connect";
            tbEcho.AppendText("Disconnected.\r\n");
        }

        private void bConnect_Click(object sender, System.EventArgs e)
        {
            bConnect.Enabled = false;
            try
            {
                if (bConnect.Text == "Connect")
                {
                    websocketclient1.Timeout = 10;
                    websocketclient1.InvokeThrough = this;

                    websocketclient1.Connect(tbServer.Text);
                    bConnect.Text = "Disconnect";
                }
                else
                {
                    websocketclient1.Disconnect();
                }
            }
            catch (IPWorksWSException ipwse)
            {
                MessageBox.Show(this, ipwse.Message, "Error", MessageBoxButtons.OK);
                bConnect.Text = "Connect";
                bConnect.Enabled = true;
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void bSend_Click(object sender, System.EventArgs e)
        {
            if (websocketclient1.Connected)
            {
                var message = new
                {
                    serviceType = "staging",
                    sellingMethod = "wholesale",
                    vinCode = "WBA3B1C53EK132571",
                    vehicleId = "198783",
                    imageType = "autopix",
                    autoInspexID = "90212-3-1-32-5",
                    sequenceNo= textBox1.Text,
                    uuid = "FD012F881A10439595E0F7239E54EE35"
                };

                var json = JsonConvert.SerializeObject(message);
                this.websocketclient1.SendText(json);
            }
            else
            {
                tbEcho.AppendText("You are not connected.\r\n");
            }
        }

        private void websocketclient1_OnSSLServerAuthentication(object sender, nsoftware.IPWorksWS.WebsocketclientSSLServerAuthenticationEventArgs e)
        {
            if (!e.Accept)
            {
                string caption =
                    "Server provided the following certificate:" + "\r\n" +
                    "Subject: " + e.CertSubject + "\r\n" +
                    "Issuer :" + e.CertIssuer + "\r\n" +
                    "The following problems have been determined for this certificate: " + e.Status + "\r\n" +
                    "Would you like to continue or cancel the connection?";
                e.Accept = MessageBox.Show(this, caption, "Alert", MessageBoxButtons.YesNoCancel) == DialogResult.Yes;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (websocketclient1.Connected)
            {
                var message = new
                {
                    serviceType = "staging",
                    sellingMethod = "wholesale",
                    vinCode = "WBA3B1C53EK132571",
                    vehicleId = "198783",
                    imageType = "autopix",
                    autoInspexID = "90212-3-1-32-5",
                    sequenceNo = "all",
                    uuid = "FD012F881A10439595E0F7239E54EE35"
                };

                var json = JsonConvert.SerializeObject(message);
                this.websocketclient1.SendText(json);
            }
            else
            {
                tbEcho.AppendText("You are not connected.\r\n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tbEcho.Text = "";
        }
    }
}




















