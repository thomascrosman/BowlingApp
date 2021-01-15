using BowlingApp.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BowlingApp
{
    /// <summary>
    /// Interaction logic for GameControl.xaml
    /// </summary>
    public partial class GameControl : UserControl
    {
        public GameControl()
        {
            InitializeComponent();
        }

        void showLoadRollsDialog_Click(object sender, RoutedEventArgs e)
        {
            GameVM gameVM = (GameVM)DataContext;
            LoadRollsDialog loadRollsDialog = new LoadRollsDialog(gameVM.Rolls);
            if (loadRollsDialog.ShowDialog() == true)
            {
                gameVM.Reset();
                foreach (int roll in loadRollsDialog.Rolls)
                {
                    gameVM.Roll(roll);
                }
            }
        }


    }
}
