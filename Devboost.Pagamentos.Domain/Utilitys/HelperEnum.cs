using System;
using System.Collections.Generic;

namespace Devboost.Pagamentos.Domain.Utilitys
{
    public static class HelperEnum
    {
        public static List<string> GetStringByEnumType<TEnum>()
        {
            List<string> listBandeiras = new List<string>();

            var contador = 0;

            foreach (var item in Enum.GetValues(typeof(TEnum)))
            {
                contador += 1;

                var valueString = contador > 0 ? ", " + item.ToString() : item.ToString();

                listBandeiras.Add(valueString);
            }

            return listBandeiras;
        }
    }
}