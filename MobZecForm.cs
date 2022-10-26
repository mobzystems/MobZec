using MobZec.Properties;
using System.Configuration;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Security.Principal;

namespace MobZec
{
  public partial class MobZecForm : Form
  {
    private const int UpdateMs = 200;

    private ImageList _imageList = new();

    private string? _rootPath;
    private string _lastOpenedPath;
    private bool _cancelled = false;

    private const string ICON_FOLDER = nameof(Resources.folder_open);
    private const string ICON_FOLDER_OPEN = nameof(Resources.folder_open_o);

    private const string ICON_FILE = nameof(Resources.file_o);
    private const string ICON_WARNING = nameof(Resources.flag_o_red);
    private const string ICON_ERROR = nameof(Resources.exclamation);

    private string _titleBase = $"MOBZec Security Explorer v{Application.ProductVersion}";

    /// <summary>
    /// A (PowerShell) command to run on a rule using the context menu
    /// </summary>
    private class DynamicCommand
    {
      public string Menu { get; init; }
      public string Command { get; init; }

      public DynamicCommand(string menu, string command)
      {
        Menu = menu;
        Command = command;
      }
    }

    public MobZecForm()
    {
      InitializeComponent();

      Icon = Resources.MobZec;
      Text = _titleBase;

      _imageList.ColorDepth = ColorDepth.Depth32Bit;
      _imageList.ImageSize = new Size(24, 24);

      _imageList.Images.Add(ICON_FILE, Resources.file_o);
      _imageList.Images.Add(ICON_FOLDER, Resources.folder_o);
      _imageList.Images.Add(ICON_FOLDER_OPEN, Resources.folder_open_o);
      _imageList.Images.Add(ICON_WARNING, Resources.flag_o_red);
      _imageList.Images.Add(ICON_ERROR, Resources.exclamation);

      _treeView.ImageList = _imageList;
      _listView.SmallImageList = _imageList;
      _openButton.ImageList = _imageList;
      _openButton.ImageKey = ICON_FOLDER_OPEN;

      _depthListBox.SelectedIndex = 0;

      _statusLabel.Text = "Open a directory to get started.";

      _lastOpenedPath = Environment.CurrentDirectory;

      // Set up the context menu for the directories in the tree view and
      // the rules in the list view:
      int ruleItemsAdded = 0;
      int dirItemsAdded = 0;

      foreach (var key in ConfigurationManager.AppSettings.AllKeys)
      {
        if (key!.StartsWith("rule:"))
        {
          var menu = key!.Substring(5);
          var command = ConfigurationManager.AppSettings[key!]!;
          var item = _listViewContextMenu.Items.Add(menu);
          item.Tag = new DynamicCommand(menu, command);
          item.Click += RuleCommandItem_Click;
          ruleItemsAdded++;
        }
        else if (key!.StartsWith("dir:"))
        {
          var menu = key!.Substring(4);
          var command = ConfigurationManager.AppSettings[key!]!;
          var item = _treeViewContextMenu.Items.Add(menu);
          item.Tag = new DynamicCommand(menu, command);
          item.Click += DirCommandItem_Click;
          dirItemsAdded++;
        }
      }
      // No rule items? No menu!
      if (ruleItemsAdded == 0)
        _listView.ContextMenuStrip = null;

      // Hide our own menu if we have custom commands
      if (dirItemsAdded > 0)
      {
        _showInExplorerMenuItem.Visible = false;
      }
    }

    /// <summary>
    /// Load the folder specified on the command line if specified
    /// </summary>
    private async void MobZecForm_Load(object sender, EventArgs e)
    {
      // [0] is the name of the executable, [1] is a folder to open and [2] is depth
      if (Environment.GetCommandLineArgs().Length > 1)
      {
        _rootPath = Environment.GetCommandLineArgs()[1];

        int depth = 0;
        if (Environment.GetCommandLineArgs().Length > 2)
          int.TryParse(Environment.GetCommandLineArgs()[2], out depth);

        await LoadSecurityAsync(_rootPath, depth);

        _lastOpenedPath = Path.GetFullPath(_rootPath);
        // Update the combo box if we chose an applicable depth
        if (depth < _depthListBox.Items.Count)
          _depthListBox.SelectedIndex = depth;
      }
    }

