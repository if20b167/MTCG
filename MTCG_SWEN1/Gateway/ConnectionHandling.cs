using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MTCG_SWEN1.HTTP;

namespace MTCG_SWEN1.Server
{
    /// <summary>
    /// Combined with the classes HttpRequest and HttpResponse, the ConnectionHandling class
    /// will act as central allocation unit of connection traffic and replace the original HttpProcessor.
    /// </summary>
    public class ConnectionHandling
    {
        private readonly TcpClient _socket;
        private readonly HttpRequest _request;
        private readonly HttpResponse _response;


        public ConnectionHandling(TcpClient socket)
        {           
            _socket = socket;
            _request = new HttpRequest(socket);
            _response = new HttpResponse(socket);
            AreParametersNull();
            ConnectionThreading();
        }

        private async void ConnectionThreading()
        {
            
            await Task.Run(() => { Process(); });
            _socket.Close();
        }

        private void Process()
        {
            _request.Receive();
            Console.WriteLine(_request.Method);
            
            try
            {
                // Assemble EndpointPath and check which class will reach
                // 
            }
            catch(Exception e)
            {
                _response.Send();
                
            }
            
            // Build try/catch block to handle the allocation and its possible exceptions.
            //_request.Receive();

            // Try/catch block.
            // In the exceptions its necessary to use _response.Send() to send exception message. 
        }

        public void AreParametersNull()
        {
            if (_request.Version == null)
                Console.WriteLine("Request Version = null at start");

            if (_response.Version == null)
                Console.WriteLine("Response Version = null at start");
        }

        public string EndpointPath()
        {
            if (_request.Path.Count(a => a == '/') > 1)
                return EndpointPath().Substring(0, EndpointPath().LastIndexOf("/"));
            else
                return _request.Path;
        }
    }
}
