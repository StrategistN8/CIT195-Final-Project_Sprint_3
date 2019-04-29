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
                LocationId = 0,
                Inventory = new ObservableCollection<GameItemQuantity>()
                {
                    new GameItemQuantity(GetGameItemById(2001), 1),
                    new GameItemQuantity(GetGameItemById(3002), 1),

                }
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

        /// <summary>
        /// initializes the game map:
        /// </summary>
        /// <returns></returns>
        public static Map GameMap()
        {
            Map caravanMap = new Map();
            caravanMap.MapLocations.Add(

            // Monger Lair
            new Location()
                {

                    LocationId = 13,
                    Name = "Baradow Barrows",
                    Description = "Baradow Barrows is thought to be a burial site built by the original Nivinite peoples before the great war that decimated their society." +
                    "One of the tombs lies wrenched open, doubtless serving as the Monger's Lair...",
                    IsAccessible = false,
                    XPModifier = 50,
                    RequiredXP = 200,
                    LocationKeyItemId = 4000,
                    LocationMessage = "",
                    LocationIconDataPath = "../Art/Forest.jpg",
                    HealthModifier = -10,
                    LocationInventory = new ObservableCollection<GameItemQuantity>
                    {
                        new GameItemQuantity(GetGameItemById(1000), 15),
                        //new GameItemQuantity(GetGameItemById(4000),1)
                    }
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
                HealthModifier = 0,
                LocationNPCs = new ObservableCollection<NPC>
                {
                   //GetNPCById(1)
                },
                LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(1000), 3),
                    new GameItemQuantity(GetGameItemById(1001),1),
                    new GameItemQuantity(GetGameItemById(3002),1)
                }
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
                HealthModifier = 25,
                LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(1000), 5),
                    new GameItemQuantity(GetGameItemById(2001),1),
                }
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
                HealthModifier = 0,
                LocationNPCs = new ObservableCollection<NPC>{
                    GetNPCById(2)
                },
                LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(1000), 1),
                    new GameItemQuantity(GetGameItemById(1001),2),
                    new GameItemQuantity(GetGameItemById(2000),1)
                }
            }
            );

            // Thalma's Tent
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 4,
                Name = "Primal Encampment",
                Description = "A clan of Primal shepherds had been accompanying the cavan to the capital for trade. The Monger attacked while the Primal were out tending to their flocks, allowing to to ramage unopposed through the camp before turning to the caravano was accompanying the carvan." +
                "Most of the camp has been picked up and several Primal have left to help the Eronites at the caravan, save for a few guards.",
                IsAccessible = true,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/Forest.jpg",
                HealthModifier = 0,
                LocationNPCs = new ObservableCollection<NPC>{
                    GameData.GetNPCById(3),
                    GameData.GetNPCById(4),
                    GameData.GetNPCById(5),

                },
                LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(3001), 3),
                    new GameItemQuantity(GetGameItemById(4001),1),

                }

            }
            );

            // Forest Edge
            caravanMap.MapLocations.Add(
            new Location()
            {
                LocationId = 5,
                Name = "Forest Edge",
                Description = "The gloomy edge of the Monger Woods. The trees here are younger and more gaunt than the ancient behemoths found further in. For those willing to brave the tangled underbrush there may be things to find here.",
                IsAccessible = false,
                RequiredXP = 40,
                XPModifier = 10,
                LocationMessage = "",
                LocationIconDataPath = "../Art/Forest.jpg",
                HealthModifier = 0,
                LocationNPCs = new ObservableCollection<NPC>
                {

                },
                LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(1000),3),
                    new GameItemQuantity(GetGameItemById(1001),3),
                }

            }
            );

            // Morleen Fens
            caravanMap.MapLocations.Add(
           new Location()
           {
               LocationId = 6,
               Name = "Morleen Fens",
               Description = "The fetid waters of the Morleen Fens border the Monger Woods. Magical runoff from the experiments of the Nivinites and Umene witch folk have polluted the marshlands, with occationally lethal results for the unwary.",
               IsAccessible = false,
               RequiredXP = 50,
               XPModifier = 20,
               LocationMessage = "",
               LocationIconDataPath = "../Art/Forest.jpg",
               HealthModifier = -10,
               LocationInventory = new ObservableCollection<GameItemQuantity>
                {
                    new GameItemQuantity(GetGameItemById(1000), 3),
                    new GameItemQuantity(GetGameItemById(2001),1),
                    new GameItemQuantity(GetGameItemById(4000),1)
                }
           }
           );
            caravanMap.CurrentMapLocation = caravanMap.MapLocations.FirstOrDefault(l => l.LocationId == 1);

            return caravanMap;
        }

        /// <summary>
        /// Initializes Game Item List
        /// </summary>
        /// <returns></returns>
        public static List<GameItem> StandardGameItems()
        {
            return new List<GameItem>()
            {
                new ItemArcane(4000,0,1,GameItem.ItemUsability.MULTIPLE,"Monger Stone", "An inky-black gemstone. It feels cold to the touch.", "The stone feels warm and glows red for a moment, then fades.", false, ItemArcane.ArcaneEffect.STOPMONGER, 0),
                new ItemArcane(4001, 1000, 5, GameItem.ItemUsability.LIMITED, "Whistle of the Forestborne", "An ornate wooden whistle decorated with Primal runes and symbols. Those who use it might find their songs answered...", "You play the whistle and feel strangely better.", false, ItemArcane.ArcaneEffect.HEALPLAYER, 5),

                new ItemWeapon(3000,0,1,0,GameItem.ItemUsability.MULTIPLE,GameItem.ItemDamageType.BLUDGONING, "Thamla's Bolas", "A finely crafted Primal bolas for capturing Groats.", "You hurl the bolas at the target!", false),
                new ItemWeapon(3001, 25, 1, 10, GameItem.ItemUsability.ONCE, GameItem.ItemDamageType.TOXIC, "Jar of Acridmorbi", "Acridmorbi are especially aggressive aerial arthropods with a painful sting and very sharp jaws. They make for an excellent weapon when trapped in clay pots and thrown at the foe.", "You equip one of the pots.", true),
                new ItemWeapon(3002, 50,1,30, GameItem.ItemUsability.MULTIPLE, GameItem.ItemDamageType.SLASHING, "Sword", "A forged sword of the sort commonly seen around Eron and surrounding territories.", "You equip the sword.", true),
                new ItemWeapon(3003, 50,1,30, GameItem.ItemUsability.MULTIPLE, GameItem.ItemDamageType.ARCANE, "Torch", "A flaming torch more commonly used to light the way.", "You equip the torch.", true),

                new ItemArmor (2000,0,1,GameItem.ItemUsability.MULTIPLE,"Armor of the Keeper","This otherworldly armor appears oddly organic in design despite being made of metal.","You don the strange armor.", false, GameItem.ItemDamageType.ARCANE, 0.9),
                new ItemArmor (2001,100,1,GameItem.ItemUsability.MULTIPLE, "Guardian Armor", "Standard-issue body armor among the Eronites. It was originally created for the citizen militia and is designed to protect against ranged projectiles.", "You equip the armor.", true, GameItem.ItemDamageType.PIERCING, 0.25),

                new ItemInventory(1000,1,1,GameItem.ItemUsability.ONCE, "1 Tals Coin", "The Tals is the standard form of currency in Eron, though many neighbors also honor it. This coin is worth 1 Tals", "You flip a coin for your amusement.", true),
                new ItemInventory(1001, 10,1, GameItem.ItemUsability.ONCE, "10 Tals Coin","The Tals is the standard form of currency in Eron, though many neighbors also honor it. This coin is worth 10 Tals", "You flip a coin for your amusement.", true ),

                // Generic Weapons and Armor - only used for defaults, they should never appear in inventory:
                new ItemArmor(0001, 0, 1, GameItem.ItemUsability.MULTIPLE, "Tunic", "A simple tunic that protects the modesty of the wearer and little else.", "", false, GameItem.ItemDamageType.BLUDGONING, 0.01), // These are the "default" items for the equipment slots.
                new ItemWeapon(0002, 0, 1, 5, GameItem.ItemUsability.MULTIPLE, GameItem.ItemDamageType.BLUDGONING, "Fists", "If you are forced to fight with your fists, something has gone very wrong...", "", false ), // If the player drops the last instance of whatever they are equipped with
                new ItemWeapon(0003, 0, 1, 10, GameItem.ItemUsability.MULTIPLE, GameItem.ItemDamageType.SLASHING, "Monger Claws", "The talons on the Monger's forelimbs are as sharp as blades and adept at tearing flesh", "", false),                                                                                                                                                                                               // these will drop in as placeholders to avoid a null reference error.
                new ItemArmor(0004, 0, 1, GameItem.ItemUsability.MULTIPLE, "Monger Hide", "The leathery hide of the Monger is supremely resistant to blades and claws.", "", false, GameItem.ItemDamageType.SLASHING, 0.1)
            };

        }

        public static List<NPC> StandardNPC()
        {
            return new List<NPC>()
            {
                new Monger(1, 1000, "The Monger", " The terrible beast that attacked the carvan! Dare you face it or do you flee?", GetGameItemById(0004) as ItemArmor,  GetGameItemById(0003) as ItemWeapon, Character.RaceType.Monger, Character.BattleStance.DEFEND, "../Art/MongerB.png"),
                new Merchant(02, 200, "The Keeper", "The Keeper is a strange creature traveling with the caravan for reasons of their own. Their wares are among the most exotic to be found, but they fetch a high price...", Character.RaceType.Scourge, Character.BattleStance.NOFIGHT,"../Art/Keeper.png", new List<string>{
                    "Ah, Keeper expected you. We have things that might aid you.",
                    "Keeper finds things others don't want. Mayhaps it might be of use to you?",
                    "We are the Keeper.",
                    "Monger bad for business..." }),
                new Fighter(03, 200, "Thalmos", "", GameData.GetGameItemById(0001) as ItemArmor, GameData.GetGameItemById(3003) as ItemWeapon,  Character.RaceType.Primal, Character.BattleStance.NOFIGHT,"../Art/ThalmosA.png"),
                new Fighter(04, 200, "Kalmas", "", GameData.GetGameItemById(0001) as ItemArmor, GameData.GetGameItemById(3002) as ItemWeapon, Character.RaceType.Primal, Character.BattleStance.NOFIGHT,"../Art/PrimalB.png"),
                new Merchant(05, 200, "Sobek", "One of the Primal elders. He might have some things to say.", Character.RaceType.Primal, Character.BattleStance.NOFIGHT,"../Art/PrimalC.png",  new List<string>
                {
                        "This is dreadful!",
                        "While these woods are far from tame, such foul creatures rarely emerge on their own accord. I fear something terrible is afoot here. ",
                        "While few beasts of nature can rival a Monger for strength, their main weapon is the insidious fumes concealed within their mantle. If you intend to confront the beast, you must find some means of defending yourself from them. "
                }
                ),
                new Monger(13, 1000, "The Monger", " The terrible beast in its lair!", GetGameItemById(0004) as ItemArmor,  GetGameItemById(0003) as ItemWeapon, Character.RaceType.Monger, Character.BattleStance.ATTACK, "../Art/MongerB.png"),

            };

        }


        #region HELPER METHODS: 

        /// <summary>
        /// Method that looks for a game item by its ID number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static GameItem GetGameItemById(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.ItemId == id);
        }

        /// <summary>
        /// Method that looks for a NPC by its ID number.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static NPC GetNPCById(int id)
        {
            return StandardNPC().FirstOrDefault(i => i.Id == id);
        }


        public static List<string> PopulateRaceBox()
        {
            // Creating data for the dropboxes:
            List<string> races = Enum.GetNames(typeof(Player.RaceType)).ToList();

            // We don't want scourge or monger to be playable
            races.Remove("Scourge");
            races.Remove("Monger");


            return races;

        }

        public static List<string> PopulateGenderBox()
        {
            List<string> gender = Enum.GetNames(typeof(Player.PlayerGender)).ToList();
            return gender;
        }

        public static List<string> PopulateRoleBox()
        {
            List<string> role = Enum.GetNames(typeof(Player.PlayerRole)).ToList();
            return role;
        }
        #endregion
    }
}

