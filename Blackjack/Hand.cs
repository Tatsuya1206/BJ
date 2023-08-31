using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Hand
    {
        private List<Card> cards;

        public Hand()
        {
            //手札をリストで管理
            cards = new List<Card>();
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int CalculateValue()
        {
            // 集計処理
            int value = cards.Sum(card => card.GetValue());

            // Aの処理：バーストしない範囲でエースを1として扱う
            foreach (var card in cards)
            {
                if (card.Number == Number.Ace && value > 21)
                {
                    value -= 10; // エースを1として扱う
                }
            }

            return value;
        }

        public void Clear()
        {
            cards.Clear();
        }

        public List<Card> GetCards()
        {
            return cards;
        }
    }

}
