using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using Protocol;

namespace TcpClient
{
    class Client
    {
		// добавить в класс имя клиента
		// to-do...

        private Socket socket;

        public Client()
        {
            socket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(string sIp, int nPort)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(sIp), nPort);

            while (!socket.Connected)
            {
                try
                {
                    socket.Connect(ep);
                }
                catch (SocketException e)
                {
                    //Console.WriteLine(e.Message);
                    Thread.Sleep(50);
                }
            }
        }

        public void StartReceive()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadReceive));
        }

        void ThreadReceive(object ob)
        {
            byte[] buffer = new byte[1024];
            while (true)
            {
                // receive message
                int nRecv = socket.Receive(buffer);

                Packet packet = Packet.ParseBytes(buffer);
                ProcessPacket(packet);
            }
        }

        private void ProcessPacket(Packet packet)
        {
            switch (packet.Type)
            {
                case PacketType.SimpleMessage:
                    {
						// считать и вывести на консоль имя отправителя и текст сообщения
                        string sName = packet.GetItem(0);
                        string sText = packet.GetItem(1);
                        Console.WriteLine("\n" + sName + ": " + sText);
                    } break;
                case PacketType.ClientList:
                    {
						// считать и вывести на консоль имена поключенных клиентов
						// to-do...
                    } break;
                case PacketType.Login:
                    {
						// считать и обработать ответ сервера на запрос имени клиента
						// если имя принято - allow, запомнить его и перейти к вводу сообщений
						// если не принято - deny, остаться в цикле ввода имени
						// to-do...
                    }
                    break;

            }
            
        }

		public void Send(Packet packet)
		{
			byte[] bufferSend = packet.ToBytes();
			socket.Send(bufferSend);
		}

		// для удобства отправки простого сообщения
		public void SendSimpleMessage(string sTo, string sText)
		{
			Packet packet = new Packet(PacketType.SimpleMessage, 2);
			packet.SetItem(0, sTo);
			packet.SetItem(1, sText);
			Send(packet);
		}
		// для удобства отправки имени серверу на проверку
		public void SendLogin(string sName)
		{
			Packet packet = new Packet(PacketType.Login, 1);
			packet.SetItem(0, sName);
			Send(packet);
		}

        public void Close()
        {
            socket.Close();
        }
    }
}
