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

                default:
                    Console.ForegroundColor = System.ConsoleColor.Red;
                    Console.WriteLine("\n################################################\n");
                    Console.WriteLine("ERRO NAO ESPECIFICADO");
                    Console.WriteLine("\n################################################\n");
                    Console.ResetColor();
                    break;
            }
        }
    }
}
