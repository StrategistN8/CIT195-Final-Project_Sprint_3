using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class Location
    {

        #region ENUMS

        #endregion

        #region FIELDS
        private int _locationId;
        private int _xpModifier;
        private int _healthModifier;
        private int _requiredXP;
        private string _description;
        private string _name;
        private string _locationMessage;
        private string _locationIconDataPath;
        private bool _isAccessible;

        #endregion

        #region PROPERTIES

        public int LocationId
        {
            get { return _locationId; }
            set { _locationId = value; }
        }
        public int XPModifier
        {
            get { return _xpModifier; }
            set { _xpModifier = value; }
        }
        public int HealthModifier
        {
            get { return _healthModifier; }
            set { _healthModifier = value; }
        }
        public int RequiredXP
        {
            get { return _requiredXP; }
            set { _requiredXP = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string LocationMessage
        {
            get { return _locationMessage; }
            set { _locationMessage = value; }
        }
        public bool IsAccessible
        {
            get { return _isAccessible; }
            set { _isAccessible = value; }
        }
        public string LocationIconDataPath
        {
            get { return _locationIconDataPath; }
            set { _locationIconDataPath = value; }
        }

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Default Constructor:
        /// </summary>
        public Location()
        {

        }
        #endregion

        #region METHODS
        /// <summary>
        /// Temporary until I find a better solution:
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _name;
        }


        #endregion

        #region EVENTS

        #endregion

    }
}
