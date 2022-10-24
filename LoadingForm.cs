using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobZec
{
  public partial class LoadingForm : Form
  {
    private Action _onCancelled;

    public LoadingForm(Action onCancelled)
    {
      _onCancelled = onCancelled;
      InitializeComponent();
    }

    private void _cancelButton_Click(object sender, EventArgs e)
    {
      _onCancelled();
      _cancelButton.Enabled = false;
    }
  }
}
