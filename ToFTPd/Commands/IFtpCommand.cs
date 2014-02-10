using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Commands
{
  public interface IFtpCommand
  {
    void Execute(object O);
  }
}
