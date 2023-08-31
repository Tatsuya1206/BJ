using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    // マーク
    public enum Mark
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    // 数字
    public enum Number
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    public class Card
    {
        public Mark Mark { get; }
        public Number Number { get; }

        public Card(Mark mark, Number number)
        {
            Mark = mark;
            Number = number;
        }

        public int GetValue()
        {
            // カードの値を返す
            switch (Number)
            {
                case Number.Ace:
                    return 11; // または 1 になる場合もある
                case Number.Jack:
                case Number.Queen:
                case Number.King:
                    return 10;
                default:
                    return (int)Number;
            }
        }
    }

}
