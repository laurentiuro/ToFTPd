using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class RESTCommand : FtpCommand
  {
    public RESTCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        var pos = long.Parse(_parameters);
        _connection.DataConnection.SetStartPosition(pos);
        WriteToClient(350, string.Format("Restarting at {0}. Send STOR or RETR to initiate transfer.", pos));
      }
      catch (Exception ex)
      {
        Logger.Write(ex);
        WriteToClient(550, "REST failed.");
      }
    }
  }
}
