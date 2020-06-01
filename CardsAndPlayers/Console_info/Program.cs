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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CardsStorage;Trusted_Connection=True;", b => b.MigrationsAssembly("Infrastructure"));
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
            Console.WriteLine("This is Opalenniy mir!");
            Player player = new Player("Деревенский",20);
            _playerRepository.Add(player);
            Card card = new Card(player.Id,"Джин",2,2,10,"Загадай желание");
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
