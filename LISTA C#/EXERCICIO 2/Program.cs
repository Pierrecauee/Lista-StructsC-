using System.Collections;
using System.Collections.Generic;

class Program
{
    struct Livros
    {
        public string titulo;
        public string autor;      //criacao da struct para armazenar dados diversos
        public int ano;
        public int prateleira;
    }

    static void addLivro(List<Livros> lista) //funcao de adicionar  o nome da funcao e depois chamnado a lista dps o nome da <struct> dps o nome ficticio
    {
        Livros novoLivro = new Livros(); // cria a lista que vai receber os valores
        Console.WriteLine("Insira o título do livro: "); // pede o dado
        novoLivro.titulo = Console.ReadLine();// armazena o dado
        Console.WriteLine("Insira o autor do livro: ");
        novoLivro.autor = Console.ReadLine();
        Console.WriteLine("Insira o ano do livro: ");
        novoLivro.ano = Convert.ToInt32(Console.ReadLine());// armazena o dado convertendo para inteiro pois o readline le apenas string
        Console.WriteLine("Insira a prateleira do livro: ");
        novoLivro.prateleira = int.Parse(Console.ReadLine());// faz o mesmo que o convert
        lista.Add(novoLivro); //adiciona os valores armazenados a lista 
    }

    static void procuraLivro(List<Livros> lista, string buscaTitulo) // faz o mesmo do add livro mais declarando uma nova string
    {
        int qtd = lista.Count(); //.count conta quantas posicoes tem no vetor

        for (int i = 0; i < qtd; i++) //faz um for para percorrer 
        {
            if (lista[i].titulo.ToUpper().Equals(buscaTitulo.ToUpper())) //se lista na posicao i titulo 
            {
                Console.WriteLine("titulo do Livro: " + lista[i].titulo); //exibe a posicao
                Console.WriteLine($"Número da prateleira: {lista[i].prateleira}");//exibe a prateleira
            }
        }
    }

    static void listarLivros(List<Livros> lista)
    {
        int qtd = lista.Count();

        for (int i = 0; i < qtd; i++)
        {
            Console.WriteLine("titulo do Livro: " + lista[i].titulo);
            Console.WriteLine("titulo do Autor: " + lista[i].autor);
            Console.WriteLine($"Número da Ano: {lista[i].ano}");
            Console.WriteLine($"Número da Prateleira: {lista[i].prateleira}");
        }
    }

    static void procuraAno(List<Livros> lista, int buscaLivro)
    {
        int qtd = lista.Count();
        for (int i = 0; i < qtd; i++)
        {
            if (lista[i].ano > buscaLivro)
            {
                Console.WriteLine("titulo do Livro: " + lista[i].titulo);
                Console.WriteLine($"Número da Ano: {lista[i].ano}");
            }
        }
    }

    static void salvarDados(List<Livros> lista, string tituloArquivo)
    {

        using (StreamWriter writer = new StreamWriter(tituloArquivo))
        {
            foreach (Livros Livros in lista)
            {
                writer.WriteLine($"{Livros.titulo},{Livros.autor},{Livros.ano},{Livros.prateleira}");
            }
        }
        Console.WriteLine("Dados salvos com sucesso!");


    }

    static void carregarDados(List<Livros> lista, string tituloArquivo)
    {
        if (File.Exists(tituloArquivo))
        {
            string[] linhas = File.ReadAllLines(tituloArquivo);
            foreach (string linha in linhas)
            {
                string[] campos = linha.Split(',');
                Livros Livros = new Livros
                {
                    titulo = campos[0],
                    autor = campos[1],
                    ano = int.Parse(campos[2]),
                    prateleira = int.Parse(campos[3])
                };
                lista.Add(Livros);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }
    static int menu()
    {
        Console.WriteLine("Menu: ");
        Console.WriteLine("1 - Adicionar Livro: ");
        Console.WriteLine("2 - Procurar por Título : ");
        Console.WriteLine("3 - Mostrar todos os livros cadastrados: ");
        Console.WriteLine("4 - Procurar por ano superior: ");
        Console.WriteLine("0 - Sair");
        int opcao = Convert.ToInt32(Console.ReadLine());
        return opcao;
    }

    static void Main()
    {
        List<Livros> listaLivros = new List<Livros>();

        int opcao;

        do
        {
            opcao = menu();

            switch (opcao)
            {
                case 1:
                    addLivro(listaLivros);
                    break;
                case 2:
                    Console.WriteLine("Digite o Título");
                    string buscaTitulo = Console.ReadLine();
                    procuraLivro(listaLivros, buscaTitulo);
                    break;
                case 3:
                    listarLivros(listaLivros);
                    break;
                case 4:
                    Console.WriteLine("Digite o ano: ");
                    int ano = Convert.ToInt32(Console.ReadLine());
                    procuraAno(listaLivros, ano);
                    break;
                case 0:
                    salvarDados(listaLivros, "dados.txt");
                    break;
                default:
                    Console.WriteLine("ERRO: opcão inválida");
                    break;
            }
            Console.ReadKey();
            Console.Clear();
        } while (opcao != 0);
    }
}