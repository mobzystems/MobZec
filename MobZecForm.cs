using MobZec.Properties;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MobZec
{
  public partial class MobZecForm : Form
  {
    private const int UpdateMs = 200;

    private ImageList _imageList = new();

    private string? _rootPath;
    private int _depthFromCommandLine = 0;
    private string _lastOpenedPath;
    private bool _cancelled = false;

    private const string ICON_FOLDER = nameof(Resources.folder_open);
    private const string ICON_FOLDER_OPEN = nameof(Resources.folder_open_o);

    private const string ICON_FILE = nameof(Resources.file_o);
    private const string ICON_WARNING = nameof(Resources.flag_o_red);

    public MobZecForm()
    {
      InitializeComponent();

      Icon = Resources.MobZec;

      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add(ICON_FILE, Resources.file_o);
      _imageList.Images.Add(ICON_FOLDER, Resources.folder_o);
      _imageList.Images.Add(ICON_FOLDER_OPEN, Resources.folder_open_o);
      _imageList.Images.Add(ICON_WARNING, Resources.flag_o_red);

      _treeView.ImageList = _imageList;
      _listView.SmallImageList = _imageList;

      if (Environment.GetCommandLineArgs().Length > 1)
      {
        _rootPath = Environment.GetCommandLineArgs()[1];
        _lastOpenedPath = Path.GetFullPath(_rootPath);
        if (Environment.GetCommandLineArgs().Length > 2)
        {
          try
          {
            _depthFromCommandLine = int.Parse(Environment.GetCommandLineArgs()[2]);
          }
          catch { }
        }

      }
      else
      {
        _lastOpenedPath = Environment.CurrentDirectory;
      }

      _statusLabel.Text = "Open a directory to get started. Shift-click to show direct children only.";
    }

    /// <summary>
    /// Load the folder specified on the command line if specified
    /// </summary>
    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      if (_rootPath != null)
      {
        await LoadSecurityAsync(_rootPath, _depthFromCommandLine);
        _depthFromCommandLine = 0;
      }
    }

    /// <summary>
    /// Load the security of a folder and (optionally) subfolders to a certain depth
    /// </summary>
    /// <param name="path">The path to load. May be relative</param>
    /// <param name="depth">The depth to load. 0 = all, 1 is path only, 2+ = additional levels</param>
    private async Task LoadSecurityAsync(string path, int depth)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true;

      try
      {
        // Hide the Open button, show the Cancel button
        _cancelled = false;
        _openButton.Visible = false;
        _statusLabel.Text = $"Loading '{path}'...";
        _cancelButton.Visible = true;

        // We only update the status label every so many ms, to prevent it eating CPU
        var lastUpdateTime = DateTime.Now.AddHours(-1);
        // This method is called on every folder. Return true to cancel
        var callback = (string name) =>
        {
          var time = DateTime.Now;
          if (time.Subtract(lastUpdateTime).TotalMilliseconds > UpdateMs)
          {
            // Update the status label USING INVOKE()
            Invoke(() => _statusLabel.Text = name);
            lastUpdateTime = time;
          }
          return _cancelled;
        };

        // This is the result:
        AclDirectory? a = null;

        try
        {
          a = await Task.Run(() => AclDirectory.FromPath(path, depth, callback));
        }
        catch (Exception ex)
        {
          // We failed somehow - no result
          MessageBox.Show(this, ex.Message, $"Error loading '{path}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
          a = null;
        }

        // Swap Open and Cancel again, disable Open while we're displaying
        _openButton.Enabled = false;
        _openButton.Visible = true;
        _cancelButton.Visible = false;

        if (a != null)
        {
          _statusLabel.Text = "Displaying results...";
          _topPanel.Update();

          _treeView.BeginUpdate();

          _treeView.Nodes.Clear();

          // Add the root node with its full path
          var rootNode = _treeView.Nodes.Add(a.FullName);
          rootNode.ImageKey = ICON_FOLDER;
          rootNode.SelectedImageKey = ICON_FOLDER_OPEN;
          if (a.HasExplicitAccessRules)
          {
            rootNode.ImageKey = ICON_WARNING;
            rootNode.SelectedImageKey = ICON_WARNING;
          }

          rootNode.Tag = a;
          AddNodes(a, rootNode);
          rootNode.Expand();
          _treeView.SelectedNode = rootNode;

          _treeView.EndUpdate();

          // Update the window title
          Text = $"{a.FullName} - MobZec";
        }
        else
        {
          _statusLabel.Text = $"Failed to load '{path}";
        }

        // Re-enable Open button
        _openButton.Enabled = true;
      }
      finally
      {
        _splitContainer.Enabled = true;
        _splitContainer.UseWaitCursor = false;
      }
    }

    /// <summary>
    /// Add a node and subnodes to the tree
    /// </summary>
    /// <returns>Whether the tree contains any items that shoud be visible (unused)</returns>
    private bool AddNodes(AclDirectory dir, TreeNode node)
    {
      bool shouldExpand = false;

      foreach (var d in dir.Directories)
      {
        var child = node.Nodes.Add(d.Name);
        child.Tag = d;
        child.ImageKey = ICON_FOLDER;
        child.SelectedImageKey = ICON_FOLDER_OPEN;

        if (d.HasExplicitAccessRules)
        {
          child.EnsureVisible();
          child.ImageKey = ICON_WARNING;
          child.SelectedImageKey = ICON_WARNING;
          shouldExpand = true;
        }
        shouldExpand |= AddNodes(d, child);
      }

      return shouldExpand;
    }

    /// <summary>
    /// Update the list view when a tree node is clicked
    /// </summary>
    private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      _listView.Items.Clear();

      if (e.Node != null && e.Node.Tag != null)
      {
        var dir = (AclDirectory)e.Node.Tag;
        _statusLabel.Text = $"{dir.FullName}";

        var rules = dir.AccessRules;
        foreach (FileSystemAccessRule rule in rules)
        {
          string account;
          try
          {
            // This may fail if the account cannot be found (anymore)
            account = new SecurityIdentifier(rule.IdentityReference.Value).Translate(typeof(NTAccount)).Value;
          }
          catch
          {
            account = rule.IdentityReference.Value;
          }

          var item = _listView.Items.Add(rule.IsInherited ? "No" : "Yes");
          item.SubItems.Add(account);
          item.SubItems.Add(rule.AccessControlType.ToString());
          item.SubItems.Add(rule.FileSystemRights.ToString());
          if (rule.IsInherited)
            item.ImageKey = ICON_FILE;
          else
            item.ImageKey = ICON_WARNING;
        }
      }
    }

    /// <summary>
    /// Allow the user to choose a folder to open and parse
    /// </summary>
    private async void _openButton_Click(object sender, EventArgs e)
    {
      // Press Shift to load a directory and its immediate children
      int maxDepth = (Control.ModifierKeys & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.Shift ? 2 : 0;

      using (var dlg = new FolderBrowserDialog())
      {
        dlg.UseDescriptionForTitle = true;

        if (maxDepth == 0)
          dlg.Description = "Choose a folder to open";
        else
          dlg.Description = "Choose a folder to open NON-RECURSIVELY";

        dlg.SelectedPath = _lastOpenedPath;

        if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedPath != null)
        {
          _lastOpenedPath = dlg.SelectedPath;
          await LoadSecurityAsync(dlg.SelectedPath, maxDepth);
        }
      }
    }

    /// <summary>
    /// Signal the current loading task that it should terminate
    /// </summary>
    private void _cancelButton_Click(object sender, EventArgs e)
    {
      _cancelled = true;
    }
  }
}