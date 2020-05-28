using Cards.Entities;
using Infrastructure.DataAccess.CRUDInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataAccess
{
    public interface IPlayerRepository : ICanAddEntity<Player>, ICanUpdateEntity<Player>, ICanGetEntity<Player>
    {
        IReadOnlyList<Player> GetPlayerByLastName(string name);
    }
}
