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

    public MobZecForm()
    {
      InitializeComponent();

      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add("file", Resources.file);
      _imageList.Images.Add("folder", Resources.folder);
      _imageList.Images.Add("folder-open", Resources.folder_open);
      _imageList.Images.Add("exclamation", Resources.exclamation);

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

    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      if (_rootPath != null)
      {
        await LoadSecurityAsync(_rootPath, _depthFromCommandLine);
        _depthFromCommandLine = 0;
      }
    }

    private async Task LoadSecurityAsync(string root, int depth)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true;

      try
      {
        _cancelled = false;
        _openButton.Visible = false;
        _statusLabel.Text = $"Loading '{root}'...";
        // _statusLabel.Visible = true;
        _cancelButton.Visible = true;

        var lastUpdateTime = DateTime.Now.AddHours(-1);
        var callback = (string name) =>
        {
          var time = DateTime.Now;
          if (time.Subtract(lastUpdateTime).TotalMilliseconds > UpdateMs)
          {
            Invoke(() => _statusLabel.Text = name);
            lastUpdateTime = time;
          }
          return _cancelled;
        };

        AclDirectory? a = null;

        try
        {
          a = await Task.Run(() =>
          {
            return AclDirectory.FromPath(root, depth, callback);
          });
        }
        catch (Exception ex)
        {
          MessageBox.Show(this, ex.Message, $"Error loading '{root}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
          a = null;
        }

        _openButton.Enabled = false;
        _openButton.Visible = true;
        _cancelButton.Visible = false;

        if (a != null)
        {
          _statusLabel.Text = "Displaying results...";

          _topPanel.Update(); ;

          _treeView.BeginUpdate();

          _treeView.Nodes.Clear();

          var rootNode = _treeView.Nodes.Add(a.FullName);
          rootNode.ImageKey = "folder";
          rootNode.SelectedImageKey = "folder-open";
          if (a.HasExplicitAccessRules)
          {
            rootNode.ImageKey = "exclamation";
            rootNode.SelectedImageKey = "exclamation";
          }

          rootNode.Tag = a;
          AddNodes(a, rootNode);
          _treeView.EndUpdate();

          // await Task.Delay(5000);

          rootNode.Expand();
          _treeView.SelectedNode = rootNode;

          Text = $"{a.FullName} - MobZec";
        }
        else
        {
          _statusLabel.Text = $"Failed to load '{root}";
        }

        _openButton.Enabled = true;
      }
      finally
      {
        _splitContainer.Enabled = true;
        _splitContainer.UseWaitCursor = false;
      }
    }

    private bool AddNodes(AclDirectory dir, TreeNode node)
    {
      bool shouldExpand = false;

      foreach (var d in dir.Directories)
      {
        var child = node.Nodes.Add(d.Name);
        child.Tag = d;
        child.ImageKey = "folder";
        child.SelectedImageKey = "folder-open";

        if (d.HasExplicitAccessRules)
        {
          child.EnsureVisible();
          child.ImageKey = "exclamation";
          child.SelectedImageKey = "exclamation";
          shouldExpand = true;
        }
        shouldExpand |= AddNodes(d, child);
      }

      return shouldExpand;
    }

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
            item.ImageKey = "file";
          else
            item.ImageKey = "exclamation";
        }
      }
    }

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

        // dlg.Description = "Choose a folder to open";
        if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedPath != null)
        {
          _lastOpenedPath = dlg.SelectedPath;
          await LoadSecurityAsync(dlg.SelectedPath, maxDepth);
        }
      }
    }

    private void _cancelButton_Click(object sender, EventArgs e)
    {
      _cancelled = true;
    }
  }
}