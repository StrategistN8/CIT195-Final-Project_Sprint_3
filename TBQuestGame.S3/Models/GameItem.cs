using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    /// <summary>
    /// I lost a lot of data in here halfway through rebuilding everything. It is a bit messy
    /// due to the time crunch. I will refactor to something tidier when I have time.
    /// </summary>
    public abstract class GameItem : ObservableObject
    {
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

        private int _itemId;
        private int _itemValue;
        private int _itemNumberOfUses;
        private string _itemName;
        private string _itemDescription;
        private string _itemOnUseMessage;
        private ItemUsability _ItemReusability;
        private bool _canBeSold;

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


        public abstract void GetItemOnUseEffect();
    }

}