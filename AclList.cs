using System.Security.AccessControl;
using System.Security.Principal;

namespace MobZec
{
  internal class AclItem
  {
    public string Name {  get; init; }
    public string FullName { get; init; }
    // The relative path of the item. "" for the root item
    public string RelativePath { get; init; }
    public FileSystemSecurity Security { get; init; }
    public List<FileSystemAccessRule> AccessRules { get; init; }

    public bool HasExplicitAccessRules { get; init; }

    public AclItem(string fullPath, string rootPath, FileSystemSecurity security)
    {
      Name = Path.GetFileName(fullPath);
      FullName = fullPath;
      RelativePath = Path.GetRelativePath(rootPath, fullPath);
      Security = security;
      AccessRules = new();
      var rules = Security.GetAccessRules(true, true, typeof(SecurityIdentifier));
      foreach (FileSystemAccessRule rule in rules)
        AccessRules.Add(rule);
      HasExplicitAccessRules = AccessRules.Any(r => !r.IsInherited);
    }
  }

  internal class AclFile : AclItem
  {
    public AclFile(string fullPath, string rootPath, FileSystemSecurity security) :
      base(fullPath, rootPath, new FileInfo(fullPath).GetAccessControl())
    {
    }
  }

  internal class AclDirectory : AclItem
  {
    public List<AclDirectory> Directories { get; init; } = new();
    public List<AclFile> Files { get; init; } = new();

    public AclDirectory(string fullPath, string rootPath, bool recursive, Func<string, bool> callback) :
      base(fullPath, rootPath, new DirectoryInfo(fullPath).GetAccessControl())
    {
      if (recursive)
      {
        foreach (var name in Directory.GetDirectories(fullPath))
        {
          if (callback(name!))
            break;
          try
          {
            Directories.Add(new AclDirectory(name, rootPath, recursive, callback));
          }
          catch { 
            // Ignore
          }
        }
      }
    }

    public static AclDirectory FromPath(string path, bool recursive, Func<string, bool> callback)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Directory '{path}' does not exist");

      var rootPath = Path.GetFullPath(path);
      var rootItem = new AclDirectory(rootPath, rootPath, recursive, callback);
      return rootItem;
    }
  }
}
