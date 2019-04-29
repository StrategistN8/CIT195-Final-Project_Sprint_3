using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    class Merchant : NPC, Ispeak
    {

        public BattleStance Stance { get; set; } // used in validation.
        public List<string> Messages { get; set; }
        private Random r;

        public Merchant(int id, int hp, string name, string description, RaceType racetype, BattleStance initialStance, string imgDataPath, List<string> messages)
             : base(id, hp, name, description, racetype, initialStance, imgDataPath)
        {
            _id = id;
            _name = name;
            _description = description;
            _race = racetype;
            Stance = initialStance;
            _imageDataPath = imgDataPath;
            _health = hp;
            Messages = messages;
            r = new Random(0);


        }
                          
        protected override string NPCInfo()
        {
            return "info";
        }

        public string Speak()
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "";
            }
        }

        private string GetMessage()
        {
            
            int messageIndex = r.Next(0, Messages.Count());
            return Messages[messageIndex];
        }
    }
}
