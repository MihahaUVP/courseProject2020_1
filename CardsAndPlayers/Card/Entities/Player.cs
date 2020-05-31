using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entities
{
    /// <summary>
    /// впиши сюда enum для тимы(не булорусских)
    /// </summary>
    public class Player : AuditableEntity
    {
        public string NickName { get; set; }
        public int Score { get; set; }
        public Player(string nickName, int score)
        {
            NickName = nickName;
            Score = score;
        }
        public Player()
        {

        }
    }
}
