using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class APPECommand : FtpCommand
  {
    public APPECommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      WriteToClient(500, "Unavailable");
    }
  }
}
