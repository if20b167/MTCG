using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Endpoints
{
    class EndpointAttribute : System.Attribute
    {
        private string _path;
        public string Path { get; set; }

        public EndpointAttribute(string path)
        {
            Path = path;
        }
    }
}
