using System;                       // Importa o namespace System (Console, Math, etc.)
using System.Threading;             // Importa threading (Thread.Sleep, Thread)
using System.Collections.Generic;   // Importa coleções genéricas (Dictionary, List, etc.)

class Program                         // Declaração da classe principal chamada Program
{
    // Matriz 3x3 que representa o tabuleiro do jogo: índices [linha, coluna]
    static string[,] tabuleiro = new string[3, 3]; // Cria a matriz do tabuleiro (3 linhas x 3 colunas)

    // Dicionário que armazena o ranking/placar do jogo (nome => pontuação)
    static Dictionary<string, int> ranking = new Dictionary<string, int>() // Inicializa o dicionário de ranking
    {
        { "Jogador X", 0 },          // Adiciona chave "Jogador X" com valor inicial 0
        { "Jogador O", 0 },          // Adiciona chave "Jogador O" com valor inicial 0
        { "Computador", 0 },         // Adiciona chave "Computador" com valor inicial 0
        { "Empates", 0 }             // Adiciona chave "Empates" com valor inicial 0
    }; // fim da inicialização do dicionário

    static void Main(string[] args)  // Método principal (ponto de entrada do programa)
    {
        Menu();                     // Chama o método Menu para iniciar a interface/fluxo do jogo
    } // fim do método Main

    static void Menu()              // Método que exibe o menu principal e trata a escolha do usuário
    {
        int opcao = 0;              // Declara variável 'opcao' que armazenará a opção escolhida pelo usuário

        do                           // Inicia um loop do-while que só termina quando opcao == 5
        {
            Creditos();             // Chama o método Creditos que exibe a tela de créditos e pausa

            Console.ForegroundColor = ConsoleColor.Blue; // Define a cor do texto para azul
            Console.WriteLine("=========================================================="); // Imprime linha separadora
            Console.WriteLine("                      Jogo da Velha"); // Imprime título do jogo
            Console.WriteLine("=========================================================="); // Imprime linha separadora
            Console.ResetColor();   // Restaura a cor padrão do console

            Console.WriteLine("Pressione a tecla correspondente a sua escolha:"); // Instrução ao usuário
            Console.WriteLine("1 - Jogar contra outro jogador"); // Opção 1
            Console.WriteLine("2 - Jogar contra o computador (nível fácil)"); // Opção 2
            Console.WriteLine("3 - Jogar contra o computador (nível difícil)"); // Opção 3
            Console.WriteLine("4 - Ver Ranking"); // Opção 4
            Console.WriteLine("5 - Sair"); // Opção 5

            Console.ForegroundColor = ConsoleColor.Blue; // Define cor azul novamente para destaque
            Console.WriteLine("=========================================================="); // Linha separadora
            Console.ResetColor();   // Restaura cor padrão
            Console.Write("Opção: "); // Mostra prompt para o usuário digitar a opção

            int.TryParse(Console.ReadLine(), out opcao); // Lê a entrada do usuário e tenta converter para int; se falhar, opcao = 0
            Console.Clear();        // Limpa o console para preparar a próxima tela

            switch (opcao)          // Estrutura switch para executar ação conforme a opção digitada
            {
                case 1:             // Caso a opção seja 1
                    JogarContraJogador(); // Chama o modo jogador vs jogador
                    break;          // Sai do case 1
                case 2:             // Caso a opção seja 2
                    Console.WriteLine("Você escolheu jogar contra o computador (nível fácil)."); // Mensagem informativa
                    JogoContraComputadorFacil(); // Inicia o modo fácil contra o computador
                    break;          // Sai do case 2
                case 3:             // Caso a opção seja 3
                    Console.WriteLine("Você escolheu jogar contra o computador (nível difícil)."); // Mensagem informativa
                    JogoContraComputadorDificil(); // Inicia o modo difícil contra o computador
                    break;          // Sai do case 3
                case 4:             // Caso a opção seja 4
                    ExibirRanking(); // Mostra o ranking atual
                    Console.WriteLine("Pressione qualquer tecla para voltar ao menu..."); // Pede para o usuário voltar
                    Console.ReadKey(); // Espera o usuário pressionar uma tecla
                    Console.Clear();   // Limpa a tela
                    break;          // Sai do case 4
                case 5:             // Caso a opção seja 5
                    Console.WriteLine("Saindo do jogo..."); // Mensagem de saída
                    break;          // Sai do case 5 (loop terminará depois)
                default:            // Caso a opção não seja 1..5
                    Console.WriteLine("Opção inválida. Tente novamente."); // Mensagem de erro
                    break;          // Sai do default
            } // fim do switch

            Console.Clear();       // Limpa o console antes de repetir ou encerrar

        } while (opcao != 5);     // Continua repetindo o menu enquanto a opção for diferente de 5 (Sair)
    } // fim do método Menu

