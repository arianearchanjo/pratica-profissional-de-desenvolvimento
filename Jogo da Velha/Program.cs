using System;
using System.Threading;
using System.Collections.Generic; 
class Program
{
    static string[,] tabuleiro = new string[3, 3];

    static Dictionary<string, int> ranking = new Dictionary<string, int>()
    {
        { "Jogador X", 0 },
        { "Jogador O", 0 },
        { "Computador", 0 },
        { "Empates", 0 }
    };

    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        int opcao = 0;

        do
        {
            Creditos();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==========================================================");
            Console.WriteLine("                      Jogo da Velha");
            Console.WriteLine("==========================================================");
            Console.ResetColor();
            Console.WriteLine("Pressione a tecla correspondente a sua escolha:");
            Console.WriteLine("1 - Jogar contra outro jogador");
            Console.WriteLine("2 - Jogar contra o computador (nível fácil)");
            Console.WriteLine("3 - Jogar contra o computador (nível difícil)");
            Console.WriteLine("4 - Ver Ranking");
            Console.WriteLine("5 - Sair");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==========================================================");
            Console.ResetColor();
            Console.Write("Opção: ");

            int.TryParse(Console.ReadLine(), out opcao);
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    JogarContraJogador();
                    break;
                case 2:
                    Console.WriteLine("Você escolheu jogar contra o computador (nível fácil).");
                    JogoContraComputadorFacil();
                    break;
                case 3:
                    Console.WriteLine("Você escolheu jogar contra o computador (nível difícil).");
                    JogoContraComputadorDificil();
                    break;
                case 4:
                    ExibirRanking();
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
                    Console.ReadKey();
                    Console.Clear();
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

    static void Creditos() //Ariane
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==========================================================");
        Console.WriteLine("Turma: 2ESCN");
        Console.WriteLine("==========================================================");
        Console.ResetColor();
        Console.WriteLine("Desenvolvedores:");
        Console.WriteLine("Ariane da Silva Archanjo - 2025106857");
        Console.WriteLine("Caio Melo Canhetti - 2025104636");
        Console.WriteLine("Lucas Vinicius Barros Dias - 2025105450 ");
        Console.WriteLine("Matheus Sizanoski Figueiredo - 2025105007");
        Console.WriteLine("Pedro Henrique Kafka Zaratino - 2025105057");
        Console.WriteLine("Rafael Martins Schreurs Sales - 2025105454");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==========================================================");
        Console.ResetColor();
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }

    static void InicializarTabuleiro() //Ariane
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

