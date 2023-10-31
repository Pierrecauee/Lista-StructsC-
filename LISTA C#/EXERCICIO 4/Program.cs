using System;
class Program
{
    public struct Emprestimo
    {
        public DateTime data;
        public string nomePessoa;
        public int emprestado;
    }
    struct Jogo
    {
        public string titulo;
        public string console;
        public int ano;
        public int ranking;
        public Emprestimo emprestimo;
    }

    static List<Jogo> jogos = new List<Jogo>();
    static void Main()
    {

        CarregarDados();

        Console.WriteLine("Escolha uma opção:");
        Console.WriteLine("1. Adicionar jogo");
        Console.WriteLine("2. Procurar jogo por título");
        Console.WriteLine("3. Listar jogos de um console");
        Console.WriteLine("4. Realizar empréstimo");
        Console.WriteLine("5. Devolver jogo");
        Console.WriteLine("6. Mostrar jogos emprestados");
        Console.WriteLine("7. Salvar e sair");

        int op;
        Console.Write("Escolha uma opção: ");
        op = Convert.ToInt32(Console.ReadLine());

        switch (op)
        {
            case 1:
                addJogos();
                break;
            case 2:
                procurarJogo();
                break;
            case 3:
                listarJogosDeConsole();
                break;
            case 4:
                RealizarEmprestimo();
                break;
            case 5:
                DevolverJogo();
                break;
            case 6:
                MostrarJogosEmprestados();
                break;
            case 7:
                SalvarDados();

                Console.WriteLine("Saindo do programa.");
                return;
            default:
                Console.WriteLine("Opção inválida. Tente novamente.");
                break;
        }
    }
    static void addJogos()
    {
        Console.Write("Nome do Jogo: ");
        string titulo = Console.ReadLine()!;

        Console.Write("Console: ");
        string console = Console.ReadLine()!;

        Console.Write("Ano: ");
        int ano = Convert.ToInt32(Console.ReadLine());

        Console.Write("ranking: ");
        int ranking = Convert.ToInt32(Console.ReadLine());

        var jogo = new Jogo
        {
            titulo = titulo,
            console = console,
            ano = ano,
            ranking = ranking,
            emprestimo = new Emprestimo { emprestado = 'N' }
        };

        jogos.Add(jogo);
        Console.WriteLine("Jogo adicionado com sucesso");
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void procurarJogo()
    {
        Console.Write("Digite o titulo do jogo a ser procurado: ");
        string tituloProcura = Console.ReadLine()!;

        var jogosEncontrados = new List<Jogo>();

        foreach (var jogo in jogos)
        {
            if (jogo.titulo == tituloProcura)
            {
                jogosEncontrados.Add(jogo);
            }
        }

        if (jogosEncontrados.Any())
        {
            foreach (var jogo in jogosEncontrados)
            {
                Console.WriteLine($"Título: {jogo.titulo}");
                Console.WriteLine($"Console: {jogo.console}");
                Console.WriteLine($"Ano: {jogo.ano}");
                Console.WriteLine($"Ranking: {jogo.ranking}");
                Console.WriteLine($"Empréstimo: {jogo.emprestimo.emprestado}");
                if (jogo.emprestimo.emprestado == 'S')
                {
                    Console.WriteLine($"Nome da pessoa: {jogo.emprestimo.nomePessoa}");
                    Console.WriteLine($"Data do empréstimo: {jogo.emprestimo.data.ToShortDateString()}");
                }
            }
        }
        else
        {
            Console.WriteLine("Nenhum jogo encontrado para o console especificado.");
        }
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void listarJogosDeConsole()
    {
        Console.WriteLine("Digite o nome do console para listar os jogos:");
        string consoleProcurado = Console.ReadLine()!;

        var jogosEncontrados = new List<Jogo>();

        foreach (var jogo in jogos)
        {
            if (jogo.console == consoleProcurado)
            {
                jogosEncontrados.Add(jogo);
            }
        }

        if (jogosEncontrados.Any())
        {
            foreach (var jogo in jogosEncontrados)
            {
                Console.WriteLine($"Título: {jogo.titulo}");
                Console.WriteLine($"Console: {jogo.console}");
                Console.WriteLine($"Ano: {jogo.ano}");
                Console.WriteLine($"Ranking: {jogo.ranking}");
                Console.WriteLine($"Empréstimo: {jogo.emprestimo.emprestado}");
                if (jogo.emprestimo.emprestado == 'S')
                {
                    Console.WriteLine($"Nome da pessoa: {jogo.emprestimo.nomePessoa}");
                    Console.WriteLine($"Data do empréstimo: {jogo.emprestimo.data.ToShortDateString()}");
                }
            }
        }
        else
        {
            Console.WriteLine("Nenhum jogo encontrado para o console especificado.");
        }
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void RealizarEmprestimo()
    {
        Console.Write("Digite o título do jogo a ser emprestado: ");
        string tituloEmprestimo = Console.ReadLine()!;

        var jogoEmprestimo = jogos.FirstOrDefault(j => j.titulo == tituloEmprestimo);

        if (jogoEmprestimo.emprestimo.emprestado == 'S')
        {
            Console.WriteLine("Este jogo já está emprestado.");
        }
        else
        {
            Console.Write("Nome da pessoa que está pegando emprestado: ");
            string nomePessoa = Console.ReadLine()!;
            jogoEmprestimo.emprestimo.nomePessoa = nomePessoa;
            jogoEmprestimo.emprestimo.data = DateTime.Now;
            jogoEmprestimo.emprestimo.emprestado = 'S';
            Console.WriteLine("Empréstimo realizado com sucesso.");
        }
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void DevolverJogo()
    {
        Console.Write("Digite o título do jogo a ser devolvido: ");
        string tituloDevolucao = Console.ReadLine();

        Jogo jogoDevolucao = default(Jogo);

        foreach (var jogo in jogos)
        {
            if (jogo.titulo == tituloDevolucao)
            {
                jogoDevolucao = jogo;
                break;
            }
        }

        if (jogoDevolucao.emprestimo.emprestado == 'N')
        {
            Console.WriteLine("Este jogo não está emprestado no momento.");
        }
        else
        {
            jogoDevolucao.emprestimo.emprestado = 'N';
            Console.WriteLine("Devolução realizada com sucesso.");
        }
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void MostrarJogosEmprestados()
    {
        var jogosEmprestados = jogos.Where(j => j.emprestimo.emprestado == 'S');

        if (jogosEmprestados.Any())
        {
            foreach (var jogo in jogosEmprestados)
            {
                Console.WriteLine($"Título: {jogo.titulo}");
                Console.WriteLine($"Nome da pessoa: {jogo.emprestimo.nomePessoa}");
                Console.WriteLine($"Data do empréstimo: {jogo.emprestimo.data.ToShortDateString()}");
            }
        }
        else
        {
            Console.WriteLine("Nenhum jogo emprestado no momento.");
        }
        Thread.Sleep(2000);
        Console.Clear();
        Main();
    }

    static void CarregarDados()
    {
        if (File.Exists("jogos.txt"))
        {
            using (StreamReader reader = new StreamReader("jogos.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string linha = reader.ReadLine();
                    string[] campos = linha.Split(',');
                    if (campos.Length == 6)
                    {
                        var jogo = new Jogo
                        {
                            titulo = campos[0],
                            console = campos[1],
                            ano = int.Parse(campos[2]),
                            ranking = int.Parse(campos[3]),
                            emprestimo = new Emprestimo
                            {
                                data = DateTime.Parse(campos[4]),
                                nomePessoa = campos[5],
                                emprestado = campos[5] == "S" ? 'S' : 'N'
                            }
                        };

                        jogos.Add(jogo);
                    }
                }
            }
        }
    }

    static void SalvarDados()
    {
        using (StreamWriter writer = new StreamWriter("jogos.txt"))
        {
            foreach (var jogo in jogos)
            {
                string linha = $"{jogo.titulo},{jogo.console},{jogo.ano},{jogo.ranking},{jogo.emprestimo.data},{jogo.emprestimo.nomePessoa},{jogo.emprestimo.emprestado}";
                writer.WriteLine(linha);
            }
        }
    }

}