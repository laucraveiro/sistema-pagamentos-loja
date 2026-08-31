namespace SistemaPagamentosLoja.Entities
{
    public class Cliente
    {
            public string Nome { get; private set; }
            public string Cpf { get; }
            
            public Cliente(string nome, string cpf)
            {
                if (string.IsNullOrWhiteSpace(nome))
                    throw new ArgumentException("O nome do cliente é obrigatório.");

                if (string.IsNullOrWhiteSpace(cpf))
                    throw new ArgumentException("O CPF do cliente é obrigatório.");

                Nome = nome;
                Cpf = cpf;
            }
    }
}