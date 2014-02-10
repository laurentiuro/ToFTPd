using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToFTPd
{
  class FTPServer
  {
    private TcpListener listener;

    internal ArrayList FTPClients = new ArrayList();
    internal bool isRunning = false;


    public void Start()
    {
      Logger.Write("FTPServer started.");
      try
      {
        listener = new TcpListener(IPAddress.Any, Settings.FTPPort);
        listener.Start();

        Logger.Write("FTPServer listening on {0}", Settings.FTPPort);

        //isRunning = true;

        // Start accepting the incoming clients.
        var state = new SharedState {ContinueProcess = true};
        listener.BeginAcceptSocket(FtpClient, state);


        Console.WriteLine("Press enter to stop the server ...");
        Console.ReadLine();


        listener.Stop();

        Logger.Write("FTPServer ended.");
        Console.ReadLine();
      }
      catch (Exception Ex)
      {
        Logger.Write(Ex);
      }
    }

    void FtpClient(IAsyncResult arg)
    {
      try
      {
        var state = (SharedState)arg.AsyncState;
        Interlocked.Increment(ref state.NumberOfClients);

        Logger.Write("Client {0} connected", state.NumberOfClients);
        var client = new FTPClientHandler(listener.EndAcceptTcpClient(arg));
        //FTPClients.Add(client);
        ThreadPool.QueueUserWorkItem(client.Process, state);
      }
      catch (Exception Ex)
      {
        Logger.Write(Ex);
      }

      try
      {
        var state = (SharedState)arg.AsyncState;
        // Start accepting the incoming clients.
        listener.BeginAcceptSocket(FtpClient, state);
      }
      catch (Exception Ex)
      {
        Logger.Write(Ex);
      }
    }
  }
}
