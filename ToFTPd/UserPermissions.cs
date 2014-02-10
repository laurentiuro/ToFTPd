using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToFTPd
{
  public class UserPermissions
  {
    public bool CanRetrieveFiles { get; set; }
    public bool CanCreateFiles { get; set; }
    public bool CanRenameFiles { get; set; }
    public bool CanDeleteFiles { get; set; }
    public bool CanViewHiddenFiles { get; set; }

    public bool CanCreateFolders { get; set; }
    public bool CanRenameFolders { get; set; }
    public bool CanDeleteFolders { get; set; }
    public bool CanViewHiddenFolders { get; set; }
  }
}
