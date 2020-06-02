using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entities
{
    enum Team 
    {
        red,
        green
    }
    public class Player : AuditableEntity
    {
        Team PlayersTeam;
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
