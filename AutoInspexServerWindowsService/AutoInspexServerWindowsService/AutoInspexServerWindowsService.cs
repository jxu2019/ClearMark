using System;
using System.ServiceProcess;
using System.Text;
using System.Collections.Generic;
using System.Xml.Linq;
using log4net;
using System.Timers;
using Newtonsoft.Json;
using nsoftware.IPWorksWS;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace AutoInspexWindowsService
{
    public partial class AutoInspexWindowsService : ServiceBase
    {
        private log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool mRunningInConsole = false;
        public AutoInspexWindowsService()
        {
            ServiceName = "AutoPix WebSocket Client Windows Service";
            InitializeComponent();
        }

        public void RunConsole(string[] args)
        {
            mRunningInConsole = true;
            if (log.IsDebugEnabled)
            {
                log.Debug($"Start {ServiceName}...");
            }
            OnStart(args);
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };

                try
                {
                    if (!WebsocketserverMain.Listening)
                    {
                        WebsocketserverMain.UseSSL = false;
                        WebsocketserverMain.LocalPort = 8801;
                        WebsocketserverMain.Listening = true;
                    }
                }
                catch (IPWorksWSException ipwse)
                {
                    if (log.IsDebugEnabled)
                    {
                        log.Error(ipwse.Message + "\r\n" + ipwse.StackTrace);
                    }
                }

                if (log.IsDebugEnabled)
                {
                    log.Debug($" {this.ServiceName} was successfully started.");
                }
            }

            catch (Exception exception1)
            {
                Exception exception = exception1;
                if (!mRunningInConsole && log.IsDebugEnabled)
                {
                    log.Error(string.Concat(exception.Message, "\r\n", exception.StackTrace));
                }
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("**********************\n");
                stringBuilder.Append("An Exception Occurred!\n");
                stringBuilder.Append(string.Concat("Message: ", exception.Message, "\n"));
                stringBuilder.Append("**********************\n");
                stringBuilder.Append("Stack Trace:\n");
                stringBuilder.Append(exception.StackTrace);
                stringBuilder.Append("\n\n\n");
                if (log.IsDebugEnabled)
                {
                    log.Debug(stringBuilder.ToString());
                }
                OnStop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (log.IsDebugEnabled)
                {
                    log.Debug
                        ($"Shutdown {this.ServiceName}...");
                }

                WebsocketserverMain.Shutdown();

                if (log.IsDebugEnabled)
                {
                    log.Debug($" {this.ServiceName} was shutdown successfully.");
                }
            }
            catch (Exception exception)
            {
                if (log.IsDebugEnabled)
                {
                    string[] message = { exception.Message, "\r\n", exception.Source, "\r\n", exception.StackTrace };

                    log.Error(string.Concat(message));
                }
            }
        }


        private void WebsocketserverMain_OnConnected(object sender, WebsocketserverConnectedEventArgs e)
        {
            log.Info("Client Connected. ConnectionID: " + e.ConnectionId);
        }

        private void WebsocketserverMain_OnDataIn(object sender, WebsocketserverDataInEventArgs ex)
        {
            try
            {
                try
                {
                    log.Info("Received Data From :" + ex.ConnectionId + ";Data: " + ex.Text);

                    var dataObj = JObject.Parse(ex.Text);
                    string inspexIQConnectionId = (string)dataObj["inspexIQConnectionId"];
                    //need to query server for the PI IPaddress
                    if (string.IsNullOrEmpty(inspexIQConnectionId))
                    {
                        string serviceType = (string)dataObj["serviceType"];
                        string sellingMethod = (string)dataObj["sellingMethod"];
                        string vinCode = (string)dataObj["vinCode"];
                        string vehicleId = (string)dataObj["vehicleId"];
                        string autoInspexID = (string)dataObj["autoInspexID"];
                        string uuid = (string)dataObj["uuid"];
                        string imageType = (string)dataObj["imageType"];
                        string sequenceNo = (string)dataObj["sequenceNo"];

                        var message = new
                        {
                            serviceType = serviceType,
                            sellingMethod = sellingMethod,
                            vinCode = vinCode,
                            vehicleId = vehicleId,
                            autoInspexID = autoInspexID,
                            uuid = uuid,
                            imageType = imageType,
                            sequenceNo = sequenceNo,
                            inspexIQConnectionId = ex.ConnectionId,
                        };

                        var json = JsonConvert.SerializeObject(message);

                        foreach (var c in WebsocketserverMain.Connections.Values)
                        {
                            WebsocketserverMain.SendText(c.ConnectionId, json);
                        }
                    }
                    else
                    {
                        WebsocketserverMain.SendText(inspexIQConnectionId, ex.Text);
                    }
                }
                catch (Exception e)
                {

                }
            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }

        private void WebsocketserverMain_OnDisconnected(object sender, WebsocketserverDisconnectedEventArgs e)
        {
            log.Info("Client Disconnected. ConnectionID: " + e.ConnectionId);
        }

        private void WebsocketserverMain_OnSSLClientAuthentication(object sender, WebsocketserverSSLClientAuthenticationEventArgs e)
        {
            e.Accept = true;
        }

        private void WebsocketserverMain_OnSSLStatus(object sender, WebsocketserverSSLStatusEventArgs e)
        {
            log.Info("ConnectionID: " + e.ConnectionId + "; Message:" + e.Message);
        }

        private void WebsocketserverMain_OnWebSocketOpenRequest(object sender, WebsocketserverWebSocketOpenRequestEventArgs e)
        {
            log.Info("ConnectionID: " + e.ConnectionId + "; Message:" + e.RequestHeaders);
        }
    }
}