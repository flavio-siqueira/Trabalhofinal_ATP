
//Flávio Siqueira rodrigues.
//cuidado com importar registro no arquivo pois o endereço do arquivo digitado estiver com erros o programa não rodará, por  isso foi colocado o exemplo abaixo quando se abre o programa.
//Dados ja estão armazenados no arquivo e salvos na mesma pasta zip do código.

class Program
{
    // Variáveis globais
    static string[] produtos;
    static int[] estoque;
    static int[,] vendas = new int[4, 30];

    static void Main()
    {
        int numero;
        //menu principal.
        do
        {
            Console.WriteLine("Os produtos ao ser importados e salvos o usuário deverá especificar o local do mesmo exemplo:\nC:\\Users\\adm\\Desktop\\Atividade_final \nUsar o da pasta como exemplo");
            Console.WriteLine("Menu");
            Console.WriteLine("1 – Importar arquivo de produtos"); //os produtos ao ser importados o usuário devera digitar o local onde se encontrar.
            Console.WriteLine("2 – Registrar venda");
            Console.WriteLine("3 – Relatório de vendas");
            Console.WriteLine("4 – Relatório de estoque");
            Console.WriteLine("5 – Criar arquivo de vendas");
            Console.WriteLine("6 – Sair");
            Console.Write("Escolha uma opção: ");
            numero = int.Parse(Console.ReadLine());
            //Ao chamar o swith as opções serão lidas e executadas de acordo com os métodos e funções. 
            switch (numero)
            {
                case 1:
                Produtos();
                    break;
                case 2:
                 Venda();
                    break;
                case 3:
                    R_Vendas();
                    break;
                case 4:
                    R_Estoque();
                    break;
                case 5:
                    Criar_Vendas();
                    break;
                case 6:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        } while (numero != 6);
    }

    //importar produtos de um arquivo
    static void Produtos()
    {
        Console.Write("nome do arquivo de produtos: ");
        string arquivo = Console.ReadLine();
        //Acesso a arquivo.txt
        string[] linhas = File.ReadAllLines(arquivo);
        produtos = new string[4];
        estoque = new int[4];

        for (int i = 0; i < linhas.Length; i++)
        {
            string[] partes = linhas[i].Split(',');
            produtos[i] = partes[0];
            estoque[i] = int.Parse(partes[1]);

            Console.WriteLine(produtos[i]+" "+ estoque[i] );
        }
        
        Console.WriteLine("Produtos importados com sucesso.");
    }

    // Método registro de vendas.
    static void Venda()
    {
        Console.Write("digite o número do produto (0-3): ");
        int produto = int.Parse(Console.ReadLine());
        Console.Write("dogite o dia do mês: ");
        int dia = int.Parse(Console.ReadLine());
        Console.Write("digite a quantidade vendida: ");
        int quantidade1 = int.Parse(Console.ReadLine());

        //condições de valores inválidos pelo teclado.
        if ((produto < 0) || (produto >= 4))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        if (dia < 1 || dia > 30)
        {
            Console.WriteLine("Dia inválido.");
            return;
        }
        //Condição de subtrair o valor venda com o estoque.
        if (estoque[produto] >= quantidade1)
        {
            estoque[produto] -= quantidade1;
            vendas[produto, dia - 1] += quantidade1;
            Console.WriteLine("Valor registrado.");
            Console.WriteLine(estoque[produto]+ " Valores " + vendas[produto,dia]);
        }
        else
        {
            Console.WriteLine("Quantidade de venda em estoque indisponível.");
        }
    }

    // Método para exibição de relatório de vendas
    static void R_Vendas()
    {
        Console.WriteLine("Relatório de Vendas:");
        Console.Write("Dia\t");

        for (int i = 0; i < produtos.Length; i++)
        {
            Console.Write($"{produtos[i]}\t");
        }
        Console.WriteLine();

        for (int t = 0; t < 30; t++)
        {
            Console.Write($"{t + 1}\t");
            for (int i = 0; i < produtos.Length; i++)
            {
                Console.Write($"{vendas[i, t]}\t");
            }
            Console.WriteLine();
        }
    }

    // Exibir relatório de estoque
    static void R_Estoque()
    {
        Console.WriteLine("Estoque Atualizado:");
        for (int i = 0; i < produtos.Length; i++)
        {
            //mostrar para usuário.
            Console.WriteLine(" produtos {0} estoque {0} ", produtos[i], estoque[i]) ;//apresentar ao usuário
        }
    }

    // Método para criar arquivo de vendas e armazena-los.
    static void Criar_Vendas()
    {
        Console.Write("digite o nome do arquivo de vendas: ");
        string arquivo = Console.ReadLine();

        using (StreamWriter sw = new StreamWriter(arquivo))
        {
            for (int i = 0; i < produtos.Length; i++)
            {
                int totalVendas = 0;
                for (int dia = 0; dia < 30; dia++)
                {
                    totalVendas += vendas[i, dia];
                }
                sw.WriteLine($"{produtos[i]},{totalVendas}");
                Console.WriteLine($"{produtos[i]},{totalVendas}");//apresentar ao usuário
            }
        }

        Console.WriteLine("Arquivo de vendas registrado com sucesso.");
    }
}