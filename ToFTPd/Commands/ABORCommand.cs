using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class ABORCommand : FtpCommand
  {
    public ABORCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      _connection.DataConnection.Abort();
      Thread.Sleep(1000); //should be enough //todo: find a way to signal it so we don't rely on faith :)
      WriteToClient(226, "ABOR processed succesfully");
    }
  }
}