    static void Creditos()        // Método que imprime os créditos e aguarda o usuário
    {
        Console.ForegroundColor = ConsoleColor.Blue; // Define cor azul para destaque
        Console.WriteLine("=========================================================="); // Linha decorativa
        Console.WriteLine("Turma: 2ESCN");      // Mostra identificação (turma)
        Console.WriteLine("=========================================================="); // Linha decorativa
        Console.ResetColor();     // Restaura cor padrão

        Console.WriteLine("Desenvolvedores:"); // Cabeçalho para a lista de desenvolvedores
        Console.WriteLine("Ariane da Silva Archanjo - 2025106857"); // Imprime desenvolvedor 1
        Console.WriteLine("Caio Melo Canhetti - 2025104636"); // Imprime desenvolvedor 2
        Console.WriteLine("Lucas Vinicius Barros Dias - 2025105450"); // Imprime desenvolvedor 3
        Console.WriteLine("Matheus Sizanoski Figueiredo - 2025105007"); // Imprime desenvolvedor 4
        Console.WriteLine("Pedro Henrique Kafka Zaratino - 2025105057"); // Imprime desenvolvedor 5
        Console.WriteLine("Rafael Martins Schreurs Sales - 2025105454"); // Imprime desenvolvedor 6

        Console.ForegroundColor = ConsoleColor.Blue; // Destaca a linha final em azul
        Console.WriteLine("=========================================================="); // Linha decorativa
        Console.ResetColor();     // Restaura cor padrão

        Console.WriteLine("Pressione qualquer tecla para continuar..."); // Informa que vai pausar
        Console.ReadKey();        // Aguarda o usuário pressionar qualquer tecla
        Console.Clear();          // Limpa o console após a pausa
    } // fim do método Creditos

    static void InicializarTabuleiro() // Método que preenche o tabuleiro com "1".."9" nas células
    {
        int posicao = 1;           // Variável que armazena o número da posição atual (1..9)
        for (int i = 0; i < 3; i++) // Loop pelas 3 linhas (i = 0,1,2)
            for (int j = 0; j < 3; j++) // Loop pelas 3 colunas (j = 0,1,2)
                tabuleiro[i, j] = posicao++.ToString(); // Atribui "1", "2", ... "9" e incrementa posicao
    } // fim do método InicializarTabuleiro

    static void MostrarTabuleiro() // Método que exibe o ranking e desenha o tabuleiro atual no console
    {
        ExibirRanking();           // Chama ExibirRanking para mostrar o placar antes do tabuleiro

        Console.ForegroundColor = ConsoleColor.Blue; // Define cor azul para o cabeçalho
        Console.WriteLine("========================"); // Linha separadora
        Console.WriteLine("    Jogo da Velha"); // Título pequeno do tabuleiro
        Console.WriteLine("========================"); // Linha separadora
        Console.ResetColor();     // Restaura cor padrão
        Console.WriteLine();      // Linha em branco para espaçamento

        for (int i = 0; i < 3; i++) // Percorre as 3 linhas do tabuleiro
        {
            Console.Write(" ");    // Imprime um espaço de margem antes da linha

            for (int j = 0; j < 3; j++) // Percorre as 3 colunas da linha atual
            {
                if (tabuleiro[i, j] == "X") Console.ForegroundColor = ConsoleColor.DarkYellow; // Se for X, cor amarelo escuro
                else if (tabuleiro[i, j] == "O") Console.ForegroundColor = ConsoleColor.Red; // Se for O, cor vermelha
                else Console.ResetColor(); // Se for número, usa cor padrão

                Console.Write(tabuleiro[i, j]); // Imprime o valor da célula (X, O ou número)
                Console.ResetColor(); // Reseta a cor imediatamente após imprimir

                if (j < 2) Console.Write(" | "); // Se não for a última coluna, imprime separador vertical
            } // fim do for j (colunas)

            Console.WriteLine();  // Pula linha após imprimir as colunas
            if (i < 2) Console.WriteLine("---+---+---"); // Se não for a última linha, imprime separador horizontal
        } // fim do for i (linhas)

        Console.WriteLine();    // Linha em branco
        Console.ForegroundColor = ConsoleColor.Blue; // Cabeçalho de fechamento em azul
        Console.WriteLine("========================"); // Linha separadora de fechamento
        Console.ResetColor();   // Reseta cor
        Console.WriteLine();    // Linha em branco
    } // fim do método MostrarTabuleiro

