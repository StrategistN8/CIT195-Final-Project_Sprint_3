using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class ItemArmor : GameItem
    {

        #region ENUMS

        #endregion

        #region FIELDS
        private ItemDamageType _armorResistanceType;
        private double _armorResistModifier;
        #endregion

        #region PROPERTIES
        public ItemDamageType ArmorResistanceType
        {
            get { return _armorResistanceType; }
            set { _armorResistanceType = value; }
        }
        public double ArmorResistModifier
        {
            get { return _armorResistModifier; }
            set { _armorResistModifier = value; }
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
        public ItemArmor(int id, int value, int uses, ItemUsability reusability, string name, string description, string onUseMessage, bool canBeSold, ItemDamageType damageResist, double resistanceModifier)
            : base (id, value, uses, reusability, name, description, onUseMessage, canBeSold)
        {
            ItemId = id;
            ItemValue = value;
            ItemNumberOfUses = uses;
            ItemReusability = reusability;
            ItemName = name;
            ItemDescription = description;
            ItemOnUseMessage = onUseMessage;
            CanBeSold = canBeSold;

            ArmorResistanceType = damageResist;
            ArmorResistModifier = resistanceModifier;
        }


        #endregion

        #region METHODS
        public override void GetItemOnUseEffect()
        {

        }
        #endregion
    }
}
