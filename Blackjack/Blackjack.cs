using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blackjack
{
    public partial class Blackjack : Form
    {
        private const string JUDE_DEALER_WIN = "0";
        private const string JUDE_PLAYER_WIN = "1";
        private const string JUDE_DORW = "9";
        private const string PNG = ".png";
        private const string OF = "Of";
        private const string CARDS_DIR = @".\TrumpPNG\";

        private Deck deck;
        // プレイヤー
        public Player player;
        // ディーラー
        public Player dealer;

        public Blackjack()
        {
            InitializeComponent();
            // コンストラクタで、ゲーム準備
            deck = new Deck();
            player = new Player();
            dealer = new Player();

            // ゲーム開始。
            StartGame();
        }

        private void Hit_Click(object sender, EventArgs e)
        {
            PlayerHit();

            if (IsBust())
            {
                // バーストしたら、終了処理
                EndGame();
            }
            DispCards(player.Hand, true);
        }

        private void Stand_Click(object sender, EventArgs e)
        {
            // ディーラー側の処理実施
            DealerPlay();
            DispCards(dealer.Hand, false);

            switch (EndGame())
            {
                case JUDE_PLAYER_WIN:
                    this.label5.Text = "プレイヤーの勝利";
                    break;
                case JUDE_DEALER_WIN:
                    this.label5.Text = "ディーラーの勝利";
                    break;
                case JUDE_DORW:
                    this.label5.Text = "引き分け";
                    break;
                default:
                    break;
            }

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
                else if (dealerPoints == playerPoints)
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

        //public Blackjack()
        //{
        //    InitializeComponent();
        //}


        //private void Blackjack_Load(object sender, EventArgs e)
        //{
        //    //ゲーム開始
        //    game = new Game();
        //}

        

        private void DispCards(Hand hand, bool isPlayer)
        {
            foreach (Card card in hand.GetCards())
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(CARDS_DIR + card.Mark.ToString() + OF + card.Number.ToString() + PNG);
                if (isPlayer)
                {
                    this.groupBox2.Controls.Add(pictureBox);
                }
                else
                {
                    this.groupBox1.Controls.Add(pictureBox);
                }
            }
        }

    }
}
