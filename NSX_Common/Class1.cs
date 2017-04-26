using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSX_Common
{
    public class LoggingManager
    {
        public class Config
        {
            // GAME SO HAN VUONG

            public static string LOG_FILE
            {
                get { return ConfigurationManager.AppSettings["LOG_FILE"]; }
            }

        }
        public static void WriteLogEx(string msg)
        {
            try
            {
                string _path = Config.LOG_FILE;
                StreamWriter f = new StreamWriter(_path + @"\\" + DateTime.Today.ToString("dd-MM-yyyy").Replace("-", "") + "TextLogEx.txt", true);
                f.WriteLine(DateTime.Today.ToString() + "\\" + DateTime.Now.ToLongTimeString() + ": " + msg);
                f.Close();
            }
            catch
            {
            }
        }
    }
}
