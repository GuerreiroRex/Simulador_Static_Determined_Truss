using Newtonsoft.Json;
using System;
using System.Collections;
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
        public static void SalvarDados(Dictionary<string, Bar> dict, Tela tela)
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
                lista.Add(Tela.Resolucao);
                lista.Add(tela.MaximoVertical);
                lista.Add(tela.MaximoHorizontal);

                string valores = JsonConvert.SerializeObject(lista);

                File.WriteAllText(sf.FileName, valores);
            }
        }

        public static void CarregarDados(ref Dictionary<string, Bar> dict, Tela tela)
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
                Tela.Resolucao = JsonConvert.DeserializeObject<int[]>(lista[1].ToString());
                tela.MaximoVertical = Convert.ToInt16(lista[2]);
                tela.MaximoHorizontal = Convert.ToInt16(lista[3]);

                Joint.AtualizarNos();

                foreach (Bar barra in dict.Values)
                {
                    barra.knots[0] = Data.nos.Values.Where(x => x.ID == barra.knots[0].ID).First();
                    barra.knots[1] = Data.nos.Values.Where(x => x.ID == barra.knots[1].ID).First();
                }
            }
        }

        public static bool CompararCalculos(Dictionary<byte, Knot> dict)
        {
            string dicionario_original = JsonConvert.SerializeObject(dict);
            string caminho = @"C:\Users\User\Desktop\dados\" + $"dicionario_original.txt";
            File.WriteAllText(caminho, dicionario_original);

            string dicionario_calculos = JsonConvert.SerializeObject(Data.calculo_Nos);
            string caminho2 = @"C:\Users\User\Desktop\dados\" + $"dicionario_calculos.txt";
            File.WriteAllText(caminho2, dicionario_calculos);

            return dicionario_original == dicionario_calculos;
        }

        public static void ImportarCalculos()
        {
            string dicionario_barra = JsonConvert.SerializeObject(Data.barras);
            string dicionario_nos = JsonConvert.SerializeObject(Data.nos);

            Data.calculo_Barras = JsonConvert.DeserializeObject<Dictionary<string, Bar>>(dicionario_barra);
            Data.calculo_Nos = JsonConvert.DeserializeObject<Dictionary<byte, Knot>>(dicionario_nos);
        }
        #endregion
    }
}
