using System;
using System.Linq;
using Mono.Unix;

namespace ToFTPd.Authentication
{
  class PasswdAuthentication : IAuthentication
  {
    private bool authenticated;
    private string UserDir;

    public bool Authorize(string Username, string Password)
    {
      CheckSystemType();
      var users = UnixUserInfo.GetLocalUsers();
      var user = users.FirstOrDefault(a => a.UserName == Username && a.Password == Password);

      authenticated = (user == null);

      if (user == null)
        return false;

      UserDir = user.HomeDirectory;

      return authenticated;
    }

    public bool IsAuthenticated()
    {
      CheckSystemType();
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
      return UserDir;
    }

    public void CheckSystemType()
    {
      if (!Helpers.IsUnix)
        throw new ApplicationException("Passwd Authentication only works on Unix type systems");
    }
  }
}
