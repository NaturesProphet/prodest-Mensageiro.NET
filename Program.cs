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

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Iniciando nova conexão com " + config.getApacheUrlConnection());
            IConnectionFactory factory = new ConnectionFactory(config.getApacheUrlConnection());
            IConnection connection = factory.CreateConnection(config.getApacheUser(), config.getApachePassword());
            ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            Console.WriteLine("ACK MODE: " + connection.AcknowledgementMode);
            IDestination destination = session.GetTopic(config.getApacheTopic());
            Console.WriteLine("Ouvindo no topico: " + destination);
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
                    String mensagemAoRabbit = JsonConvert.SerializeObject(conteudoEnvio);
                    try
                    {
                        carteiro.send(mensagemAoRabbit);
                    }
                    catch (Exception co)
                    {
                        Console.ForegroundColor = System.ConsoleColor.Red;
                        Console.WriteLine("\n################################################\n");
                        Console.WriteLine("ERRO AO ENVIAR PRO RABBIT\n" + co.Message);
                        Console.WriteLine("\n################################################\n");
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine("\n################################################\n");
                Console.WriteLine($"Erro ao processar o envio de mensagens: {e.Message}");
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("\n################################################\n");
                Console.ResetColor();
            }
        }
    }
}
