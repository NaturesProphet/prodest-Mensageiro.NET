using System;
using System.Collections.Generic;
namespace Mensageiro
{
    class Mensagem
    {
        public Dictionary<string, object> data = new Dictionary<string, object>();
        public Dictionary<string, object> LOCALIZACAO = new Dictionary<string, object>();

        public void add(string key, object value)
        {
            data.Add(key, value);
        }
        public void format()
        {
            try
            {
                String LAT = data["LATITUDE"].ToString();
                String LONG = data["LONGITUDE"].ToString();
                String coordinates = "[" + LONG + "," + LAT + "]";
                data.Remove("LATITUDE");
                data.Remove("LONGITUDE");
                LOCALIZACAO.Add("type", "Point");
                LOCALIZACAO.Add("coordinates", coordinates);
            }
            catch (Exception)
            {
                Console.WriteLine("LAT/LONG não encontrados na mensagem");
            }
        }
    }
}
