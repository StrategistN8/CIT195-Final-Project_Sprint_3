using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame.Models;
using System.Collections.ObjectModel;

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
                "\n\t You had been traveling as part of a carvan heading towards the capital of Eron to trade. Due to tensions with the neighboring Umene tribes" +
                " the cavan elders decided to detour through the Monger Woods.  Named for the legendary beasts thought to dwell in the depths of the boggy forest, the area has long held a dread reputation. " +
                "\n\n\tOn the fifth day of travel, the caravan fell under attack by a ferocious monstrocity.  During the fighting you were knocked out and buried beneath the wreckage.  Now, you must find out what happened. " +
                "\n\n Good luck to you and beware the Monger.... "
            };
        }

        ///// <summary>
        ///// Instantiates the initial starting location.
        ///// </summary>
        ///// <returns></returns>
        //public static Location InitialLocation()
        //{
        //    return new Location()
        //    {

        //        LocationId = 1,
        //        Name = "Umbruk's Wagon",
        //        Description = "The smashed wreckage of Umbruk's wagon.  " +
        //        "The wagon was stuck by a large moss-caked rock dragged from the woods " +
        //        "and hurled by the Monger. Now all that remains is little better than matchwood and a few scattered coins.",
        //        IsAccessible = true,
        //        XPModifier = 0

        //    };

        //}

        public static Map GameMap()
        {
            Map caravanMap = new Map();
            caravanMap.MapLocations.Add(

                // Monger Lair
                new Location()
                {

                    LocationId = 13,
                    Name = "Baradow Barrows",
                    Description = "Baradow Barrows is thought to be a burial site built by the original Nivinite peoples before the great war." +
                  "One of the tombs lies wrenched open, doubtless serving as the Monger's Lair...",
                    IsAccessible = false,
                    XPModifier = 50,
                    RequiredXP = 200,
                    LocationMessage = "",
                    LocationIconDataPath = "../Art/Forest.jpg",
                    HealthModifier = -10
                }
                );

            // Umbruk's Wagon: Starting Area
            caravanMap.MapLocations.Add(
            new Location()
            {

                LocationId = 1,
                Name = "Umbruk's Wagon",
                Description = "The smashed wreckage of Umbruk's wagon.  " +
                    "The wagon was stuck by a large moss-caked rock dragged from the woods " +
                    "and hurled by the Monger. Now all that remains is little better than matchwood and a few scattered coins.",
                IsAccessible = true,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/SmashedWagon.png",
                HealthModifier = 0
            }
            );

            // Apothecary Wagon: Healing and Quest
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 2,
                Name = "Apothecary Wagon",
                Description = "The wagon carrying the caravan's apothecary suffered only minor damage during the Monger attack, with " +
                "one wheel being wrenched free by the monster along with some planks of wood siding, nothing that can't be fixed.  The staff is a bit shaken still, " +
                "but have been doing their best to tend to the wounded.",
                IsAccessible = true,
                XPModifier = 10,
                LocationMessage = "The apothercary checks you over for injuries and does what they can. As the medicine takes effect, you feel better.",
                LocationIconDataPath = "../Art/SmashedWagon.png",
                HealthModifier = 25
            }
            );

            // Keeper's Wagon: Quest and Trade
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 3,
                Name = "The Keeper's Wagon",
                Description = "The shadowy creature known as 'Keeper' maintains a ranshakle wagon on the outskirts of the carvan. No one " +
                "knows exactly what Keeper is, but their reclusiveness seems to have proven a boon during the attack as their wagon remains more or less intact.",
                IsAccessible = true,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/Forest.jpg",
                HealthModifier = 0
            }
            );

            // Thalma's Tent
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 4,
                Name = "Thalma's Tent",
                Description = "Thalma is a Primal shepherdess who was accompanying the carvan. Her tent was pitched on the outskirts of the carvan when " +
                "the Monger struck, saving it from the brunt of the attack. The same can not be said of her sled, which was one of the things reduced to kindling by the monster...",
                IsAccessible = true,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/Forest.jpg",
                HealthModifier = 0

            }
            );

            // Forest Edge
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 5,
                Name = "Forest Edge",
                Description = "The gloomy edge of the Monger Woods. The trees here are younger and spindly than the ancient behemoths found further in.",
                IsAccessible = false,
                RequiredXP = 40,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/Forest.jpg",
                HealthModifier = 0

            } 
            );

            // Morleen Fens
            caravanMap.MapLocations.Add(
           new Location()
           {
               LocationId = 6,
               Name = "Morleen Fens",
               Description = "The fetid waters of the Morleen Fens border the Monger Woods. Magical runoff from the experiments of the Nivinites and Umene witch folk have polluted the marshlands, occationally with lethal results for the unwary.",
               IsAccessible = false,
               RequiredXP = 50,
               XPModifier = 20,
               LocationMessage = "",
               LocationIconDataPath = "../Art/Forest.jpg",
               HealthModifier = -10

           }
           );
            caravanMap.CurrentMapLocation = caravanMap.MapLocations.FirstOrDefault(l => l.LocationId == 1);

            return caravanMap;
        }





    }
}

