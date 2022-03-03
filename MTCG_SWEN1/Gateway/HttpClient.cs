using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.Gateway
{
    // Preperation for including Client interface.
    class HttpClient
    {
        private int _port { get => _port; set => _port = value; }

        public HttpClient(int port)
        {
            _port = port;
        }
    }
}
