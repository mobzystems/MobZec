using System.Security.AccessControl;
using System.Security.Principal;

namespace MobZec
{
  /// <summary>
  /// Base class for security information for a file or directory
  /// </summary>
  internal class AclItem
  {
    // Base name of this item (file.ext)
    public string Name { get; init; }
    // Full name of this item (drive:\path\file.ext)
    public string FullName { get; init; }
    // The relative path of the item. "." for the root item
    public string RelativePath { get; init; }
    // The secutiry for this item
    public FileSystemSecurity Security { get; init; }
    // Type-safe list of access rules for this item
    public List<FileSystemAccessRule> AccessRules { get; init; }
    // Are any of the access rules explicit, i.e. non-inherited?
    public bool HasExplicitAccessRules { get; init; }

    protected AclItem(string fullPath, string rootPath, FileSystemSecurity security)
    {
      Name = Path.GetFileName(fullPath);
      FullName = fullPath;
      RelativePath = Path.GetRelativePath(rootPath, fullPath);
      Security = security;

      // Make the typeless collection of access rules type safe in AccessRules:
      AccessRules = new();
      var rules = Security.GetAccessRules(true, true, typeof(SecurityIdentifier));
      foreach (FileSystemAccessRule rule in rules)
        AccessRules.Add(rule);
      HasExplicitAccessRules = AccessRules.Any(r => !r.IsInherited);
    }
  }

  /// <summary>
  /// Security info for a file
  /// </summary>
  internal class AclFile : AclItem
  {
    protected AclFile(string fullPath, string rootPath, FileSystemSecurity security) :
      base(fullPath, rootPath, new FileInfo(fullPath).GetAccessControl())
    {
    }
  }

  /// <summary>
  /// Security info for a directory and subdirectories
  /// </summary>
  internal class AclDirectory : AclItem
  {
    public List<AclDirectory> Directories { get; init; } = new();
    public List<AclFile> Files { get; init; } = new();

    protected AclDirectory(string fullPath, string rootPath, int maxDepth, int currentDepth, Func<string, bool> callback) :
      base(fullPath, rootPath, new DirectoryInfo(fullPath).GetAccessControl())
    {
      if (maxDepth == 0 || currentDepth < maxDepth)
      {
        foreach (var name in Directory.GetDirectories(fullPath))
        {
          if (callback(name!))
            break;
          try
          {
            Directories.Add(new AclDirectory(name, rootPath, maxDepth, currentDepth + 1, callback));
          }
          catch
          {
            // Ignore
          }
        }
      }
    }

    /// <summary>
    /// Load security information from a path
    /// </summary>
    /// <param name="path">The (full or relative) 'root' path</param>
    /// <param name="depth">0 = recursive, 1 = path only, 2+ = more levels of children</param>
    /// <param name="callback">A function to call upon entering each directory. Can return true to cancel the operation</param>
    /// <returns>The AclDirectory of the root path. Contains all other (files and) directories</returns>
    /// <exception cref="DirectoryNotFoundException"></exception>
    public static AclDirectory FromPath(string path, int depth, Func<string, bool> callback)
    {
      if (!Directory.Exists(path))
        throw new DirectoryNotFoundException($"Directory '{path}' does not exist");

      var rootPath = Path.GetFullPath(path);
      var rootItem = new AclDirectory(rootPath, rootPath, depth, 1, callback);
      return rootItem;
    }
  }
}
