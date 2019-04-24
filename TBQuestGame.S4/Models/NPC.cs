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

        private ObservableCollection<GameItemQuantity> _inventory;
        private string  _description;
        private string _info;
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
        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        protected abstract string NPCInfo();

        #endregion


    }
}
