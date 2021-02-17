namespace AutoInspexClientWindowsService
{
    partial class AutoInspexClientWindowsService
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private nsoftware.IPWorksWS.Websocketclient websocketclientToServer;
        private nsoftware.IPWorksWS.Websocketserver WebsocketserverMain;
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.websocketclientToServer = new nsoftware.IPWorksWS.Websocketclient(this.components);
            this.WebsocketserverMain = new nsoftware.IPWorksWS.Websocketserver(this.components);
            // 
            // websocketclientToServer
            // 
            this.websocketclientToServer.About = "IPWorks WebSockets 2020 [Build 7525]";
            this.websocketclientToServer.Firewall.Port = 1080;
            this.websocketclientToServer.OnConnected += new nsoftware.IPWorksWS.Websocketclient.OnConnectedHandler(this.websocketclientToServer_OnConnected);
            this.websocketclientToServer.OnDataIn += new nsoftware.IPWorksWS.Websocketclient.OnDataInHandler(this.websocketclientToServer_OnDataIn);
            this.websocketclientToServer.OnDisconnected += new nsoftware.IPWorksWS.Websocketclient.OnDisconnectedHandler(this.websocketclientToServer_OnDisconnected);
            this.websocketclientToServer.OnError += new nsoftware.IPWorksWS.Websocketclient.OnErrorHandler(this.websocketclientToServer_OnError);
            this.websocketclientToServer.OnLog += new nsoftware.IPWorksWS.Websocketclient.OnLogHandler(this.Websocketclient_OnLog);
            // 
            // WebsocketserverMain
            // 
            this.WebsocketserverMain.About = "IPWorks WebSockets 2020 [Build 7525]";
            this.WebsocketserverMain.DefaultTimeout = 1000;
            this.WebsocketserverMain.OnConnected += new nsoftware.IPWorksWS.Websocketserver.OnConnectedHandler(this.WebsocketserverMain_OnConnected);
            this.WebsocketserverMain.OnDataIn += new nsoftware.IPWorksWS.Websocketserver.OnDataInHandler(this.WebsocketserverMain_OnDataIn);
            this.WebsocketserverMain.OnDisconnected += new nsoftware.IPWorksWS.Websocketserver.OnDisconnectedHandler(this.WebsocketserverMain_OnDisconnected);
            this.WebsocketserverMain.OnWebSocketOpenRequest += new nsoftware.IPWorksWS.Websocketserver.OnWebSocketOpenRequestHandler(this.WebsocketserverMain_OnWebSocketOpenRequest);
            // 
            // AutoInspexClientWindowsService
            // 
            this.ServiceName = "AutoPix WebSocket Client Windows Service";

        }

        #endregion

    }
}
