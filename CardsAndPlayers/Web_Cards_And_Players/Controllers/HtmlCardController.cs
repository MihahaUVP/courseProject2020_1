using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cards.Entities;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebCard.Controllers
{
    [Route("html/[controller]")]
    public class HtmlCardController : Controller
    {
        private ICardRepository _cardRepository { get; set; }

        public HtmlCardController(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(_cardRepository.GetAll());
        }


        [HttpGet("{id}")]
        public ActionResult Details(int id)
        {
            return View(_cardRepository.Get(id));
        }

        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Card card)
        {
            try
            {
                _cardRepository.Add(card);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("update/{id}")]
        public ActionResult Edit(int id)
        {
            return View(_cardRepository.Get(id));
        }

        [HttpPost("update/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [FromForm] Card card)
        {
           // try
            //{
                _cardRepository.Update(card);

                return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
              //  return View();
            //}
        } 
    }
}