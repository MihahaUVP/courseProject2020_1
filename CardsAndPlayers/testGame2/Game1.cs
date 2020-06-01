using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardsAndDecks;
namespace Players
{
    public class Game1
    {
        public Game1()
        {

        }
        //public Player MyPlayer { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player EnemyPlayer   { get; set; }
        public int GameMode { get; set; }

        public void NewGame()
        {
            Hero King = new Hero(30, true);
            Hero enemyKing = new Hero(30, false);
            //MyPlayer = new Player(20, King, 2, Team.red);
            CurrentPlayer = new Player(20, King, 2, Team.red);
            EnemyPlayer =   new Player(20, enemyKing, 2, Team.green);
        }
    }
    public class Hero
    {
        private int maxHealth;
        private int health;
        private bool playersTeam;
        public void receiveDamage(int dmg)
        {
            health -= dmg;
        }
        public int getHealth()
        {
            return health;
        }
        public bool getTeam()
        {
            return playersTeam;
        }
        public Hero(int maxHealth, bool Team)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.playersTeam = Team;
        }
    }

    interface IHasTeam
    {
        Team getMyTeam();
        Team getEnemyTeam();
    }
    
    public enum Team
    {
        red,green
    }
    public class Statistics
    {
        private int gold;
        private int maxGold;
        private int energy;
        private int maxEnergy;
        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
            }
        }
        public int Energy
        {
            get
            {
                return energy;
            }
            set
            {
                energy = value;
            }
        }
        public int MaxEnergy
        {
            get
            {
                return maxEnergy;
            }
        }
        public Statistics(int maxGold,int maxEnergy)
        {
            this.maxEnergy = maxEnergy;
            this.maxGold = maxGold;
            gold = maxGold;
            energy = maxEnergy;
        }
    }
    
    public class Player:IHasTeam
    {
        Statistics stat;
        public Statistics Stat
        {
            get
            {
                return stat;
            }
            set
            {
                stat = value;
            }
        }
        public Team getMyTeam()
        {
            return myTeam;
        }
        public Team getEnemyTeam()
        {
            if (myTeam == Team.green)
                return Team.red;
            return Team.green;
        }
        Card currentCard;
        public Card CurrentCard
        {
            get
            {
                return currentCard;
            }
            set
            {
                currentCard = value;
            }
        }
        Card[] hand = new Card[3];
        Card[] cardsInPlay = new Card[5];
        Deck discardDeck;
        public void AddDiscardToDeck()
        {
            int nokd = discardDeck.numberOfCards();
            for (int i = 0; i < nokd; i++)
            {
                myDeck.AddToDeck(discardDeck.GetTopCard());
            }
        }
        public Deck DiscardDeck
        {
            get
            {
                return discardDeck;
            }
            set
            {
                discardDeck = value;
            }
        }    
        public void PlayAtackCard(Player enemy,int number)
        {
            if (this.GetCardInPlay(number) is NullCard)
            {
                return;
            }
            // костыль пока нет БД и здравого смысла!
            //if (this.GetCardInPlay(number).getEnemyTeam() == Team.green)
           // {
                this.GetCardInPlay(number).attack(enemy.hero);
           // }
           // else
          //  {
          //      this.GetCardInPlay(number).attack(this.hero);
          //      
          //  }
            discardDeck.AddToDeck(GetCardInPlay(number));
            this.SetCardInPlay(number, new NullCard());
            this.Stat.Energy--;
            
        }
        public Card GetCardInPlay(int n)
        {
            return cardsInPlay[n];
        }
        public void SetCardInPlay(int n, Card cardInPlay)
        {
            cardsInPlay[n] = cardInPlay;
        }
        public Card getCardFromHand(int number)
        {
            return hand[number];
        }
        public void setCardFromHand(Card card,int number)
        {
            hand[number] = card;
        }
        Team myTeam;
        int numberOfTheTopCard = 0;
        public int NumberOfCardInPlay { get; set; }
        public int NumberOfTheTopCard
        {
            get
            {
                return numberOfTheTopCard;
            }
            set
            {
                numberOfTheTopCard = value;
            }
        }
        public Hero hero;
        Deck myDeck;
        public Deck MyDeck
        {
            get
            {
                return myDeck;
            }
            set
            {
                myDeck = value;
            }
        }    
        public Player(int maxGold, Hero hero, int maxEnergy,Team myTeam)
        {
            this.stat = new Statistics(maxGold,maxEnergy);
            this.hero = hero;
            this.myTeam = myTeam;
            myDeck = new Deck(myTeam);
            discardDeck = new Deck();
            currentCard = new NullCard();
            for (int i = 0; i < 3;i++ )
            {
                hand[i] = new NullCard();
            }
            for(int i = 0; i < 5; i++)
            {
                cardsInPlay[i] = new NullCard();
            }
        }
    }
}