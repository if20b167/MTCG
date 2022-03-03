using MTCG_SWEN1.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Endpoints
{
    class MethodAttribute : System.Attribute
    {
        private EHttpMethods _method;
        public EHttpMethods Method { get; set; }

        public MethodAttribute(EHttpMethods method)
        {
            Method = method;
        }
    }
}
