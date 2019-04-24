using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestGame.Models
{
    public class GameItemQuantity
    {
        // Properties
        public GameItem GameItem { get; set; }
        public int Quantity { get; set; }

        //Constructor:
        public GameItemQuantity()
        {

        }

        public GameItemQuantity(GameItem gameItem, int quantity)
        {
            GameItem = gameItem;
            Quantity = quantity;    
        }
    }

}
