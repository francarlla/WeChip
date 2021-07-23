using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChip
{
    public class Produtos

    {
        public static Dictionary<Enumerados.Produtos, object[]> produtos = new Dictionary<Enumerados.Produtos, object[]>()
        {
            //primeira posição do array representa 'Preço' ; segunda posição representa 'Tipo do produto'
            { Enumerados.Produtos.Mouse, new object[] { 20 ,Enumerados.TipoProduto.HARDWARE } },
            { Enumerados.Produtos.Teclado, new object[] { 30, Enumerados.TipoProduto.HARDWARE } },
            { Enumerados.Produtos.Monitor17, new object[] { 350, Enumerados.TipoProduto.HARDWARE } },
            { Enumerados.Produtos.PenDrive8GB, new object[] { 30, Enumerados.TipoProduto.HARDWARE } },
            { Enumerados.Produtos.PenDrive16GB, new object[] { 50, Enumerados.TipoProduto.HARDWARE } },
            { Enumerados.Produtos.AVAST, new object[] { 199.9, Enumerados.TipoProduto.SOFTWARE } },
            { Enumerados.Produtos.PacoteOffice, new object[] { 499, Enumerados.TipoProduto.SOFTWARE } },
            { Enumerados.Produtos.Spotify3Meses, new object[] { 45.5, Enumerados.TipoProduto.SOFTWARE} },
            { Enumerados.Produtos.Netflix1Mes, new object[] { 19.9, Enumerados.TipoProduto.SOFTWARE } }

        };


    }

}
