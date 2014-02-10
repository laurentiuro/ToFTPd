using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class RETRCommand : FtpCommand
  {
    public RETRCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        var perm = _connection.Authentication.getUserPermissions();
        if (!perm.CanRetrieveFiles)
        {
          WriteToClient(426, "Access Denied.");
          return;
        }

        if (_connection.FileSystem.FileExists(_parameters, perm))
        {
          var stream = _connection.FileSystem.GetFileStream(_parameters);
          if (stream == null)
          {
            WriteToClient(550, "Access denied.");
            return;
          }
          WriteToClient(150, "Starting data transfer...");

          _connection.DataConnection.Send(stream);
          _connection.DataConnection.Close();
        }
        else
        {
          WriteToClient(550, "Access denied.");
          return;
        }

        WriteToClient(226, "Transfer Complete.");
      }
      catch (Exception ex)
      {
        Logger.Write(ex);
        WriteToClient(426, "Connection closed; transfer aborted.");
      }
    }
  }
}
