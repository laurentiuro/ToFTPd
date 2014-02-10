using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd
{
  static class Settings
  {
    public static int FTPPort = 21;

    public static string LogPath
    {
      get { return "c:\\temp"; }
      set { throw new NotImplementedException(); }
    }
  }
}
