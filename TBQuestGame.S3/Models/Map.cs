﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestGame.Models
{
    public class Map
    {

        #region FIELDS

        private List<Location> _mapLocations;
        private List<GameItemQuantity> _standardGameItems;
        private Location _currentMapLocation;
        private ObservableCollection<Location> _accessibleMapLocations;
        #endregion

        #region PROPERTIES
        public List<Location> MapLocations
        {
            get { return _mapLocations; }
            set { _mapLocations = value; }
        }
        public List<GameItemQuantity> StandardGameItems
        {
            get { return _standardGameItems; }
            set { _standardGameItems = value; }
        }
        public Location CurrentMapLocation
        {
            get { return _currentMapLocation; }
            set { _currentMapLocation = value; }
        }
        public ObservableCollection<Location> AccessibleMapLocations
        {
            get
            {
                _accessibleMapLocations = new ObservableCollection<Location>();
                foreach (Location location in _mapLocations)
                {
                    if (location.IsAccessible == true)
                    {
                        _accessibleMapLocations.Add(location);
                    }
                }
                return _accessibleMapLocations;
            }
            set { _accessibleMapLocations = value; }
        }
        #endregion

        #region CONSTRUCTORS
        public Map()
        {
            _mapLocations = new List<Location>();
            _standardGameItems = new List<GameItemQuantity>();
        }

        #endregion

        #region METHODS

        public void Move(Location location)
        {
            _currentMapLocation = location;
        }

        public bool CanMove(Location location)
        {
            return location.IsAccessible;
        }
        
        //todo: impliment a method to unlock locations based on items.
        
        #endregion

    }
}
