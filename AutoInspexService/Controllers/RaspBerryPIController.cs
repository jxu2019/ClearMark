using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Data;
using Dapper;
using log4net;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Web.Hosting;
using Newtonsoft.Json.Linq;

namespace AutoInspexService.Controllers
{
    public class RaspBerryPIController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        [Route("api/PICameraStatus")]
        public IEnumerable<PICameraStatus> GetPICameraStatus()
        {
            try
            {

                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                log.Info(path);

                var list = new List<PICameraStatus>();
                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    var output = cnn.Query<PICameraStatus>("select * from PICameraStatus");

                    return output;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        [Route("api/PICameras")]
        public IEnumerable<PICamera> GetPICameras()
        {
            try
            {

                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                log.Info(path);

                var list = new List<PICamera>();
                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    var output = cnn.Query<PICamera>("select * from PICameras");

                    return output;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        [Route("api/PICamera/{id}")]
        public PICamera Get(int id)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    IEnumerable<PICamera> piCameras = cnn.Query<PICamera>(string.Concat("select * from PICameras where PICameraId=", id), new DynamicParameters(), null, true, null, null);
                    if (piCameras != null && piCameras.Any())
                    {
                        return piCameras.FirstOrDefault();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        [HttpGet]
        [Route("api/PICameras/{autoInspexID}")]
        public IEnumerable<PICamera> FindCameraAutoInspexID(string autoInspexID)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    IEnumerable<PICamera> piCameras = cnn.Query<PICamera>(string.Concat("select * from PICameras where autoInspexID='", autoInspexID + "'"), new DynamicParameters(), null, true, null, null);
                    if (piCameras != null && piCameras.Any())
                    {
                        return piCameras;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }

        public PICamera FindCameraBySerialNumber(string SerialNumber)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    IEnumerable<PICamera> piCameras = cnn.Query<PICamera>(string.Concat("select * from PICameras where SerialNumber='", SerialNumber + "'"), new DynamicParameters(), null, true, null, null);
                    if (piCameras != null && piCameras.Any())
                    {
                        return piCameras.FirstOrDefault();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return null;
            }
        }
        [HttpPost]
        [Route("api/SavePIData")]
        public HttpResponseMessage Post([FromBody] JObject data)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                var HousingID = data["HousingID"] + "";
                var IPAddress = data["IPAddress"] + "";
                var SensorID = data["SensorID"];
                var PiVersion = data["PiVersion"] + "";
                var PiOSVersion = data["PiOSVersion"] + "";
                var LensID = data["LensID"] + "";
                var OS_ID = data["OS_ID"] + "";
                var SerialNumber = data["SerialNumber"] + "";
                var Status = data["Status"] + "";
                var AutoInspexID = data["AutoInspexID"] + "";
                var RingPosition = data["RingPosition"] + "";

                var picamera = FindCameraBySerialNumber(SerialNumber);
                if (picamera != null)
                {
                    using (IDbConnection cnn = new SQLiteConnection(connstr))
                    {
                        string sql = string.Concat(new object[] { "update PICameras set HousingID='", HousingID, "',IPAddress='", IPAddress, "',LensID='", LensID, "',RingPosition='", RingPosition, "',SensorID='", SensorID, "',PiOSVersion='", PiOSVersion, "',OS_ID='", OS_ID, "',PiVersion='", PiVersion, "',TimeStamp='", DateTime.Now, "',Status='" + Status + "', AutoInspexID='" + AutoInspexID + "' where SerialNumber='", SerialNumber, "'" });
                        log.Info(sql);
                        var ret = cnn.Execute(sql);
                        if (ret > 0)
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }
                    }
                }
                else
                {
                    using (IDbConnection cnn = new SQLiteConnection(connstr))
                    {
                        string sql = string.Concat(new object[] { "insert into PICameras (HousingID,IPAddress,SensorID,PiVersion,PiOSVersion,LensID,OS_ID,SerialNumber,Status,AutoInspexID,RingPosition) values(", "'" + HousingID + "',", "'" + IPAddress + "',", "'" + SensorID + "',", "'" + PiVersion + "',", "'" + PiOSVersion + "',", "'" + LensID + "',", "'" + OS_ID + "',", "'" + SerialNumber + "',", "'" + Status + "',", "'" + AutoInspexID + "','"+ RingPosition + "')" });
                        log.Info(sql);
                        var ret = cnn.Execute(sql);
                        if (ret > 0)
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }
                    }
                }
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("api/InstallPI4Camera")]
        public HttpResponseMessage PostIntallPI([FromBody] JObject data)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                var InstallerName = data["InstallerName"] + "";
                var RingPosition = data["RingPosition"] + "";
                var InstallDate = data["InstallDate"] + "";
                var SerialNumber = data["SerialNumber"] + "";
                var AutoInspexID = data["AutoInspexID"] + "";

                var picamera = FindCameraBySerialNumber(SerialNumber);
                if (picamera != null)
                {

                    using (IDbConnection cnn = new SQLiteConnection(connstr))
                    {
                        string sql = string.Concat(new object[] { "update PICameras set InstallerName='", InstallerName, "',RingPosition='", RingPosition, "',InstallDate='", InstallDate, "',AutoInspexID='", AutoInspexID, "',TimeStamp='", DateTime.Now.ToShortDateString(), "',Status='Inactive' where SerialNumber='", SerialNumber + "'" });
                        log.Info(sql);
                        var ret = cnn.Execute(sql);
                        if (ret > 0)
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }
                    }
                }
                else
                {
                    using (IDbConnection cnn = new SQLiteConnection(connstr))
                    {
                        string sql = string.Concat(new object[] { "insert into PICameras (InstallerName,RingPosition,InstallDate,AutoInspexID,TimeStamp,SerialNumber,Status) values(", "'" + InstallerName + "',", "'" + RingPosition + "',", "'" + InstallDate + "',", "'" + AutoInspexID + "',", "'" + DateTime.Now + "'," + "'" + SerialNumber + "',", "'Inactive')" });
                        log.Info(sql);
                        var ret = cnn.Execute(sql);
                        if (ret > 0)
                        {
                            return this.Request.CreateResponse(HttpStatusCode.OK);
                        }
                        else
                        {
                            return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                        }
                    }
                }
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        // PUT api/values/5
        public void Put()
        {

        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var path = HostingEnvironment.MapPath("~") + "/AutoInspexCamerasDB.db";
                var connstr = "Data Source=" + path + ";Version=3;";

                using (IDbConnection cnn = new SQLiteConnection(connstr))
                {
                    string sql = string.Concat(new object[] { "delete from PICameras where PICameraId='", id, "'" });
                    log.Info(sql);
                    var ret = cnn.Execute(sql);
                    if (ret > 0)
                    {
                        return this.Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
