using System;
using RabbitMQ.Client;
using System.Text;

namespace Mensageiro
{
    public class Carteiro
    {
        public Carteiro()

        {
            EnvConfig env = new EnvConfig();
            this.RabbitHost = env.getRabbitUrlConnection();
            this.RabbitTopic = env.getRabbitTopic();
            this.factory = new ConnectionFactory() { HostName = this.RabbitHost };
            this.KeyMongo = env.getRabbitRoutingKeyMongo();
            this.KeySQL = env.getRabbitRoutingKey();
        }

        private ConnectionFactory factory;
        private String RabbitHost;
        private String RabbitTopic;
        private String KeySQL;
        private String KeyMongo;

        /**
        @param String em formato json com os dados de uma mensagem real-time de um ônibus qualquer.
        Este método envia duas mensagens ao tópico da ceturb dentro do nosso RabbitMQ,
        uma delas vai para a fila do popMQ (SQL-Server) e a outra para a fila do popMongo (MongoDB)
        */
        public void send(String dadosEnvio)
        {
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: RabbitTopic, type: "topic", durable: true);
                var body = Encoding.UTF8.GetBytes(dadosEnvio);
                //envia uma cópia para a fila do SQL-Server
                channel.BasicPublish(exchange: RabbitTopic, routingKey: this.KeySQL, basicProperties: null, body: body);
                //envia outra cópia para a fila do MongoDB
                channel.BasicPublish(exchange: RabbitTopic, routingKey: this.KeyMongo, basicProperties: null, body: body);
            }
        }
    }
}
