namespace MobZec
{
  partial class MobZecForm
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this._splitContainer = new System.Windows.Forms.SplitContainer();
      this._treeView = new System.Windows.Forms.TreeView();
      this._treeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._showInExplorerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._listView = new System.Windows.Forms.ListView();
      this._explicitColumn = new System.Windows.Forms.ColumnHeader();
      this._nameColumn = new System.Windows.Forms.ColumnHeader();
      this._typeColumn = new System.Windows.Forms.ColumnHeader();
      this._rightsColumn = new System.Windows.Forms.ColumnHeader();
      this._listViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this._showDirectMembersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._showAllMembersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this._topPanel = new System.Windows.Forms.Panel();
      this._loadingPanel = new System.Windows.Forms.ToolStrip();
      this._cancelButton = new System.Windows.Forms.ToolStripButton();
      this._statusLabel = new System.Windows.Forms.Label();
      this._openPanel = new System.Windows.Forms.ToolStrip();
      this._openButton = new System.Windows.Forms.ToolStripButton();
      this._depthListBox = new System.Windows.Forms.ToolStripComboBox();
      this._updateAvailableButton = new System.Windows.Forms.ToolStripDropDownButton();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._treeViewContextMenu.SuspendLayout();
      this._listViewContextMenu.SuspendLayout();
      this._topPanel.SuspendLayout();
      this._loadingPanel.SuspendLayout();
      this._openPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // _splitContainer
      // 
      this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this._splitContainer.Location = new System.Drawing.Point(0, 32);
      this._splitContainer.Name = "_splitContainer";
      // 
      // _splitContainer.Panel1
      // 
      this._splitContainer.Panel1.Controls.Add(this._treeView);
      // 
      // _splitContainer.Panel2
      // 
      this._splitContainer.Panel2.Controls.Add(this._listView);
      this._splitContainer.Size = new System.Drawing.Size(800, 418);
      this._splitContainer.SplitterDistance = 266;
      this._splitContainer.TabIndex = 0;
      // 
      // _treeView
      // 
      this._treeView.ContextMenuStrip = this._treeViewContextMenu;
      this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._treeView.FullRowSelect = true;
      this._treeView.HideSelection = false;
      this._treeView.Location = new System.Drawing.Point(0, 0);
      this._treeView.Name = "_treeView";
      this._treeView.Size = new System.Drawing.Size(266, 418);
      this._treeView.TabIndex = 0;
      this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
      this._treeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._treeView_NodeMouseClick);
      // 
      // _treeViewContextMenu
      // 
      this._treeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._showInExplorerMenuItem});
      this._treeViewContextMenu.Name = "_treeViewContextMenu";
      this._treeViewContextMenu.Size = new System.Drawing.Size(163, 26);
      // 
      // _showInExplorerMenuItem
      // 
      this._showInExplorerMenuItem.Name = "_showInExplorerMenuItem";
      this._showInExplorerMenuItem.Size = new System.Drawing.Size(162, 22);
      this._showInExplorerMenuItem.Text = "Show in &Explorer";
      this._showInExplorerMenuItem.Click += new System.EventHandler(this._showInExplorerMenuItem_Click);
      // 
      // _listView
      // 
      this._listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._explicitColumn,
            this._nameColumn,
            this._typeColumn,
            this._rightsColumn});
      this._listView.ContextMenuStrip = this._listViewContextMenu;
      this._listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._listView.FullRowSelect = true;
      this._listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
      this._listView.HideSelection = true;
      this._listView.Location = new System.Drawing.Point(0, 0);
      this._listView.Name = "_listView";
      this._listView.Size = new System.Drawing.Size(530, 418);
      this._listView.TabIndex = 0;
      this._listView.UseCompatibleStateImageBehavior = false;
      this._listView.View = System.Windows.Forms.View.Details;
      // 
      // _explicitColumn
      // 
      this._explicitColumn.Text = "Explicit?";
      this._explicitColumn.Width = 80;
      // 
      // _nameColumn
      // 
      this._nameColumn.Text = "Name";
      this._nameColumn.Width = 200;
      // 
      // _typeColumn
      // 
      this._typeColumn.Text = "Access";
      // 
      // _rightsColumn
      // 
      this._rightsColumn.Text = "Rights";
      this._rightsColumn.Width = 200;
      // 
      // _listViewContextMenu
      // 
      this._listViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._showDirectMembersMenuItem,
            this._showAllMembersMenuItem});
      this._listViewContextMenu.Name = "_listViewContextMenu";
      this._listViewContextMenu.Size = new System.Drawing.Size(190, 48);
      // 
      // _showDirectMembersMenuItem
      // 
      this._showDirectMembersMenuItem.Name = "_showDirectMembersMenuItem";
      this._showDirectMembersMenuItem.Size = new System.Drawing.Size(189, 22);
      this._showDirectMembersMenuItem.Text = "Show direct &members";
      this._showDirectMembersMenuItem.Visible = false;
      this._showDirectMembersMenuItem.Click += new System.EventHandler(this._showDirectMembersMenuItem_Click);
      // 
      // _showAllMembersMenuItem
      // 
      this._showAllMembersMenuItem.Name = "_showAllMembersMenuItem";
      this._showAllMembersMenuItem.Size = new System.Drawing.Size(189, 22);
      this._showAllMembersMenuItem.Text = "Show &all members";
      this._showAllMembersMenuItem.Visible = false;
      this._showAllMembersMenuItem.Click += new System.EventHandler(this._showAllMembersMenuItem_Click);
      // 
      // _topPanel
      // 
      this._topPanel.Controls.Add(this._loadingPanel);
      this._topPanel.Controls.Add(this._statusLabel);
      this._topPanel.Controls.Add(this._openPanel);
      this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this._topPanel.Location = new System.Drawing.Point(0, 0);
      this._topPanel.Name = "_topPanel";
      this._topPanel.Size = new System.Drawing.Size(800, 32);
      this._topPanel.TabIndex = 1;
      // 
      // _loadingPanel
      // 
      this._loadingPanel.Dock = System.Windows.Forms.DockStyle.Left;
      this._loadingPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._loadingPanel.ImageScalingSize = new System.Drawing.Size(24, 24);
      this._loadingPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._cancelButton});
      this._loadingPanel.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this._loadingPanel.Location = new System.Drawing.Point(308, 0);
      this._loadingPanel.Name = "_loadingPanel";
      this._loadingPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this._loadingPanel.Size = new System.Drawing.Size(105, 32);
      this._loadingPanel.TabIndex = 7;
      this._loadingPanel.Visible = false;
      // 
      // _cancelButton
      // 
      this._cancelButton.Image = global::MobZec.Properties.Resources.cancel;
      this._cancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.Size = new System.Drawing.Size(71, 29);
      this._cancelButton.Text = "Cancel";
      this._cancelButton.ToolTipText = "Cancel the current operation";
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _statusLabel
      // 
      this._statusLabel.AutoEllipsis = true;
      this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this._statusLabel.Location = new System.Drawing.Point(308, 0);
      this._statusLabel.Margin = new System.Windows.Forms.Padding(3);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
      this._statusLabel.Size = new System.Drawing.Size(492, 32);
      this._statusLabel.TabIndex = 2;
      this._statusLabel.Text = "...";
      this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._statusLabel.UseMnemonic = false;
      // 
      // _openPanel
      // 
      this._openPanel.Dock = System.Windows.Forms.DockStyle.Left;
      this._openPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this._openPanel.ImageScalingSize = new System.Drawing.Size(24, 24);
      this._openPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openButton,
            this._depthListBox,
            this._updateAvailableButton});
      this._openPanel.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
      this._openPanel.Location = new System.Drawing.Point(0, 0);
      this._openPanel.Name = "_openPanel";
      this._openPanel.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
      this._openPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this._openPanel.Size = new System.Drawing.Size(308, 32);
      this._openPanel.TabIndex = 6;
      this._openPanel.Text = "toolStrip1";
      // 
      // _openButton
      // 
      this._openButton.Image = global::MobZec.Properties.Resources.folder_open_o;
      this._openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._openButton.Name = "_openButton";
      this._openButton.Size = new System.Drawing.Size(98, 29);
      this._openButton.Text = "Open folder";
      this._openButton.Click += new System.EventHandler(this._openButton_Click);
      // 
      // _depthListBox
      // 
      this._depthListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this._depthListBox.Items.AddRange(new object[] {
            "Load all subdirectories",
            "Load top directory only",
            "Top directory and subdirectories",
            "Top directory and 2 subdirectories",
            "Top directory and 3 subdirectories"});
      this._depthListBox.Name = "_depthListBox";
      this._depthListBox.Size = new System.Drawing.Size(200, 32);
      // 
      // _updateAvailableButton
      // 
      this._updateAvailableButton.Image = global::MobZec.Properties.Resources.exclamation;
      this._updateAvailableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
      this._updateAvailableButton.Name = "_updateAvailableButton";
      this._updateAvailableButton.ShowDropDownArrow = false;
      this._updateAvailableButton.Size = new System.Drawing.Size(44, 29);
      this._updateAvailableButton.Text = "...";
      this._updateAvailableButton.Visible = false;
      this._updateAvailableButton.Click += new System.EventHandler(this._updateAvailableButton_Click);
      // 
      // MobZecForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this._splitContainer);
      this.Controls.Add(this._topPanel);
      this.Name = "MobZecForm";
      this.Text = "MOBZec";
      this.Load += new System.EventHandler(this.MobZecForm_Load);
      this._splitContainer.Panel1.ResumeLayout(false);
      this._splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
      this._splitContainer.ResumeLayout(false);
      this._treeViewContextMenu.ResumeLayout(false);
      this._listViewContextMenu.ResumeLayout(false);
      this._topPanel.ResumeLayout(false);
      this._topPanel.PerformLayout();
      this._loadingPanel.ResumeLayout(false);
      this._loadingPanel.PerformLayout();
      this._openPanel.ResumeLayout(false);
      this._openPanel.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private ColumnHeader _nameColumn;
    private ColumnHeader _explicitColumn;
    private Panel _topPanel;
    private ColumnHeader _typeColumn;
    private ColumnHeader _rightsColumn;
    private Label _statusLabel;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showInExplorerMenuItem;
        private ContextMenuStrip _listViewContextMenu;
        private ToolStripMenuItem _showDirectMembersMenuItem;
        private ToolStripMenuItem _showAllMembersMenuItem;
        private ToolStrip _openPanel;
        private ToolStripButton _openButton;
        private ToolStripComboBox _depthListBox;
        private ToolStripDropDownButton _updateAvailableButton;
        private ToolStrip _loadingPanel;
        private ToolStripButton _cancelButton;
    }
}