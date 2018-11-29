using System;
using RabbitMQ.Client;
using System.Text;


namespace Mensageiro
{
    public class Coelho
    {
        public Coelho()
        {
            this.conf = new EnvConfig();
            this.RabbitHost = conf.getRabbitUrlConnection();
            this.RabbitTopic = conf.getRabbitTopic();
            this.factory = new ConnectionFactory() { HostName = this.RabbitHost };
        }
        EnvConfig conf;
        protected ConnectionFactory factory;
        String RabbitHost;
        String RabbitTopic;
        public void send(String args)
        {
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: RabbitTopic, type: "topic", durable: true);
                var message = args;//GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: RabbitTopic, routingKey: "transcol", basicProperties: null, body: body);
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
