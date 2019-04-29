using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class ItemWeapon : GameItem
    {
        #region ENUMS

        #endregion

        #region FIELDS


        #endregion

        #region PROPERTIES

        public ItemDamageType WeaponDamageType { get; set; }
        public double WeaponDamageAmount { get; set; }

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
        public ItemWeapon(int id, int value, int uses, double weaponDamageAmount, ItemUsability reusability, ItemDamageType weaponDamageType, string name, string description, string onUseMessage, bool canBeSold)
            : base(id, value, uses, reusability, name, description, onUseMessage, canBeSold)
        {
            ItemId = id;
            ItemValue = value;
            ItemNumberOfUses = uses;
            ItemReusability = reusability;
            ItemName = name;
            ItemDescription = description;
            ItemOnUseMessage = onUseMessage;
            CanBeSold = canBeSold;

            WeaponDamageType = weaponDamageType;
            WeaponDamageAmount = weaponDamageAmount;

        }


        #endregion

        #region METHODS
        public override void GetItemOnUseEffect()
        {

        }
        #endregion
    }
}
