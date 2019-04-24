using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestGame.Models
{
    public class Location
    {

        #region ENUMS

        public enum LocationDamageType
        {
            SLASHING,
            PIERCING,
            BLUDGONING,
            CORROSIVE,
            TOXIC,
            INCINDIARY,
            ARCANE
        }
        #endregion

        #region FIELDS
        private int _locationId;
        private int _xpModifier;
        private int _healthModifier;
        private int _requiredXP;
        private int _locationKeyItemId;
        private string _description;
        private string _name;
        private string _locationMessage;
        private string _locationIconDataPath;
        private bool _isAccessible;
        private ObservableCollection<GameItemQuantity> _locationInventory;
        private ObservableCollection<NPC> _locationNPCs;

        #endregion

        #region PROPERTIES
        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        public int XPModifier
        {
            get { return _xpModifier; }
            set { _xpModifier = value; }
        }
        public int HealthModifier
        {
            get { return _healthModifier; }
            set { _healthModifier = value; }
        }
        public int RequiredXP
        {
            get { return _requiredXP; }
            set { _requiredXP = value; }
        }
        public int LocationKeyItemId
        {
            get { return _locationKeyItemId; }
            set { _locationKeyItemId = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string LocationMessage
        {
            get { return _locationMessage; }
            set { _locationMessage = value; }
        }
        public bool IsAccessible
        {
            get { return _isAccessible; }
            set { _isAccessible = value; }
        }
        public string LocationIconDataPath
        {
            get { return _locationIconDataPath; }
            set { _locationIconDataPath = value; }
        }
        public ObservableCollection<GameItemQuantity> LocationInventory
        {
            get { return _locationInventory; }
            set { _locationInventory = value; }
        }
        public ObservableCollection<NPC> LocationNPCs
        {
            get { return _locationNPCs; }
            set { _locationNPCs = value; }
        }
        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Default Constructor:
        /// </summary>
        public Location()
        {
            _locationInventory = new ObservableCollection<GameItemQuantity>();
            _locationNPCs = new ObservableCollection<NPC>();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Temporary until I find a better solution:
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Method that adds an item to the inventory system:
        /// </summary>
        /// <param name="selectedGameItemQuantity"></param>
        public void AddGameItemToInventory(GameItemQuantity selectedGameItemQuantity)
        {
            GameItemQuantity gameItemQuantity = _locationInventory.FirstOrDefault(i => i.GameItem.ItemId == selectedGameItemQuantity.GameItem.ItemId);

            // The item isn't in the inventory yet, so we need to add it as a new quantity.
            if (gameItemQuantity == null)
            {
                GameItemQuantity newgameItemQuantity = new GameItemQuantity();
                newgameItemQuantity.GameItem = selectedGameItemQuantity.GameItem;
                newgameItemQuantity.Quantity = 1;

                _locationInventory.Add(newgameItemQuantity);
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
            GameItemQuantity gameItemQuantity = _locationInventory.FirstOrDefault(i => i.GameItem.ItemId == selectedGameItemQuantity.GameItem.ItemId);

            // The item isn't in the inventory yet, so we need to add it as a new quantity.
            if (gameItemQuantity != null)
            {

                if (gameItemQuantity.Quantity == 1)
                {
                    _locationInventory.Remove(gameItemQuantity);
                }
                else
                {
                    gameItemQuantity.Quantity--;
                }
            }

            UpdateInventory();
        }

        /// <summary>
        /// Updates the location's inventory:
        /// </summary>
        public void UpdateInventory()
        {
            ObservableCollection<GameItemQuantity> updatedInventory = new ObservableCollection<GameItemQuantity>();

            foreach (var item in _locationInventory)
            {
                updatedInventory.Add(item);
            }

            _locationInventory.Clear();

            foreach (var item in updatedInventory)
            {
                _locationInventory.Add(item);
            }

        }

        #endregion

        #region EVENTS

        #endregion

    }
}
