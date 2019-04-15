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
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.PresentationLayer
{
    /// <summary>
    /// Interaction logic for GameSessionView.xaml
    /// </summary>
    public partial class GameSessionView : Window
    {
        GameViewModel _gameSessionViewModel;

        public GameSessionView()
        {
            InitializeComponent();
        }

        public GameSessionView(GameViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;
            InitializeComponent();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.QuitApplication();
        }

        private void btn_pick_up_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.AddItemToPlayerInventory();
        }

        private void btn_drop_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.RemoveItemFromPlayerInventory();
        }

        private void btn_use_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.OnUseGameItem();
        }
    }
}
