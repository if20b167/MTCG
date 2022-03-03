using MTCG_SWEN1.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Models
{
    class Trading
    {
        public Guid ID { get; set; }
        public User Username { get; set; }
        public CardMethods Card { get; set; }

        public Trading(Guid id, User user, CardMethods card)
        {
            ID = id;
            Username = user;
            Card = card;
        }
    }
}
