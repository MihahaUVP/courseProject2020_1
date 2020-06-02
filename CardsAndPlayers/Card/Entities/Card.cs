using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entities
{
    public class Card : AuditableEntity
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Gold { get; set; }
        //У карты есть игрок, отражаем связь к 1 игроку
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public Card(int playerId, string title, int health, int damage, int gold, string text)
        {
            PlayerId = playerId;
            Name = title;
            Text = text;
            Health = health;
            Damage = damage;
            Gold = gold;
        }

        //Для EF Core
        public Card()
        {

        }
    }
}

