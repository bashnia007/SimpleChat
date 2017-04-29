using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Client.Proxy;

namespace Client
{
    public class ChatProxy : Proxy.IChatServiceCallback
    {
        public void RecieveMessage(string user, string message)
        {
            Console.WriteLine("{0}: {1}", user, message);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InstanceContext context = new InstanceContext(new ChatProxy());
                Proxy.ChatServiceClient server = new ChatServiceClient(context);

                Console.WriteLine("Введите логин");
                var username = Console.ReadLine();
                server.Join(username);

                Console.WriteLine();
                Console.WriteLine("Введите сообщение");
                Console.WriteLine("Для выхода нажмите Q");

                var message = Console.ReadLine();
                while (message != "Q" && message != "q")
                {
                    if (!string.IsNullOrEmpty(message)) server.SendMessage(message);

                    message = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Возникла ошибка. Срочно передайте звездюлей и текст этой ошибки программисту!");
                Console.WriteLine(ex);
                throw;
            }
            
        }
    }
}
