using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.Authentication
{
  public class DummyAuthentication : IAuthentication
  {
    private bool authenticated;

    public bool Authorize(string Username, string Password)
    {
      if (Username == Password)
      {
        authenticated = true;
        return true;
      }
      return false;
    }

    public bool IsAuthenticated()
    {
      return authenticated;
    }

    public UserPermissions getUserPermissions()
    {
      return new UserPermissions()
               {
                 CanCreateFiles = true,
                 CanCreateFolders = true,
                 CanDeleteFiles = true,
                 CanDeleteFolders = true,
                 CanRenameFiles = true,
                 CanRenameFolders = true,
                 CanRetrieveFiles = true,
                 CanViewHiddenFiles = true,
                 CanViewHiddenFolders = true
               };
    }

    public string GetUserRootDir()
    {
      return @"c:\temp";
    }
  }
}
