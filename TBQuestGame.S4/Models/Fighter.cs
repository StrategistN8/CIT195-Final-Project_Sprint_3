using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.PresentationLayer;

namespace TBQuestGame.Models
{
   public class Fighter : NPC, Ifight
    {
        #region PROPERTIES
        public GameItem EquippedWeapon { get; set; }
        public GameItem EquippedArmor { get; set; }
        public BattleStance Stance { get; set; }
        #endregion

        #region CONSTRUCTORS

        public Fighter(int id, int hp, string name, string description, ItemArmor armor, ItemWeapon weapon, RaceType racetype, BattleStance initialStance, string imgDataPath)
            : base(id, hp, name, description, racetype, initialStance, imgDataPath)
        {
            _id = id;
            _name = name;
            _description = description;
            _race = racetype;
            Stance = initialStance;
            _imageDataPath = imgDataPath;
            _health = hp;
            EquippedWeapon = weapon;
            EquippedArmor = armor;

        }

        #endregion


        #region METHODS

        /// <summary>
        /// Controls battle based on current stance.
        /// </summary>
        /// <param name="Stance"></param>
        public void SetStanceModifier(Ifight attacker, Ifight defender)
        {
            ItemWeapon attackerWeapon = attacker.EquippedWeapon as ItemWeapon;
            ItemArmor attackerArmor = attacker.EquippedArmor as ItemArmor;
            ItemArmor defenderArmor = attacker.EquippedArmor as ItemArmor;

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
                    damage = 0;
                    break;
                default:
                    break;
            }

            Player defenderHealth = defender as Player;

            healthModifier = (int)damage;

            defenderHealth.Health = -healthModifier;




        }

        /// <summary>
        /// Method that adjusts health based on battle damage.
        /// </summary>
        /// <param name="currentLocation"></param>
        public void CheckNPCHealthByCombat(int damage)
        {
            if (Health > 1 && damage > 0)
            {
                Health -= damage;
                if (Health <= 0 )
                {
                    Health = 0;
                }

            }
        }


        protected override string NPCInfo()
        {
            return $"This {Race} is wearing {EquippedArmor.ItemName} and wields a {EquippedWeapon.ItemName}.";
        }
        #endregion
    }
}
