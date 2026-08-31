namespace SistemaPagamentosLoja.Entities
{
   public class PagamentoPix : FormaPagamento
    {
        public override string Nome => "PIX";

        public override decimal CalcularValorFinal(decimal valor)
        {
            return valor - (valor * 0.05m); // 5% de desconto
        }
    }
}