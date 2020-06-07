using System;
using System.Collections.Generic;
using System.Text;
using Cards.Entities;
using Infrastructure.DataAccess.CRUDInterfaces;
namespace Infrastructure.DataAccess
{
    public interface ICardRepository: ICanDeleteEntity<Card>
    {
        IReadOnlyList<Card> GetAll();
        Card Get(int id);
        void Add(Card card);
        void Update(Card card);
        IReadOnlyList<Card> GetCardsByName(string name); 
    }
}
