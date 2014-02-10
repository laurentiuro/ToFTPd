using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class AuthFirstMessage : FtpCommand
  {
    public AuthFirstMessage(FTPConnection connection,string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      WriteToClient(530, "Authenticate first !");
    }
  }
}
