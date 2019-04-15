using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;
using System.Collections.ObjectModel;

namespace TBQuestGame.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {


        #region ENUMS



        #endregion

        #region FIELDS
        private Player _player;
        private List<string> _messages;
        private DateTime _gameStartTime;
        private Map _gameMap;
        private Location _currentLocation;
        private string _currentLocationName;
        private ObservableCollection<Location> _accessibleLocations;

        #endregion

        #region PROPERTIES

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return FormatMessagesForViewer(); }
        }

        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set
            {
                _currentLocationName = value;
                OnPlayerMove();
                OnPropertyChanged(nameof(CurrentLocation));
            }
        }

        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessibleLocations; }
            set
            {
                _accessibleLocations = value;
                OnPropertyChanged(nameof(AccessibleLocations));
            }
        }


        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor that requires player data and initial messages.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="initialMessages"></param>
        public GameSessionViewModel(Player player, List<string> initialMessages, Map gameMap, Location currentLocation)
        {
            _player = player;
            _messages = initialMessages;
            _gameMap = gameMap;
            _currentLocation = gameMap.CurrentMapLocation;
            _accessibleLocations = new ObservableCollection<Location>();

            InitializeView();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initial setup of the game session view
        /// </summary>
        private void InitializeView()
        {
            // Get current time: 
            _gameStartTime = DateTime.Now;

            // Initilize accessible locations: 
            UpdateAccessibleLocations();
        }

        /// <summary>
        /// (Velis) Generates a sting of mission messages with time stamp with most current first
        /// </summary>
        /// <returns>string of formated mission messages</returns>
        private string FormatMessagesForViewer()
        {
            List<string> lifoMessages = new List<string>();

            for (int index = 0; index < _messages.Count; index++)
            {
                lifoMessages.Add($" <T:{GameTime().ToString(@"hh\:mm\:ss")}> " + _messages[index]);
            }

            lifoMessages.Reverse();

            return string.Join("\n\n", lifoMessages);
        }

        /// <summary>
        /// (Velis) running time of game
        /// </summary>
        /// <returns></returns>
        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        /// <summary>
        /// Adjusts data based on player moving:
        /// </summary>
        private void OnPlayerMove()
        {
            // Set New Current Location:
            _currentLocation = AccessibleLocations.FirstOrDefault(l => l.Name == _currentLocationName);

            if (!_player.LocationsVisited.Contains(_currentLocation))
            {
                _player.LocationsVisited.Add(_currentLocation);

                // XP Modifier
                _player.ExperiencePoints += _currentLocation.XPModifier;

                               
                // todo Impliment Live Modifiers:
                // Lives | NOT IMPLIMENTED
                // if (_currentLocation.ModifyLives != 0) _player.Lives += _currentLocation.LivesModifier;
                //}

            }
            
            // Health
            if (_player.Health < 100 && _currentLocation.HealthModifier > 0)
            {
                _player.Health += _currentLocation.HealthModifier;
                if (_player.Health > 100)
                {
                    _player.Health = 100;
                    //_player.Lives++;
                }

                _messages.Add("\n\tSomething in this area has restored some of your health.");

            }

            if (_currentLocation.HealthModifier != 0 && _currentLocation.HealthModifier < 0)
            {
                _player.Health += _currentLocation.HealthModifier;
                if (_player.Health == 0)
                {
                    _player.Health = 100;
                    _player.Lives--;
                }

                _messages.Add("\n\tSomething in this area has sapped some of your vitality.");

            }

            // Message: 
            OnPropertyChanged(nameof(MessageDisplay));

            // Update Location List:
            UpdateAccessibleLocations();

        }

        /// <summary>
        /// Updates location list:
        /// </summary>
        private void UpdateAccessibleLocations()
        {
            // Reset List:
            _accessibleLocations.Clear();

            // Add All Locations To List:
            foreach (Location location in _gameMap.MapLocations)
            {
                if (location.IsAccessible == true || _player.ExperiencePoints >= location.RequiredXP)
                {
                    _accessibleLocations.Add(location);
                }

            }

            //Remove Current Location: 
            _accessibleLocations.Remove(_accessibleLocations.FirstOrDefault(l => l == _currentLocation));

            // Event Caller:
            OnPropertyChanged(nameof(AccessibleLocations));
        }

        /// <summary>
        /// Exit Application:
        /// </summary>
        public void QuitApplication()
        {

            Environment.Exit(0);
        }
        #endregion

        #region EVENTS



        #endregion
    }
}
