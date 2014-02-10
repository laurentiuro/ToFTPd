using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.FileSystem
{
  public class UnixFileSystem : IFileSystem
  {
    private string Root;
    private string WD = "/";

    public UnixFileSystem()
    {
      Logger.Write("Running on Unix FS");
    }

    public string GetFSType()
    {
      return "UNIX Type: L8";
    }

    public bool SetRoot(string Dir)
    {
      return Directory.Exists(Root = Dir);
    }

    public string GetWD()
    {
      return WD;
    }

    public bool SetWD(string Dir)
    {
      var tmpWD = Path.Combine(WD, Dir);
      return Directory.Exists(Path.Combine(Root,WD)) && ((WD = tmpWD) == WD);
    }

    public void MkDir(string Dir, UserPermissions perm)
    {
      throw new NotImplementedException();
    }

    public void RmDir(string Dir, UserPermissions perm)
    {
      throw new NotImplementedException();
    }

    public string GetFileList(UserPermissions perm)
    {
      return string.Format("-rwxr--r--  1   owner   group 123   1970 01 01  test1\n-rwxr--r--  1   owner   group 640   1970 01 01  test2\n");
    }

    public bool FileExists(string FileName, UserPermissions perm)
    {
      throw new NotImplementedException();
    }

    public Stream GetFileStream(string FileName)
    {
      throw new NotImplementedException();
    }
  }
}
