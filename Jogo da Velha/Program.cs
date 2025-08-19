using System;

class Program
{
    static string[,] tabuleiro = new string[3, 3];

    static void Main(string[] args)
    {
        int opcao = 0;

        do
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("                      Jogo da Velha");
            Console.WriteLine("==========================================================");
            Console.WriteLine("Pressione a tecla correspondente a sua escolha:");
            Console.WriteLine("1 - Jogar contra outro jogador");
            Console.WriteLine("2 - Jogar contra o computador (nível fácil)");
            Console.WriteLine("3 - Jogar contra o computador (nível difícil)");
            Console.WriteLine("4 - Ver Ranking");
            Console.WriteLine("5 - Sair");
            Console.WriteLine("==========================================================");
            Console.Write("Opção: ");

            int.TryParse(Console.ReadLine(), out opcao);
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Você escolheu jogar contra outro jogador.");
                    InicializarTabuleiro();
                    MostrarTabuleiro();
                    Console.ReadKey();
                    break;
                case 2:
                    Console.WriteLine("Você escolheu jogar contra o computador (nível fácil).");
                    InicializarTabuleiro();
                    MostrarTabuleiro();
                    Console.ReadKey();
                    break;
                case 3:
                    Console.WriteLine("Você escolheu jogar contra o computador (nível difícil).");
                    InicializarTabuleiro();
                    MostrarTabuleiro();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine("Você escolheu ver o ranking.");
                    // Ranking será implementado depois
                    Console.ReadKey();
                    break;
                case 5:
                    Console.WriteLine("Saindo do jogo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.Clear();

        } while (opcao != 5);
    }

    static void InicializarTabuleiro()
    {
        int posicao = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                tabuleiro[i, j] = posicao.ToString(); // coloca números de 1-9
                posicao++;
            }
        }
    }

    static void MostrarTabuleiro()
    {
        Console.WriteLine("Jogo da Velha");
        Console.WriteLine();

        for (int i = 0; i < 3; i++)
        {
            Console.Write(" ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(tabuleiro[i, j]);
                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("---+---+---");
        }
        Console.WriteLine();
    }
}
