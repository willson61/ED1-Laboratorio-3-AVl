using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Laboratorio_3.Clases
{
    public class Log
    {
        static string filename;
        public static void beginLog()
        {
            Log.filename = "Log" + DateTime.Now.ToBinary() + ".txt";
        }

        public static void SendToLog(string logMessage, TimeSpan time)
        {
            StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath(path: @"~\Log\" + filename));
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("Event:{0}     Duration:{1}", logMessage, Convert.ToString(time));
            w.WriteLine("-------------------------------");
            w.Close();
        }

        public static void CleanLog()
        {
            System.IO.File.Delete(@"C:\Users\Williams Monterroso\source\repos\EstructuraDatosI\Log.txt");
        }
    }
}