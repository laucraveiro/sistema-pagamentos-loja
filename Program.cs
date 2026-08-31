using System.Globalization;
using SistemaPagamentosLoja.Entities;

namespace SistemaPagamentosLoja
{
class Program
{
    static List<Venda> vendas = new List<Venda>();

    static void Main()
    {
        bool sair = false;

        while (!sair)
        {
            ExibirMenu();
            string? opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarVenda();
                    break;
                case "2":
                    ListarVendas();
                    break;
                case "3":
                    RealizarPagamento();
                    break;
                case "0":
                    sair = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void ExibirMenu()
    {
        Console.WriteLine("================================");
        Console.WriteLine("SISTEMA DE VENDAS");
        Console.WriteLine("================================");
        Console.WriteLine("1 - Cadastrar venda");
        Console.WriteLine("2 - Listar vendas");
        Console.WriteLine("3 - Realizar pagamento");
        Console.WriteLine("0 - Sair");
        Console.WriteLine("================================");
        Console.Write("Escolha uma opção: ");
    }

    static void CadastrarVenda()
    {
        try
        {
            Console.Write("Número da venda: ");
            string? entradaNumero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entradaNumero) || !int.TryParse(entradaNumero, out int numero))
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            if (vendas.Any(v => v.Numero == numero))
            {
                Console.WriteLine("Já existe uma venda com esse número.");
                return;
            }

            Console.Write("Cliente: ");
            string? nome = Console.ReadLine();

            Console.Write("CPF: ");
            string? cpf = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(cpf))
            {
                Console.WriteLine("Nome e CPF são obrigatórios.");
                return;
            }

            Console.Write("Valor: ");
            string? entradaValor = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entradaValor) || !decimal.TryParse(entradaValor, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal valor))
            {
                Console.WriteLine("Valor inválido.");
                return;
            }

            var cliente = new Cliente(nome, cpf);
            var venda = new Venda(numero, cliente, valor);

            vendas.Add(venda);

            Console.WriteLine();
            Console.WriteLine("Venda cadastrada com sucesso!");
            Console.WriteLine($"Situação: {venda.Situacao}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao cadastrar venda: {ex.Message}");
        }
    }

    static void ListarVendas()
    {
        if (!vendas.Any())
        {
            Console.WriteLine("Nenhuma venda cadastrada.");
            return;
        }

        foreach (var venda in vendas)
        {
            Console.WriteLine($"Venda: {venda.Numero}");
            Console.WriteLine($"Cliente: {venda.Cliente.Nome}");
            Console.WriteLine($"Valor original: R$ {venda.ValorCompra:F2}");
            Console.WriteLine($"Situação: {venda.Situacao}");

            if (venda.Situacao == SituacaoVenda.Pago)
            {
                Console.WriteLine($"Forma de pagamento: {venda.FormaPagamentoUtilizada?.Nome ?? "Não informada."}");
                Console.WriteLine($"Valor final: R$ {venda.ValorFinal:F2}");
            }

            Console.WriteLine();
        }
    }

    static void RealizarPagamento()
    {
        try
        {
            Console.Write("Número da venda: ");
            string? entradaNumero = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(entradaNumero) || !int.TryParse(entradaNumero, out int numero))
            {
                Console.WriteLine("Número inválido.");
                return;
            }

            var venda = vendas.FirstOrDefault(v => v.Numero == numero);

            if (venda is null)
            {
                Console.WriteLine("Venda não encontrada.");
                return;
            }

            if (venda.Situacao == SituacaoVenda.Pago)
            {
                Console.WriteLine("Esta venda já foi paga.");
                return;
            }

            Console.WriteLine("Escolha a forma de pagamento:");
            Console.WriteLine("1 - PIX");
            Console.WriteLine("2 - Cartão de crédito");
            Console.WriteLine("3 - Dinheiro");
            Console.Write("Opção: ");

            string? opcao = Console.ReadLine();

            FormaPagamento? formaPagamento = opcao switch
            {
                "1" => new PagamentoPix(),
                "2" => new PagamentoCartao(),
                "3" => new PagamentoDinheiro(),
                _ => null
            };

            if (formaPagamento is null)
            {
                Console.WriteLine("Opção de pagamento inválida.");
                return;
            }

            decimal valorOriginal = venda.ValorCompra;

            venda.Pagar(formaPagamento);

            Console.WriteLine();
            Console.WriteLine($"Valor original: R$ {valorOriginal:F2}");
            Console.WriteLine($"Forma de pagamento: {formaPagamento.Nome}");
            Console.WriteLine($"Valor final: R$ {venda.ValorFinal:F2}");
            Console.WriteLine("Pagamento realizado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao realizar pagamento: {ex.Message}");
        }
    }
}
}