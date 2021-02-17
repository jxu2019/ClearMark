using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Configuration;
using RestSharp;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace AutoInspexClientWindowsService
{

    static class Program
    {
        private static log4net.ILog log =
    log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <summary>
        /// </summary>
        static void Main(params string[] parameters)
        {
            //Task.Factory.StartNew(()=> InstallnewPI());
            if (parameters.Length > 0 && parameters[0].ToLower() == "/console")
            {
                new global::AutoInspexClientWindowsService.AutoInspexClientWindowsService().RunConsole(parameters);
            }
            else if (parameters.Length > 0 && parameters[0].ToLower() == "/c")
            {
                new global::AutoInspexClientWindowsService.AutoInspexClientWindowsService().RunConsole(parameters);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new global::AutoInspexClientWindowsService.AutoInspexClientWindowsService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }

        private static Dictionary<string, object> ToJsonDict(object obj)
        {
            if (obj == null) return null;

            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }

        static async void InstallnewPI()
        {
            try
            {

                var message = new
                {
                    AutoInspexID = "90212-3-1-32-5",
                    InstallerName = "Keveb Tester",
                    RingPosition = "23",
                    InstallDate = DateTime.Now.ToShortDateString(),
                    SerialNumber = "eqerq24524524524",
                };
                Dictionary<string, object> postData = ToJsonDict(message); ;

                var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
                var client = new RestClient(AutoInspexWebServiceUrl);
                var request = new RestRequest("api/InstallPI4Camera", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(postData);


                //var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
                //var client = new RestClient(AutoInspexWebServiceUrl);
                //var request = new RestRequest("api/RaspBerryPI/", Method.POST);
                //var jsonToSend = JsonConvert.SerializeObject(message);
                //request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                //request.RequestFormat = DataFormat.Json;

                try
                {
                    var ret = await client.ExecuteAsync(request);
                    if (ret.StatusCode != HttpStatusCode.OK)
                    {
                        log.Error(ret.ErrorMessage, ret.ErrorException);
                    }
                }
                catch (Exception ext)
                {
                    log.Error(ext.Message, ext);
                }

            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }

        static async void saveData()
        {
            try
            {

                var message = new
                {
                    AutoInspexID = "90212-3-1-32-5",
                    SerialNumber = "qsw23452345",
                    HousingID = "234523",
                    LensID = "1",
                    SensorID = "1",
                    PiVersion = "Raspbian 15",
                    PiOSVersion = "PI 4",
                    OS_ID = "1",
                    IPAddress = "192.168.0.1",
                    Status = "Active"
                };
                Dictionary<string, object> postData = ToJsonDict(message); ;

                var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
                var client = new RestClient(AutoInspexWebServiceUrl);
                var request = new RestRequest("api/SavePIData", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(postData);


                //var AutoInspexWebServiceUrl = ConfigurationManager.AppSettings["AutoInspexWebServiceUrl"] + "";
                //var client = new RestClient(AutoInspexWebServiceUrl);
                //var request = new RestRequest("api/RaspBerryPI/", Method.POST);
                //var jsonToSend = JsonConvert.SerializeObject(message);
                //request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
                //request.RequestFormat = DataFormat.Json;

                try
                {
                    var ret = await client.ExecuteAsync(request);
                    if (ret.StatusCode != HttpStatusCode.OK)
                    {
                        log.Error(ret.ErrorMessage, ret.ErrorException);
                    }
                }
                catch (Exception ext)
                {
                    log.Error(ext.Message, ext);
                }

            }
            catch (Exception ext)
            {
                log.Error(ext.Message, ext);
            }
        }
    }
}
