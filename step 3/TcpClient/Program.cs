using System;
using System.Text;


namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Connect("172.27.216.134", 2021);
            Console.WriteLine("connected to server");
            client.StartReceive();

			// организовать цикл ввода имени клиента (login) до тех пор, пока сервер не одобрит имя
			// обработка ответа - в классе Client
			//while (...)
			//{

			//}
			// to-do...

            while (true)
            {
                // enter and send message
				Console.Write("enter receiver name: ");
                string sTo = Console.ReadLine();
				Console.Write("enter message text: ");
				string sText = Console.ReadLine();
				client.SendSimpleMessage(sTo, sText);
            }
            client.Close();
        }

        
    }
}
