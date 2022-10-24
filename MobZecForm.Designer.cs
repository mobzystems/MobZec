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
      this._nameColumn = new System.Windows.Forms.ColumnHeader();
      this._inheritedColumn = new System.Windows.Forms.ColumnHeader();
      this._typeColumn = new System.Windows.Forms.ColumnHeader();
      this._rightColumn = new System.Windows.Forms.ColumnHeader();
      this._topPanel = new System.Windows.Forms.Panel();
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
            this._nameColumn,
            this._inheritedColumn,
            this._typeColumn,
            this._rightColumn});
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
      // _nameColumn
      // 
      this._nameColumn.Text = "Naam";
      this._nameColumn.Width = 200;
      // 
      // _inheritedColumn
      // 
      this._inheritedColumn.Text = "Expliciet";
      this._inheritedColumn.Width = 80;
      // 
      // _typeColumn
      // 
      this._typeColumn.Text = "Soort";
      // 
      // _rightColumn
      // 
      this._rightColumn.Text = "Rechten";
      // 
      // _topPanel
      // 
      this._topPanel.Controls.Add(this._openButton);
      this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this._topPanel.Location = new System.Drawing.Point(0, 0);
      this._topPanel.Name = "_topPanel";
      this._topPanel.Size = new System.Drawing.Size(800, 32);
      this._topPanel.TabIndex = 1;
      // 
      // _openButton
      // 
      this._openButton.Dock = System.Windows.Forms.DockStyle.Left;
      this._openButton.Location = new System.Drawing.Point(0, 0);
      this._openButton.Name = "_openButton";
      this._openButton.Size = new System.Drawing.Size(79, 32);
      this._openButton.TabIndex = 0;
      this._openButton.Text = "Open...";
      this._openButton.UseVisualStyleBackColor = true;
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
        private ColumnHeader _inheritedColumn;
        private Panel _topPanel;
        private Button _openButton;
    private ColumnHeader _typeColumn;
    private ColumnHeader _rightColumn;
  }
}