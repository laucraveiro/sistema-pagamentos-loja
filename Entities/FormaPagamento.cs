namespace SistemaPagamentosLoja.Entities
{
    public abstract class FormaPagamento
    {
        public abstract string Nome { get; }
        public abstract decimal CalcularValorFinal(decimal valor);
    }
}