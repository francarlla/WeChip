using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_WeChip.Models
{
    public class Oferta
    {
        public int idOferta { get; set; }
        public int idStatus { get; set; }
        public List<int> idProduto { get; set; }
        public string cpf { get; set; }

        public Oferta(int oferta, int status, List<int> produtos, string cpf)
        {
            this.idOferta = oferta;
            this.idStatus = status;
            this.idProduto = produtos;
            this.cpf = cpf;
        }
    }
}
