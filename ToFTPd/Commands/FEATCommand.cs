using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class FEATCommand : FtpCommand
  {
    public FEATCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      WriteToClient("211-Features:");
      //todo:
      WriteToClient(211, "End");
    }
  }
}
