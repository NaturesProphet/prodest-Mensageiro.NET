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
            try
            {
                this.connection = factory.CreateConnection();
                this.channel = connection.CreateModel();
                channel.ExchangeDeclare(exchange: RabbitTopic, type: "topic", durable: true);
            }
            catch (Exception e)
            {
                X9.OQueRolouNaParada(e, 4);
            }
        }

        private ConnectionFactory factory;
        private String RabbitHost;
        private String RabbitTopic;
        private String KeySQL;
        private String KeyMongo;
        private IConnection connection;
        private IModel channel;

        /**
        @param String em formato json com os dados de uma mensagem real-time de um ônibus qualquer.
        Este método envia duas mensagens ao tópico da ceturb dentro do nosso RabbitMQ,
        uma delas vai para a fila do popMQ (SQL-Server) e a outra para a fila do popMongo (MongoDB)
        */
        public void send(String dadosEnvio)
        {
            {
                try
                {
                    var conteudoMensagem = Encoding.UTF8.GetBytes(dadosEnvio);
                    //envia uma cópia para a fila do SQL-Server
                    this.channel.BasicPublish(exchange: RabbitTopic, routingKey: this.KeySQL, basicProperties: null, body: conteudoMensagem);
                    //envia outra cópia para a fila do MongoDB
                    this.channel.BasicPublish(exchange: RabbitTopic, routingKey: this.KeyMongo, basicProperties: null, body: conteudoMensagem);

                }
                catch (Exception e)
                {
                    X9.OQueRolouNaParada(e, 5);
                }
            }
        }
    }
}
