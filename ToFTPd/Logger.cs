using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd
{
  internal class Logger
  {
    internal static void Write(Exception ex)
    {
      Console.WriteLine(ex.Message + Environment.NewLine);
    }

    internal static void Write(string Message)
    {
      Write(Message, new object[] { });
    }

    internal static void Write(string Message, params object[] args)
    {
      Console.WriteLine(Message, args);
    }


    //private static FileStream LogStream;
    //private static StreamWriter Log;
    //private static string LogFilePath;
    //internal static bool EnableLogging = true;

    //[MethodImpl(MethodImplOptions.Synchronized)]
    //internal static void Write(Exception Ex)
    //{
    //  if (Ex == null || !EnableLogging) return;
    //  if (Ex.InnerException != null) Ex = Ex.InnerException;

    //  try
    //  {
    //    if (!Directory.Exists(Settings.LogPath + "\\"))
    //      Directory.CreateDirectory(Settings.LogPath + "\\");

    //    LogFilePath = Settings.LogPath + "\\LOG." + DateTime.Today.ToString("yyyy-MM-dd") + ".EXCEPTION";

    //    LogStream = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
    //    Log = new StreamWriter(LogStream, Encoding.UTF8);

    //    var SB = new StringBuilder();

    //    //if (LogStream.Length < 21)
    //    //  SB.AppendLine("<ERRORLOG>");
    //    //else
    //    //  LogStream.Position = LogStream.Length - 13;

    //    //SB.AppendLine("  <LOG TIME=\"{0}\" SOURCE=\"{1}\">");
    //    //SB.AppendLine("    <EXCEPTION>{2}</EXCEPTION>");
    //    //SB.AppendLine("    <MESSAGE>{3}</MESSAGE>");
    //    //SB.AppendLine("    <SOURCE>{4}</SOURCE>");
    //    //SB.AppendLine("    <STACK>{5}</STACK>");
    //    //SB.AppendLine("    <TARGETSITE>{6}</TARGETSITE>");
    //    //SB.AppendLine("  </LOG>");

    //    //SB.AppendLine("</ERRORLOG>"); // End the xml file

    //    //Log.Write(
    //    //  string.Format(SB.ToString(),
    //    //                DateTime.Now.ToString("HH:mm:ss"),
    //    //                Ex.ToString(),
    //    //                Ex.Message,
    //    //                Ex.Source,
    //    //                Ex.StackTrace,
    //    //                Ex.TargetSite.Name));

    //    SB = null;
    //  }
    //  catch (Exception Exn)
    //  {
    //  }
    //  finally
    //  {
    //    if (Log != null) Log.Close();
    //    Log = null;
    //    if (LogStream != null) LogStream.Close();
    //    LogStream = null;
    //  }
    //}

    //[MethodImpl(MethodImplOptions.Synchronized)]
    //internal static void Write(string Message)
    //{
    //  if (String.IsNullOrEmpty(Message) || !EnableLogging) return;

    //  try
    //  {
    //    if (!Directory.Exists(Settings.LogPath + "\\"))
    //      Directory.CreateDirectory(Settings.LogPath + "\\");

    //    LogFilePath = Settings.LogPath + "\\LOG." + DateTime.Today.ToString("yyyy-MM-dd") + ".msg";

    //    LogStream = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
    //    Log = new StreamWriter(LogStream, Encoding.UTF8);

    //    Log.Write(Message + Environment.NewLine);
    //  }
    //  catch (Exception Exn)
    //  {
    //  }
    //  finally
    //  {
    //    if (Log != null) Log.Close();
    //    Log = null;
    //    if (LogStream != null) LogStream.Close();
    //    LogStream = null;
    //  }
    //}

    //[MethodImpl(MethodImplOptions.Synchronized)]
    //internal static void Write(string Message,params object[] p)
    //{
    //  if (String.IsNullOrEmpty(Message) || !EnableLogging) return;

    //  try
    //  {
    //    if (!Directory.Exists(Settings.LogPath + "\\"))
    //      Directory.CreateDirectory(Settings.LogPath + "\\");

    //    LogFilePath = Settings.LogPath + "\\LOG." + DateTime.Today.ToString("yyyy-MM-dd") + ".msg";

    //    LogStream = new FileStream(LogFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
    //    Log = new StreamWriter(LogStream, Encoding.UTF8);

    //    Log.Write(string.Format(Message,p));
    //  }
    //  catch (Exception Exn)
    //  {
    //  }
    //  finally
    //  {
    //    if (Log != null) Log.Close();
    //    Log = null;
    //    if (LogStream != null) LogStream.Close();
    //    LogStream = null;
    //  }
    //}
  }
}
