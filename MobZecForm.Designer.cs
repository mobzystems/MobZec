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
      this._splitContainer = new System.Windows.Forms.SplitContainer();
      this._treeView = new System.Windows.Forms.TreeView();
      this._listView = new System.Windows.Forms.ListView();
      this._explicitColumn = new System.Windows.Forms.ColumnHeader();
      this._nameColumn = new System.Windows.Forms.ColumnHeader();
      this._typeColumn = new System.Windows.Forms.ColumnHeader();
      this._rightsColumn = new System.Windows.Forms.ColumnHeader();
      this._topPanel = new System.Windows.Forms.Panel();
      this._statusLabel = new System.Windows.Forms.Label();
      this._cancelButton = new System.Windows.Forms.Button();
      this._openButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
      this._splitContainer.Panel1.SuspendLayout();
      this._splitContainer.Panel2.SuspendLayout();
      this._splitContainer.SuspendLayout();
      this._topPanel.SuspendLayout();
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
      this._treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this._treeView.FullRowSelect = true;
      this._treeView.HideSelection = false;
      this._treeView.Location = new System.Drawing.Point(0, 0);
      this._treeView.Name = "_treeView";
      this._treeView.Size = new System.Drawing.Size(266, 418);
      this._treeView.TabIndex = 0;
      this._treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._treeView_AfterSelect);
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
      this._topPanel.Controls.Add(this._openButton);
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
      this._statusLabel.Location = new System.Drawing.Point(154, 0);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Size = new System.Drawing.Size(646, 32);
      this._statusLabel.TabIndex = 2;
      this._statusLabel.Text = "...";
      this._statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this._statusLabel.UseMnemonic = false;
      // 
      // _cancelButton
      // 
      this._cancelButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._cancelButton.Location = new System.Drawing.Point(79, 0);
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.Size = new System.Drawing.Size(75, 32);
      this._cancelButton.TabIndex = 1;
      this._cancelButton.Text = "Cancel";
      this._cancelButton.UseVisualStyleBackColor = true;
      this._cancelButton.Visible = false;
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _openButton
      // 
      this._openButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._openButton.Location = new System.Drawing.Point(0, 0);
      this._openButton.Name = "_openButton";
      this._openButton.Size = new System.Drawing.Size(79, 32);
      this._openButton.TabIndex = 0;
      this._openButton.Text = "Open...";
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
      this._topPanel.ResumeLayout(false);
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
  }
}