using System;
using System.Collections.Generic;
namespace Mensageiro
{
    class Mensagem
    {
        public Dictionary<string, object> data = new Dictionary<string, object>();

        public void add(string key, object value)
        {
            data.Add(key, value);
        }


    }



}
