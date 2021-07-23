using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WeChip.Enumerados;

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
        private List<Produtos> produtos;

        public Endereco Endereco_entrega { get => endereco_entrega; set => endereco_entrega = value; }
        public List<Produtos> Produtos { get => produtos; set => produtos = value; }

        public Oferta()
        {

        }
    }
}
