using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TBQuestGame.Models
{
    public interface Ifight
    {

        GameItem EquippedWeapon { get; set; }
        GameItem EquippedArmor { get; set; }


        Character.BattleStance Stance { get; set; }

        // Method that controls the battle round:
        void CycleBattleRound(Character.BattleStance Stance);
    }
}
