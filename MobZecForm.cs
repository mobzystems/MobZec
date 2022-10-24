using MobZec.Properties;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace MobZec
{
  public partial class MobZecForm : Form
  {
    private ImageList _imageList = new();

    private string? _rootPath;

    public MobZecForm()
    {
      InitializeComponent();

      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add("folder", Resources.folder);
      _imageList.Images.Add("folder-open", Resources.folder_open);
      _imageList.Images.Add("exclamation", Resources.exclamation);

      _treeView.ImageList = _imageList;

      if (Environment.GetCommandLineArgs().Length > 1)
      {
        _rootPath = Environment.GetCommandLineArgs()[1];
      }
    }

    private async Task StartLoadingTask(string root)
    {
      var cancelled = false;

      var f = new LoadingForm(() => cancelled = true);
      f._rootLabel.Text = root;
      f.Show(this);

      var lastUpdateTime = DateTime.Now.AddHours(-1);
      var callback = (string name) =>
      {
        var time = DateTime.Now;
        if (time.Subtract(lastUpdateTime).TotalMilliseconds > 500)
        {
          Invoke(() => f._statusLabel.Text = name);
          lastUpdateTime = time;
        }
        return cancelled;
      };

      AclDirectory? a = null;

      try
      {
        a = await Task.Run(() =>
        {
          return AclDirectory.FromPath(root, true, callback);
        });
      }
      catch (Exception ex) {
        MessageBox.Show(this, ex.Message, $"Error loading '{root}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
        a = null;
      }
      f.Close();

      if (a != null)
      {
        _treeView.Nodes.Clear();
        var rootNode = _treeView.Nodes.Add(root);
        rootNode.ImageKey = "folder";
        rootNode.SelectedImageKey = "folder-open";

        rootNode.Tag = a;

        AddNodes(a, rootNode);

        rootNode.Expand();
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
          // node.Expand();
          child.EnsureVisible();
          child.ImageKey = "exclamation";
          child.SelectedImageKey = "exclamation";
          shouldExpand = true;
        }
        shouldExpand |= AddNodes(d, child);
        //if (shouldExpand)
        //  child.Expand();
      }

      return shouldExpand;
    }

    private void _treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
      _listView.Items.Clear();

      if (e.Node != null)
      {
        var dir = e.Node.Tag as AclDirectory;
        var rules = dir!.AccessRules;
        foreach (FileSystemAccessRule rule in rules)
        {
          var account = new SecurityIdentifier(rule.IdentityReference.Value).Translate(typeof(NTAccount)).Value;
          var item = _listView.Items.Add(account);
          item.SubItems.Add(rule.IsInherited ? "Nee" : "JA");
          item.SubItems.Add(rule.AccessControlType.ToString());
          item.SubItems.Add(rule.FileSystemRights.ToString());
        }
      }
    }

    private async void _openButton_Click(object sender, EventArgs e)
    {
      await StartLoadingTask(@"f:\data\homes\markus");
    }

    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      if (_rootPath != null)
        await StartLoadingTask(_rootPath);
    }
  }
}