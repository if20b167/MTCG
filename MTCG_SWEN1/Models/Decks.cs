using MTCG_SWEN1.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Models
{
    class Decks
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public List<CardMethods> Cards { get; set; } = new();

        public Decks(Guid deckID, string username, string name)
        {
            ID = deckID;
            UserName = username;
            Name = name;
        }
    }
}
