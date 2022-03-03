using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_SWEN1.HTTP;

namespace MTCG_SWEN1.Endpoints
{
    [Endpoint("/battle")]
    class BattleEndpoint
    {
        public BattleEndpoint()
        {
            // ?
        }

        [Method(EHttpMethods.POST)]
        public void BattlePost()
        {

        }
    }
}
