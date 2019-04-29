using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Monger : Fighter, Ifight
    {

        #region FIELDS

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public Monger(int id, int hp, string name, string description, ItemArmor armor, ItemWeapon weapon, RaceType racetype, BattleStance initialStance, string imgDataPath)
            : base( id, hp, name, description, armor, weapon, racetype, initialStance, imgDataPath)
        {
            _id = id;
            _name = name;
            _description = description;
            _race = RaceType.Monger; // Has to be a Monger - silly to be anything else. :P
            Stance = initialStance;
            _imageDataPath = imgDataPath;
            _health = hp;
            EquippedArmor = armor;
            EquippedWeapon = weapon;

        }

        

        #endregion

        #region METHODS
        protected override string NPCInfo()
        {
            return "";
        }
        #endregion


    }
}
