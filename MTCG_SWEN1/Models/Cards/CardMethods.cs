using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MTCG_SWEN1.Models.Cards
{
    class CardMethods
    {
        public Guid ID { get; set; }
        public string Name { get; set; } = "";
        public string UserName { get; set; }
        public int Damage { get; set; } = 0;
        public bool IsInDeck { get; set; } = false;
        public ECardType CardType { get; set; }
        public EElementType ElementType { get; set; }
        public bool IsChosenForTrade { get; set; } = false;

        public EElementType GetElementType()
        {
            var elementNames = Enum.GetNames(typeof(EElementType));

            foreach (var type in elementNames)
            {
                if (this.Name == type)
                {
                    return Enum.Parse<EElementType>(type);
                }
            }

            return EElementType.NORMAL;
        }

        public ECardType GetCardType()
        {
            var cardNames = Enum.GetNames(typeof(ECardType));

            foreach (var type in cardNames)
            {
                if (this.Name == type)
                {
                    return Enum.Parse<ECardType>(type);
                }
            }

            return Enum.Parse<ECardType>("Ghost");
        }

    }
}
