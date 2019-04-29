using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;
using TBQuestGame.DataLayer;
using TBQuestGame.Models;
using System.Collections.ObjectModel;

namespace TBQuestGame.BusinessLayer
{
    public class GameBusiness
    {

        #region FIELDS

        private GameViewModel _gameSessionViewModel;
        private bool _newPlayer = true; // Disable For testing - re-enable when done.
        protected Player _player = new Player();
        private PlayerStartupView _playerSetupView;
        private List<string> _messages;
        private Map _gameMap;
        private Location _currentLocation;
        
        #endregion

        #region PROPERTIES

        public PlayerStartupView _playerSetUpView
        {
            get { return _playerSetupView; }
            set { _playerSetupView = value; }
        }
        public List<string> Messages
        {
            get { return _messages; }
            set { _messages = value; }
        }
        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set { _currentLocation = value; }
        }

        #endregion
                          
        #region CONSTRUCTORS
        /// <summary>
        /// Default Constructor that calls SetupPlayer method and InstantiateAndShowView method.
        /// </summary>
        public GameBusiness()
        {
            SetupPlayer();
            InitializeDataSet();
            InstantiateAndShowView();
        }

        #endregion

        #region METHODS
        
        /// <summary>
        /// Method to setup new or existing player
        /// </summary>
        private void SetupPlayer()
        {
            if (_newPlayer)
            {
                _playerSetUpView = new PlayerStartupView(_player);
                _playerSetUpView.ShowDialog();

                _player.ExperiencePoints += 0;
                _player.Health += 80;
                _player.Lives += 3;
                _player.EquippedArmor = GameData.GetGameItemById(0001);
                _player.EquippedWeapon = GameData.GetGameItemById(0002);
                _player.Inventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GameData.GetGameItemById(1001),3),
                    new GameItemQuantity(GameData.GetGameItemById(1000),1)
                };

            }
            else
            {
                _player = GameData.PlayerData();
            }
        }
        
        /// <summary>
        /// Method that loads initial dataset for testing.
        /// </summary>
        private void InitializeDataSet()
        {
            _messages = GameData.InitialMessages();
            _gameMap = GameData.GameMap();
            _currentLocation = _gameMap.CurrentMapLocation;
        }

        /// <summary>
        /// Method that instantiates the initial view model and data set.
        /// </summary>
        private void InstantiateAndShowView()
        {
            // Instantiate the view model and initialize the initial data set
            _gameSessionViewModel = new GameViewModel(_player,_messages, _gameMap, _currentLocation);
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel)
            {
                DataContext = _gameSessionViewModel
            };

            // Display main gameplay window.
            gameSessionView.Show();

            // Closes the set-up window if open.
            _playerSetupView.Close();
            
        }
        #endregion
    }

}






