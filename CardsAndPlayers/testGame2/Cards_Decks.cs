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
using testGame2;
using Cards.Entities;
namespace CardsAndDecks
{
    public class Cards_Decks
    {
        public Cards_Decks()
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
            /// чисто для теста
            // using (Program._appContext)
            {
                Image img = null;
                var cards = Program._appContext.Cards.ToList();
                Console.WriteLine("Текущая колода:");
                Console.WriteLine("Красный:");
                foreach (var c in cards)
                {
                    // временная мера, потом будет свич или картинки будут в бд(если это возможно)
                    if (c.Title == "Варвар")
                        img = testGame2.Properties.Resources.Red_Barbarian;
                    if(c.Title == "Джинн")
                        img = testGame2.Properties.Resources.Red_Genie;
                    if(c.Title == "Доктор")
                        img = testGame2.Properties.Resources.Red_Doctor;
                    if (c.Title == "Барон")
                        img = testGame2.Properties.Resources.Red_Baron;
                    if (c.Title == "Лягушка")
                        img = testGame2.Properties.Resources.Red_Frog;
                    if (c.Title == "Поп")
                        img = testGame2.Properties.Resources.Red_Pope;


                    if (c.Title == "Солоха")
                        img = testGame2.Properties.Resources.Green_Solokha;
                    if (c.Title == "Разбойница")
                        img = testGame2.Properties.Resources.Green_Thief;
                    if (c.Title == "Всадник без головы")
                        img = testGame2.Properties.Resources.Green_HeadlessHorseman;
                    if (c.Title == "Воланд")
                        img = testGame2.Properties.Resources.Green_Woland;
                    if (c.Title == "Пыточных дел мастер")
                        img = testGame2.Properties.Resources.Green_TortureMaster;
                    if (c.Title == "Змей Горыныч")
                        img = testGame2.Properties.Resources.Green_ZmeiGorynych;

                    if (img == null)
                        img = testGame2.Properties.Resources.GreenCard;
                    deck.Add(new Card(img, c.Title, c.Gold, c.Damage, t));
                }
            }
            
           /* deck.Add(new Card(testGame2.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Barbarian, "Barbarian", 3, 1, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Doctor, "Doctor", 10, 2, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Genie, "Genie", 20, 3, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Ghost_Night, "Ghost Night", 5, 3, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Host, "Host", 10, 0, t));
            deck.Add(new Card(testGame2.Properties.Resources.Red_Minester, "Minister", 15, 0, t));*/
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
        private Image img;
        private String text;
        private int health;
        private int damage;
        private int gold;
        public Card(Image i, String t, int gold, int damage, Team myTeam)
        {
            this.img = i;
            this.text = t;
            this.gold = gold;
            this.damage = damage;
            this.myTeam = myTeam;
            health = 1;
        }
        public int getDamage()
        {
            return damage;
        }
        public int getGold()
        {
            return gold;
        }
        public Image getImg()
        {
            return img;
        }
        public String getText()
        {
            return text;
        }
        public void attack(Hero target) //возможно! тип цели не герой, а интерфейс ИмеющийЗдоровье
        {
            target.receiveDamage(damage);
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