using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_SWEN1.HTTP;

namespace MTCG_SWEN1.Endpoints
{
    [Endpoint("/tradings")]
    class TradingEndpoint
    {
        public TradingEndpoint()
        {
            // ?
        }

        [Method(EHttpMethods.GET)]
        public void TradeGet()
        {

        }

        [Method(EHttpMethods.POST)]
        public void TradePost()
        {

        }
    }
}
