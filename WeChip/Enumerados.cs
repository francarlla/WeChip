using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChip
{
    public static class Enumerados
    {
        public enum Status
        {
            [Description("Nome Livre")]
            NomeLivre = 1,
            [Description("Não deseja ser contatado")]
            NaoContratado = 7,
            [Description("Cliente Aceitou Oferta")]
            OfertaAceita = 9,
            [Description("Caiu a ligação")]
            CaiuLigacao = 15,
            [Description("Viajou")]
            Viajou = 19,
            [Description("Falecido")]
            Falecido = 21
        }

        public enum Produtos
        {
            [Description("Mouse")]
            Mouse = 15,
            [Description("Teclado")]
            Teclado = 106,
            [Description("Monitor 17’")]
            Monitor17 = 200,
            [Description("Pen Drive 8 GB")]
            PenDrive8GB = 211,
            [Description("Pen Drive 16 GB")]
            PenDrive16GB = 314,
            [Description("AVAST")]
            AVAST = 459,
            [Description("Pacote Office")]
            PacoteOffice = 1104,
            [Description("Spotify (3 meses)")]
            Spotify3Meses = 1108,
            [Description("Netflix (1 mês)")]
            Netflix1Mes = 1107
        }

        public enum TipoProduto
        {
            [Description("HARDWARE")]
            HARDWARE = 1,
            [Description("SOFTWARE")]
            SOFTWARE = 2
        }

        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

        public static IList EnumToList<T>()
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T isn't an enumerated type");

            IList list = new List<T>();
            Type type = typeof(T);
            if (type != null)
            {
                Array enumValues = Enum.GetValues(type);
                foreach (T value in enumValues)
                {
                    list.Add(value);
                }
            }

            return list;
        }
    }
}
