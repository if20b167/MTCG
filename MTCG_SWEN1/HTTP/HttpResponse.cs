using MTCG_SWEN1.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MTCG_SWEN1.HTTP
{
    class HttpResponse
    {
        private bool BodyNotNull = false;

        public string StatusMessage { get; set; }
        public string Version { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        //private Dictionary<HttpStatusCode, string> StatusCodeString;

        private TcpClient _socket;

        // Use for sending data as answer on clients request.

        public HttpResponse(TcpClient socket)
        {
            _socket = socket;
            Headers = new();
        }


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
                            return;
                        }

                        if (Version == null)
                            ParseVersionAndStatus(line);
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

        private void ParseVersionAndStatus(string line)
        {
            var responseFirstLine = line.Split(' ');
            Version = responseFirstLine[0];

            // Placeholder version to get the regarding status message.
            string statusCode = responseFirstLine[1];
            StatusMessage = HttpStatusMessageConverter.GetPlaceholderStatusCode(int.Parse(statusCode));
        }   
        
        private void ParseHeader(string line)
        {
            var responseHeader = line.Split(": ");
            Headers.Add(responseHeader[0], responseHeader[1]);
        }

        private void ParseBody(StreamReader reader)
        {
            if (Headers.ContainsKey("Content-Length"))
            {
                var bodyBuffer = new char[int.Parse(Headers["Content-Length"])];
                Body = new string(bodyBuffer);
                BodyNotNull = true;
            }
            else
            {
                Body = null;
            }
        }

        public void Send()
        {
            StreamWriter writer = new(_socket.GetStream()) { AutoFlush = true };
            SendServerAnswer();

            WriteLine(writer, $"HTTP/{Version} {StatusMessage}");
            WriteLine(writer, $"Datestamp: {DateTime.UtcNow.AddHours(1)}");
            WriteLine(writer, $"Server: {HttpServer.GetServerStatic._serverName}");
            if (Headers.Count != 0)
                foreach (var pair in Headers)
                    WriteLine(writer, $"{pair.Key}: {pair.Value}");
            if (BodyNotNull)
                WriteLine(writer, $"Content-Type: application/json; charset=UTF-8");
            WriteLine(writer, $"Content-Lenght: {Body.Length}");

            if (BodyNotNull)
                WriteLine(writer, $"\n{Body}");
            
            writer.Flush();
            writer.Close();
        }

        private void SendServerAnswer()
        {
            Console.WriteLine($"Response sent - Status: {StatusMessage}");
        }

        private void WriteLine(StreamWriter writer, string message)
        {
            writer.WriteLine(message);
        }

        

        
    }
}
