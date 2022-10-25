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
    // The security for this item
    protected FileSystemSecurity? _security;
    public FileSystemSecurity? Security
    {
      get
      {
        return _security;
      }
      set
      {
        if (value != null)
        {
          // Make the typeless collection of access rules type safe in AccessRules:
          AccessRules = new();
          var rules = value.GetAccessRules(true, true, typeof(SecurityIdentifier));
          foreach (FileSystemAccessRule rule in rules)
            AccessRules.Add(rule);
          HasExplicitAccessRules = AccessRules.Any(r => !r.IsInherited);
        }
        _security = value;
      }
    }
    // Type-safe list of access rules for this item
    // Mat be null when directory could not be read!
    public List<FileSystemAccessRule>? AccessRules { get; protected set; }
    // Are any of the access rules explicit, i.e. non-inherited?
    public bool HasExplicitAccessRules { get; protected set; }
    public Exception? Exception { get; protected set; }

    protected AclItem(string fullPath, string rootPath)
    {
      Name = Path.GetFileName(fullPath);
      FullName = fullPath;
      RelativePath = Path.GetRelativePath(rootPath, fullPath);
      HasExplicitAccessRules = false;
    }
  }

  /// <summary>
  /// Security info for a file
  /// </summary>
  internal class AclFile : AclItem
  {
    protected AclFile(string fullPath, string rootPath, FileSystemSecurity security) :
      base(fullPath, rootPath)
    {
      try
      {
        Security = new FileInfo(fullPath).GetAccessControl();
      }
      catch (Exception ex)
      {
        Exception = ex;
      }
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
      base(fullPath, rootPath)
    {
      try
      {
        Security = new DirectoryInfo(fullPath).GetAccessControl();

        if (maxDepth == 0 || currentDepth < maxDepth)
        {
          foreach (var name in Directory.GetDirectories(fullPath))
          {
            // Do the callback to see if we should cancel
            // (and notify the UI of this directory)
            if (callback(name!))
              break;
            // Not cancelled? Then recurse here:
            Directories.Add(new AclDirectory(name, rootPath, maxDepth, currentDepth + 1, callback));
          }
        }
      }
      catch (Exception ex)
      {
        // Store the exception to signal the UI that the directory could not be loaded
        Exception = ex;
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
