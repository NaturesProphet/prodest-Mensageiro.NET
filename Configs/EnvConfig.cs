using System;
namespace Mensageiro
{
    public class EnvConfig
    {
        private string apacheUrlConnection;
        private string apacheUser;
        private string apachePassword;
        private string apacheTopic;
        private string rabbitUrlConnection;
        private string rabbitTopic;
        private string rabbitRoutingKey;
        private bool isProduction;




        public String getApacheUrlConnection()
        {
            this.apacheUrlConnection = Environment.GetEnvironmentVariable("APACHE_URL_CONNECTION");
            if (this.apacheUrlConnection is null)
            {
                this.apacheUrlConnection = "activemq:tcp://localhost:61613";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("URL do apache não detectada no ambiente. usando url local.");
                Console.ResetColor();
            }
            return this.apacheUrlConnection;
        }

        public String getApacheUser()
        {
            this.apacheUser = Environment.GetEnvironmentVariable("APACHE_USER");
            if (this.apacheUser is null)
            {
                this.apacheUser = "guest";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Usuario do apache não detectado no ambiente. usando o default 'guest'");
                Console.ResetColor();
            }
            return this.apacheUser;
        }

        public String getApachePassword()
        {
            this.apachePassword = Environment.GetEnvironmentVariable("APACHE_PASSWORD");
            if (this.apachePassword is null)
            {
                this.apachePassword = "guest";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Senha do apache não detectada no ambiente. usando o default 'guest'");
                Console.ResetColor();
            }
            return this.apachePassword;
        }

        public String getApacheTopic()
        {
            this.apacheTopic = Environment.GetEnvironmentVariable("APACHE_TOPIC");
            if (this.apacheTopic is null)
            {
                this.apacheTopic = "TOPICO";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Topico do apache não detectado no ambiente. usando o default 'guest'");
                Console.ResetColor();
            }
            return this.apacheTopic;
        }

        public String getRabbitUrlConnection()
        {
            this.rabbitUrlConnection = Environment.GetEnvironmentVariable("RABBIT_URL_CONNECTION");
            if (this.rabbitUrlConnection is null)
            {
                this.rabbitUrlConnection = "localhost";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("URL do RabbitMQ não detectada no ambiente. usando url local.");
                Console.ResetColor();
            }
            return this.rabbitUrlConnection;
        }

        public String getRabbitTopic()
        {
            this.rabbitTopic = Environment.GetEnvironmentVariable("RABBIT_TOPIC");
            if (this.rabbitTopic is null)
            {
                this.rabbitTopic = "CETURB";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Topico do RabbitMQ não detectado no ambiente. usando o default 'CETURB'");
                Console.ResetColor();
            }
            return this.rabbitTopic;
        }

        public String getRabbitRoutingKey()
        {
            this.rabbitRoutingKey = Environment.GetEnvironmentVariable("RABBIT_ROUTING_KEY");
            if (this.rabbitRoutingKey is null)
            {
                this.rabbitRoutingKey = "CETURB";
                Console.ForegroundColor = System.ConsoleColor.Yellow;
                Console.WriteLine("Chave de rota do RabbitMQ não detectada no ambiente. usando o default 'CETURB'");
                Console.ResetColor();
            }
            return this.rabbitRoutingKey;
        }

        public bool isProductionEnv()
        {
            String stringValue = this.rabbitRoutingKey = Environment.GetEnvironmentVariable("NODE_ENV");
            this.isProduction = (stringValue.Equals("production"));
            return this.isProduction;
        }
    }
}
