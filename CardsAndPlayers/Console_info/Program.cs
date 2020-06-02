using System;
using System.Linq;
using Cards.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Comsole_info
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CardsStorage_1;Trusted_Connection=True;", b => b.MigrationsAssembly("Infrastructure"));
            return new AppDbContext(optionsBuilder.Options);
        }
    }
    class Program
    {
        private static readonly AppDbContext _appContext;
        private static ICardRepository _cardRepository;
        private static IPlayerRepository _playerRepository;
        static Program()
        {
            AppDbContextFactory factory = new AppDbContextFactory();
            _appContext = factory.CreateDbContext(null);
            _playerRepository = new PlayerRepository(_appContext);
            _cardRepository = new CardRepository(_appContext);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("This! Is! Burnt World!");
            Player player = new Player("CurrentPlayerNick",20);
            _playerRepository.Add(player);
            Card card = new Card(player.Id,"Воланд",5,5,20,"Боевой клич: Замешивает в колоду Кота Бегемота, Кота в сапогах и Кота Учёного");
            _cardRepository.Add(card);
            card = new Card(player.Id, "Джинн", 6, 6, 20, "Боевой клич: Замешивает в колоду одну бронзовую, одну серебряную и одну золотую карту");
            _cardRepository.Add(card);
            card = new Card(player.Id, "Разбойница", 4, 4, 10, "Боевой клич: Крадёт из казны противника 5 монет");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Доктор", 5, 5, 10, "Предсмертный хрип: Лечит своего короля на количество своего урона");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Змей Горыныч", 7, 7, 10, "Боевой клич: Увеличивает на 10 максимальную вместимость своей казны");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Барон", 6, 6, 10, "Провокация: Поглощает весь урон, нанесённый своему королю");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Пыточных дел мастер", 1, 1, 10, "Эффект: Увеличивает урон на 1 каждый раз, когда карта из этого ряда попадает в сброс");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Берсерк", 3, 3, 5, "Единство: Если на столе есть три Берсерка, в колоду замешивается Ярл Берсерков");
            //_cardRepository.Add(card);
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Солоха", 0, 0, 5, "Диверсия: Размещается в ряду противника");
            //_cardRepository.Add(card);
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Лягушка", 1, 1, 5, "Предсмертный хрип: Призывает Принцессу в крайнюю левую незанятую клетку этого ряда");
            //_cardRepository.Add(card);
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Всадник без головы", 3, 3, 5, "Единство: Если в этом ряду есть три Всадника без головы, в колоду замешивается Кентервильское привидение");
            //_cardRepository.Add(card);
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            card = new Card(player.Id, "Поп", 2, 2, 5, "Эффект: Пока Поп находится на столе, Воланд, Всадник без головы и Кентервильское привидение не могут атаковать");
            //_cardRepository.Add(card);
            _cardRepository.Add(card);
            Console.WriteLine("Hello " + card.Name);

            using (_appContext)
            {
                var cards = _appContext.Cards.ToList();
                Console.WriteLine("Текущая колода:");
                foreach(Card c in cards)
                {
                    Console.WriteLine($"{c.Id}) {c.Name}: {c.Gold} gold, {c.Damage} atk; <<{c.Text}>>");
                }
            }
        }
    }
}
