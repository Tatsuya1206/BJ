using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
   
    public class Deck
    {
        private List<Card> cards;
        private Random random;

        public Deck()
        {
            // デッキの作成
            cards = new List<Card>();
            foreach (Mark mark in Enum.GetValues(typeof(Mark)))
            {
                foreach (Number number in Enum.GetValues(typeof(Number)))
                {
                    cards.Add(new Card(mark, number));
                }
            }

            // 乱数の初期化
            random = new Random();
        }

        public void Shuffle()
        {
            int i = cards.Count;
            while (i > 1)
            {
                // 最後の一枚になるまで
                i--;
                // 乱数で適当な位置を取得（0～ループ中の一番最後）
                int n = random.Next(i + 1);

                // 以下、ループ中の一番最後のカードと乱数で取得したカードの位置を入れ替え

                // 取得した位置のカードを設定（一時避難）
                Card value = cards[n];
                // 取得したカード位置に現在のカードを設定
                cards[n] = cards[i];
                // 現在のカード位置に取得した位置のカードを設定
                cards[i] = value;
            }
        }

        public Card DrawCard()
        {
            if (cards.Count == 0)
            {
                // デッキが空の場合は新しいデッキを作成し、シャッフル
                cards = new List<Card>();
                foreach (Mark mark in Enum.GetValues(typeof(Mark)))
                {
                    foreach (Number number in Enum.GetValues(typeof(Number)))
                    {
                        cards.Add(new Card(mark, number));
                    }
                }
                Shuffle();
            }

            // 最初のカードを取得
            Card drawnCard = cards.First();
            // カードが引かれたのでデッキから削除
            cards.RemoveAt(0);
            return drawnCard;
        }
    }

}
