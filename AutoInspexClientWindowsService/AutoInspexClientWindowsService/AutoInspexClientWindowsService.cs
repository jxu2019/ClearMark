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
using System.Text.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace AutoInspexClientWindowsService
{
    public partial class AutoInspexClientWindowsService : ServiceBase
    {
        private log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private bool mRunningInConsole = false;
        private bool isDataReceived = false;
        private string dataReceived = null;
        public AutoInspexClientWindowsService()
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
                    var AutoInspexWebSocketServerUrl = ConfigurationManager.AppSettings["AutoInspexWebSocketServerUrl"] + "";
                    websocketclientToServer.Timeout = 120;
                    websocketclientToServer.Connect(AutoInspexWebSocketServerUrl);
                    websocketclientToServer.Config("KeepAlive=true");
                }
                catch (IPWorksWSException ipwse)
                {
                    log.Error(ipwse.Message + "\r\n" + ipwse.StackTrace);
                }

                try
                {
                    if (!WebsocketserverMain.Listening)
                    {
                        WebsocketserverMain.UseSSL = false;
                        WebsocketserverMain.LocalPort = 6001;
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

                websocketclientToServer.Disconnect();
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


        private void websocketclientToServer_OnConnected(object sender, nsoftware.IPWorksWS.WebsocketclientConnectedEventArgs e)
        {
            log.Info("Client Connected. ConnectionID: " + e.Description);
        }

        private async void websocketclientToServer_OnDataIn(object sender, nsoftware.IPWorksWS.WebsocketclientDataInEventArgs e)
        {
            try
            {
                log.Info("Received Data From AutoPix backend server:" + e.Text);
                var AutoInspexID = ConfigurationManager.AppSettings["AutoInspexID"] + "";
                var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
                List<int> camerasSequecences = null;
                var dataObj = JObject.Parse(e.Text);
                var sequenceNums = (string)dataObj["sequenceNo"];
                if (sequenceNums.ToLower() == "all")
                {
                    camerasSequecences = Enumerable.Range(1, 72).ToList();
                }
                else
                {
                    var sequenceNoArray = sequenceNums.Split(',');
                    var list = new List<int>();
                    foreach (var i in sequenceNoArray)
                    {
                        try
                        {
                            list.Add(Convert.ToInt32(i));
                        }
                        catch (Exception ext)
                        {
                            log.Error(ext.Message, ext);
                        }
                    }
                    camerasSequecences = list;
                }
                if (dataObj != null && (string)dataObj["autoInspexID"] == AutoInspexID)
                {
                    string serviceType = (string)dataObj["serviceType"];
                    string sellingMethod = (string)dataObj["sellingMethod"];
                    string vinCode = (string)dataObj["vinCode"];
                    string vehicleId = (string)dataObj["vehicleId"];
                    string autoInspexID = (string)dataObj["autoInspexID"];
                    string uuid = (string)dataObj["uuid"];
                    string imageType = (string)dataObj["imageType"];
                    string sequenceNo = (string)dataObj["sequenceNo"];
                    string inspexIQConnectionId = (string)dataObj["inspexIQConnectionId"];

                    var client = new RestClient(AutoInspexWebServiceUrl);
                    var request = new RestRequest("/api/PICameras/" + autoInspexID, DataFormat.Json);
                    var piCameras = await client.GetAsync<List<PICamera>>(request);
                    int countCamera = 0;
                    if (piCameras != null && piCameras.Any())
                    {
                        foreach (var p in piCameras.Where(p => camerasSequecences.Contains(Convert.ToInt32(p.RingPosition))))
                        {
                            countCamera++;

                            try
                            {
                                string piIPAddress = (string)dataObj["piIPAddress"];

                                this.components = new System.ComponentModel.Container();
                                var websocketclient = new nsoftware.IPWorksWS.Websocketclient(this.components);
                                websocketclient.Firewall.Port = 1080;
                                websocketclient.OnLog += Websocketclient_OnLog;
                                websocketclient.Firewall.FirewallType = nsoftware.IPWorksWS.FirewallTypes.fwNone;
                                websocketclient.OnDataIn += new nsoftware.IPWorksWS.Websocketclient.OnDataInHandler(this.websocketclient_OnDataIn);
                                websocketclient.OnConnected += Websocketclient_OnConnected;
                                websocketclient.OnError += Websocketclient_OnError;
                                websocketclient.Connect($"ws://{p.IPAddress}:5001");

                                var message = new
                                {
                                    serviceType = serviceType,
                                    sellingMethod = sellingMethod,
                                    vinCode = vinCode,
                                    vehicleId = vehicleId,
                                    autoInspexID = autoInspexID,
                                    uuid = uuid,
                                    imageType = imageType,
                                    sequenceNo = p.RingPosition,
                                    inspexIQConnectionId = inspexIQConnectionId,
                                };

                                var json = JsonConvert.SerializeObject(message);
                                websocketclient.DataToSend = json;
                                websocketclient.Timeout = 120;
                            }
                            catch (IPWorksWSException ipwse)
                            {
                                log.Error(ipwse.Message + "\r\n" + ipwse.StackTrace);

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
                                    inspexIQConnectionId = inspexIQConnectionId,
                                    error = "Unable to connect to the camera at postion " + p.RingPosition
                                };

                                var json = JsonConvert.SerializeObject(message);
                                this.websocketclientToServer.SendText(json);
                            }
                            catch (Exception ext)
                            {
                                log.Error(ext.Message, ext);
                            }
                        }
                        if (countCamera == 0)
                        {
                            log.Error("countCamera=0");

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
                                inspexIQConnectionId = inspexIQConnectionId,
                                error = "No camera found to take pictures. Please check the sequenceNo"
                            };

                            var json = JsonConvert.SerializeObject(message);
                            this.websocketclientToServer.SendText(json);
                        }
                    }
                    else
                    {
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
                            inspexIQConnectionId = inspexIQConnectionId,
                            error = "No camera configured for  autoInspexID" + autoInspexID
                        };

                        var json = JsonConvert.SerializeObject(message);
                        this.websocketclientToServer.SendText(json);
                    }
                }
            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }

        private void Websocketclient_OnError(object sender, WebsocketclientErrorEventArgs e)
        {
            try
            {
                log.Error("Unable to connect to PI4:" + e.Description);
                //var socketClienbt = sender as Websocketclient;

                //var dataObj = JObject.Parse(socketClienbt.DataToSend);
                //if (dataObj != null)
                //{
                //    string piIPAddress = (string)dataObj["piIPAddress"];
                //    log.Error("Unable to connect to PI4:" + e.Description);
                //    this.websocketclientToServer.SendText(dataReceived);
                //}
            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }

        private void Websocketclient_OnConnected(object sender, WebsocketclientConnectedEventArgs e)
        {
            log.Info("Client to PI Connected. ConnectionID: " + e.Description);
        }

        private void websocketclient_OnDataIn(object sender, nsoftware.IPWorksWS.WebsocketclientDataInEventArgs e)
        {
            try
            {

                dataReceived = e.Text;
                log.Info(dataReceived);
                isDataReceived = false;
                this.websocketclientToServer.SendText(dataReceived);

            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }

        private void Websocketclient_OnLog(object sender, WebsocketclientLogEventArgs e)
        {
            log.Info(e.Message);
        }

        private void websocketclientToServer_OnDisconnected(object sender, nsoftware.IPWorksWS.WebsocketclientDisconnectedEventArgs e)
        {
            log.Info("Client Connected. ConnectionID: " + e.Description);
        }

        private void WebsocketserverMain_OnConnected(object sender, WebsocketserverConnectedEventArgs e)
        {
            log.Info("Client Connected. ConnectionID: " + e.ConnectionId);
            WebsocketserverMain.SendText(e.ConnectionId, "hell you are connected.");
        }

        private async void WebsocketserverMain_OnDataIn(object sender, WebsocketserverDataInEventArgs ex)
        {
            //try
            //{
            //    log.Info(ex.Text);

            //    var message = new PICamera()
            //    {
            //        AutoInspexID = AutoInspexID,
            //        SerialNumber = SerialNumber,
            //        HousingID = HousingID,
            //        LensID = LensID,
            //        SensorID = SensorID,
            //        PiVersion = PiVersion,
            //        PiOSVersion = PiOSVersion,
            //        OS_ID = OS_ID,
            //        IPAddress = IPAddress,
            //        Status = "Active",
            //        AutoInspexID = "Active",
            //    };

            //    var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
            //    var client = new RestClient(AutoInspexWebServiceUrl);
            //    var request = new RestRequest("api/PICameras/UpdateStatus", Method.POST);
            //    var jsonToSend = JsonConvert.SerializeObject(ex.Text);
            //    request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
            //    request.RequestFormat = DataFormat.Json;

            //    try
            //    {
            //       var ret = await client.ExecuteAsync(request);
            //        if(ret.StatusCode!= HttpStatusCode.OK)
            //        {
            //            log.Error(ret.ErrorMessage,ret.ErrorException);
            //        }
            //    }
            //    catch (Exception ext)
            //    {
            //        log.Error(ext.Message, ext);
            //    }

            //}
            //catch (Exception ext)
            //{
            //    log.Error(ext.Message, ext);
           // }
        }

        private void WebsocketserverMain_OnDisconnected(object sender, WebsocketserverDisconnectedEventArgs e)
        {
            log.Info("Client Disconnected. ConnectionID: " + e.ConnectionId);
        }


        private void WebsocketserverMain_OnWebSocketOpenRequest(object sender, WebsocketserverWebSocketOpenRequestEventArgs e)
        {
            log.Info("ConnectionID: " + e.ConnectionId + "; Message:" + e.RequestHeaders);
        }

        private void websocketclientToServer_OnError(object sender, WebsocketclientErrorEventArgs e)
        {
            try
            {
                log.Error("Unable to connect to AutoInpex Web Socket Server:" + e.Description);
            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }
    }
}