using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class CDUPCommand : FtpCommand
  {
    public CDUPCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      if (_connection.FileSystem.SetWD("../"))
        WriteToClient(250, "CDUP command successful.");
      else
        WriteToClient(550, string.Format("Can't find directory {0}.", _parameters));
    }
  }
}
