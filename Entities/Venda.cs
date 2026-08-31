namespace SistemaPagamentosLoja.Entities
{
    public class Venda
    {
        public int Numero { get; }
        public Cliente Cliente { get; }
        public decimal ValorCompra { get; private set; }
        public SituacaoVenda Situacao { get; private set; }

        public FormaPagamento? FormaPagamentoUtilizada { get; private set;}
        public decimal? ValorFinal { get; private set; }
    
        public Venda(int numero, Cliente cliente, decimal valorCompra)
        {
            if (cliente is null)
                throw new ArgumentNullException(nameof(cliente));
    
            if (valorCompra <= 0)
                throw new ArgumentException("O valor da venda deve ser maior que zero.");
    
            Numero = numero;
            Cliente = cliente;
            ValorCompra = valorCompra;
            Situacao = SituacaoVenda.Pendente;
        }
    
        public void Pagar(FormaPagamento formaPagamento)
        {
            if (formaPagamento is null)
                throw new ArgumentNullException(nameof(formaPagamento));
    
            if (Situacao == SituacaoVenda.Pago)
                throw new InvalidOperationException("Esta venda já foi paga.");
    
            ValorFinal = formaPagamento.CalcularValorFinal(ValorCompra);
            FormaPagamentoUtilizada = formaPagamento;
            Situacao = SituacaoVenda.Pago;
        }
    }
}