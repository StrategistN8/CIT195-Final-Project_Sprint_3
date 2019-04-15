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
using TBQuestGame.Models;

namespace TBQuestGame.PresentationLayer
{
    /// <summary>
    /// Interaction logic for PlayerStartupView.xaml
    /// </summary>
    public partial class PlayerStartupView : Window
    {
        private Player _player;

        public PlayerStartupView(Player player)
        {
            _player = player;

            InitializeComponent();

            SetupWindow();
        }

        /// <summary>
        /// Geneerates the lists used to populate the combo boxes.
        /// </summary>
        private void SetupWindow()
        {
            // Creating data for the dropboxes:

            List<string> races = Enum.GetNames(typeof(Player.RaceType)).ToList();
            List<string> gender = Enum.GetNames(typeof(Player.PlayerGender)).ToList();
            List<string> role = Enum.GetNames(typeof(Player.PlayerRole)).ToList();
            cb_RaceBox.ItemsSource = races;
            cb_GenderBox.ItemsSource = gender;
            cb_RoleBox.ItemsSource = role;

            // Error Handling Box:
            ErrorMessageTextBlock.Visibility = Visibility.Hidden;


        }


        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
           // Not implimented yet - will call the close method eventually.
        }

        private void btn_input_Click(object sender, RoutedEventArgs e)
        {
            string errorMessage;

            if (IsValidInput(out errorMessage))
            {
                // Retrieve Values from user input:
                Enum.TryParse(cb_RaceBox.SelectionBoxItem.ToString(), out Player.RaceType race);
                Enum.TryParse(cb_GenderBox.SelectionBoxItem.ToString(), out Player.PlayerGender gender);
                Enum.TryParse(cb_RoleBox.SelectionBoxItem.ToString(), out Player.PlayerRole role);

                int.TryParse(tb_AgeBox.Text, out int age);


                // Set values:

               _player.Name = tb_NameBox.Text.ToString();
               _player.Race = race;
               _player.Gender = gender;
               _player.Role = role;
               _player.Age = age;
              
                Visibility = Visibility.Hidden;
            }
            else
            {
               
                // display error messages
                ErrorMessageTextBlock.Visibility = Visibility.Visible;
                ErrorMessageTextBlock.Text = errorMessage;
            }
        }
    
        /// <summary>
        /// Validates user input and generates appropriate error messages
        /// </summary>
        /// <param name="errorMessage">user feedback</param>
        /// <returns>is user input valid</returns>
        private bool IsValidInput(out string errorMessage)
        {
            errorMessage = "";

            if (tb_NameBox.Text == "")
            {
                errorMessage += "Player Name is required.\n";
            }
         
            if (!int.TryParse(tb_AgeBox.Text, out int age))
            {
                errorMessage += "Player Age is required and must be an integer.\n";
            }
           

            return errorMessage == "" ? true : false;
        }
    }
}



    