    static void MostrarTabuleiro() //Ariane
    {
        Console.WriteLine("Jogo da Velha\n");

        for (int i = 0; i < 3; i++)
        {
            Console.Write(" ");
            for (int j = 0; j < 3; j++)
            {
                // Colore X azul e O vermelho
                if (tabuleiro[i, j] == "X")
                    Console.ForegroundColor = ConsoleColor.Blue;
                else if (tabuleiro[i, j] == "O")
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ResetColor();

                Console.Write(tabuleiro[i, j]);
                Console.ResetColor();

                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("---+---+---");
        }
        Console.WriteLine();
    }

    static void MostrarTurno(string jogador) //Ariane
    {
        for (int i = 0; i < 2; i++)
        {
            Console.Clear();
            MostrarTabuleiro();

            Console.ForegroundColor = jogador == "X" ? ConsoleColor.Blue : ConsoleColor.Red;
            Console.WriteLine($"Vez do jogador {jogador}");
            Console.ResetColor();
            Thread.Sleep(500);

            Console.Clear();
            MostrarTabuleiro();
            Thread.Sleep(1000);
        }
    }

    static void JogarContraJogador() //Ariane
    {
        InicializarTabuleiro();
        string JogadorAtual = "X";
        int jogadas = 0;
        bool vitoria = false;

        while (jogadas < 9 && !vitoria)
        {
            MostrarTurno(JogadorAtual);

            Console.WriteLine($"Escolha uma posição (1-9): ");
            string entrada = Console.ReadLine();
            int.TryParse(entrada, out int posicao);

            if (posicao >= 1 && posicao <= 9)
            {
                int linha = (posicao - 1) / 3;
                int coluna = (posicao - 1) % 3;
                if (tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O")
                {
                    tabuleiro[linha, coluna] = JogadorAtual;
                    jogadas++;
                    Console.Beep(500, 150);

                    // Verificar vitória
                    vitoria = VerificarVencedor(JogadorAtual);

                    if (!vitoria)
                        JogadorAtual = (JogadorAtual == "X") ? "O" : "X"; // Alternar jogador
                }
                else
                {
                    Console.WriteLine("Posição já ocupada. Tente novamente.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Tente novamente.");
                Console.ReadKey();
            }
            Console.Clear();
        }

        MostrarTabuleiro();

        if (vitoria)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Jogador {JogadorAtual} venceu!");
            Console.Beep(800, 400);

            // Atualiza ranking PvP
            if (JogadorAtual == "X")
                ranking["Jogador X"]++;
            else
                ranking["Jogador O"]++;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Empate!");
            Console.Beep(600, 400);
            ranking["Empates"]++;
        }

        Console.Clear();
        Menu();
    }

    static bool VerificarVencedor(string jogador) // Ariane
    {
        // Linhas
        for (int i = 0; i < 3; i++)
            if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                return true;

        // Colunas
        for (int j = 0; j < 3; j++)
            if (tabuleiro[0, j] == jogador && tabuleiro[1, j] == jogador && tabuleiro[2, j] == jogador)
                return true;

        // Diagonais
        if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            return true;

        if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            return true;

        return false;
    }

    static void JogoContraComputadorFacil() //Pedro e Ariane
    {
        InicializarTabuleiro();
        string jogadorHumano = "X";
        string jogadorComputador = "O";
        string jogadorAtual = jogadorHumano;
        int jogadas = 0;
        bool vitoria = false;
        int posicao;
        int linha, coluna;

        Random random = new Random();

        while (jogadas < 9 && !vitoria)
        {
            if (jogadorAtual == jogadorHumano)
            {
                MostrarTurno(jogadorHumano);
                Console.WriteLine("Sua vez (X). Escolha uma posição (1-9): ");
                string entrada = Console.ReadLine();
                int.TryParse(entrada, out posicao);

                linha = (posicao - 1) / 3;
                coluna = (posicao - 1) % 3;

                if (posicao >= 1 && posicao <= 9 && tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O")
                {
                    tabuleiro[linha, coluna] = jogadorHumano;
                    jogadas++;
                    Console.Beep(500, 150);
                    vitoria = VerificarVencedor(jogadorHumano);
                    jogadorAtual = jogadorComputador;
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente.");
                    Console.ReadKey();
                }
            }
            else
            {
                MostrarTurno(jogadorComputador);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Computador pensando");
                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(800);
                    Console.Write(".");
                }
                Console.WriteLine();

                do
                {
                    posicao = random.Next(1, 10);
                    linha = (posicao - 1) / 3;
                    coluna = (posicao - 1) % 3;
                }
                while (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O");

                tabuleiro[linha, coluna] = jogadorComputador;
                jogadas++;
                Console.Beep(400, 200);
                vitoria = VerificarVencedor(jogadorComputador);
                jogadorAtual = jogadorHumano;
                Console.WriteLine($"Computador escolheu a posição {posicao}.");
                Thread.Sleep(1000);
            }

            Console.Clear();
            MostrarTabuleiro();
            Thread.Sleep(800);
        }

        if (vitoria)
        {
            if (jogadorAtual == jogadorHumano)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Computador venceu!");
                Console.Beep(700, 400);
                ranking["Computador"]++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Você venceu!");
                Console.Beep(900, 400);
                ranking["Jogador X"]++;
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Empate!");
            Console.Beep(600, 400);
            ranking["Empates"]++;
        }

        Console.Clear();
        Menu();
    }

    static void escolherJogadaMinimax() //Caio e Rafael
    {

    }

    static void JogoContraComputadorDificil() //Caio e Rafael
    {

    }
    static void ExibirRanking()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("======= Ranking =======");
        Console.ResetColor();

        foreach (var item in ranking)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }

        Console.WriteLine();
    }
}