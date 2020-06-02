using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Players;
using CardsAndDecks;
namespace testGame2
{
    public partial class Form1 : Form
    {
        Game1 game;
        //int game_mode = 0;//0 - начало игры. 1 - фаза вербовки. 2 - фаза боя; заменить на enum,как team
        int maxGold = 20;
        public Form1()
        {
            InitializeComponent();
            game = new Game1();
            game.NewGame();
            for (int i = 0; i < 5; i++)
            {
                game.CurrentPlayer.SetCardInPlay(i, new NullCard());
            }
            labelEnemyHealth.Text = "" + game.EnemyPlayer.hero.getHealth();
            labelHeroHealth.Text =""+ game.CurrentPlayer.hero.getHealth();
            label_money.Text = "" + game.CurrentPlayer.Stat.Gold;
            Team t = game.CurrentPlayer.getMyTeam();
            game.CurrentPlayer.MyDeck.shuffle();
            label1.Text += game.CurrentPlayer.MyDeck.numberOfCards();
        }

        private void newHand()
        {
            for (int i = 0; i < 3;i++)
                if (!(game.CurrentPlayer.getCardFromHand(i) is NullCard))
                {
                    game.CurrentPlayer.DiscardDeck.AddToDeck(game.CurrentPlayer.getCardFromHand(i));
                }

            if (game.CurrentPlayer.MyDeck.numberOfCards() - 3 >= game.CurrentPlayer.NumberOfTheTopCard)
            {
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(),0);
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 1);
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 2);
            } else
                if (game.CurrentPlayer.MyDeck.numberOfCards() - 2 == game.CurrentPlayer.NumberOfTheTopCard)
            {
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 0);
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 1);
                game.CurrentPlayer.setCardFromHand(new NullCard(), 2);
            } else
                    if (game.CurrentPlayer.MyDeck.numberOfCards() - 1 == game.CurrentPlayer.NumberOfTheTopCard)
            {
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 0);
                game.CurrentPlayer.setCardFromHand(new NullCard(), 1);
                game.CurrentPlayer.setCardFromHand(new NullCard(), 2);
            }else
                {
                game.CurrentPlayer.AddDiscardToDeck();
                game.CurrentPlayer.MyDeck.shuffle();
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 0);
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 1);
                game.CurrentPlayer.setCardFromHand(game.CurrentPlayer.MyDeck.GetTopCard(), 2);
                }
            drawHand();
        }
        private void button_newHand_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 0)
            {
                button_newHand.Text = "Закончить вербовку";
                game.GameMode = 1;
                newHand();
                label_res.Text = "";
                debugLabelDeck.Text = "";
                label1.Text = "" + (game.CurrentPlayer.MyDeck.numberOfCards() - game.CurrentPlayer.NumberOfTheTopCard);

            }
            else if (game.GameMode == 1)
            {
                game.CurrentPlayer.Stat.Energy = game.CurrentPlayer.Stat.MaxEnergy;
                label_energy.Text = "" + game.CurrentPlayer.Stat.Energy;
                button_newHand.Text = "Закончить бой";
                game.GameMode = 2;
            }
            else if (game.GameMode == 2)
            {
                Player t = game.CurrentPlayer;
                game.CurrentPlayer = game.EnemyPlayer;
                game.EnemyPlayer = t;
                newTurn();
                newHand();
                button_newHand.Enabled = false;
                AIplay();
                
            }
        }
        private void AIplay()
        {
            PictureBox[] pbs = { pictureBox7,pictureBox8,pictureBox9,pictureBox10,pictureBox11 };
            int j = 0;
            while ((j <3)&&(game.CurrentPlayer.Stat.Gold >= game.CurrentPlayer.getCardFromHand(j).getGold()))
            {
                game.CurrentPlayer.CurrentCard = game.CurrentPlayer.getCardFromHand(j);
                for (int i = 0; i < 5; i++)
                {
                    if ((game.CurrentPlayer.GetCardInPlay(i) is NullCard))
                    {
                        game.CurrentPlayer.NumberOfCardInPlay = j;
                        playCard(pbs[i], i);
                        break;
                    }
                }
                j++;
            }
            timer1.Enabled = true;
            
        }
        private void pictureBoxHand3_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                Card card = game.CurrentPlayer.getCardFromHand(2);
                game.CurrentPlayer.CurrentCard = card;
                game.CurrentPlayer.NumberOfCardInPlay = 2;
            }
        }

        private void pictureBoxHand2_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                Card card = game.CurrentPlayer.getCardFromHand(1);
                game.CurrentPlayer.CurrentCard = card;
                game.CurrentPlayer.NumberOfCardInPlay = 1;
            }
        }

        private void PictureBox_Hand1_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                Card card = game.CurrentPlayer.getCardFromHand(0);
                game.CurrentPlayer.CurrentCard = card;
                game.CurrentPlayer.NumberOfCardInPlay = 0;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                this.playCard(pictureBox_InPlay1,0);
            }
            if (game.GameMode == 2)
            {
                if (game.CurrentPlayer.Stat.Energy > 0)
                {
                    playAttack(0, pictureBox_InPlay1);
                }
            }
        }
        private void playCard(PictureBox pb,int numberOfCard)
        {
            if (game.CurrentPlayer.Stat.Gold >= game.CurrentPlayer.CurrentCard.getGold() && (!(game.CurrentPlayer.CurrentCard is NullCard)))
            {
                game.CurrentPlayer.SetCardInPlay(numberOfCard, game.CurrentPlayer.CurrentCard);
                pb.Image = game.CurrentPlayer.CurrentCard.getImg();
                game.CurrentPlayer.Stat.Gold -= game.CurrentPlayer.CurrentCard.getGold();
                label_money.Text = "" + game.CurrentPlayer.Stat.Gold;
                game.CurrentPlayer.setCardFromHand(new NullCard(), game.CurrentPlayer.NumberOfCardInPlay);
                drawHand();
                game.CurrentPlayer.CurrentCard = new NullCard();
            }
        }

        private void pictureBox_InPlay2_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                this.playCard(pictureBox_InPlay2,1);
            }
            if (game.GameMode == 2)
            {
                if (game.CurrentPlayer.Stat.Energy > 0)
                {
                    playAttack(1, pictureBox_InPlay2);
                }
            }
        }

        private void pictureBox_InPlay3_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                this.playCard(pictureBox_InPlay3,2);
            }
            if (game.GameMode == 2)
            {
                if (game.CurrentPlayer.Stat.Energy > 0)
                {
                    playAttack(2, pictureBox_InPlay3);
                }
            }
        }

        private void pictureBox_InPlay4_Click(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                this.playCard(pictureBox_InPlay4, 3);
            }
            if (game.GameMode == 2)
            {
                if (game.CurrentPlayer.Stat.Energy > 0)
                {
                    playAttack(3, pictureBox_InPlay4);
                }
            }
        }

        private void pictureBox_InPlay5_Click_1(object sender, EventArgs e)
        {
            if (game.GameMode == 1)
            {
                this.playCard(pictureBox_InPlay5, 4);
            }
            if (game.GameMode == 2)
            {
                if (game.CurrentPlayer.Stat.Energy > 0)
                {
                    playAttack(4, pictureBox_InPlay5);
                }
            }
        }
        public void playAttack(int number,PictureBox pb)
        {
            if (game.CurrentPlayer.getMyTeam() == Team.red) //временное решение
            {
                game.CurrentPlayer.PlayAtackCard(game.EnemyPlayer, number);
                labelEnemyHealth.Text = "" + game.EnemyPlayer.hero.getHealth();
                pb.Image = game.CurrentPlayer.GetCardInPlay(number).getImg();
                label_energy.Text = "" + game.CurrentPlayer.Stat.Energy;
                if(game.EnemyPlayer.hero.getHealth() <= 0)
                {
                    labelWin.Text = "Победа!";
                }
            }
            else
            {
                game.CurrentPlayer.PlayAtackCard(game.EnemyPlayer, number);
                labelHeroHealth.Text = "" + game.EnemyPlayer.hero.getHealth();
                pb.Image = game.CurrentPlayer.GetCardInPlay(number).getImg();
                label_energy.Text = "" + game.CurrentPlayer.Stat.Energy;
                if (game.EnemyPlayer.hero.getHealth() <= 0)
                {
                    labelWin.Text = "Поражение!";
                }
            }
        }
        public void newTurn()
        {
            game.CurrentPlayer.Stat.Gold = maxGold;
            game.CurrentPlayer.Stat.Energy = 2;
            label_money.Text = "" + game.CurrentPlayer.Stat.Gold;
            game.GameMode = 0;
            button_newHand.Text = "Начать вербовку";
            //сделать функцию с интерфейсом
        }
        private void drawHand()
        {
            if (game.CurrentPlayer.getMyTeam() ==Team.red)
            {
                PictureBox_Hand1.Image = game.CurrentPlayer.getCardFromHand(0).getImg();// hand[0].getImg();
                pictureBoxHand2.Image = game.CurrentPlayer.getCardFromHand(1).getImg();
                pictureBoxHand3.Image = game.CurrentPlayer.getCardFromHand(2).getImg();
                label_Hand1.Text = "" + game.CurrentPlayer.getCardFromHand(0).getGold();// hand[0].getGold();
                label_Hand2.Text = "" + game.CurrentPlayer.getCardFromHand(1).getGold();
                label_Hand3.Text = "" + game.CurrentPlayer.getCardFromHand(2).getGold();
                labeldamage1.Text = "" + game.CurrentPlayer.getCardFromHand(0).getDamage();//hand[0].getDamage();
                labelDamage2.Text = "" + game.CurrentPlayer.getCardFromHand(1).getDamage();
                labelDamage3.Text = "" + game.CurrentPlayer.getCardFromHand(2).getDamage();
            }
            else
            {
                pictureBoxEnemyHand1.Image = game.CurrentPlayer.getCardFromHand(0).getImg();// hand[0].getImg();
                pictureBoxEnemyHand2.Image = game.CurrentPlayer.getCardFromHand(1).getImg();
                pictureBoxEnemyHand3.Image = game.CurrentPlayer.getCardFromHand(2).getImg();
              //вывод руки противнкика временный для более лёгкой отладки
            }
        }


       /*private void pictureBoxHand2_MouseEnter(object sender, EventArgs e)
        {
            //pictureBoxHand2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void pictureBoxHand3_MouseEnter(object sender, EventArgs e)
        {
            //pictureBoxHand3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void PictureBox_Hand1_MouseEnter(object sender, EventArgs e)
        {
            //PictureBox_Hand1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        }

        private void pictureBoxHand2_MouseLeave(object sender, EventArgs e)
        {
            //pictureBoxHand2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void pictureBoxHand3_MouseLeave(object sender, EventArgs e)
        {
            //pictureBoxHand3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void PictureBox_Hand1_MouseLeave(object sender, EventArgs e)
        {
            //PictureBox_Hand1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            PictureBox[] pbs = { pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11 };
            for (int i = 0; i < 5; i++)
            {
                if ((!(game.CurrentPlayer.GetCardInPlay(i) is NullCard))&&(game.CurrentPlayer.Stat.Energy > 0))
                {
                    playAttack(i, pbs[i]);
                }
            }
            timer1.Stop();
            Player t = game.CurrentPlayer;
            game.CurrentPlayer = game.EnemyPlayer;
            game.EnemyPlayer = t;
            newTurn();
            button_newHand.Enabled = true;
        }
    }
}
