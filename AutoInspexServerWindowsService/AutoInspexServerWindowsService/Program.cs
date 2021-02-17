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

namespace AutoInspexServerWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <summary>
        /// </summary>
        static void Main(params string[] parameters)
        {
            if (parameters.Length > 0 && parameters[0].ToLower() == "/console")
            {
                new AutoInspexWindowsService.AutoInspexWindowsService().RunConsole(parameters);
            }
            else if (parameters.Length > 0 && parameters[0].ToLower() == "/c")
            {
                new AutoInspexWindowsService.AutoInspexWindowsService().RunConsole(parameters);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new AutoInspexWindowsService.AutoInspexWindowsService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
