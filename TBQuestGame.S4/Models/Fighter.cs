using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    class Fighter : NPC, Ifight
    {
        public GameItem EquippedWeapon { get; set; }
        public GameItem EquippedArmor { get; set; }
        public BattleStance Stance { get; set; }

        public void CycleBattleRound(BattleStance Stance)
        {
            throw new NotImplementedException();
        }

        protected override string NPCInfo()
        {
            return "";
        }
    }
}
