using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class PASVCommand : FtpCommand
  {
    public PASVCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        var rPort = _connection.DataConnection.PrepareListener();

        string SocketEndPoint = _connection.DataConnection.GetLocalEndpoint().ToString();
        SocketEndPoint = SocketEndPoint.Substring(0, SocketEndPoint.IndexOf(":"))
                           .Replace(".", ",") + "," + (rPort >> 8) + "," + (rPort & 255);

        WriteToClient(227, "Entering Passive Mode (" + SocketEndPoint + ").");
      }
      catch (Exception ex)
      {
        Logger.Write(ex);
        WriteToClient(500, "PASV Command failed!");
      }

    }
  }
}
