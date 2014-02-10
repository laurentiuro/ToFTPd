using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ToFTPd.Commands;

namespace ToFTPd
{
  public class FTPCommandHandler
  {
    private FTPConnection _connection;
    private Queue<string> cmdQueue = new Queue<string>();

    public FTPCommandHandler(FTPConnection connection)
    {
      _connection = connection;
    }

    public IFtpCommand Parse(string iMessage)
    {
      iMessage = Regex.Replace(iMessage, @"[^\u0000-\u007F]", string.Empty);

      Logger.Write("parse :" + iMessage);
      iMessage = iMessage.Trim();

      var commandArguments = "";

      int cmdEnd;

      if ((cmdEnd = iMessage.IndexOf(' ')) == -1)
        cmdEnd = iMessage.Length;
      else
        commandArguments = iMessage.Substring(cmdEnd).TrimStart(' ');

      string command = iMessage.Substring(0, cmdEnd).ToUpper();

      switch (command)
      {
        case "USER":
          return new USERCommand(_connection, commandArguments);
        case "PASS":
          return new PASSCommand(_connection, commandArguments);
        case "QUIT":
          return new QUITCommand(_connection, commandArguments);

        default:
          if (_connection.Authentication == null || !_connection.Authentication.IsAuthenticated())
            return new AuthFirstMessage(_connection, commandArguments);

          var cmdClass = Type.GetType("ToFTPd.Commands." + command + "Command");
          if (cmdClass == null)
            return new UnsupportedCommand(_connection, commandArguments);

          return (FtpCommand)Activator.CreateInstance(cmdClass, new object[] { _connection, commandArguments });
      }
    }

    public void Read()
    {
      if (!_connection.ClientConnection.isConnected())
        throw new SocketException();
      var ns = _connection.ClientConnection.GetStream();
      if (ns.DataAvailable)
      {
        var stream = new StreamReader(ns);
        cmdQueue.Enqueue(stream.ReadLine());
      }
    }

    public bool getNextCommand(out string line)
    {
      line = "";
      if (cmdQueue.Count == 0)
        return false;

      line = cmdQueue.Dequeue();
      return true;
    }
  }
}
