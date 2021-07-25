namespace WeChip
{
    public class Cliente
    {
        private int identificador;
        private string nome;
        private string cpf;
        private double? credito;
        private string telefone;
        private Enumerados.Status status;

        public int Identificador { get => identificador; set => identificador = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public double? Credito { get => credito; set => credito = value; }
        public Enumerados.Status Status { get => status; set => status = value; }

        public Cliente()
        {

        }

        public Cliente(int identificador, string nome, string cpf, double credito, string telefone, Enumerados.Status status)
        {
            this.Identificador = identificador;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Credito = credito;
            this.Telefone = telefone;
            this.Status = status;
        }
    }
}
