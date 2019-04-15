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
        private List<Location> _locationsVisited;

  
        #endregion

        #region PROPERTIES

        public int Lives
        {
            get { return _lives; }
            set
            {
              _lives = value;
                OnPropertyChanged(nameof(Lives));
            }
        }

        public PlayerGender Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
                OnPropertyChanged(nameof(Gender)); // I have no idea when this will be used.
            }
        }

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }

        public PlayerRole Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public List<Location> LocationsVisited
        {
            get { return _locationsVisited; }
            set { _locationsVisited = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public Player()
        {
             _locationsVisited = new List<Location>();
    }

        #endregion

        #region METHODS

        static void PlayerHealSelf (Location currentLocation)
        {
            if (currentLocation.HealthModifier > 0)
            {
                
            }
        }

        #endregion

        #region EVENTS



        #endregion

    }
}
