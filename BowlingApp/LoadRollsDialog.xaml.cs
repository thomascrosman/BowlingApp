using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BowlingApp
{
    /// <summary>
    /// Interaction logic for LoadRollsDialog.xaml
    /// </summary>
    public partial class LoadRollsDialog : Window
    {
		public LoadRollsDialog(int[] rolls)
		{
			InitializeComponent();
			tbRolls.Text = string.Join(",", rolls);
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void btnDialogCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			tbRolls.Focus();
		}

		public int[] Rolls
		{
			get
			{
				int[] rolls = tbRolls.Text.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
				return rolls;
			}
		}
	}
}
