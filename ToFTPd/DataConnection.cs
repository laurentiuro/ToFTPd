using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ToFTPd
{
  public class DataConnection
  {
    private TcpListener listener;
    private Socket socket;
    private IPEndPoint endPoint;
    private Encoding encoding = new ASCIIEncoding();
    private bool pasv = false;
    private int Timeout = 30;
    private long startPosition = 0;
    private bool abort;

    public void SetEncoding(Encoding enc)
    {
      encoding = enc;
    }

    public void SetEndPoint(IPEndPoint ipEndPoint)
    {
      endPoint = ipEndPoint;
    }

    public int PrepareListener()
    {
      if (listener != null)
        listener.Stop();

      var random = new Random();
      int rPort = 1024;

      while (PortInUse(rPort))
        rPort = random.Next(1025, 65000);

      listener = new TcpListener(IPAddress.Any, rPort);
      listener.Start();

      SetPassive();

      return rPort;
    }

    public void SetPassive()
    {
      pasv = true;
    }

    public void SetActive()
    {
      pasv = false;
    }

    public void SetStartPosition(long position)
    {
      startPosition = position;
    }

    public EndPoint GetLocalEndpoint()
    {
      return listener.LocalEndpoint;
    }

    public void Send(string data)
    {
      Send(new MemoryStream(encoding.GetBytes(data ?? "")));
    }

    public void Send(Stream stream)
    {
      try
      {
        if (pasv)
        {
          int Count = 1;

          while (!listener.Pending())
          {
            Thread.Sleep(1000);

            // Time out after 30 seconds
            if (Count++ > Timeout)
              throw new ApplicationException("Data Connection Timed out");
          }

          socket = listener.AcceptSocket();
        }
        else
        {
          socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

          var result = socket.BeginConnect(endPoint, null, null);

          if (!result.AsyncWaitHandle.WaitOne(Timeout * 1000, true))
            throw new ApplicationException("Data Connection Timed out");

        }

        socket.SendTimeout = 5000;

        var state = new DataState
                      {
                        dataStream = new BinaryReader(stream),
                        dataSocket = socket,
                        Ev = new ManualResetEvent(false),
                        exception = null
                      };
        state.dataStream.BaseStream.Position = startPosition;

        ThreadPool.QueueUserWorkItem(Send, state);
        state.Ev.WaitOne();

        if (state.exception != null)
          throw state.exception;
      }
      catch (Exception ex)
      {
        Logger.Write(ex);
        throw new ApplicationException("Data Connection Failed");
      }
    }

    public void Send(object State)
    {
      var state = (DataState)State;
      try
      {
        var buffer = new byte[state.dataSocket.SendBufferSize];
        var dataStream = state.dataStream;

        if (dataStream.BaseStream.Position < dataStream.BaseStream.Length)
        {
          var b_pos = dataStream.BaseStream.Position;

          var BytesRead = state.dataStream.Read(buffer, 0, buffer.Length);
          var BytesSent = state.dataSocket.Send(buffer, 0, BytesRead, SocketFlags.None);

          dataStream.BaseStream.Position = b_pos + BytesSent;

          ThreadPool.QueueUserWorkItem(this.Send, state);
        }
        else
        {
          state.Ev.Set();
        }
      }
      catch (Exception ex)
      {
        Logger.Write(ex);

        if (state.dataSocket.Connected)
          state.dataSocket.Close();

        state.exception = ex;

        state.Ev.Set();
      }
    }

    public Stream Receive()
    {
      return null;
    }

    public void Close()
    {
      if (socket.Connected)
      {
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
      }
    }

    public void Abort()
    {
      abort = true;
      Close();
    }

    private bool PortInUse(int port)
    {
      var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
      var ipEndPoints = ipProperties.GetActiveTcpListeners();

      return ipEndPoints.Any(endPoint => endPoint.Port == port);
    }
  }

  public class DataState
  {
    public Socket dataSocket;
    public BinaryReader dataStream;
    public ManualResetEvent Ev;
    public Exception exception;
  }
}
