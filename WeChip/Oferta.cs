using System.Collections.Generic;

namespace WeChip
{
    public struct Endereco
    {
        public string cep;
        public string rua;
        public int? numero;
        public string complemento;
        public string bairro;
        public string cidade;
        public string estado;
    }

    public class Oferta
    {
        private Cliente cliente;
        private Endereco endereco_entrega;
        private List<Enumerados.Produtos> produtos;

        public Endereco Endereco_entrega { get => endereco_entrega; set => endereco_entrega = value; }
        public List<Enumerados.Produtos> Produtos { get => produtos; set => produtos = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }

        public Oferta()
        {

        }
    }
}
