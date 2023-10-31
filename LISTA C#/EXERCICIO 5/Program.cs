using System;
using System.IO;

struct Data
{
    public int mes;
    public int ano;
}

struct Gado
{
    public int codigo;
    public double leite;
    public double alim;
    public Data nasc;
    public char abate;
}

class Program
{
    static Gado[] fazenda = new Gado[100];
    static int quantidadeDeGado = 0;

    static void Main()
    {
        char opcao;

        do
        {
            Console.WriteLine("Menu de Opções:");
            Console.WriteLine("a) Ler a base de dados");
            Console.WriteLine("b) Preencher o campo abate");
            Console.WriteLine("c) Retornar a quantidade total de leite produzida por semana");
            Console.WriteLine("d) Retornar a quantidade total de alimento consumido por semana");
            Console.WriteLine("e) Listar os animais que devem ir para o abate");
            Console.WriteLine("f) Salvar dados em arquivo e carregar dados");
            Console.WriteLine("g) Sair do programa");
            Console.Write("Escolha uma opção: ");
            opcao = char.ToLower(Console.ReadKey().KeyChar);
            Console.WriteLine();

            switch (opcao)
            {
                case 'a':
                    LerDados();
                    break;

                case 'b':
                    PreencherCampoAbate();
                    break;

                case 'c':
                    double totalLeiteProduzido = TotalLeiteProduzido();
                    Console.WriteLine($"Total de leite produzido por semana: {totalLeiteProduzido} litros");
                    break;

                case 'd':
                    Console.WriteLine($"Total de alimento consumido por semana: {TotalAlimentoConsumido()} quilos");
                    break;

                case 'e':
                    ListarAnimaisParaAbate();
                    break;

                case 'f':
                    SalvarDadosEmArquivo();
                    CarregarDadosDeArquivo();
                    break;

                case 'g':
                    Console.WriteLine("Saindo do programa...");
                    break;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.WriteLine();
        } while (opcao != 'g');
    }

    static void LerDados()
    {
        Console.Write("Quantos registros deseja adicionar? ");
        int n = int.Parse(Console.ReadLine());

        if (quantidadeDeGado + n > 100)
        {
            Console.WriteLine("Não é possível adicionar mais registros. Limite excedido.");
            return;
        }

        for (int i = quantidadeDeGado; i < quantidadeDeGado + n; i++)
        {
            Console.WriteLine($"Registro {i + 1}:");
            fazenda[i].codigo = i + 1;

            Console.Write("Número de litros de leite produzido por semana: ");
            fazenda[i].leite = double.Parse(Console.ReadLine());

            Console.Write("Quantidade de alimento ingerida por semana (em quilos): ");
            fazenda[i].alim = double.Parse(Console.ReadLine());

            Console.Write("Data de nascimento (mês): ");
            fazenda[i].nasc.mes = int.Parse(Console.ReadLine());

            Console.Write("Data de nascimento (ano): ");
            fazenda[i].nasc.ano = int.Parse(Console.ReadLine());

            fazenda[i].abate = ' ';
        }

        quantidadeDeGado += n;
        Console.WriteLine("Dados adicionados com sucesso.");
    }

    static void PreencherCampoAbate()
    {
        for (int i = 0; i < quantidadeDeGado; i++)
        {
            if (fazenda[i].nasc.ano <= DateTime.Now.Year - 5 || fazenda[i].leite < 40)
                fazenda[i].abate = 'S';
            else
                fazenda[i].abate = 'N';
        }

        Console.WriteLine("Campos 'abate' preenchidos com sucesso.");
    }

    static double TotalLeiteProduzido()
    {
        double totalLeite = 0;

        for (int i = 0; i < quantidadeDeGado; i++)
        {
            totalLeite += fazenda[i].leite;
        }

        return totalLeite;
    }

    static double TotalAlimentoConsumido()
    {
        double totalAlimento = 0;

        for (int i = 0; i < quantidadeDeGado; i++)
        {
            totalAlimento += fazenda[i].alim;
        }

        return totalAlimento;
    }

    static void ListarAnimaisParaAbate()
    {
        Console.WriteLine("Animais que devem ir para o abate:");

        for (int i = 0; i < quantidadeDeGado; i++)
        {
            if (fazenda[i].abate == 'S')
            {
                Console.WriteLine($"Código: {fazenda[i].codigo}, Nascimento: {fazenda[i].nasc.mes}/{fazenda[i].nasc.ano}");
            }
        }
    }

    static void SalvarDadosEmArquivo()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter("dados.txt"))
            {
                for (int i = 0; i < quantidadeDeGado; i++)
                {
                    writer.WriteLine($"{fazenda[i].codigo} {fazenda[i].leite} {fazenda[i].alim} {fazenda[i].nasc.mes} {fazenda[i].nasc.ano} {fazenda[i].abate}");
                }
            }
            Console.WriteLine("Dados salvos em arquivo com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar os dados em arquivo: {ex.Message}");
        }
    }

    static void CarregarDadosDeArquivo()
    {
        try
        {
            using (StreamReader reader = new StreamReader("dados.txt"))
            {
                string line;
                quantidadeDeGado = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    fazenda[quantidadeDeGado].codigo = int.Parse(parts[0]);
                    fazenda[quantidadeDeGado].leite = double.Parse(parts[1]);
                    fazenda[quantidadeDeGado].alim = double.Parse(parts[2]);
                    fazenda[quantidadeDeGado].nasc.mes = int.Parse(parts[3]);
                    fazenda[quantidadeDeGado].nasc.ano = int.Parse(parts[4]);
                    fazenda[quantidadeDeGado].abate = char.Parse(parts[5]);
                    quantidadeDeGado++;
                }
            }
            Console.WriteLine("Dados carregados do arquivo com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao carregar os dados do arquivo: {ex.Message}");
        }
    }
}