using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class LISTCommand : FtpCommand
  {
    public LISTCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        WriteToClient(150, "Opening data connection for LIST.");

        var data = GetListing();
        _connection.DataConnection.Send(data);
        _connection.DataConnection.Close();
      }
      catch(Exception ex)
      {
        Logger.Write(ex);

        WriteToClient(425, "Can't open data connection.");
        WriteToClient(550, "LIST unable to establish data connection.");
      }

      WriteToClient(226, "Transfer Complete.");
    }


    public string GetListing()
    {
      return _connection.FileSystem.GetFileList(_connection.Authentication.getUserPermissions());
    }
  }
}
