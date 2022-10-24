namespace MobZec
{
  partial class LoadingForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this._cancelButton = new System.Windows.Forms.Button();
      this._rootLabel = new System.Windows.Forms.Label();
      this._statusLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // _cancelButton
      // 
      this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this._cancelButton.Location = new System.Drawing.Point(562, 122);
      this._cancelButton.Name = "_cancelButton";
      this._cancelButton.Size = new System.Drawing.Size(82, 27);
      this._cancelButton.TabIndex = 0;
      this._cancelButton.Text = "Cancel";
      this._cancelButton.UseVisualStyleBackColor = true;
      this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
      // 
      // _rootLabel
      // 
      this._rootLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._rootLabel.Location = new System.Drawing.Point(12, 9);
      this._rootLabel.Name = "_rootLabel";
      this._rootLabel.Size = new System.Drawing.Size(632, 23);
      this._rootLabel.TabIndex = 1;
      this._rootLabel.Text = "...";
      // 
      // _statusLabel
      // 
      this._statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this._statusLabel.Location = new System.Drawing.Point(12, 32);
      this._statusLabel.Name = "_statusLabel";
      this._statusLabel.Size = new System.Drawing.Size(632, 23);
      this._statusLabel.TabIndex = 2;
      this._statusLabel.Text = "...";
      // 
      // LoadingForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this._cancelButton;
      this.ClientSize = new System.Drawing.Size(656, 161);
      this.Controls.Add(this._statusLabel);
      this.Controls.Add(this._rootLabel);
      this.Controls.Add(this._cancelButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "LoadingForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Loading...";
      this.ResumeLayout(false);

    }

        #endregion

        private Button _cancelButton;
        public Label _rootLabel;
        public Label _statusLabel;
    }
}