using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Authentication
{
  public interface IAuthentication
  {
    bool Authorize(string Username, string Password);
    bool IsAuthenticated();
    UserPermissions getUserPermissions();
    string GetUserRootDir();
  }
}
