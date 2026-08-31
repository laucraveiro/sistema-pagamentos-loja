namespace SistemaPagamentosLoja.Entities
{
   public class PagamentoDinheiro : FormaPagamento
    {
        public override string Nome => "Dinheiro";

        public override decimal CalcularValorFinal(decimal valor)
        {
            return valor; // Sem taxa
        }
    }
}