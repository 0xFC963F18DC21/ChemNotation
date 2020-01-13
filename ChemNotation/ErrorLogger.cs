using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace ChemNotation
{
    public class ErrorLogger
    {
        // !! Set this to false when releasing. !!
        public static bool AllowLogging { get; } = true;

        private Type Caller { get; set; }

        public ErrorLogger(Type caller)
        {
            LogMessage(caller.ToString() + " has created an ErrorLogger instance.", GetType(), null, LogType.Debug);
            Caller = caller;
        }

        public static void ShowErrorMessageBox(Exception e)
        {
            try
            {
                LogMessage("ERROR BOX SHOWN. ERROR DETAILS:", typeof(ErrorLogger), e, LogType.Error);
                MessageBox.Show("Uncaught Runtime Exception:\n\n" + e.ToString(), e.Source + " Error");
            } catch
            {
                // Do nothing.
            }
        }

        private static readonly string LogFileName = Path.GetFullPath("Logs/ChemNotation_LOGFILE_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff") + ".txt");

        public static void LogMessage(string text, Type caller = null, Exception e = null, LogType type = LogType.Log)
        {
            if (!AllowLogging) return;

            string logPreface = "";
            switch (type)
            {
                case LogType.Log:
                    logPreface = "LOG  ";
                    break;
                case LogType.Debug:
                    logPreface = "DEBUG";
                    break;
                case LogType.Error:
                    logPreface = "ERROR";
                    break;
                case LogType.Info:
                    logPreface = "INFO ";
                    break;
                default:
                    break;
            }

            try
            {
                // Print error log to file.
                DateTime nowTime = DateTime.Now;

                string pref = nowTime.ToString("[yyyy/MM/dd HH:mm:ss.fff] ") + logPreface + ((caller != null) ? " " + caller.ToString() + ": " : ": ");
                text = pref + text;

                if (e != null)
                {
                    text += "\r\n";
                    text += e.ToString();
                }

                if (!Directory.Exists("Logs")) Directory.CreateDirectory("Logs");
                using (StreamWriter sw = (File.Exists(LogFileName)) ? File.AppendText(LogFileName) : File.CreateText(LogFileName))
                {
                    sw.WriteLine(text);
                }
            } catch (Exception e2)
            {
                Debug.WriteLine(e2.ToString());
            }
        }

        public void LogMessage(string text, Exception e = null, LogType type = LogType.Log)
        {
            LogMessage(text, Caller, e, type);
        }

        public void LogMessageGeneral(string text, Exception e = null)
        {
            LogMessage(text, Caller, e, LogType.Log);
        }

        public void LogMessageDebug(string text, Exception e = null)
        {
            LogMessage(text, Caller, e, LogType.Debug);
        }

        public void LogMessageInfo(string text, Exception e = null)
        {
            LogMessage(text, Caller, e, LogType.Info);
        }

        public void LogMessageError(string text, Exception e = null)
        {
            LogMessage(text, Caller, e, LogType.Error);
        }

        /// <summary>
        /// Logging type prefix.
        /// </summary>
        public enum LogType
        {
            Log, Debug, Error, Info
        }
    }
}
