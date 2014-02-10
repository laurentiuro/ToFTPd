using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class TYPECommand : FtpCommand
  {
    public TYPECommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      switch (_parameters.ToUpper())
      {
        case "A":
          _connection.BinaryMode = false;
          WriteToClient(200, "Type set to A.");
          break;

        case "I":
          _connection.BinaryMode = true;
          WriteToClient(200, "Type set to I");
          break;

        default:
          WriteToClient(550, string.Format("Unknown data mode \"{0}\"", _parameters));
          break;
      }
    }
  }
}
