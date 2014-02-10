using System;


namespace ToFTPd.Commands
{
  public class PASSCommand : FtpCommand
  {
    public PASSCommand(FTPConnection connection, string commandArguments)
      : base(connection, commandArguments)
    {
    }

    public override void Execute(object O)
    {

      _connection.Password = _parameters;

      if (string.IsNullOrWhiteSpace(_connection.Username))
      {
        WriteToClient(503, "Username First!");
        return;
      }

      if (_connection.Authentication != null && _connection.Authentication.Authorize(_connection.Username, _connection.Password))
      {
        WriteToClient(230, "Authentication Successful!");
        _connection.FileSystem.SetRoot(_connection.Authentication.GetUserRootDir());
      }
      else
      {
        WriteToClient(530, "Authentication Failed!");
        _connection.Username = string.Empty;
      }
    }
  }
}
