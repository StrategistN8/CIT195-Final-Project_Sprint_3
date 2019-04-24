using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;



namespace TBQuestGame.Models
{
   public class NPC : Character
    {
        #region ENUM

        public enum PlayerTolerance
        {
            FRIENDLY,
            INDIFFERENT,
            HOSTILE,
        }
        #endregion

        #region FIELDS
        private PlayerTolerance _tolerance;
        private ObservableCollection<GameItemQuantity> _inventory;

        #endregion

        #region PROPERTIES

        public ObservableCollection<GameItemQuantity> Inventory
        {
            get { return _inventory; }
            set { _inventory = value; }
        }
        public PlayerTolerance Tolerance
        {
            get { return _tolerance; }
            set { _tolerance = value; }
        }
        #endregion

        #region CONSTRUCTORS

        #endregion

        #region METHODS

        #endregion


    }
}
