using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;



namespace TBQuestGame.Models
{
   public abstract class NPC : Character
    {
        #region ENUM

        #endregion

        #region FIELDS

        protected ObservableCollection<GameItemQuantity> _inventory;
        protected int _health;
        protected string  _description;
        protected string _info;
        protected string _imageDataPath;

        #endregion

        #region PROPERTIES

        public ObservableCollection<GameItemQuantity> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public string Info
        {
            get { return NPCInfo(); }
            set { }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string ImageDataPath
        {
            get { return _imageDataPath; }
            set { _imageDataPath = value; }
        }
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }
        #endregion

        #region CONSTRUCTORS

        public NPC(int id, int hp, string name, string description, RaceType race, BattleStance initialStance, string imgDataPath)
            : base(id, name, race)
        {
            _name = name;
            _description = description;
            _race = race;
            _inventory = new ObservableCollection<GameItemQuantity>();
            _imageDataPath = imgDataPath;
            _health = hp;
        }

        #endregion

        #region METHODS

        protected abstract string NPCInfo();
        
        /// <summary>
        /// Temporary until I find a better solution:
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }

        #endregion


    }
}
