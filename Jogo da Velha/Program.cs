using System;

class Program
{
    static string[,] tabuleiro = new string[3, 3];

    static void Main(string[] args)
    {
        int opcao = 0;

        do
        {
            Creditos();
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
                    JogarContraJogador();
                    break;
                case 2:
                    Console.WriteLine("Você escolheu jogar contra o computador (nível fácil).");
                    JogoContraComputadorFacil();
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

    static void Creditos()
    {
        Console.WriteLine("==========================================================");
        Console.WriteLine("Turma: 2ESCN");
        Console.WriteLine("==========================================================");
        Console.WriteLine("Desenvolvedores:");
        Console.WriteLine("Ariane da Silva Archanjo - 2025106857");
        Console.WriteLine("Lucas Vinicius Barros Dias - 2025105450 ");
        Console.WriteLine("Pedro Henrique Kafka Zaratino - 2025105057");
        Console.WriteLine("Caio Melo Canhetti - 2025104636");
        Console.WriteLine("Rafael Martins Schreurs Sales - 2025105454");
        Console.WriteLine("Matheus Sizanoski Figueiredo - 2025105007");
        Console.WriteLine("==========================================================");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
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

    static void JogarContraJogador()
    {
        InicializarTabuleiro();
        string JogadorAtual = "X";
        int jogadas = 0;
        bool vitoria = false;

        while (jogadas < 9 && !vitoria)
        {
            Console.Clear();
            MostrarTabuleiro();
            Console.WriteLine($"Vez do jogador {JogadorAtual}. Escolha uma posição (1-9): ");
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
        }

        Console.Clear();
        MostrarTabuleiro();

        if (vitoria)
            Console.WriteLine($"Jogador {JogadorAtual} venceu!");
        else
            Console.WriteLine("Empate!");

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    static bool VerificarVencedor(string jogador)
    {
        // Linhas
        for (int i = 0; i < 3; i++)
        {
            if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                return true;
        }

        // Colunas
        for (int j = 0; j < 3; j++)
        {
            if (tabuleiro[0, j] == jogador && tabuleiro[1, j] == jogador && tabuleiro[2, j] == jogador)
                return true;
        }

        // Diagonais
        if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            return true;

        if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            return true;

        return false;
    }

    static void JogoContraComputadorFacil()
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
            Console.Clear();
            MostrarTabuleiro();

            if (jogadorAtual == jogadorHumano)
            {
                Console.WriteLine($"Sua vez (X). Escolha uma posição (1-9): ");
                string entrada = Console.ReadLine();
                int.TryParse(entrada, out posicao);

                linha = (posicao - 1) / 3;
                coluna = (posicao - 1) % 3;

                if (posicao >= 1 && posicao <= 9 && tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O")
                {
                    tabuleiro[linha, coluna] = jogadorHumano;
                    jogadas++;
                    vitoria = VerificarVencedor(jogadorHumano);
                    jogadorAtual = jogadorComputador;
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente.");
                    Console.ReadKey();
                }
            }
            else if (jogadorAtual == jogadorComputador)
            {
                // computador joga aleatório
                do
                {
                    posicao = random.Next(1, 10);
                    linha = (posicao - 1) / 3;
                    coluna = (posicao - 1) % 3;
                }
                while (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O");

                tabuleiro[linha, coluna] = jogadorComputador;
                jogadas++;
                vitoria = VerificarVencedor(jogadorComputador);
                jogadorAtual = jogadorHumano;
                Console.WriteLine($"Computador escolheu a posição {posicao}. Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        Console.Clear();
        MostrarTabuleiro();
        if (vitoria)
        {
            Console.WriteLine(jogadorAtual == jogadorHumano ? "Computador venceu!" : "Você venceu!");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Empate!");
            Console.ReadKey();
        }
    }
}
