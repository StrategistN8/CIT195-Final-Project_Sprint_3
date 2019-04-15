using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;
using TBQuestGame.DataLayer;
using TBQuestGame.Models;

namespace TBQuestGame.BusinessLayer
{
    public class GameBusiness
    {

        private GameSessionViewModel _gameSessionViewModel;
        private bool _newPlayer = true; 
        protected Player _player = new Player();
        private PlayerStartupView _playerSetupView;

        public PlayerStartupView _playerSetUpView
        {
            get { return _playerSetupView; }
            set { _playerSetupView = value; }
        }



        /// <summary>
        /// Default Constructor that calls SetupPlayer method and InstantiateAndShowView method.
        /// </summary>
        public GameBusiness()
        {
            SetupPlayer();
            //InitializeDataSet();
            InstantiateAndShowView();
        }

        /// <summary>
        /// Method to setup new or existing player
        /// </summary>
        private void SetupPlayer()
        {
            if (_newPlayer)
            {
                _playerSetUpView = new PlayerStartupView(_player);
                _playerSetUpView.ShowDialog();

                _player.ExperiencePoints = 0;
                _player.Health = 100;
                _player.Lives = 3;

            }
            else
            {
                _player = GameData.PlayerData();
            }
        }

        /// <summary>
        /// Methodd that instantiates the initial view model and data set.
        /// </summary>
        private void InstantiateAndShowView()
        {

            // instantiate the view model and initialize the data set
            _gameSessionViewModel = new GameSessionViewModel(
            _player,
            GameData.InitialMessages()
            );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel)
            {
                DataContext = _gameSessionViewModel
            };

            gameSessionView.Show();

            
            _playerSetupView.Close();
        }
    }

}






