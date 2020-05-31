using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cards.Entities;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCard_Players.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ICardRepository _cardRepository { get; set; }

        public CardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public IEnumerable<Card> Get()
        {
            return _cardRepository.GetAll();
        }

        
        [HttpGet("{id}")]
        public Card Get(int id)
        {
            return _cardRepository.Get(id);
        }

       
        [HttpPost]
        public ActionResult Post([FromBody] Card card)
        {
            _cardRepository.Add(card);
            return Ok();
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Card card)
        {
            _cardRepository.Update(card);
            return Ok();
        }       
       
    }
}
