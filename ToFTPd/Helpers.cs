using System;
using System.Net.Sockets;
using System.Text;

namespace ToFTPd
{
  static class Helpers
  {
    public static bool isConnected(this TcpClient connection)
    {
      try
      {
        if (connection.Client.Poll(0, SelectMode.SelectRead))
        {
          var buff = new byte[1];
          if (connection.Client.Receive(buff, SocketFlags.Peek) == 0)
            return false;
        }
        return true;
      }
      catch
      {
        return false;
      }

    }

    public static void WriteToSocket(this TcpClient connection, string Message)
    {
      if (!connection.isConnected())
        throw new SocketException();

      var stream = connection.GetStream();
      stream.Write(Encoding.ASCII.GetBytes(Message), 0, Message.Length);
    }

    public static string EmptyIfNull(this string str)
    {
      return string.IsNullOrWhiteSpace(str) ? string.Empty : str;
    }

    public static bool IsUnix
    {
      get
      {
        var p = (int)Environment.OSVersion.Platform;
        return (p == 4) || (p == 6) || (p == 128);
      }
    }
  }
}
