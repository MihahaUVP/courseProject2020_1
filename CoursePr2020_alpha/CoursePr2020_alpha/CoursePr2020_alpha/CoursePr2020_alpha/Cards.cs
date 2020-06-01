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
namespace CardsAndDecks
{
    public class Cards
    {
        public Cards()
        {
        }
    }
    public class Deck 
    {
        private List<Card> deck = new List<Card>(); ///временно!!! А может и нет(
        public Deck()
        {
            deck = new List<Card>();
        }
        public Deck(Team t)
        {
            createDeck(t);
        }
        public void AddToDeck(Card card)
        {
            deck.Add(card);
        }
        public Card GetTopCard()
        {
            if(deck.Count > 0)
            {
            Card card = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return card;
            }
            return new NullCard();
        }
        
        public int numberOfCards()
        {
            return deck.Count;
        }
        public void createDeck(Team t)
        {
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Doctor, "Doctor", 10, 2, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Genie, "Genie", 20, 3, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Ghost_Night, "Ghost Knight", 5, 3, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Host, "Host", 10, 0, t));
            deck.Add(new Card(CoursePr2020_alpha.Properties.Resources.Red_Minester, "Minister", 15, 0, t));
        }
        public void shuffle()
        {
            MyShuffle.Shuffle<Card>(deck);
        }
        //здесь будут различные операции с картами в колоде
        //например, взятие верхней
        //сброс верхней карты
        //используется для сброса и колоды
        //можно реализовать удаление верхней карты
        // этот класс будет полем в классе Player
    }
    public static class MyShuffle
    {
        private static Random rnd = new Random();
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public class Card : IHasTeam
    {
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
        private Team myTeam;
        public Image Img { get; set; }
        public String Text { get; set; }
        private int health;
        public int Damage { get; set; }
        public int Gold { get; set; }
        public Card(Image i, String t, int gold, int damage, Team myTeam)
        {
            this.Img = i;
            this.Text = t;
            this.Gold = gold;
            this.Damage = damage;
            this.myTeam = myTeam;
            health = 1;
        }
        public void attack(Hero target) //возможно! тип цели не герой, а интерфейс ИмеющийЗдоровье
        {
            target.receiveDamage(Damage);
        }
    }
    public class NullCard : Card
    {
        public NullCard()
            : base(null, "", 0, 0, Team.red)
        {

        }
    }
}