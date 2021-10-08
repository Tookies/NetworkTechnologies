using System;
using System.Text;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Bind("172.27.216.134", 2021);
            server.StartAccept();

            while (true) System.Threading.Thread.Sleep(100);
            
            server.Close();
        }
    }
}
