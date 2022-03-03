using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCG_SWEN1.HTTP;

namespace MTCG_SWEN1.Endpoints
{
    [Endpoint("/stats")]
    class StatisticsEndpoint
    {
        public StatisticsEndpoint()
        {
            // ?
        }

        [Method(EHttpMethods.GET)]
        public void StatsGet()
        {

        }
    }
}
