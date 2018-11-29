using System;
using RabbitMQ.Client;
using System.Text;

namespace Mensageiro
{
    public class Carteiro
    {
        public Carteiro()

        {
            this.conf = new EnvConfig();
            this.RabbitHost = conf.getRabbitUrlConnection();
            this.RabbitTopic = conf.getRabbitTopic();
            this.factory = new ConnectionFactory() { HostName = this.RabbitHost };
        }
        private EnvConfig conf;
        private ConnectionFactory factory;
        private String RabbitHost;
        private String RabbitTopic;


        public void send(String dadosEnvio)
        {
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: RabbitTopic, type: "topic", durable: true);
                var message = dadosEnvio;
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: RabbitTopic, routingKey: conf.getRabbitRoutingKey(), basicProperties: null, body: body);
                //printa muito verbosamente na tela apenas em ambientes de desenvolvimento
                if (!conf.isProductionEnv())
                {
                    Console.ForegroundColor = System.ConsoleColor.Green;
                    Console.WriteLine("[  RABBITMQ   ]   " + message + "\n");
                    Console.ResetColor();
                }
            }
        }
    }
}
