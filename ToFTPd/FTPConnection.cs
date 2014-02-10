using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ToFTPd.Authentication;
using ToFTPd.FileSystem;

namespace ToFTPd
{
  public class FTPConnection
  {
    public FTPConnection(TcpClient connection)
    {
      ClientConnection = connection;
    }

    public string Username;
    public string Password;
    public string RootDir;
    public string CurrentDir;
    public TcpClient ClientConnection;
    public IAuthentication Authentication;
    public IFileSystem FileSystem;
    public bool BinaryMode;

    public DateTime LastActivity;
    public bool Quit = false;

    public DataConnection DataConnection;
    public Encoding encoding = new ASCIIEncoding();
  }
}
