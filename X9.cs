using System;

/**
Fiz esta classe para obter um código mais limpo no programa principal.
O objetivo é apenas printar informações no terminal.
*/
namespace Mensageiro
{
    static class X9
    {
        public static void OQueRolouNaParada(Exception e, int NumeroMagico)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("ERRO AO ENVIAR PRO RABBIT\n" + e.Message);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;

                case 2:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine($"Erro ao processar o envio de mensagens: {e.Message}");
                    Console.WriteLine(e.StackTrace);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;

                case 3:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("Falha na construcao do JSON (Mensagem.cs)");
                    Console.WriteLine("Chaves LATITUDE e LONGITUDE nao encontrados na mensagem");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;

                case 4:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("Falhou ao tentar se conectar ao RabbitMQ");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;

                case 5:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("Falhou ao tentar Enviar mensagens ao RabbitMQ");
                    Console.WriteLine(e.Message);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;

                default:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("ERRO NAO ESPECIFICADO");
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;
            }
        }

        public static void showInfo(int NumeroMagico, String msg1, String msg2, String msg3)
        {
            switch (NumeroMagico)
            {
                case 1:
                    Console.ForegroundColor = System.ConsoleColor.Green;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("Mensagem processada:" + msg1);
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;
            }

        }
    }
}
