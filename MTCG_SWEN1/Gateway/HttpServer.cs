using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.NetworkInformation;
using MTCG_SWEN1.HTTP;

namespace MTCG_SWEN1.Server
{
    class HttpServer
    {
        private int _port = 10001;
        private IPAddress _ip = IPAddress.Loopback;

        public readonly string _serverName = "MTCG_SWEN1";
        private readonly string _creator = "Lukas Varga [if20b167]";
        private volatile bool _serverIsActive = false;
        
        private TcpListener _tcpListener;
        
        private readonly static HttpServer s_staticServer = new HttpServer();

        private Thread t_serverThread;

        public static HttpServer GetServerStatic { get => s_staticServer; }

        public int Port { get => _port; set => _port = value; }
       
        private HttpServer()
        {
            _tcpListener = new TcpListener(_ip, _port);            
        }        

        public void StartServerThread()
        {
            if (_serverIsActive)
                return;

            t_serverThread = new Thread(RunServer);
            t_serverThread.Start();

            while (!_serverIsActive)
                Thread.Sleep(1);
        }

        private void RunServer()
        {            
            _tcpListener.Start(5);
            Console.WriteLine($"Server {_serverName} started by {_creator}, {DateTime.UtcNow.AddHours(1)}\nWaiting for connection to {_ip}...");
            _serverIsActive = true;
            Thread.Sleep(1500);
            Console.WriteLine($"Connected to {GetHostName()}, {DateTime.UtcNow.AddHours(1)}\n");

            while (_serverIsActive)
            {
                TcpClient socket = _tcpListener.AcceptTcpClient();
                new ConnectionHandling(socket);
                //HttpProcessor processor = new HttpProcessor(socket, this);
                //new Thread(processor.Process).Start();
                //Thread.Sleep(1);
            }
        }

        internal string GetHostName()
        {
            if (_ip.ToString() == "127.0.0.1")
                return "localhost";

            IPHostEntry hostEntry = Dns.GetHostEntry(HttpServer.GetServerStatic._ip);
            return hostEntry.HostName;
        }
    }
}
