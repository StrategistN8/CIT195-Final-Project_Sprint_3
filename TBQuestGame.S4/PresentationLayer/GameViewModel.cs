﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace TBQuestGame.PresentationLayer
{
    public class GameViewModel : ObservableObject
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
        private GameItemQuantity _currentGameItem;
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
        public GameItemQuantity CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor that requires player data and initial messages.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="initialMessages"></param>
        public GameViewModel(Player player, List<string> initialMessages, Map gameMap, Location currentLocation)
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

            // Inventory:
            _player.UpdateInventory();
            _player.CalculateInventoryValue();
        }

        /// <summary>
        /// Takes item from location and adds it to the player's inventory
        /// </summary>
        public void AddItemToPlayerInventory()
        {
            if (_currentGameItem != null && _currentLocation.LocationInventory.Contains(_currentGameItem))
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;

                _currentLocation.RemoveGameItemFromInventory(selectedGameItemQuantity);
                _player.AddGameItemToInventory(selectedGameItemQuantity);

                //OnPlayerPickup(selectedGameItemQuantity);
            }
            
            
        }

        /// <summary>
        /// Takes item from player inventory and adds it to location.
        /// </summary>
        public void RemoveItemFromPlayerInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;
                _currentLocation.AddGameItemToInventory(selectedGameItemQuantity);
                _player.RemoveGameItemFromInventory(selectedGameItemQuantity);
                
                OnPlayerPutDown(selectedGameItemQuantity);
            }
        }

        /// <summary>
        /// Small helper method that adjusts player stats when inventory is gained.
        /// </summary>
        /// <param name="gameItem"></param>
        private void OnPlayerPickup(GameItemQuantity gameItemQuantity)
        {
            _player.Wealth += gameItemQuantity.GameItem.ItemValue;
        }

        /// <summary>
        /// Small helper method that adjusts player stats when inventory is lost.
        /// </summary>
        /// <param name="gameItem"></param>
        private void OnPlayerPutDown(GameItemQuantity gameItemQuantity)
        {
            _player.Wealth -= gameItemQuantity.GameItem.ItemValue;
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

            }

            // Health
            if (_player.Health < 100 && _currentLocation.HealthModifier > 0)
            {
                _player.CheckPlayerHealthByLocation(_currentLocation);

                _messages.Add("\n\tSomething in this area has restored some of your health.");

            }

            if (_currentLocation.HealthModifier != 0 && _currentLocation.HealthModifier < 0)
            {
                _player.CheckPlayerHealthByLocation(_currentLocation);
                _messages.Add("\n\tSomething in this area has sapped some of your vitality.");

            }

            // Message: 
            OnPropertyChanged(nameof(MessageDisplay));

            // Update Location List:
            UpdateAccessibleLocations();

        }
        
        public static void OnPlayerFlee()
        {
            OnPlayerFlee();
        }



        private void PlayerFlee()
        {
            // Set New Current Location:
            Location newLocation = AccessibleLocations.FirstOrDefault(l => l.LocationId == (_currentLocation.LocationId - 1));
            _currentLocation = newLocation;

            //Hurt the player for fleeing: 
            _player.Health = _player.Health - 10;

            // Message:
            _messages.Add("You flee the fight!");

            OnPropertyChanged(nameof(MessageDisplay));

            // Update Location List:
            UpdateAccessibleLocations();

        }
        
        /// <summary>
        /// Undertaker method that handles player death.
        /// </summary>
        public static void OnPlayerDie(string message)
        {
            //Local Variables:
            string messagetext = message + "\n Would you like to play again?";
            string titleText = "Death";

            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    //ResetPlayer();
                    Environment.Exit(0);
                    break;
                case MessageBoxResult.No:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Processes Inventory Item useage:
        /// </summary>
        public void OnUseGameItem()
        {
            switch (_currentGameItem.GameItem)
            {
                case ItemWeapon weapon:
                    _player.EquippedWeapon = weapon;
                    _messages.Add(weapon.ItemOnUseMessage);
                    break;
                case ItemArmor armor:
                    _player.EquippedArmor = armor;
                    _messages.Add(armor.ItemOnUseMessage);
                    break;
                case ItemArcane arcaneItem:
                    arcaneItem.GetArcaneItemOnUseEffect(_player, arcaneItem);
                    _messages.Add(arcaneItem.ItemOnUseMessage);
                    break;
                case ItemInventory inventoryItem:
                    _messages.Add(inventoryItem.ItemOnUseMessage);
                    break;
                default:
                    break;
            }

            OnPropertyChanged(nameof(MessageDisplay));
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
