using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoInspexService.Controllers
{
    public class PICamera
    {
        public int PICameraId { get; set; }
        public string AutoInspexID { get; set; }
        public string InstallerName { get; set; }
        public string RingPosition { get; set; }
        public string InstallDate { get; set; }
        public string SerialNumber { get; set; }
        public int HousingID { get; set; }
        public string LensID { get; set; }
        public string SensorID { get; set; }
        public string PiVersion { get; set; }
        public string PiOSVersion { get; set; }
        public string OS_ID { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
    }

    public class PICameraStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; }
    }
}