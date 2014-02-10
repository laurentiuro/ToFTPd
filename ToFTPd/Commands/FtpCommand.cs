using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public class FtpCommand : IFtpCommand
  {
    protected FTPConnection _connection;
    protected string _parameters;

    public FtpCommand(FTPConnection connection, string commandArguments)
    {
      _connection = connection;
      _parameters = commandArguments;
    }


    protected string ReturnMessage(int ReturnCode, string Message)
    {
      return string.Format("{0} {1}\r\n", ReturnCode, Message);
    }

    protected void WriteToClient(int ReturnCode, string Message)
    {
      if (!_connection.ClientConnection.isConnected())
        throw new SocketException();

      var stream = _connection.ClientConnection.GetStream();
      var message = ReturnMessage(ReturnCode, Message);

      Logger.Write(message);

      stream.Write(Encoding.ASCII.GetBytes(message),0,message.Length);
    }

    protected void WriteToClient(string Message)
    {
      if (!_connection.ClientConnection.isConnected())
        throw new SocketException();

      var stream = _connection.ClientConnection.GetStream();

      Message += "\r\n";

      Logger.Write(Message);

      stream.Write(Encoding.ASCII.GetBytes(string.Format(Message)), 0, Message.Length);
    }

    public virtual void Execute(object O)
    {
      Logger.Write("Exec");
    }
  }
}
