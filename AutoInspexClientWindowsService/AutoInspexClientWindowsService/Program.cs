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
      
    }
}
