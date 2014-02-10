using System;
using System.Threading;

namespace ToFTPd
{
  public class SharedState
  {
    public bool ContinueProcess;
    public int NumberOfClients = 0;
    public AutoResetEvent Ev;
  }
}
