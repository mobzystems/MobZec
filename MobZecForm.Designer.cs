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
      this._topPanel = new System.Windows.Forms.Panel();
      this._statusLabel = new System.Windows.Forms.Label();
      this._cancelButton = new System.Windows.Forms.Button();
      this._openPanel = new System.Windows.Forms.Panel();
      this._depthListBox = new System.Windows.Forms.ComboBox();
      this._openButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._treeViewContextMenu.SuspendLayout();
      this._topPanel.SuspendLayout();
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
      this._treeViewContextMenu.Size = new System.Drawing.Size(181, 48);
      // 
      // _showInExplorerMenuItem
      // 
      this._showInExplorerMenuItem.Name = "_showInExplorerMenuItem";
      this._showInExplorerMenuItem.Size = new System.Drawing.Size(180, 22);
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
      // _topPanel
      // 
      this._topPanel.Controls.Add(this._statusLabel);
      this._topPanel.Controls.Add(this._cancelButton);
      this._topPanel.Controls.Add(this._openPanel);
      this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this._topPanel.Location = new System.Drawing.Point(0, 0);
      this._topPanel.Name = "_topPanel";
      this._topPanel.Size = new System.Drawing.Size(800, 32);
      this._topPanel.TabIndex = 1;
      // 
      // _statusLabel
      // 
      this._statusLabel.AutoEllipsis = true;
      this._statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this._statusLabel.Location = new System.Drawing.Point(408, 0);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Size = new System.Drawing.Size(392, 32);
      this._statusLabel.TabIndex = 2;
      this._statusLabel.Text = "...";
      this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._statusLabel.UseMnemonic = false;
      // 
      // _cancelButton
      // 
      this._cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._cancelButton.Location = new System.Drawing.Point(333, 0);
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.Size = new System.Drawing.Size(75, 32);
      this._cancelButton.TabIndex = 1;
      this._cancelButton.Text = "Cancel";
      this._cancelButton.UseVisualStyleBackColor = true;
      this._cancelButton.Visible = false;
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _openPanel
      // 
      this._openPanel.Controls.Add(this._depthListBox);
      this._openPanel.Controls.Add(this._openButton);
      this._openPanel.Dock = System.Windows.Forms.DockStyle.Left;
      this._openPanel.Location = new System.Drawing.Point(0, 0);
      this._openPanel.Name = "_openPanel";
      this._openPanel.Size = new System.Drawing.Size(333, 32);
      this._openPanel.TabIndex = 3;
      // 
      // _depthListBox
      // 
      this._depthListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this._depthListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this._depthListBox.FormattingEnabled = true;
      this._depthListBox.ItemHeight = 15;
      this._depthListBox.Items.AddRange(new object[] {
            "Load all subdirectories",
            "Load top directory only",
            "Top directory and subdirectories",
            "Top directory and 2 subdirectories",
            "Top directory and 3 subdirectories"});
      this._depthListBox.Location = new System.Drawing.Point(110, 4);
      this._depthListBox.Name = "_depthListBox";
      this._depthListBox.Size = new System.Drawing.Size(217, 23);
      this._depthListBox.TabIndex = 2;
      // 
      // _openButton
      // 
      this._openButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._openButton.Location = new System.Drawing.Point(0, 0);
      this._openButton.Name = "_openButton";
      this._openButton.Size = new System.Drawing.Size(104, 32);
      this._openButton.TabIndex = 0;
      this._openButton.Text = "Open...";
      this._openButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
      this._openButton.Click += new System.EventHandler(this._openButton_Click);
      // 
      // MobZecForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this._splitContainer);
      this.Controls.Add(this._topPanel);
      this.Name = "MobZecForm";
      this.Text = "MobZec";
      this.Load += new System.EventHandler(this.MobZecForm_Load);
      this._splitContainer.Panel1.ResumeLayout(false);
      this._splitContainer.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
      this._splitContainer.ResumeLayout(false);
      this._treeViewContextMenu.ResumeLayout(false);
      this._topPanel.ResumeLayout(false);
      this._openPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private SplitContainer _splitContainer;
    private TreeView _treeView;
    private ListView _listView;
    private ColumnHeader _nameColumn;
    private ColumnHeader _explicitColumn;
    private Panel _topPanel;
    private Button _openButton;
    private ColumnHeader _typeColumn;
    private ColumnHeader _rightsColumn;
    private Label _statusLabel;
    private Button _cancelButton;
    private Panel _openPanel;
    private ComboBox _depthListBox;
    private ContextMenuStrip _treeViewContextMenu;
    private ToolStripMenuItem _showInExplorerMenuItem;
  }
}