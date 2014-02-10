using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class PORTCommand : FtpCommand
  {
    public PORTCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {
      var IP_Parts = _parameters.Split(',');

      if (IP_Parts.Length != 6)
      {
        WriteToClient(550, "Invalid arguments.");
        return;
      }

      var ClientIP = IP_Parts[0] + "." + IP_Parts[1] + "." + IP_Parts[2] + "." + IP_Parts[3];
      var Port = (Convert.ToInt32(IP_Parts[4]) << 8) | Convert.ToInt32(IP_Parts[5]);

      _connection.DataConnection = new DataConnection();
      _connection.DataConnection.SetEndPoint(new IPEndPoint(IPAddress.Parse(ClientIP), Port));
      _connection.DataConnection.SetActive();

      WriteToClient(200, "Port command ready!");
    }
  }
}
