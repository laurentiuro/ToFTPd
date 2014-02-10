using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class RMDCommand : FtpCommand
  {
    public RMDCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        _connection.FileSystem.RmDir(_parameters, _connection.Authentication.getUserPermissions());

        WriteToClient(257, "\"" + _parameters + "\" removed.");
      }
      catch (Exception ex)
      {
        WriteToClient(550, ex.Message);
      }
    }
  }
}
