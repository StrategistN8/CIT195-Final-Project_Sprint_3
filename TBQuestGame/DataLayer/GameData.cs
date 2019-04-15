using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;

namespace TBQuestGame.DataLayer
{
   public class GameData
    {
        /// <summary>
        /// Instantiates a player object for the game. 
        /// </summary>
        /// <returns></returns>
        public static Player PlayerData()
        {
            // Default Player Character: 
            return new Player()
            {
                Id = 1,
                Name = "Arcus",
                Age = 23,
                Gender = Player.PlayerGender.Male,
                Race = Character.RaceType.Eronite,
                Health = 100,
                Lives = 3,
                ExperiencePoints = 0,
                LocationId = 0
            };
        }

        /// <summary>
        /// Displays the initial story messages when the game launches.
        /// </summary>
        /// <returns></returns>
        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "\t Beware the Monger.... " 
            };
        }

    }
}
