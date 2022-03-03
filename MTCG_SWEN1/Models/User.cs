using MTCG_SWEN1.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Models
{
    class User
    {
        public Guid ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Coins { get; set; }
        public CardMethods[] Deck { get; set; }
        public int ELO { get; set; }

        public string GetUserData()
        {
            List<Object> userData = new List<Object>();
            userData.Add(UserName);
            userData.Add(Coins);
            userData.Add(ELO);
            return JsonSerializer.Serialize(userData);
        }
    }
}
