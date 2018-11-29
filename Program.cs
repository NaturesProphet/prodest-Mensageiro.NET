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
        protected static TimeSpan receiveTimeout = TimeSpan.FromSeconds(10);


        public static void Main(string[] args)
        {
            EnvConfig config = new EnvConfig();
            String connecturi = config.getApacheUrlConnection();
            //new Uri("failover:(tcp://dadosrast-gvbus.geocontrol.com.br:24987)");


            Console.WriteLine("About to connect to " + connecturi);

            // NOTE: ensure the nmsprovider-activemq.config file exists in the executable folder.

            IConnectionFactory factory = new ConnectionFactory(connecturi);

            // using (IConnection connection = factory.CreateConnection("rast_prodest", "ZgFVt5kPhhV2"))
            using (IConnection connection = factory.CreateConnection("rast_prodest", "ZgFVt5kPhhV2"))
            {
                using (ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge))
                {
                    IDestination destination = session.GetTopic("DadosRastreioPRODEST");

                    Console.WriteLine("Using destination: " + destination);

                    // Create a consumer and producer
                    using (IMessageConsumer consumer = session.CreateConsumer(destination))
                    {
                        // Start the connection so that messages will be processed.
                        connection.Start();
                        // producer.Persistent = true;
                        consumer.Listener += new MessageListener(OnMessage);

                        // Wait for the message

                        semaphore.WaitOne();
                    }
                }
            }
        }

        protected static void OnMessage(IMessage receivedMsg)
        {
            Coelho coelho = new Coelho();
            List<String> lista = new List<String>();
            try
            {
                if (receivedMsg is ActiveMQMapMessage)
                {
                    var message = receivedMsg as ActiveMQMapMessage;
                    var keys = message.Body.Keys;

                    Mensagem mensagem2ActiveMQ = new Mensagem();

                    foreach (var key in keys)
                    {
                        mensagem2ActiveMQ.add(key.ToString(), message.Body[key.ToString()]);
                    }

                    String mensagem2Rabbit = JsonConvert.SerializeObject(mensagem2ActiveMQ);

                    try
                    {
                        coelho.send(mensagem2Rabbit);
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
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
