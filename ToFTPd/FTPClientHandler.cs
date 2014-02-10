using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ToFTPd.Authentication;
using ToFTPd.Commands;
using ToFTPd.FileSystem;

namespace ToFTPd
{
  public class FTPClientHandler
  {
    private readonly Queue<IFtpCommand> CommandQueue = new Queue<IFtpCommand>();
    private readonly FTPCommandHandler commandReader;
    private readonly FTPConnection connection;

    public FTPClientHandler(TcpClient ClientConnection)
    {
      connection = new FTPConnection(ClientConnection);
      commandReader = new FTPCommandHandler(connection);

      connection.Authentication = new DummyAuthentication();
      connection.FileSystem = Helpers.IsUnix ? (IFileSystem)new UnixFileSystem() : new WindowsFileSystem();

      ClientConnection.WriteToSocket(string.Format("220 ToFTP Server Ready\r\n"));
    }

    public void Process(Object O)
    {
      try
      {
        var SharedStateObj = (SharedState)O;
        try
        {
          if (!connection.ClientConnection.isConnected())
            throw new SocketException();

          commandReader.Read();

          string line;
          if (commandReader.getNextCommand(out line))
          {
            CommandQueue.Enqueue(commandReader.Parse(line));
          }
          
          if (CommandQueue.Count > 0)
          {
            var cmd = CommandQueue.Dequeue();
            ThreadPool.QueueUserWorkItem(cmd.Execute, null);
          }

        }
        catch (IOException e)
        {
          connection.Quit = true;
          Console.WriteLine("IOException!" + e.Message);
        } // Timeout
        catch (SocketException)
        {
          connection.Quit = true;
          Console.WriteLine("Conection is broken!");
        }


        // Schedule task again 
        if (SharedStateObj.ContinueProcess && !connection.Quit)
          ThreadPool.QueueUserWorkItem(this.Process, SharedStateObj);
        else
        {
          //if (networkStream != null)
          //  networkStream.Close();
          connection.ClientConnection.Close();

          Interlocked.Decrement(ref SharedStateObj.NumberOfClients);
          Console.WriteLine("A client left, number of connections is {0}", SharedStateObj.NumberOfClients);
        }

        // Signal main process if this is the last client connections main thread requested to stop.
        if (!SharedStateObj.ContinueProcess && SharedStateObj.NumberOfClients == 0) SharedStateObj.Ev.Set();
      }
      catch (Exception ex)
      {
        Console.WriteLine("IOException!" + ex.Message);
        var SharedStateObj = (SharedState)O;
        Interlocked.Decrement(ref SharedStateObj.NumberOfClients);
      }
    }

  }

}
