using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd.FileSystem
{
  public interface IFileSystem
  {
    string GetFSType();
    bool SetRoot(string Dir);
    string GetWD();
    bool SetWD(string Dir);
    void MkDir(string Dir, UserPermissions perm);
    void RmDir(string Dir, UserPermissions perm);
    string GetFileList(UserPermissions perm);
    bool FileExists(string FileName, UserPermissions perm);
    Stream GetFileStream(string FileName);
  }
}
