using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class USERCommand : FtpCommand
  {
    public USERCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      _connection.Username = _parameters;
      WriteToClient(331, "User Ok, need password");
    }
  }
}