    private async void RuleCommandItem_Click(object? sender, EventArgs e)
    {
      var rule = (DynamicCommand)(((ToolStripItem)sender!).Tag!);
      await RunCommandOnRule(rule.Command);
    }

    private async void DirCommandItem_Click(object? sender, EventArgs e)
    {
      var rule = (DynamicCommand)(((ToolStripItem)sender!).Tag!);
      await RunCommandOnDirectory(rule.Command);
    }

    /// <summary>
    /// Load the security of a folder and (optionally) subfolders to a certain depth
    /// </summary>
    /// <param name="path">The path to load. May be relative</param>
    /// <param name="depth">The depth to load. 0 = all, 1 is path only, 2+ = additional levels</param>
    private async Task LoadSecurityAsync(string path, int depth)
    {
      _splitContainer.Enabled = false;
      _splitContainer.UseWaitCursor = true; // Doesn't work?

      try
      {
        // Hide the Open button, show the Cancel button
        _cancelled = false;
        _openPanel.Visible = false;
        _statusLabel.Text = $"Loading '{path}'...";
        _cancelButton.Visible = true;

        // Keep a (case insensitive!) tab on which directory was added to the tree where
        var nodeDict = new Dictionary<string, TreeNode>(StringComparer.OrdinalIgnoreCase);

        // Add the root path as the only node
        _treeView.Nodes.Clear();
        var firstNode = _treeView.Nodes.Add(Path.GetFullPath(path));
        nodeDict.Add(firstNode.Text, firstNode);
        firstNode.Expand();

        _listView.Items.Clear();

        // We only update the status label every so many ms, to prevent it eating CPU
        var lastUpdateTime = DateTime.Now.AddHours(-1);
        // This method is called on every folder. Return true to cancel
        var callback = (string name) =>
        {
          // Skip paths we already added
          if (!nodeDict.ContainsKey(name))
          {
            // Find the parent:
            var parentPath = Path.GetDirectoryName(name);
            // Update only first level nodes to show progress
            if (nodeDict.TryGetValue(parentPath!, out TreeNode? node) && node == firstNode)
            {
              // We found the parent: add the node
              Invoke(() =>
              {
                var newNode = node.Nodes.Add(Path.GetFileName(name));
                nodeDict.Add(name, newNode);
                // Show this node
                newNode.EnsureVisible();
                _treeView.EndUpdate();
                _treeView.Update();
                _treeView.BeginUpdate();
              });
            }
          }

          var time = DateTime.Now;
          if (time.Subtract(lastUpdateTime).TotalMilliseconds > UpdateMs)
          {
            // Update the status label USING INVOKE()
            Invoke(() =>
            {
              _statusLabel.Text = name;
            });

            lastUpdateTime = time;
          }
          return _cancelled;
        };

        // This is the result:
        AclDirectory? a = null;

        try
        {
          // Block updates to the tree
          _treeView.BeginUpdate();
          a = await Task.Run(() => AclDirectory.FromPath(path, depth, callback));
        }
        catch (Exception ex)
        {
          // We failed somehow - no result
          MessageBox.Show(this, ex.Message, $"Error loading '{path}'", MessageBoxButtons.OK, MessageBoxIcon.Error);
          a = null;
        }
        finally
        {
          _treeView.EndUpdate();
        }

        // Swap Open and Cancel again, disable Open while we're displaying
        _openPanel.Enabled = false;
        _openPanel.Visible = true;
        _cancelButton.Visible = false;

        if (a != null)
        {
          _statusLabel.Text = "Displaying results...";
          _topPanel.Update();

          _treeView.BeginUpdate();

          _treeView.Nodes.Clear();

          // Add the root node with its full path
          var rootNode = _treeView.Nodes.Add(a.FullName);
          ColorNode(rootNode, a);

          rootNode.Tag = a;
          AddNodes(a, rootNode);
          rootNode.Expand();
          _treeView.SelectedNode = rootNode;

          _treeView.EndUpdate();

          // Update the window title
          Text = $"{a.FullName} - {_titleBase}";
        }
        else
        {
          _statusLabel.Text = $"Failed to load '{path}'";
        }

        // Re-enable Open button
        _openPanel.Enabled = true;
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
        ColorNode(child, d);

        if (d.HasExplicitAccessRules)
        {
          child.EnsureVisible();
          shouldExpand = true;
        }
        shouldExpand |= AddNodes(d, child);
      }

      return shouldExpand;
    }

