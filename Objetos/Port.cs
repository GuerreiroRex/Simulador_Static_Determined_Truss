using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CalculoTre.Objetos
{
    public static class Port
    {
        private static string dir = Environment.CurrentDirectory;

        #region Barras
        public static void SalvarDados(Dictionary<string, Bar> dict)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Json files (*.json)|*.json";
            sf.DefaultExt = "arquivo";
            sf.FilterIndex = 2;
            sf.InitialDirectory = dir + @"Data\";
            sf.RestoreDirectory = true;
            sf.Title = @"Salvar arquivo";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                List<object> lista = new List<object>();

                lista.Add(dict);
                lista.Add(Resolution.Resolucao);
                lista.Add(Resolution.EscalaVertical);
                lista.Add(Resolution.EscalaHorizontal);

                string valores = JsonConvert.SerializeObject(lista);

                File.WriteAllText(sf.FileName, valores);
            }
        }

        public static void CarregarDados(ref Dictionary<string, Bar> dict)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Json files (*.json)|*.json";
            of.DefaultExt = "arquivo";
            of.InitialDirectory = dir + @"Data\";
            of.FilterIndex = 2;
            of.RestoreDirectory = true;
            of.Title = @"Salvar arquivo";
            if (of.ShowDialog() == DialogResult.OK)
            {
                List<object> lista = new List<object>();

                using (StreamReader r = new StreamReader(of.FileName))
                {
                    string json = r.ReadToEnd();
                    lista = JsonConvert.DeserializeObject<List<object>>(json, new JsonSerializerSettings
                    {
                        ConstructorHandling = ConstructorHandling.Default
                    });
                }

                dict = JsonConvert.DeserializeObject<Dictionary<string, Bar>>(lista[0].ToString());
                Resolution.Resolucao = JsonConvert.DeserializeObject<int[]>(lista[1].ToString());
                Resolution.EscalaVertical = Convert.ToInt16(lista[2]);
                Resolution.EscalaHorizontal = Convert.ToInt16(lista[3]);

                Joint.AtualizarNos();

                foreach (Bar barra in dict.Values)
                {
                    barra.knots[0] = Data.nos.Values.Where(x => x.ID == barra.knots[0].ID).First();
                    barra.knots[1] = Data.nos.Values.Where(x => x.ID == barra.knots[1].ID).First();
                }
            }
        }
        #endregion
    }
}
