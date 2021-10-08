using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using Protocol;

namespace TcpServer
{
    class Server
    {
        private Socket socketServer;
        private List<ClientOnServer> clients;

        public Server()
        {
            socketServer = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            clients = new List<ClientOnServer>();
        }

        public void Bind(string sIp, int nPort)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(sIp), nPort);
            socketServer.Bind(ep);
            socketServer.Listen(30);
        }

        public void Close()
        {
            socketServer.Close();
        }

        public void StartAccept()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadAccept));
        }

        private void ThreadAccept(object ob)
        {
            while (true)
            {
                Socket socketClient = socketServer.Accept();

                ClientOnServer client = new ClientOnServer(socketClient, this);
                client.StartReceive();
                clients.Add(client);
                Console.WriteLine("client #" + clients.Count.ToString() + " connected");
            }
        }

        public void ProcessPacket(Packet packet)
        {
            switch (packet.Type)
            {
                case PacketType.SimpleMessage:
                    {
                        string sName = packet.GetItem(0);
                        string sText = packet.GetItem(1);

						// найти в списке клиентов получателя - sName и переслать ему сообщение sText
						// to-do...
						
                    }
                    break;
                case PacketType.Login:
                    {
						// считать имя нового клиента из пакета
						// если в списке клиентов есть имя клиента - отправить ответ "deny"
						// иначе - отправить ответ "allow" и отправить каждому клиенту полный список имен
						// to-do...
                    }
                    break;

            }

			// пример прохода по списку клиентов

			////for (int i = 0; i < clients.Count; i++)
			////{
			////    clients[i].Send(s);
			////}
			//foreach (ClientOnServer client in clients)
			//{
			//	client.Send(s);
			//}
        }
    }
}
