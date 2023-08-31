using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Blackjack
{
    public class Game
    {
        private Deck deck;
        // プレイヤー
        public Player player { get; }
        // ディーラー
        public Player dealer { get; }

        private const string JUDE_DEALER_WIN = "0";
        private const string JUDE_PLAYER_WIN = "1";
        private const string JUDE_DORW = "9";

        public Game()
        {
            // コンストラクタで、ゲーム準備
            deck = new Deck();
            player = new Player();
            dealer = new Player(); 

            // ゲーム開始。
            StartGame();
        }

        public void StartGame()
        {
            // デッキシャッフル
            deck.Shuffle();
            // 手札を空にする。
            player.ClearHand();
            // ディーラーの手札を空にする。
            dealer.ClearHand();

            // ２枚ずつカードを配る
            player.Hand.AddCard(deck.DrawCard());
            dealer.Hand.AddCard(deck.DrawCard());
            player.Hand.AddCard(deck.DrawCard());
            dealer.Hand.AddCard(deck.DrawCard());

        }

        public void PlayerHit()
        {
            player.Hand.AddCard(deck.DrawCard());
        }

        public void DealerPlay()
        {
            // ディーラーは17以上になるまでヒット
            while (dealer.Hand.CalculateValue() < 17)
            {
                dealer.Hand.AddCard(deck.DrawCard());
            }
        }

        public string EndGame()
        {
            // ゲーム結果の判定
            return Jude();

        }

        public string Jude()
        {
            string jude = JUDE_DEALER_WIN;

            int playerPoints = player.Hand.CalculateValue();
            int dealerPoints = dealer.Hand.CalculateValue();

            if (playerPoints <= 21 && dealerPoints <= 21)
            {
                // 両者21以下の場合
                if (dealerPoints < playerPoints)
                {
                    // プレイヤーが21に近ければ、プレイヤーの勝利
                    jude = JUDE_PLAYER_WIN;
                }
                else if(dealerPoints == playerPoints)
                {
                    // 引き分け
                    jude = JUDE_DORW;
                }
            }
            else if (playerPoints <= 21 && 21 < dealerPoints)
            {
                // ディーラーがバーストしている場合、プレイヤーの勝利
                jude = JUDE_PLAYER_WIN;
            }

            return jude;
        }

        public bool IsBust()
        {
            bool ret = false;
            int i = player.Hand.CalculateValue();

            if (i > 21)
            {
                // バーストした場合。
                ret = true;
            }
            return ret;
        }

        public bool IsGameOver()
        {
            // ゲームオーバーの条件をここに追加する
            return false;
        }

    }

}
