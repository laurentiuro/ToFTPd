using System;
using System.IO;
using System.Text;

namespace ToFTPd.FileSystem
{
  public class WindowsFileSystem : IFileSystem
  {
    private string Root;
    private string WD = "\\";

    public WindowsFileSystem()
    {
      Logger.Write("Running on Windows FS");
    }

    public string GetFSType()
    {
      return "Windows_NT";
    }

    public bool SetRoot(string Dir)
    {
      Logger.Write(string.Format("SetRoot : {0}", Dir));
      if (Directory.Exists(Dir))
      {
        Root = Dir.EndsWith("\\") ? Dir : Dir + "\\";
        return true;
      }
      return false;
    }

    public string GetWD()
    {
      Logger.Write(string.Format("GetWD : {0}", WD));
      return WD.Replace('\\', '/');
    }

    public bool SetWD(string Dir)
    {
      try
      {
        var tmpWD = Dir.Replace('/', '\\');

        var pc = PathCombine(Root, PathCombine(WD.Replace('/', '\\'), tmpWD));

        var absolute = Path.GetFullPath(pc);

        Logger.Write(string.Format("SetWD : {0},Exists? :{1}", absolute, Directory.Exists(absolute)));

        if (!absolute.StartsWith(Root) || !Directory.Exists(absolute))
          return false;

        WD = absolute.Substring(Root.Length).Replace('\\', '/');

        WD = string.IsNullOrEmpty(WD) ? "\\" : WD;

        Logger.Write(string.Format("SetWD-: {0}", WD));

        return true;
      }
      catch (Exception ex)
      {
        Logger.Write(ex);
        return false;
      }
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
      string[] FilesList = Directory.GetFiles(PathCombine(Root, WD), "*.*", SearchOption.TopDirectoryOnly);
      string[] FoldersList = Directory.GetDirectories(PathCombine(Root, WD), "*.*", SearchOption.TopDirectoryOnly);

      var List = new StringBuilder();

      foreach (string Folder in FoldersList)
      {
        if (!perm.CanViewHiddenFolders && (new DirectoryInfo(Folder).Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

        string date = Directory.GetCreationTime(Folder).ToString("MM-dd-yy hh:mmtt");
        List.AppendLine(date + " <DIR> " + Folder.Substring(Folder.Replace('\\', '/').LastIndexOf('/') + 1));
      }

      foreach (string FileName in FilesList)
      {
        if (!perm.CanViewHiddenFiles && (File.GetAttributes(FileName) & FileAttributes.Hidden) == FileAttributes.Hidden) continue;

        string date = File.GetCreationTime(FileName).ToString("MM-dd-yy hh:mmtt");
        List.AppendLine(date + " " + new FileInfo(FileName).Length.ToString() + " " + FileName.Substring(FileName.Replace('\\', '/').LastIndexOf('/') + 1));
      }

      return List.ToString();
    }

    public bool FileExists(string FileName, UserPermissions perm)
    {
      var pc = PathCombine(Root, PathCombine(WD.Replace('/', '\\'), FileName));
      var absolute = Path.GetFullPath(pc);

      if (!absolute.StartsWith(Root) || !File.Exists(absolute))
        return false;

      return true;
    }

    public Stream GetFileStream(string FileName)
    {
      var pc = PathCombine(Root, PathCombine(WD.Replace('/', '\\'), FileName));
      var absolute = Path.GetFullPath(pc);

      if (!absolute.StartsWith(Root) || !File.Exists(absolute))
        return null;

      return new FileStream(absolute, FileMode.Open, FileAccess.Read, FileShare.Read);
    }

    public string PathCombine(string str1, string str2)
    {
      str1 = str1.EndsWith("\\") ? str1 : str1 + "\\";
      str2 = str2.EmptyIfNull().TrimStart('\\');
      return str1 + str2;
    }
  }
}
