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

    static void Creditos()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("==========================================================");
        Console.WriteLine("Turma: 2ESCN");
        Console.WriteLine("==========================================================");
        Console.ResetColor();
        Console.WriteLine("Desenvolvedores:");
        Console.WriteLine("Ariane da Silva Archanjo - 2025106857");
        Console.WriteLine("Caio Melo Canhetti - 2025104636");
        Console.WriteLine("Lucas Vinicius Barros Dias - 2025105450");
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

    static void InicializarTabuleiro()
    {
        int posicao = 1;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                tabuleiro[i, j] = posicao++.ToString();
    }

    static void MostrarTabuleiro()
    {
        ExibirRanking();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("========================");
        Console.WriteLine("    Jogo da Velha");
        Console.WriteLine("========================");
        Console.ResetColor();
        Console.WriteLine();


        for (int i = 0; i < 3; i++)
        {
            Console.Write(" ");
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] == "X") Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (tabuleiro[i, j] == "O") Console.ForegroundColor = ConsoleColor.Red;
                else Console.ResetColor();

                Console.Write(tabuleiro[i, j]);
                Console.ResetColor();

                if (j < 2) Console.Write(" | ");
            }
            Console.WriteLine();
            if (i < 2) Console.WriteLine("---+---+---");
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("========================");
        Console.ResetColor();
        Console.WriteLine();
    }

    static void MostrarTurno(string jogador)
    {
        for (int i = 0; i < 2; i++)
        {
            Console.Clear();
            MostrarTabuleiro();
            Console.ForegroundColor = jogador == "X" ? ConsoleColor.DarkYellow : ConsoleColor.Red;
            Console.WriteLine($"Vez do jogador {jogador}");
            Console.ResetColor();
            Thread.Sleep(500);
            Console.Clear();
            MostrarTabuleiro();
            Thread.Sleep(1000);
        }
    }

    static void JogarContraJogador()
    {
        InicializarTabuleiro();
        string jogadorAtual = "X";
        int jogadas = 0;
        bool vitoria = false;

        while (jogadas < 9 && !vitoria)
        {
            MostrarTurno(jogadorAtual);

            Console.WriteLine("Escolha uma posição (1-9): ");
            string entrada = Console.ReadLine();
            int.TryParse(entrada, out int posicao);

            if (posicao >= 1 && posicao <= 9)
            {
                int linha = (posicao - 1) / 3;
                int coluna = (posicao - 1) % 3;
                if (tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O")
                {
                    tabuleiro[linha, coluna] = jogadorAtual;
                    jogadas++;
                    Console.Beep(500, 150);
                    vitoria = VerificarVencedor(jogadorAtual);

                    if (!vitoria)
                        jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
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
            if (jogadorAtual == "X")
                Console.ForegroundColor = ConsoleColor.DarkYellow; 
            else
                Console.ForegroundColor = ConsoleColor.Red;    

            Console.WriteLine($"Jogador {jogadorAtual} venceu!");
            ranking[jogadorAtual == "X" ? "Jogador X" : "Jogador O"]++;
            Console.Beep(800, 400);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Empate!");
            ranking["Empates"]++;
            Console.Beep(600, 400);
        }

        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }

    static bool VerificarVencedor(string jogador)
    {
        for (int i = 0; i < 3; i++)
            if ((tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador) ||
                (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador))
                return true;

        if ((tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador) ||
            (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador))
            return true;

        return false;
    }

    static void JogoContraComputadorFacil()
    {
        InicializarTabuleiro();
        string jogadorHumano = "X";
        string jogadorComp = "O";
        string jogadorAtual = jogadorHumano;
        int jogadas = 0;
        bool vitoria = false;

        Random rnd = new Random();
        int posicao, linha, coluna;

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
                    jogadorAtual = jogadorComp;
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente.");
                    Console.ReadKey();
                }
            }
            else
            {
                MostrarTurno(jogadorComp);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Computador pensando");
                for (int i = 0; i < 3; i++) { Thread.Sleep(800); Console.Write("."); }
                Console.WriteLine();

                do
                {
                    posicao = rnd.Next(1, 10);
                    linha = (posicao - 1) / 3;
                    coluna = (posicao - 1) % 3;
                } while (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O");

                tabuleiro[linha, coluna] = jogadorComp;
                jogadas++;
                Console.Beep(400, 200);
                vitoria = VerificarVencedor(jogadorComp);
                jogadorAtual = jogadorHumano;
                Console.WriteLine($"Computador escolheu a posição {posicao}.");
                Thread.Sleep(1000);
            }

            Console.Clear();
            MostrarTabuleiro();
            Thread.Sleep(500);
        }

        if (vitoria)
        {
            if (jogadorAtual == jogadorHumano)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Computador venceu!");
                ranking["Computador"]++;
                Console.Beep(700, 400);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Você venceu!");
                ranking["Jogador X"]++;
                Console.Beep(900, 400);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Empate!");
            ranking["Empates"]++;
            Console.Beep(600, 400);
        }

        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }

    static void JogoContraComputadorDificil()
    {
        InicializarTabuleiro();
        string jogadorHumano = "X";
        string jogadorComp = "O";
        string jogadorAtual = jogadorHumano;
        int jogadas = 0;
        bool vitoria = false;
        Random rnd = new Random();

        while (jogadas < 9 && !vitoria)
        {
            if (jogadorAtual == jogadorHumano)
            {
                MostrarTurno(jogadorHumano);
                Console.WriteLine("Sua vez (X). Escolha uma posição (1-9): ");
                string entrada = Console.ReadLine();
                int.TryParse(entrada, out int posicao);
                int linha = (posicao - 1) / 3;
                int coluna = (posicao - 1) % 3;

                if (posicao >= 1 && posicao <= 9 && tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O")
                {
                    tabuleiro[linha, coluna] = jogadorHumano;
                    jogadas++;
                    Console.Beep(500, 150);
                    vitoria = VerificarVencedor(jogadorHumano);
                    jogadorAtual = jogadorComp;
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente.");
                    Console.ReadKey();
                }
            }
            else
            {
                MostrarTurno(jogadorComp);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Computador pensando");
                for (int i = 0; i < 3; i++) { Thread.Sleep(600); Console.Write("."); }
                Console.WriteLine();

                int posicaoEscolhida;
                if (rnd.Next(0, 100) < 80)
                    posicaoEscolhida = EscolherJogadaMinimax(jogadorComp, jogadorHumano);
                else
                {
                    int pos, lin, col;
                    do
                    {
                        pos = rnd.Next(1, 10);
                        lin = (pos - 1) / 3;
                        col = (pos - 1) % 3;
                    } while (tabuleiro[lin, col] == "X" || tabuleiro[lin, col] == "O");
                    posicaoEscolhida = pos;
                }

                int linhaC = (posicaoEscolhida - 1) / 3;
                int colunaC = (posicaoEscolhida - 1) % 3;
                tabuleiro[linhaC, colunaC] = jogadorComp;
                jogadas++;
                Console.Beep(400, 200);
                vitoria = VerificarVencedor(jogadorComp);
                jogadorAtual = jogadorHumano;

                Console.WriteLine($"Computador escolheu a posição {posicaoEscolhida}.");
                Thread.Sleep(1000);
            }

            Console.Clear();
            MostrarTabuleiro();
            Thread.Sleep(500);
        }

        if (vitoria)
        {
            if (jogadorAtual == jogadorHumano)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Computador venceu!");
                ranking["Computador"]++;
                Console.Beep(700, 400);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Você venceu!");
                ranking["Jogador X"]++;
                Console.Beep(900, 400);
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Empate!");
            ranking["Empates"]++;
            Console.Beep(600, 400);
        }

        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
    }

    static int EscolherJogadaMinimax(string jogadorComp, string jogadorHum)
    {
        int melhorPontuacao = int.MinValue;
        List<int> melhoresJogadas = new List<int>();

        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O")
                {
                    string temp = tabuleiro[i, j];
                    tabuleiro[i, j] = jogadorComp;
                    int pontuacao = Minimax(0, false, jogadorComp, jogadorHum);
                    tabuleiro[i, j] = temp;

                    int pos = i * 3 + j + 1;

                    if (pontuacao > melhorPontuacao)
                    {
                        melhorPontuacao = pontuacao;
                        melhoresJogadas.Clear();
                        melhoresJogadas.Add(pos);
                    }
                    else if (pontuacao == melhorPontuacao)
                        melhoresJogadas.Add(pos);
                }
            }

        Random rnd = new Random();
        return melhoresJogadas[rnd.Next(melhoresJogadas.Count)];
    }

    static int Minimax(int profundidade, bool isMax, string jogadorComp, string jogadorHum)
    {
        if (VerificarVencedor(jogadorComp)) return 5 - profundidade;
        if (VerificarVencedor(jogadorHum)) return -5 + profundidade;

        bool movimentosRestantes = false;
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O")
                    movimentosRestantes = true;

        if (!movimentosRestantes) return 0;

        if (isMax)
        {
            int melhor = int.MinValue;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O")
                    {
                        string temp = tabuleiro[i, j];
                        tabuleiro[i, j] = jogadorComp;
                        melhor = Math.Max(melhor, Minimax(profundidade + 1, false, jogadorComp, jogadorHum));
                        tabuleiro[i, j] = temp;
                    }
            return melhor;
        }
        else
        {
            int melhor = int.MaxValue;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O")
                    {
                        string temp = tabuleiro[i, j];
                        tabuleiro[i, j] = jogadorHum;
                        melhor = Math.Min(melhor, Minimax(profundidade + 1, true, jogadorComp, jogadorHum));
                        tabuleiro[i, j] = temp;
                    }
            return melhor;
        }
    }

    static void ExibirRanking()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("========================");
        Console.WriteLine("        Ranking");
        Console.WriteLine("========================");
        foreach (var item in ranking)
            Console.WriteLine($"{item.Key}: {item.Value}");
        Console.WriteLine("========================");
        Console.ResetColor();
        Console.WriteLine();
    }
}
