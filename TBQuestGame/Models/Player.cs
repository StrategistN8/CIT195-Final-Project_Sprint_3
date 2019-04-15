using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Player : Character
    {

        #region ENUMS

        public enum PlayerGender
        { Male,
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
        private PlayerGender _gender;
        private PlayerRole _role;

        public PlayerRole Role
        {
            get { return _role; }
            set { _role = value; }
        }


        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public PlayerGender Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {

        }

        #endregion

        #region METHODS

        #endregion

        #region EVENTS



        #endregion

    }
}