    static void MostrarTurno(string jogador) // Método que faz uma pequena animação indicando o jogador atual
    {
        for (int i = 0; i < 2; i++) // Repete a animação duas vezes para dar destaque
        {
            Console.Clear();       // Limpa a tela para mostrar o tabuleiro atualizado
            MostrarTabuleiro();    // Mostra o tabuleiro atual
            Console.ForegroundColor = jogador == "X" ? ConsoleColor.DarkYellow : ConsoleColor.Red; // Se jogador == "X", usa amarelo; senão vermelho
            Console.WriteLine($"Vez do jogador {jogador}"); // Imprime mensagem informando de quem é a vez
            Console.ResetColor();  // Restaura cor padrão
            Thread.Sleep(500);     // Pausa por 500ms para o usuário ver a mensagem
            Console.Clear();       // Limpa a tela novamente
            MostrarTabuleiro();    // Mostra o tabuleiro outra vez (efeito de "piscar")
            Thread.Sleep(1000);    // Pausa por 1000ms antes de continuar
        } // fim do for que controla a animação
    } // fim do método MostrarTurno

    static void JogarContraJogador() // Modo onde dois jogadores humanos jogam alternadamente
    {
        InicializarTabuleiro();    // Preenche o tabuleiro com 1..9
        string jogadorAtual = "X"; // Define o jogador inicial como "X"
        int jogadas = 0;           // Contador de jogadas realizadas
        bool vitoria = false;      // Flag que indica se houve vitória

        while (jogadas < 9 && !vitoria) // Loop principal: repete enquanto houver jogadas e não houver vencedor
        {
            MostrarTurno(jogadorAtual); // Mostra a animação do turno atual

            Console.WriteLine("Escolha uma posição (1-9): "); // Pede ao jogador para escolher posição
            string entrada = Console.ReadLine(); // Lê a entrada do usuário como string
            int.TryParse(entrada, out int posicao); // Tenta converter para inteiro; se falhar, posicao = 0

            if (posicao >= 1 && posicao <= 9) // Verifica se a posição está no intervalo válido 1..9
            {
                int linha = (posicao - 1) / 3;   // Calcula a linha correspondente (0..2)
                int coluna = (posicao - 1) % 3;  // Calcula a coluna correspondente (0..2)

                if (tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O") // Verifica se célula está livre
                {
                    tabuleiro[linha, coluna] = jogadorAtual; // Marca a célula com o símbolo do jogador atual
                    jogadas++;             // Incrementa o contador de jogadas
                    Console.Beep(500, 150); // Toca um beep curto (feedback sonoro)
                    vitoria = VerificarVencedor(jogadorAtual); // Verifica se a jogada causou vitória

                    if (!vitoria)        // Se não houve vitória com essa jogada...
                        jogadorAtual = (jogadorAtual == "X") ? "O" : "X"; // ...alterna o jogador atual
                }
                else
                {
                    Console.WriteLine("Posição já ocupada. Tente novamente."); // Aviso se célula já ocupada
                    Console.ReadKey();    // Aguarda que o usuário pressione alguma tecla
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Tente novamente."); // Aviso se entrada inválida (não é 1..9)
                Console.ReadKey();    // Aguarda tecla para que o usuário veja a mensagem
            }
            Console.Clear();         // Limpa o console antes da próxima iteração
        } // fim do while principal

        MostrarTabuleiro();         // Exibe o tabuleiro final após o término do jogo

        if (vitoria)                // Se a flag vitoria for verdadeira, houve um vencedor
        {
            if (jogadorAtual == "X") // Se o jogador atual for X...
                Console.ForegroundColor = ConsoleColor.DarkYellow; // ...usa cor DarkYellow
            else
                Console.ForegroundColor = ConsoleColor.Red; // ...senão usa cor Red

            Console.WriteLine($"Jogador {jogadorAtual} venceu!"); // Exibe qual jogador venceu
            ranking[jogadorAtual == "X" ? "Jogador X" : "Jogador O"]++; // Atualiza o ranking do jogador vencedor
            Console.Beep(800, 400);  // Beep de vitória
            Console.ResetColor();    // Reseta a cor do console
        }
        else                        // Se não houve vitória, foi empate
        {
            Console.ForegroundColor = ConsoleColor.Gray; // Muda cor para cinza
            Console.WriteLine("Empate!"); // Exibe mensagem de empate
            ranking["Empates"]++;  // Incrementa contador de empates no ranking
            Console.Beep(600, 400); // Beep de empate
        }

        Console.ResetColor();       // Reseta cor (segurança)
        Thread.Sleep(1000);         // Pausa de 1 segundo antes de limpar
        Console.Clear();            // Limpa o console ao final do jogo
    } // fim do método JogarContraJogador

    static bool VerificarVencedor(string jogador) // Método que verifica se 'jogador' venceu
    {
        for (int i = 0; i < 3; i++) // Loop que verifica linhas e colunas
            if ((tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador) || // Checa linha i completa
                (tabuleiro[0, i] == jogador && tabuleiro[1, i] == jogador && tabuleiro[2, i] == jogador))   // Checa coluna i completa
                return true; // Retorna true se encontrou linha ou coluna completa

        if ((tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador) || // Checa diagonal principal
            (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador))   // Checa diagonal secundária
            return true; // Retorna true se alguma diagonal estiver completa

        return false; // Se nenhuma linha/coluna/diagonal completa, retorna false
    } // fim do método VerificarVencedor

    static void JogoContraComputadorFacil() // Modo fácil: computador joga aleatoriamente
    {
        InicializarTabuleiro();    // Inicializa o tabuleiro com 1..9
        string jogadorHumano = "X"; // Símbolo do humano
        string jogadorComp = "O";   // Símbolo do computador
        string jogadorAtual = jogadorHumano; // Começa com o humano
        int jogadas = 0;           // Contador de jogadas
        bool vitoria = false;      // Flag de vitória

        Random rnd = new Random(); // Instancia gerador aleatório para as jogadas do computador
        int posicao, linha, coluna; // Declara variáveis auxiliares para posição/índices

        while (jogadas < 9 && !vitoria) // Loop principal do jogo
        {
            if (jogadorAtual == jogadorHumano) // Se for a vez do humano
            {
                MostrarTurno(jogadorHumano); // Mostra animação do turno do humano
                Console.WriteLine("Sua vez (X). Escolha uma posição (1-9): "); // Pede a posição
                string entrada = Console.ReadLine(); // Lê entrada do usuário
                int.TryParse(entrada, out posicao); // Tenta converter para int (posicao)

                linha = (posicao - 1) / 3; // Calcula linha
                coluna = (posicao - 1) % 3; // Calcula coluna

                if (posicao >= 1 && posicao <= 9 && tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O") // Valida posição e se está livre
                {
                    tabuleiro[linha, coluna] = jogadorHumano; // Marca X no tabuleiro
                    jogadas++;          // Incrementa contador de jogadas
                    Console.Beep(500, 150); // Som de confirmação
                    vitoria = VerificarVencedor(jogadorHumano); // Verifica se humano venceu
                    jogadorAtual = jogadorComp; // Passa vez para o computador
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente."); // Aviso de jogada inválida
                    Console.ReadKey();   // Aguarda o usuário pressionar algo
                }
            }
            else // Se for a vez do computador (modo fácil)
            {
                MostrarTurno(jogadorComp); // Mostra animação do turno do computador

                Console.ForegroundColor = ConsoleColor.Red; // Muda cor para vermelho
                Console.Write("Computador pensando"); // Texto animado "pensando"
                for (int i = 0; i < 3; i++) { Thread.Sleep(800); Console.Write("."); } // 3 pontos com pausas
                Console.WriteLine(); // Quebra de linha após os pontos

                // Escolhe aleatoriamente uma posição livre: repete até achar uma célula que não seja X nem O
                do
                {
                    posicao = rnd.Next(1, 10); // Gera número aleatório de 1 a 9 (10 exclusivo)
                    linha = (posicao - 1) / 3; // Calcula linha
                    coluna = (posicao - 1) % 3; // Calcula coluna
                } while (tabuleiro[linha, coluna] == "X" || tabuleiro[linha, coluna] == "O"); // Repete se célula ocupada

                tabuleiro[linha, coluna] = jogadorComp; // Marca a posição com "O"
                jogadas++;              // Incrementa o contador de jogadas
                Console.Beep(400, 200); // Som de jogada do computador
                vitoria = VerificarVencedor(jogadorComp); // Verifica se computador venceu
                jogadorAtual = jogadorHumano; // Passa vez de volta para humano
                Console.WriteLine($"Computador escolheu a posição {posicao}."); // Informa qual posição o computador escolheu
                Thread.Sleep(1000); // Pequena pausa antes de continuar
            }

            Console.Clear();        // Limpa a tela
            MostrarTabuleiro();     // Mostra o tabuleiro atualizado
            Thread.Sleep(500);      // Pausa para o usuário visualizar a atualização
        } // fim do while principal do modo fácil

        if (vitoria) // Se houve vitória
        {
            if (jogadorAtual == jogadorHumano) // Observação: aqui a variável jogadorAtual indica quem jogaria em seguida, mas o código usa-a para decidir o vencedor conforme a lógica existente
            {
                Console.ForegroundColor = ConsoleColor.Red; // Define cor vermelha
                Console.WriteLine("Computador venceu!"); // Mensagem de vitória do computador
                ranking["Computador"]++; // Incrementa pontuação do computador
                Console.Beep(700, 400); // Som de vitória do computador
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Define cor amarelo escuro
                Console.WriteLine("Você venceu!"); // Mensagem de vitória do humano
                ranking["Jogador X"]++; // Incrementa pontuação do Jogador X
                Console.Beep(900, 400); // Som de vitória do humano
            }
        }
        else // Se não houve vitória, foi empate
        {
            Console.ForegroundColor = ConsoleColor.Gray; // Cor cinza para empate
            Console.WriteLine("Empate!"); // Mensagem de empate
            ranking["Empates"]++; // Incrementa contador de empates
            Console.Beep(600, 400); // Som de empate
        }

        Console.ResetColor();    // Reseta cor do console
        Thread.Sleep(1000);      // Pausa antes de limpar a tela
        Console.Clear();         // Limpa a tela finalizando o modo fácil
    } // fim do método JogoContraComputadorFacil

    static void JogoContraComputadorDificil() // Modo difícil: computador usa Minimax (80%) ou aleatório (20%)
    {
        InicializarTabuleiro(); // Inicializa o tabuleiro com 1..9
        string jogadorHumano = "X"; // Símbolo do humano
        string jogadorComp = "O";   // Símbolo do computador
        string jogadorAtual = jogadorHumano; // Jogador inicial é o humano
        int jogadas = 0;           // Contador de jogadas
        bool vitoria = false;      // Flag de vitória
        Random rnd = new Random(); // Gerador aleatório (usado para 20% de aleatoriedade)

        while (jogadas < 9 && !vitoria) // Loop principal do jogo difícil
        {
            if (jogadorAtual == jogadorHumano) // Vez do humano (mesma lógica de validação das entradas)
            {
                MostrarTurno(jogadorHumano); // Animação de turno do humano
                Console.WriteLine("Sua vez (X). Escolha uma posição (1-9): "); // Pede posição
                string entrada = Console.ReadLine(); // Lê entrada do usuário
                int.TryParse(entrada, out int posicao); // Converte string para int (posicao)
                int linha = (posicao - 1) / 3; // Calcula linha
                int coluna = (posicao - 1) % 3; // Calcula coluna

                if (posicao >= 1 && posicao <= 9 && tabuleiro[linha, coluna] != "X" && tabuleiro[linha, coluna] != "O") // Valida posição
                {
                    tabuleiro[linha, coluna] = jogadorHumano; // Marca a jogada do humano
                    jogadas++; // Incrementa contador
                    Console.Beep(500, 150); // Som de jogada
                    vitoria = VerificarVencedor(jogadorHumano); // Verifica vitória do humano
                    jogadorAtual = jogadorComp; // Troca para vez do computador
                }
                else
                {
                    Console.WriteLine("Jogada inválida. Tente novamente."); // Erro de entrada
                    Console.ReadKey(); // Aguarda tecla
                }
            }
            else // Vez do computador (modo difícil)
            {
                MostrarTurno(jogadorComp); // Animação de turno do computador

                Console.ForegroundColor = ConsoleColor.Red; // Define cor vermelho
                Console.Write("Computador pensando"); // Texto inicial
                for (int i = 0; i < 3; i++) { Thread.Sleep(600); Console.Write("."); } // Pontos com pausas menores que no modo fácil
                Console.WriteLine(); // Nova linha após os pontos

                int posicaoEscolhida; // Variável que armazenará a posição que o computador escolherá

                if (rnd.Next(0, 100) < 80) // 80% de chance de usar Minimax
                    posicaoEscolhida = EscolherJogadaMinimax(jogadorComp, jogadorHumano); // Chama função que usa Minimax para escolher
                else // 20% de chance de escolher aleatoriamente
                {
                    int pos, lin, col; // Variáveis auxiliares
                    do // Escolhe aleatoriamente até encontrar célula livre
                    {
                        pos = rnd.Next(1, 10); // Gera número aleatório 1..9
                        lin = (pos - 1) / 3; // Calcula linha
                        col = (pos - 1) % 3; // Calcula coluna
                    } while (tabuleiro[lin, col] == "X" || tabuleiro[lin, col] == "O"); // Repete se ocupada
                    posicaoEscolhida = pos; // Atribui a posição escolhida
                }

                int linhaC = (posicaoEscolhida - 1) / 3; // Converte posição escolhida para linha
                int colunaC = (posicaoEscolhida - 1) % 3; // Converte para coluna
                tabuleiro[linhaC, colunaC] = jogadorComp; // Marca "O" no tabuleiro
                jogadas++; // Incrementa contador de jogadas
                Console.Beep(400, 200); // Som de jogada do computador
                vitoria = VerificarVencedor(jogadorComp); // Verifica se o computador venceu
                jogadorAtual = jogadorHumano; // Passa a vez para o humano

                Console.WriteLine($"Computador escolheu a posição {posicaoEscolhida}."); // Informa qual posição foi escolhida
                Thread.Sleep(1000); // Pausa para o usuário ver a escolha
            }

            Console.Clear(); // Limpa o console
            MostrarTabuleiro(); // Mostra o tabuleiro atualizado
            Thread.Sleep(500); // Pausa curta
        } // fim do while principal do modo difícil

        if (vitoria) // Se houve vitória
        {
            if (jogadorAtual == jogadorHumano) // Observação: usa a mesma lógica de decisão de vencedor do código original
            {
                Console.ForegroundColor = ConsoleColor.Red; // Cor vermelha
                Console.WriteLine("Computador venceu!"); // Mensagem de vitória do computador
                ranking["Computador"]++; // Incrementa pontuação do computador
                Console.Beep(700, 400); // Som de vitória
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Cor amarelo escuro
                Console.WriteLine("Você venceu!"); // Mensagem de vitória do humano
                ranking["Jogador X"]++; // Incrementa pontuação do Jogador X
                Console.Beep(900, 400); // Som de vitória
            }
        }
        else // Empate
        {
            Console.ForegroundColor = ConsoleColor.Gray; // Cor cinza
            Console.WriteLine("Empate!"); // Mensagem de empate
            ranking["Empates"]++; // Incrementa empates
            Console.Beep(600, 400); // Som de empate
        }

        Console.ResetColor(); // Reseta cor
        Thread.Sleep(1000); // Pausa final
        Console.Clear(); // Limpa o console
    } // fim do método JogoContraComputadorDificil

    static int EscolherJogadaMinimax(string jogadorComp, string jogadorHum) // Avalia todas jogadas e retorna a melhor (1..9)
    {
        int melhorPontuacao = int.MinValue; // Inicializa melhor pontuação com mínimo possível (para comparar)
        List<int> melhoresJogadas = new List<int>(); // Lista das melhores jogadas (caso haja empate de pontuação)

        for (int i = 0; i < 3; i++) // Percorre linhas do tabuleiro
            for (int j = 0; j < 3; j++) // Percorre colunas do tabuleiro
            {
                if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O") // Se a célula estiver livre (não é X nem O)
                {
                    string temp = tabuleiro[i, j]; // Salva o valor atual (número da posição) para restaurar depois
                    tabuleiro[i, j] = jogadorComp; // Simula a jogada do computador nesta célula

                    int pontuacao = Minimax(0, false, jogadorComp, jogadorHum); // Chama Minimax para avaliar a pontuação dessa jogada

                    tabuleiro[i, j] = temp; // Restaura o valor original da célula (backtracking)

                    int pos = i * 3 + j + 1; // Converte índices (i,j) para posição 1..9

                    if (pontuacao > melhorPontuacao) // Se a pontuação é superior à melhor encontrada até agora
                    {
                        melhorPontuacao = pontuacao; // Atualiza melhor pontuação
                        melhoresJogadas.Clear(); // Limpa a lista de melhores jogadas
                        melhoresJogadas.Add(pos); // Adiciona a nova melhor jogada
                    }
                    else if (pontuacao == melhorPontuacao) // Se a pontuação empata com a melhor existente
                        melhoresJogadas.Add(pos); // Adiciona a posição à lista de empates
                }
            } // fim dos loops que varrem o tabuleiro

        Random rnd = new Random(); // Instancia Random para desempate aleatório entre melhoresJogadas
        return melhoresJogadas[rnd.Next(melhoresJogadas.Count)]; // Retorna uma das melhores jogadas aleatoriamente
    } // fim do método EscolherJogadaMinimax

    static int Minimax(int profundidade, bool isMax, string jogadorComp, string jogadorHum) // Função recursiva Minimax
    {
        if (VerificarVencedor(jogadorComp)) return 5 - profundidade; // Se computador venceu: retorna pontuação positiva ajustada pela profundidade
        if (VerificarVencedor(jogadorHum)) return -5 + profundidade; // Se humano venceu: retorna pontuação negativa ajustada pela profundidade

        bool movimentosRestantes = false; // Flag para verificar se ainda há movimentos possíveis
        for (int i = 0; i < 3; i++) // Percorre linhas
            for (int j = 0; j < 3; j++) // Percorre colunas
                if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O") // Se encontrar alguma célula livre
                    movimentosRestantes = true; // Marca que há movimentos restantes

        if (!movimentosRestantes) return 0; // Se não há movimentos restantes, é empate => retorna 0

        if (isMax) // Se é a vez do maximizador (computador), queremos maximizar a pontuação
        {
            int melhor = int.MinValue; // Inicializa melhor como menor inteiro possível
            for (int i = 0; i < 3; i++) // Percorre linhas
                for (int j = 0; j < 3; j++) // Percorre colunas
                    if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O") // Se célula livre
                    {
                        string temp = tabuleiro[i, j]; // Salva valor atual para restaurar
                        tabuleiro[i, j] = jogadorComp; // Simula jogada do computador
                        melhor = Math.Max(melhor, Minimax(profundidade + 1, false, jogadorComp, jogadorHum)); // Chama Minimax recursivo para o lado minimizador e atualiza 'melhor'
                        tabuleiro[i, j] = temp; // Restaura a célula (backtracking)
                    }
            return melhor; // Retorna o melhor valor encontrado para o maximizador
        }
        else // Caso contrário, é a vez do minimizador (humano), queremos minimizar a pontuação
        {
            int melhor = int.MaxValue; // Inicializa com maior inteiro possível
            for (int i = 0; i < 3; i++) // Percorre linhas
                for (int j = 0; j < 3; j++) // Percorre colunas
                    if (tabuleiro[i, j] != "X" && tabuleiro[i, j] != "O") // Se célula livre
                    {
                        string temp = tabuleiro[i, j]; // Salva valor atual
                        tabuleiro[i, j] = jogadorHum; // Simula jogada do humano
                        melhor = Math.Min(melhor, Minimax(profundidade + 1, true, jogadorComp, jogadorHum)); // Chama Minimax recursivo para o maximizador e atualiza 'melhor'
                        tabuleiro[i, j] = temp; // Restaura a célula (backtracking)
                    }
            return melhor; // Retorna o menor valor encontrado para o minimizador
        }
    } // fim do método Minimax

    static void ExibirRanking() // Método que limpa a tela e imprime o ranking atual
    {
        Console.Clear();          // Limpa o console
        Console.ForegroundColor = ConsoleColor.Green; // Define cor verde para o título do ranking
        Console.WriteLine("========================"); // Linha decorativa
        Console.WriteLine("        Ranking");   // Cabeçalho do ranking
        Console.WriteLine("========================"); // Linha decorativa

        foreach (var item in ranking) // Itera sobre cada par (chave, valor) no dicionário ranking
            Console.WriteLine($"{item.Key}: {item.Value}"); // Imprime "Nome: Pontuação" para cada item do ranking

        Console.WriteLine("========================"); // Linha final do ranking
        Console.ResetColor();     // Reseta cor
        Console.WriteLine();      // Linha em branco
    } // fim do método ExibirRanking
} // fim da classe Program
