using Cards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.DataAccess
{
    public class PlayerRepository : AuditableRepository<Player>, IPlayerRepository
    {
        private readonly AppDbContext _dbContext;
        public PlayerRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyList<Player> GetPlayerByLastName(string name)
        {
            return _dbContext.Players.Where(x => x.NickName.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
