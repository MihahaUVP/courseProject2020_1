using System;
using System.Collections.Generic;
using System.Text;

namespace Cards.Entities
{
    public abstract class AuditableEntity : Entity
    {
        public DateTime CreatedAt { get; set; }   

        public DateTime? ModifiedAt { get; set; }       
    }
}
