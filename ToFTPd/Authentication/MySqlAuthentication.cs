using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Authentication
{
  class MySqlAuthentication : IAuthentication
  {
    public bool Authorize(string Username, string Password)
    {
      throw new NotImplementedException();
    }

    public bool IsAuthenticated()
    {
      throw new NotImplementedException();
    }

    public UserPermissions getUserPermissions()
    {
      throw new NotImplementedException();
    }

    public string GetUserRootDir()
    {
      throw new NotImplementedException();
    }
  }
}
