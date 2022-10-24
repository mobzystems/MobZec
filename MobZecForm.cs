using MobZec.Properties;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MobZec
{
  public partial class MobZecForm : Form
  {
    private const int UpdateMs = 200;

    private ImageList _imageList = new();

    private string? _rootPath;
    private string? _lastOpenedPath;
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
        _lastOpenedPath = _rootPath;
      } else
      {
        _lastOpenedPath = null;// Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
      }

      _statusLabel.Text = "Open a directory to get started";
    }

    private async Task StartLoadingTask(string root)
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
            return AclDirectory.FromPath(root, true, callback);
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

          var rootNode = _treeView.Nodes.Add(root);
          rootNode.ImageKey = "folder";
          rootNode.SelectedImageKey = "folder-open";

          rootNode.Tag = a;
          AddNodes(a, rootNode);
          _treeView.EndUpdate();

          // await Task.Delay(5000);

          rootNode.Expand();
          _treeView.SelectedNode = rootNode;
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
      using(var dlg = new FolderBrowserDialog())
      {
        dlg.SelectedPath = _lastOpenedPath;

        // dlg.Description = "Choose a folder to open";
        if (dlg.ShowDialog(this) == DialogResult.OK && dlg.SelectedPath != null)
        {
          _lastOpenedPath = dlg.SelectedPath;
          await StartLoadingTask(dlg.SelectedPath);
        }
      }
    }

    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      if (_rootPath != null)
        await StartLoadingTask(_rootPath);
    }

    private void _cancelButton_Click(object sender, EventArgs e)
    {
      _cancelled = true;
    }
  }
}