    /// <summary>
    /// Set node properties based on AclDirectory
    /// </summary>
    private void ColorNode(TreeNode node, AclDirectory dir)
    {
      if (dir.Exception != null)
      {
        node.ImageKey = ICON_ERROR;
        node.SelectedImageKey = ICON_ERROR;
      }
      else if (dir.HasExplicitAccessRules)
      {
        node.ImageKey = ICON_WARNING;
        node.SelectedImageKey = ICON_WARNING;
      }
      else
      {
        node.ImageKey = ICON_FOLDER;
        node.SelectedImageKey = ICON_FOLDER_OPEN;
      }
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

        if (dir.AccessRules != null)
        {
          var rules = dir.AccessRules;
          foreach (FileSystemAccessRule rule in rules)
          {
            string account = GetSidName(rule.IdentityReference.Value);
            var item = _listView.Items.Add(rule.IsInherited ? "No" : "Yes");
            item.Tag = rule;
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
    }

    /// <summary>
    /// Convert an SID ("S-xxx-xxx") to an account name. Leaves the SID on error
    /// </summary>
    private string GetSidName(string sid)
    {
      try
      {
        // This may fail if the account cannot be found (anymore)
        return new SecurityIdentifier(sid).Translate(typeof(NTAccount)).Value;
      }
      catch
      {
        return sid;
      }

    }
    /// <summary>
    /// Allow the user to choose a folder to open and parse
    /// </summary>
    private async void _openButton_Click(object sender, EventArgs e)
    {
      // Press Shift to load a directory and its immediate children
      // int maxDepth = (Control.ModifierKeys & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.Shift ? 2 : 0;
      int maxDepth = _depthListBox.SelectedIndex;

      using (var dlg = new FolderBrowserDialog())
      {
        dlg.UseDescriptionForTitle = true;
        dlg.Description = "Choose a folder to open";
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

    private void _treeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
      // Also select items using the right mouse button
      if (e.Node != null && e.Button != MouseButtons.Left)
        _treeView.SelectedNode = e.Node;
    }

    /// <summary>
    /// Default menu item for directories. Hidden if there are custom commands in AppSettings
    /// </summary>
    private void _showInExplorerMenuItem_Click(object sender, EventArgs e)
    {
      if (_treeView.SelectedNode != null)
      {
        var path = ((AclDirectory)_treeView.SelectedNode.Tag).FullName;
        Process.Start("explorer.exe", $"/select,\"{path}\"");
      }
    }

    /// <summary>
    /// Start a command on a rule
    /// </summary>
    private async Task RunCommandOnRule(string commandLine)
    {
      if (_listView.SelectedItems.Count == 0)
        return;

      var item = _listView.SelectedItems[0];
      if (item == null)
        return;

      var rule = (FileSystemAccessRule)item.Tag!;
      var commandToRun = commandLine
       .Replace("#ID#", rule.IdentityReference.Value)
       .Replace("#NAME#", GetSidName(rule.IdentityReference.Value));

      string workingDirectory;

      if (_treeView.SelectedNode != null)
      {
        var dir = (AclDirectory)(_treeView.SelectedNode.Tag!);
        workingDirectory = dir.FullName;
      }
      else
      {
        workingDirectory = Environment.CurrentDirectory;
      }

      await StartProcess(commandToRun, workingDirectory);
    }

    /// <summary>
    /// Start a command on a directory
    /// </summary>
    private async Task RunCommandOnDirectory(string commandLine)
    {
      if (_treeView.SelectedNode == null)
        return;

      var dir = (AclDirectory)(_treeView.SelectedNode.Tag!);
      string commandToRun = commandLine.Replace("#PATH#", dir.FullName);

      await StartProcess(commandToRun, dir.FullName);
    }


    /// <summary>
    /// Start a process based on a command line. The "scheme" of the command line
    /// can be pwsh: powershell: cmd: or shell: or it can be absent.
    /// </summary>
    /// <param name="commandToRun"></param>
    /// <param name="workingDirectory"></param>
    private async Task StartProcess(string commandToRun, string workingDirectory)
    {
      var pi = new ProcessStartInfo();

      if (commandToRun.StartsWith("powershell:"))
      {
        pi.FileName = "powershell.exe";
        pi.ArgumentList.Add("-NoLogo");
        pi.ArgumentList.Add("-Sta");
        pi.ArgumentList.Add("-NoProfile");
        pi.ArgumentList.Add("-NonInteractive");
        pi.ArgumentList.Add("-ExecutionPolicy");
        pi.ArgumentList.Add("Unrestricted");
        pi.ArgumentList.Add("-Command ");
        pi.ArgumentList.Add(commandToRun.Substring(11));

        pi.RedirectStandardOutput = true;
        pi.UseShellExecute = false;
        pi.CreateNoWindow = true;
      }
      else if (commandToRun.StartsWith("pwsh:"))
      {
        pi.FileName = "pwsh.exe";
        pi.ArgumentList.Add("-NoLogo");
        pi.ArgumentList.Add("-Sta");
        pi.ArgumentList.Add("-NoProfile");
        pi.ArgumentList.Add("-NonInteractive");
        pi.ArgumentList.Add("-ExecutionPolicy");
        pi.ArgumentList.Add("Unrestricted");
        pi.ArgumentList.Add("-Command ");
        pi.ArgumentList.Add(commandToRun.Substring(5));

        pi.RedirectStandardOutput = true;
        pi.UseShellExecute = false;
        pi.CreateNoWindow = true;
      }
      else if (commandToRun.StartsWith("cmd:"))
      {
        pi.FileName = "cmd.exe";
        pi.ArgumentList.Add("/c");
        pi.ArgumentList.Add(commandToRun.Substring(4));

        pi.RedirectStandardOutput = true;
        pi.UseShellExecute = false;
        pi.CreateNoWindow = true;
      }
      else if (commandToRun.StartsWith("shell:"))
      {
        pi.FileName = commandToRun.Substring(6);

        pi.RedirectStandardOutput = false;
        pi.UseShellExecute = true;
        pi.CreateNoWindow = false;
      }
      else
      {
        var parts = commandToRun.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
        {
          pi.FileName = commandToRun;
        }
        else
        {
          pi.FileName = parts[0];
          pi.Arguments = parts[1];
        }

        pi.RedirectStandardOutput = false;
        pi.UseShellExecute = false;
        pi.CreateNoWindow = false;
      }

      pi.WorkingDirectory = workingDirectory;

      var process = Process.Start(pi);
      if (process != null)
      {
        await process.WaitForExitAsync();
        if (pi.RedirectStandardOutput)
        {
          var output = process.StandardOutput.ReadToEnd();
          if (!string.IsNullOrWhiteSpace(output))
          {
            MessageBox.Show(this, output, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
    }

    /// <summary>
    /// Hidden menu item
    /// </summary>
    private async void _showDirectMembersMenuItem_Click(object sender, EventArgs e)
    {
      await RunCommandOnRule("Get-AdGroupMember -Identity \"#ID#\" | Select-Object SamAccountName, Name | Sort-Object SamAccountName, Name | Out-GrdiView -Title 'Direct members of group #NAME#'");
    }

    /// <summary>
    /// Hidden menu item
    /// </summary>
    private async void _showAllMembersMenuItem_Click(object sender, EventArgs e)
    {
      await RunCommandOnRule("Get-AdGroupMember -Identity \"#ID#\" -Recursive | Select-Object SamAccountName, Name | Sort-Object SamAccountName, Name| Out-GrdiView -Title 'All members of group #NAME#'");
    }
  }
}