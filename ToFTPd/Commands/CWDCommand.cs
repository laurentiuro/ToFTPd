using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class CWDCommand : FtpCommand
  {
    public CWDCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      if (_connection.FileSystem.SetWD(_parameters))
        WriteToClient(250, "CWD command successful.");
      else
        WriteToClient(550, string.Format("Can't find directory {0}.", _parameters));
   }
  }
}
