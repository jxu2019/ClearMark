using System;
using System.IO;
using System.Text;

namespace AutoInspexServerWindowsService
{
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