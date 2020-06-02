using Cards.Entities;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DataAccess
{
    public class CardRepository : AuditableRepository<Card>, ICardRepository
    {
        private readonly AppDbContext _dbContext;
        public CardRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override Card Get(int id)
        {
            return _dbContext.Cards.Include(b => b.Player).FirstOrDefault(b => b.Id == id);
        }

        public IReadOnlyList<Card> GetAll()
        {
            return _dbContext.Cards.Include(b => b.Player).ToList();
        }

        public IReadOnlyList<Card> GetCardsByName(string title)
        {
            return _dbContext.Cards.Where(x => x.Name.ToLower().Contains(title.ToLower())).ToList();
        }        
    }
}
