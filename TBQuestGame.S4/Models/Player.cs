using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TBQuestGame.DataLayer;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
    public class Player : Character, Ifight, Itrade
    {

        #region ENUMS

        public enum PlayerGender
        {
            Male,
            Female
        }

        public enum PlayerRole
        {
            Guard,
            Merchant,
            Explorer

        }
        #endregion

        #region FIELDS

        private int _lives;
        private int _health;
        private int _experiencePoints;
        private int _wealth;
        private PlayerGender _gender;
        private PlayerRole _role;
        private GameItem _equippedWeapon = GameData.GetGameItemById(0002);
        private GameItem _equippedArmor = GameData.GetGameItemById(0001);
        private List<Location> _locationsVisited;
        private ObservableCollection<GameItemQuantity> _inventory;
        private ObservableCollection<GameItemQuantity> _arcaneItems;
        private ObservableCollection<GameItemQuantity> _armor;
        private ObservableCollection<GameItemQuantity> _weapons;
        private ObservableCollection<GameItemQuantity> _inventoryItem;

        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set
            {
                _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }
        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Wealth
        {
            get { return _wealth; }
            set
            {
                _wealth = value;
                OnPropertyChanged(nameof(Wealth));
            }
        }
        public PlayerGender Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender)); // I have no idea when this will be used.
            }
        }
        public PlayerRole Role
        {
            get { return _role; }
            set { _role = value; }
        }
        public GameItem EquippedWeapon
        {
            get { return _equippedWeapon; }
            set { _equippedWeapon = value; }
        }
        public GameItem EquippedArmor
        {
            get { return _equippedArmor; }
            set { _equippedArmor = value; }
        }
        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }
        public ObservableCollection<GameItemQuantity> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public ObservableCollection<GameItemQuantity> InventoryItem
        {
            get { return _inventoryItem; }
            set { _inventoryItem = value; }
        }
        public ObservableCollection<GameItemQuantity> Weapons
        {
            get { return _weapons; }
            set { _weapons = value; }
        }
        public ObservableCollection<GameItemQuantity> Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }
        public ObservableCollection<GameItemQuantity> ArcaneItems
        {
            get { return _arcaneItems; }
            set { _arcaneItems = value; }
        }
        public BattleStance Stance { get; set; }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
            _locationsVisited = new List<Location>();
            _inventory = new ObservableCollection<GameItemQuantity>();
            _arcaneItems = new ObservableCollection<GameItemQuantity>();
            _armor = new ObservableCollection<GameItemQuantity>();
            _weapons = new ObservableCollection<GameItemQuantity>();
            _inventoryItem = new ObservableCollection<GameItemQuantity>();
        }

        #endregion

        #region METHODS

        #region HEALTH METHODS

        /// <summary>
        /// Method that adjusts player health based on location.
        /// </summary>
        /// <param name="currentLocation"></param>
        public void CheckPlayerHealthByCombat()
        {
            if (Health <= 0 && Lives > 0)
            {
                Health = 100;
                Lives--;
            }
            if (Health == 0 && Lives == 0)
            {
                GameViewModel.OnPlayerDie("You have fallen.");
            }
        

        }

        /// <summary>
        /// Method that adjusts player health based on location.
        /// </summary>
        /// <param name="currentLocation"></param>
        public void CheckPlayerHealthByLocation(Location currentLocation)
        {
            if (Health < 100 && currentLocation.HealthModifier > 0)
            {
                Health += currentLocation.HealthModifier;
                if (Health > 100)
                {
                    Health = 100;
                    //_player.Lives++;
                }
            }

            if (currentLocation.HealthModifier != 0 && currentLocation.HealthModifier < 0)
            {
                Health += currentLocation.HealthModifier;
                if (Health == 0)
                {
                    Health = 100;
                    Lives--;

                    if (Health == 0 && Lives == 0)
                    {
                        GameViewModel.OnPlayerDie("You have fallen.");
                    }
                }

            }
        }

        /// <summary>
        /// Method that updates player health on using an item:
        /// </summary>
        /// <param name="arcaneItem"></param>
        public void CheckPlayerHealthByItem(ItemArcane arcaneItem)
        {
            if (Health < 100 && arcaneItem.HealthModifier > 0)
            {
                Health += arcaneItem.HealthModifier;
                if (Health > 100)
                {
                    Health = 100;
                    //_player.Lives++;
                }
            }

            if (arcaneItem.HealthModifier != 0 && arcaneItem.HealthModifier < 0)
            {
                Health += arcaneItem.HealthModifier;
                if (Health == 0)
                {
                    Health = 100;
                    Lives--;
                }

            }
        }
        #endregion

        #region INVENTORY METHODS

        /// <summary>
        /// Method that adds an item to the inventory system:
        /// </summary>
        /// <param name="selectedGameItemQuantity"></param>
        public void AddGameItemToInventory(GameItemQuantity selectedGameItemQuantity)
        {
            GameItemQuantity gameItemQuantity = _inventory.FirstOrDefault(i => i.GameItem.ItemId == selectedGameItemQuantity.GameItem.ItemId);

            // The item isn't in the inventory yet, so we need to add it as a new quantity.
            if (gameItemQuantity == null)
            {
                GameItemQuantity newgameItemQuantity = new GameItemQuantity();
                newgameItemQuantity.GameItem = selectedGameItemQuantity.GameItem;
                newgameItemQuantity.Quantity = 1;

                _inventory.Add(newgameItemQuantity);
            }
            else
            {
                gameItemQuantity.Quantity++;
            }

            UpdateInventory();
        }

        /// <summary>
        /// Method that removes an item from the inventory system.
        /// </summary>
        /// <param name="selectedGameItemQuantity"></param>
        public void RemoveGameItemFromInventory(GameItemQuantity selectedGameItemQuantity)
        {
            GameItemQuantity gameItemQuantity = _inventory.FirstOrDefault(i => i.GameItem.ItemId == selectedGameItemQuantity.GameItem.ItemId);

            // The item isn't in the inventory yet, so we need to add it as a new quantity.
            if (gameItemQuantity != null)
            {
                // Clearing the equipped weapon or armor if the item dropped was the only one.
                if (gameItemQuantity.Quantity == 1)
                {
                    if (gameItemQuantity.GameItem.ItemId == _equippedWeapon.ItemId)
                    {
                        _equippedWeapon = GameData.GetGameItemById(0002);
                    }
                    if (gameItemQuantity.GameItem.ItemId == _equippedArmor.ItemId)
                    {
                        _equippedArmor = GameData.GetGameItemById(0001);
                    }
                    _inventory.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateInventory();
        }

        /// <summary>
        /// Updates the player inventory:
        /// </summary>
        public void UpdateInventory()
        {
            Armor.Clear();
            ArcaneItems.Clear();
            InventoryItem.Clear();
            Weapons.Clear();

            foreach (var item in _inventory)
            {
                if (item.GameItem is ItemArmor) Armor.Add(item);
                if (item.GameItem is ItemArcane) ArcaneItems.Add(item);
                if (item.GameItem is ItemWeapon) Weapons.Add(item);
                if (item.GameItem is ItemInventory) InventoryItem.Add(item);
            }

            CalculateInventoryValue();
        }
        //Helper method that calculates the player's wealth based on the value of what they are carrying.
        public void CalculateInventoryValue()
        {
            Wealth = 0;

            foreach (var item in _inventory)
            {
                Wealth += (item.GameItem.ItemValue * item.Quantity);
            }
        }
        #endregion

        /// <summary>
        /// Controls battle based on current stance.
        /// </summary>
        /// <param name="Stance"></param>
        public void SetStanceModifier(Ifight attacker, Ifight defender)
        {
            ItemWeapon attackerWeapon = attacker.EquippedWeapon as ItemWeapon;
            ItemArmor attackerArmor = attacker.EquippedArmor as ItemArmor;
            ItemArmor defenderArmor = defender.EquippedArmor as ItemArmor;

            double damage = 0;
            int healthModifier;

            switch (Stance)
            {
                case BattleStance.ATTACK:
                    if (attackerWeapon.WeaponDamageType == defenderArmor.ArmorResistanceType)
                    {
                        damage = Math.Round(attackerWeapon.WeaponDamageAmount * defenderArmor.ArmorResistModifier);
                    }
                    else
                    {
                        damage = attackerWeapon.WeaponDamageAmount;
                    }
                    break;
                case BattleStance.DEFEND:
                    damage = 0;
                    break;
                case BattleStance.FLEE:
                    //GameViewModel.OnPlayerFlee();
                    damage = 0;
                    break;
                default:
                    break;
            }

            NPC defenderHealth = defender as NPC;

            healthModifier = (int)damage;

            defenderHealth.Health = defenderHealth.Health - healthModifier;
            // Temporary: To be moved to a method in the Fighter NPC
            switch (healthModifier)
            {
                case 0:
                    defender.Stance = BattleStance.ATTACK;
                    break;
                case 35:
                    defender.Stance = BattleStance.DEFEND;
                    break;
                case 50:
                    defender.Stance = BattleStance.FLEE;
                    break;
                default:
                    break;
            }

        }


        #endregion

        #region EVENTS



        #endregion

    }
}
