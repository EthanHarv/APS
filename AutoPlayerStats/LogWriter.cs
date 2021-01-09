using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPlayerStats
{
    public static class LogWriter
    {
        public static void Write(string logMessage)
        {
            try
            {


                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;

                string logFilePath = Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.ApplicationData), "AutoPlayerStats\\" + "Log-" + DateTime.Today.ToString("MM-dd-yyyy") + "." + "log");
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (!logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                log = new StreamWriter(fileStream);
                log.WriteLine(logMessage);
                log.Close();
            }
            catch
            {
                // File is unwritable - what do you even do at that point? Can't exactly log the error lmao. Ran into this so im just wrapping it in a try-catch.
                // Lazy solution, I know, but it'll work as a patch for now.
            }
        }
    }
}
