using System;
using RabbitMQ.Client;
using System.Text;




namespace Mensageiro
{
    public class Coelho
    {
        protected ConnectionFactory factory;
        protected String RabbitHost = "localhost";
        protected String RabbitTopic = "CETURB";




        public void send(String args)
        {
            this.factory = new ConnectionFactory() { HostName = this.RabbitHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: RabbitTopic, type: "topic", durable: true);
                var message = args;//GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: RabbitTopic, routingKey: "#", basicProperties: null, body: body);
                Console.ForegroundColor = System.ConsoleColor.Green;
                Console.WriteLine("[  RABBITMQ   ]   " + message + "\n");
                Console.ResetColor();
            }
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "info: Vazio");
        }
    }
}
