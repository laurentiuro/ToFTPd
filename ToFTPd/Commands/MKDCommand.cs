using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class MKDCommand : FtpCommand
  {
    public MKDCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      try
      {
        _connection.FileSystem.MkDir(_parameters, _connection.Authentication.getUserPermissions());

        WriteToClient(257, "\"" + _parameters + "\" directory created.");
      }
      catch (Exception ex)
      {
        WriteToClient(550, ex.Message);
      }
    }
  }
}
