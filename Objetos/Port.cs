using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Port
    {
        #region Barras
        public static void SalvarDados(string arquivo, Dictionary<string, Bar> dict)
        {
            if (File.Exists(arquivo))
                File.Delete(arquivo);

            List<object> lista = new List<object>();

            lista.Add(dict);
            lista.Add(Resolution.Resolucao);
            lista.Add(Resolution.EscalaVertical);
            lista.Add(Resolution.EscalaHorizontal);

            string valores = JsonConvert.SerializeObject(lista);

            File.WriteAllText(arquivo, valores);
        }

        public static void CarregarDados(string arquivo, ref Dictionary<string, Bar> dict)
        {
            List<object> lista = new List<object>();

            using (StreamReader r = new StreamReader(arquivo))
            {
                string json = r.ReadToEnd();
                lista = JsonConvert.DeserializeObject<List<object>>(json, new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.Default
                });

                dict = JsonConvert.DeserializeObject<Dictionary<string, Bar>>(lista[0].ToString());
                Resolution.Resolucao = JsonConvert.DeserializeObject<int[]>(lista[1].ToString());
                Resolution.EscalaVertical = Convert.ToInt16(lista[2]);
                Resolution.EscalaHorizontal = Convert.ToInt16(lista[3]);
            }
        }
        #endregion
    }
}
