using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;
using System.Collections.ObjectModel;
using System.Windows;
using TBQuestGame.DataLayer;

namespace TBQuestGame.PresentationLayer
{
    public class GameViewModel : ObservableObject
    {


        #region ENUMS



        #endregion

        #region FIELDS
        private Player _player;
        private List<string> _messages;
        private string _messagesNPC;
        private string _combatLog;
        private DateTime _gameStartTime;
        private Map _gameMap;
        private Location _currentLocation;
        private string _currentLocationName;
        private ObservableCollection<Location> _accessibleLocations;
        private GameItemQuantity _currentGameItem;
        private NPC _currentNPC;
        private NPC _currentTarget;
        private int _battleRounds;


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
        public string MessagesNPC
        {
            get { return _messagesNPC; }
            set
            {
                _messagesNPC = value;
                OnPropertyChanged(nameof(MessagesNPC));
            }
        }
        public string CombatLog
        {
            get { return _combatLog; }
            set
            {
                _combatLog = value;
                OnPropertyChanged(nameof(CombatLog));
            }
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
        public NPC CurrentNPC
        {
            get { return _currentNPC; }
            set
            {
                _currentNPC = value;
                OnPropertyChanged(nameof(CurrentNPC));
            }
        }
        public NPC CurrentTarget
        {
            get { return _currentTarget; }
            set { _currentTarget = value; }
        }

        Random r = new Random(1);

        public int BattleRounds
        {
            get { return _battleRounds; }
            set { _battleRounds = value; }
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
           // _currentNPC = _currentLocation.LocationNPCs.First(n => n.Id >= 1);
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

        #region INVENTORY CONTROLS:
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

                // OnPlayerPutDown(selectedGameItemQuantity);
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
        /// Processes Inventory Item useage:
        /// </summary>
        public void OnUseGameItem()
        {
            if (_currentGameItem != null)
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
            }
            OnPropertyChanged(nameof(MessageDisplay));
        }

        #endregion

        #region TIME METHODS
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
        #endregion

        #region MOVEMENT METHODS:

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

            // chance of spawning the Monger:
            if (!_currentLocation.LocationNPCs.Contains(GameData.GetNPCById(1)) && _currentLocation.LocationId < 13 && _currentLocation.LocationId != 4)
            {
                switch (r.Next(1,11))
                {
                    case 10:
                        _messages.Add("Something is lurking in the trees... You hear a horrible shriek as the Monger reveals itself!");
                        _currentLocation.LocationNPCs.Add(GameData.GetNPCById(1));
                        break;
                    default:
                        break;
                    
                }
            }

            // Message: 
            OnPropertyChanged(nameof(MessageDisplay));

            // Update Location List:
            UpdateAccessibleLocations();

        }

        /////// <summary>
        /////// Allows the player class to call the PlayerFlee Method;
        /////// </summary>
        ////public static void OnPlayerFlee()
        ////{
        ////    PlayerFlee();
        ////}

        /// <summary>
        /// Withdraws the player from combat.
        /// </summary>
        public void PlayerFlee()
        {
            if (_currentLocation != null)
            {
                // Set New Current Location:
                if (_currentLocation.LocationId == 1)
                {
                    Location newLocation = AccessibleLocations.FirstOrDefault(l => l.LocationId == (CurrentLocation.LocationId + 1));
                    CurrentLocation = newLocation;
                }
                else
                {
                    Location newLocation = AccessibleLocations.FirstOrDefault(l => l.LocationId == (CurrentLocation.LocationId + 1));
                    CurrentLocation = newLocation;
                }


                //Hurt the player for fleeing if they are above 10 hitpoints: 
                if (Player.Health > 10)
                {
                    Player.Health = Player.Health - 10;
                }

                // Message: need to remove backingfield...
                _messages.Add("You flee the fight!");

                OnPropertyChanged(nameof(MessageDisplay));

                // Update Location List:
                UpdateAccessibleLocations();
            }
        }

        ///// <summary>
        ///// Allows the NPC classes that inherit Ifight to call the NPC flee Method.
        ///// </summary>
        //public static void OnNPCFlee()
        //{
        //    //OnPlayerFlee();
        //}

