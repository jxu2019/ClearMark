namespace AutoInspexWindowsService
{
    partial class AutoInspexWindowsService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            
            this.WebsocketserverMain = new nsoftware.IPWorksWS.Websocketserver(this.components);
            
            // WebsocketserverMain
            // 
            this.WebsocketserverMain.About = "IP*Works! WebSocket Server 2016 [Build 6800]";
            this.WebsocketserverMain.DefaultTimeout = 1000;
            this.WebsocketserverMain.OnConnected += new nsoftware.IPWorksWS.Websocketserver.OnConnectedHandler(this.WebsocketserverMain_OnConnected);
            this.WebsocketserverMain.OnDataIn += new nsoftware.IPWorksWS.Websocketserver.OnDataInHandler(this.WebsocketserverMain_OnDataIn);
            this.WebsocketserverMain.OnDisconnected += new nsoftware.IPWorksWS.Websocketserver.OnDisconnectedHandler(this.WebsocketserverMain_OnDisconnected);
            this.WebsocketserverMain.OnSSLClientAuthentication += new nsoftware.IPWorksWS.Websocketserver.OnSSLClientAuthenticationHandler(this.WebsocketserverMain_OnSSLClientAuthentication);
            this.WebsocketserverMain.OnSSLStatus += new nsoftware.IPWorksWS.Websocketserver.OnSSLStatusHandler(this.WebsocketserverMain_OnSSLStatus);
            this.WebsocketserverMain.OnWebSocketOpenRequest += new nsoftware.IPWorksWS.Websocketserver.OnWebSocketOpenRequestHandler(this.WebsocketserverMain_OnWebSocketOpenRequest);
            // 
            // RobotControlService
            // 
            this.ServiceName = "AutoPix WebSocket Server Windows Service";

        }



        #endregion

        private nsoftware.IPWorksWS.Websocketserver WebsocketserverMain;
    }
}
