namespace SistemaPagamentosLoja.Entities
{
   public class PagamentoCartao : FormaPagamento
    {
        public override string Nome => "Cartão de crédito";

        public override decimal CalcularValorFinal(decimal valor)
        {
            return valor * 1.03m; // 3% de taxa
        }
    }
}