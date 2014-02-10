using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class UnsupportedCommand : FtpCommand
  {
    public UnsupportedCommand(FTPConnection connection,string commandArguments)
      : base(connection, commandArguments)
    {
      _connection = connection;
    }

    public override void Execute(object O)
    {
      WriteToClient(502, "Command not implemented!");
    }
  }
}
