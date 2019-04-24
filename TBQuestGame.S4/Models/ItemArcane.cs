using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class ItemArcane : GameItem
    {
        #region ENUMS

        public enum ArcaneEffect
        {
            HEALPLAYER,
            SHIELDPLAYER,
            SPELL,
            STOPMONGER,
            UNLOCKREGION,
        }

        #endregion

        #region FIELDS


        #endregion

        #region PROPERTIES
        public ArcaneEffect ItemArcaneEffect { get; set; }
        public int HealthModifier { get; set; }
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
        public ItemArcane(int id, int value, int uses, ItemUsability reusability, string name, string description, string onUseMessage, bool canBeSold, ArcaneEffect arcaneEffect, int healthModifier
            )
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
            ItemArcaneEffect = arcaneEffect;

        }


        #endregion

        #region METHODS

        public override void GetItemOnUseEffect()
        {
            //Placeholder
        }

        public void GetArcaneItemOnUseEffect(Player player, ItemArcane item)
        {
            switch (ItemArcaneEffect)
            {
                case ArcaneEffect.HEALPLAYER:
                    player.Health += item.HealthModifier;
                    break;
                case ArcaneEffect.SHIELDPLAYER:
                    break;
                case ArcaneEffect.SPELL:
                    break;
                case ArcaneEffect.STOPMONGER:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }

}
