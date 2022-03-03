using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.HTTP
{
    class HttpRequest
    {
        // Use for asking data as client demands.
        // Query stands for request of data from database

        private TcpClient _socket;
        public EHttpMethods Method { get; set; }
        public string Path { get; set; }
        public string Version { get; set; }
        public string Body { get; set; }

        public Dictionary<string, string> Headers;
        public Dictionary<string, string> EndpointParameters;

        public HttpRequest(TcpClient socket)
        {
            _socket = socket;
            Headers = new();
            EndpointParameters = new();

            // Initialized with null to check at Receive() if first line processed or not.
            Path = null;
        }
                
        // Receive incoming data request from client.
        public void Receive()
        {            
            try
            {
                // Create an instance of StreamReader to read from a file.
                StreamReader reader = new(_socket.GetStream());
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached.
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Empty line announces content with next line.
                        if (line.Length == 0)
                        {
                            ParseBody(reader);
                            SendServerAnswer();
                            return;
                        }                        

                        if (Version == null)
                        {
                            ParseFirstLineRequest(line);
                            ParseParametersForQuery();
                        }
                        else
                            ParseHeader(line);
                    }
                }
            }
            catch (Exception err)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The Request could not be read:");
                Console.WriteLine(err);
            }
        }
                
        private void ParseFirstLineRequest(string line)
        {
            var requestFirstLine = line.Split(' ');

            // Control usable methods via enum EHttpMethods.
            try
            {
                if (!Enum.IsDefined(typeof(EHttpMethods), requestFirstLine[0].ToUpper()))
                    throw new ArgumentException($"Using method ({requestFirstLine[0].ToUpper()}) is prohibited.");

                Method = Enum.Parse<EHttpMethods>(requestFirstLine[0].ToUpper());
            }
            catch (ArgumentException err)
            {               
                Console.WriteLine(err.Message);
                System.Environment.Exit(0);
            }            
            Path = requestFirstLine[1];
            Version = requestFirstLine[2];
        }

        private void ParseHeader(string line)
        {
            var requestHeader = line.Split(": ");
            Headers.Add(requestHeader[0], requestHeader[1]);
        }

        private void ParseBody(StreamReader reader)
        {            
            if (Headers.ContainsKey("Content-Length"))
            {
                var bodyBuffer = new char[int.Parse(Headers["Content-Length"])];
                Body = new string(bodyBuffer);
            }
            else
            {
                Body = null;
            }
        }

        private void ParseParametersForQuery()
        {
            if (Path.Contains('?'))
            {
                var tempPath = Path.Split('?');
                Path = tempPath[0];
                tempPath = tempPath[1].Split('&');

                foreach (var item in tempPath)
                {
                    var keyValuePair = item.Split('=');
                    EndpointParameters.Add(keyValuePair[0], keyValuePair[1]);
                }
            }
            else
                return;            
        }

        public void SendServerAnswer()
        {
            if (Headers.ContainsKey("Content-Type"))
                Console.WriteLine($"Received Request - Method: {Method}, Content-Type: {Headers["Content-Type"]}.");
            else if (Headers.ContainsKey("Authorization"))
                Console.WriteLine($"Received Request - Method: {Method}, Authorization: /.");
            else
                Console.WriteLine($"Received Request - Method: {Method}, no content.");
        }


        /// ToDo: Exception handling for POST/PUT Requests without content.
        /*private void CheckMethodContent()
        {
            try
            {
                if (Method == EHttpMethods.POST || Method == EHttpMethods.PUT && Body == null)
                    throw new ArgumentNullException("Request missing Body for used Method (POST/PUT).");
            }
            catch (ArgumentNullException err)
            {
                Console.WriteLine(err.Message);
                HttpResponse.AddBody("application/json", "{\"response\":\"Internal Server Error\"}");
            }
        }*/

        
    }
}
