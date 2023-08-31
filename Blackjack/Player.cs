using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Player
    {
        public Hand Hand { get; }

        public Player()
        {
            Hand = new Hand();
        }

        public void ClearHand()
        {
            Hand.Clear();
        }
    }

}
