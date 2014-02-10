using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class PWDCommand : FtpCommand
  {
    public PWDCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      WriteToClient(257,string.Format(" \" {0} \" is the current directory",_connection.FileSystem.GetWD()));
    }
  }
}
