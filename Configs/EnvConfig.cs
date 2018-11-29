using System;
namespace Mensageiro
{
    public class EnvConfig
    {
        private string apacheUrlConnection;
        private string rabbitUrlConnection;
        private string rabbitTopic;


        public String getApacheUrlConnection()
        {
            String url = Environment.GetEnvironmentVariable("APACHE_URL_CONNECTION");
            if (url is null)
            {
                url = "activemq:tcp://localhost:61613";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("URL do apache não detectada no ambiente. usando url local.");
                Console.ResetColor();
            }
            return url;
        }

        public String getRabbitUrlConnection()
        {
            string url = Environment.GetEnvironmentVariable("RABBIT_URL_CONNECTION");
            if (url is null)
            {
                url = "localhost";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("URL do RabbitMQ não detectada no ambiente. usando url local.");
                Console.ResetColor();
            }
            return url;
        }

        public String getRabbitTopic()
        {
            string topic = Environment.GetEnvironmentVariable("RABBIT_TOPIC");
            if (topic is null)
            {
                topic = "CETURB";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Topico do RabbitMQ não detectado no ambiente. usando o default 'CETURB'");
                Console.ResetColor();
            }
            return topic;
        }

    }
}
