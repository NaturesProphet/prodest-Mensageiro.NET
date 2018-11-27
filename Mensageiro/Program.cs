using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System;
using System.Threading;


namespace Mensageiro
{
    class Program
    {
        protected static AutoResetEvent semaphore = new AutoResetEvent(false);
        protected static TimeSpan receiveTimeout = TimeSpan.FromSeconds(10);

        public static void Main(string[] args)
        {
            // Example connection strings:W
            //    activemq:tcp://activemqhost:61616
            //    stomp:tcp://activemqhost:61613
            //    ems:tcp://tibcohost:7222
            //    msmq://localhost
            // Uri connecturi = new Uri("activemq:tcp://dadosrast-gvbus.geocontrol.com.br:24987");
            Uri connecturi;

            if (args.Length > 0 && args[0] == "teste")
            {
                Console.ForegroundColor = System.ConsoleColor.Blue;
                Console.WriteLine("\nIniciando em modo de teste.\nAcessando o Apache de testes local...\n");
                Console.ResetColor();
                connecturi = new Uri("activemq:tcp://localhost:61613");
            }
            else
            {
                connecturi = new Uri("failover:(tcp://dadosrast-gvbus.geocontrol.com.br:24987)");
            }

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
            try
            {
                if (receivedMsg is ActiveMQMapMessage)
                {
                    var message = receivedMsg as ActiveMQMapMessage;
                    // if (((ActiveMQMapMessage)receivedMsg).GetObjectProperty("IDENTIFIER").ToString() == "303652")
                    // {
                    String cartinha = $"{DateTime.Now.ToString("O")} {message.Body["IGNICAO"]}; {message.Body["LATITUDE"]}; {message.Body["LONGITUDE"]}; {message.Body["CURSO"]}; {message.Body["ED1_ACIONADA"]}; {message.Body["ED2_ACIONADA"]}; {message.Body["ED3_ACIONADA"]}; {message.Body["ED4_ACIONADA"]}; {message.Body["ROTULO"]}; {message.Body["SD1_ACIONADA"]}; {message.Body["SD2_ACIONADA"]}; {message.Body["SD3_ACIONADA"]}; {message.Body["SD4_ACIONADA"]}; {message.Body["STATUS_GPS"]}; {message.Body["VELOCIDADE"]}";
                    Console.ForegroundColor = System.ConsoleColor.Blue;

                    Console.WriteLine("[  ACTIVEMQ   ]   " + cartinha);
                    Console.ResetColor();
                    try
                    {
                        coelho.send(cartinha);
                    }
                    catch (Exception co)
                    {
                        Console.ForegroundColor = System.ConsoleColor.Red;
                        Console.WriteLine("\n################################################\n");
                        Console.WriteLine("ERRO AO ENVIAR PRO RABBIT\n" + co.Message);
                        Console.WriteLine("\n################################################\n");
                        Console.ResetColor();
                    }
                    // }
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
