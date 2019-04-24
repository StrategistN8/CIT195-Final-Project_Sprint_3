using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class ItemInventory : GameItem
    {
        #region ENUMS

        #endregion

        #region FIELDS


        #endregion

        #region PROPERTIES

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
        public ItemInventory(int id, int value, int uses, ItemUsability reusability, string name, string description, string onUseMessage, bool canBeSold)
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
        }


        #endregion

        #region METHODS
        public override void GetItemOnUseEffect()
        {

        }
        #endregion
    }
}
