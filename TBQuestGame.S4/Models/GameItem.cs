using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public abstract class GameItem : ObservableObject
    {
        #region ENUMS  
        public enum ItemUsability
        {
            ONCE, 
            MULTIPLE,
            LIMITED,
        }
        public enum ItemDamageType
        {
            SLASHING,
            BLUDGONING,
            PIERCING,
            TOXIC,
            ARCANE
        }
        public enum ItemDamageResist
        {
            SLASHING,
            BLUDGONING,
            PIERCING,
            TOXIC,
            ARCANE
        }
        #endregion

        #region FIELDS

        private int _itemId;
        private int _itemValue;
        private int _itemNumberOfUses;
        private string _itemName;
        private string _itemDescription;
        private string _itemOnUseMessage;
        private ItemUsability _ItemReusability;
        private bool _canBeSold;
        #endregion

        #region PROPERTIES      
        public bool CanBeSold
        {
            get { return _canBeSold; }
            set { _canBeSold = value; }
        }
        public ItemUsability ItemReusability
        {
            get { return _ItemReusability; }
            set { _ItemReusability = value; }
        }
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }
        public string ItemDescription
        {
            get { return _itemDescription; }
            set { _itemDescription = value; }
        }
        public string ItemOnUseMessage
        {
            get { return _itemOnUseMessage; }
            set { _itemOnUseMessage = value; }
        }
        public int ItemId
        {
            get { return _itemId; }
            set { _itemId = value; }
        }
        public int ItemNumberOfUses
        {
            get { return _itemNumberOfUses; }
            set { _itemNumberOfUses = value; }
        }
        public int ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="uses"></param>
        /// <param name="reusability"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="onUseMessage"></param>
        /// <param name="canBeSold"></param>
        public GameItem(int id, int value, int uses, ItemUsability reusability, string name, string description, string onUseMessage, bool canBeSold)
        {
            ItemId = id;
            ItemValue = value;
            ItemNumberOfUses = uses;
            ItemReusability = reusability;
            ItemName = name;
            ItemDescription = description;
            ItemOnUseMessage = onUseMessage;
            CanBeSold = canBeSold;
        }
        #endregion

        #region METHODS
        public abstract void GetItemOnUseEffect();
        #endregion
    }

}