        /// <summary>
        /// Removes the fleeing NPC from the battle.
        /// </summary>
        private void NPCFlee()
        {
            // Remove NPC from location list:

            if (_currentNPC != null)
            {
                _currentLocation.LocationNPCs.Remove(_currentLocation.LocationNPCs.FirstOrDefault(c => c == _currentNPC));


                // Message:
                _messages.Add("Your foe has fled!");
                CombatLog += "Your foe has fled! \n";


                OnPropertyChanged(nameof(MessageDisplay));

                // Update Location List:
                UpdateAccessibleLocations();
            }
        }

        #endregion


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
        /// Methods that allows an NPC to die:
        /// </summary>
        /// 
        public void CheckIfNPCDie()
        {
            if (_currentNPC != null && _currentNPC.Health == 0)
            {
                MessagesNPC = $"{_currentNPC.Name} has fallen.";
                _currentLocation.LocationNPCs.Remove(_currentNPC);
            }
        }

        /// <summary>
        /// Player Speaking to a NPC:
        /// </summary>
        public void OnPlayerTalkTo()
        {
            if (CurrentNPC != null && CurrentNPC is Ispeak)
            {
                Ispeak speakingNpc = CurrentNPC as Ispeak;
                MessagesNPC = speakingNpc.Speak();
            }
            else if (CurrentNPC != null)
            {
                MessagesNPC = "This creature does not wish to speak with you or is incapable of such.";
            }
        }

        public void PlayerStanceAttack()
        {
            Player.Stance = Character.BattleStance.ATTACK;
            OnPlayerFight();
        }
        public void PlayerStanceDefend()
        {
            Player.Stance = Character.BattleStance.DEFEND;
            OnPlayerFight();
        }
        public void PlayerStanceFlee()
        {
            Player.Stance = Character.BattleStance.FLEE;
            OnPlayerFight();
        }


        /// <summary>
        /// Method that controls combat: 
        /// </summary>
        public void OnPlayerFight()
        {

            if (_currentNPC != null && _currentNPC is Ifight)
            {
                Ifight combatNPC = _currentNPC as Ifight;
                if (combatNPC.Stance == Character.BattleStance.NOFIGHT)
                {
                    CombatLog += "What are you doing? That one is a friend! Ease up a bit..." + "\n";
                }
                else
                {

                    switch (Player.Stance)
                    {
                        case Character.BattleStance.NOFIGHT:
                            break;
                        case Character.BattleStance.ATTACK:
                            CombatLog += $"You attack with your {Player.EquippedWeapon.ItemName}" + "\n";
                            break;
                        case Character.BattleStance.DEFEND:
                            CombatLog += $"You attempt to brace yourself agaisnt the foe with your {Player.EquippedArmor.ItemName} to protect you." + "\n";
                            break;
                        case Character.BattleStance.FLEE:
                            PlayerFlee();
                            break;
                        case Character.BattleStance.AUTOFIGHT:
                            break;
                        default:
                            break;
                    }
                    if (_player.Stance != Character.BattleStance.FLEE)
                    {

                        _player.SetStanceModifier(_player, combatNPC);
                        _currentNPC.Health = combatNPC.Health;

                        CheckIfNPCDie();

                        if (combatNPC != null)
                        {
                            BattleRounds++;
                            if (combatNPC is Monger && BattleRounds == 2)
                            {
                                combatNPC.Stance = Character.BattleStance.FLEE;
                                BattleRounds = 0;
                            }
                            switch (combatNPC.Stance)
                            {
                                case Character.BattleStance.NOFIGHT:
                                    break;
                                case Character.BattleStance.ATTACK:
                                    CombatLog += $"Your foe attacks you with their {combatNPC.EquippedWeapon.ItemName}" + "\n";
                                    break;
                                case Character.BattleStance.DEFEND:
                                    CombatLog += $"Your foe leverages their {combatNPC.EquippedArmor.ItemName} in attempt to deflects some of your attack." + "\n";
                                    break;
                                case Character.BattleStance.FLEE:
                                    NPCFlee();
                                    break;
                                case Character.BattleStance.AUTOFIGHT:
                                    break;
                                default:
                                    break;
                            }
                            if (combatNPC != null)
                            {
                                combatNPC.SetStanceModifier(combatNPC, _player);
                            }
                            _player.CheckPlayerHealthByCombat();
                        }
                    }
                }


            }





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
