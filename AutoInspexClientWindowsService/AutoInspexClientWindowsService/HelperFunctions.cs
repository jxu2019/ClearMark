using System;
using System.IO;
using System.Text;

namespace AutoInspexClientWindowsService
{
    public class PICamera
    {
        public int PICameraId { get; set; }
        public string AutoInspexID { get; set; }
        public string InstallerName { get; set; }
        public string RingPosition { get; set; }
        public string InstallDate { get; set; }
        public string SerialNumber { get; set; }
        public string HousingID { get; set; }
        public string LensID { get; set; }
        public string SensorID { get; set; }
        public string PiVersion { get; set; }
        public string PiOSVersion { get; set; }
        public string OS_ID { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public string TimeStamp { get; set; }
    }

    public class HelperFunctions
    {
        public static string ReadFromFile(string filePath)
        {
            FileInfo theSourceFile = new FileInfo(filePath);
            StreamReader reader = theSourceFile.OpenText();

            string text;
            string readText = "";

            do
            {
                text = reader.ReadLine();
                readText += text + "\n";
            } while (text != null);

            reader.Close();

            return readText;

        }
        public static string RandomString()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < 5; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString().ToLower();
        }
    }
}