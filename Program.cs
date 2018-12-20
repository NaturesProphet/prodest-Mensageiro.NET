using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Mensageiro
{
    class Program
    {
        protected static AutoResetEvent semaphore = new AutoResetEvent(false);
        protected static EnvConfig config = new EnvConfig();
        protected static String apacheUrlConnection;
        protected static String apacheUser;
        protected static String apachePassword;
        protected static String apacheTopic;


        public static void Main(string[] args)
        {
            apacheUrlConnection = config.getApacheUrlConnection();
            apacheUser = config.getApacheUser();
            apachePassword = config.getApachePassword();
            apacheTopic = config.getApacheTopic();

            Console.Clear();
            Console.WriteLine("Iniciando nova conexão com " + apacheUrlConnection);
            IConnectionFactory factory = new ConnectionFactory(apacheUrlConnection);
            IConnection connection = factory.CreateConnection(apacheUser, apachePassword);
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            Console.WriteLine("ACK MODE: " + connection.AcknowledgementMode);
            IDestination destination = session.GetTopic(apacheTopic);
            Console.WriteLine("Ouvindo o Apache no topico: " + destination);
            IMessageConsumer consumer = session.CreateConsumer(destination);
            connection.Start();
            consumer.Listener += new MessageListener(OnMessage);
            semaphore.WaitOne();
        }

        protected static void OnMessage(IMessage mensagemDoApache)
        {
            Carteiro carteiro = new Carteiro();
            try
            {
                if (mensagemDoApache is ActiveMQMapMessage)
                {
                    var mensagemRecebida = mensagemDoApache as ActiveMQMapMessage;
                    var chaves = mensagemRecebida.Body.Keys;
                    Mensagem conteudoEnvio = new Mensagem();
                    foreach (var chave in chaves)
                    {
                        conteudoEnvio.add(chave.ToString(), mensagemRecebida.Body[chave.ToString()]);
                    }
                    conteudoEnvio.format();
                    String mensagemAoRabbit = JsonConvert.SerializeObject(conteudoEnvio);
                    try
                    {
                        carteiro.send(mensagemAoRabbit);
                    }
                    catch (Exception co)
                    {
                        X9.OQueRolouNaParada(co, 1);
                    }
                }
            }
            catch (Exception e)
            {
                X9.OQueRolouNaParada(e, 2);
            }
        }
    }
}
