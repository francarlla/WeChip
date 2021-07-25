using System.Collections.Generic;

namespace WeChip
{
    public static class Status
    {
        private static Dictionary<Enumerados.Status, bool[]> statusCliente = new Dictionary<Enumerados.Status, bool[]>()
        {
            //primeira posição do array representa 'Finaliza Cliente' ; segunda posição representa 'Contabiliza Venda'
            { Enumerados.Status.NomeLivre, new bool[] {false,false } },
            { Enumerados.Status.NaoContratado, new bool[] {true,false } },
            { Enumerados.Status.OfertaAceita, new bool[] {true,true } },
            { Enumerados.Status.CaiuLigacao, new bool[] {false,false } },
            { Enumerados.Status.Viajou, new bool[] {false,false } },
            { Enumerados.Status.Falecido, new bool[] {true,false } }
        };

        public static bool FinalizaCliente(Enumerados.Status status)
        {
            return statusCliente[status][0];
        }

        public static bool ContabilizaVenda(Enumerados.Status status)
        {
            return statusCliente[status][1];
        }

    }

}
