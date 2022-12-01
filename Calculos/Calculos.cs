using CalculoTre.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace CalculoTre.Calculos
{
    internal static class Calcular
    {
        public static void InicioCalculo(Tela tela)
        {
            if (Data.barras.Values.Count == 0)
            {
                MessageBox.Show("Sem barras");
                return;
            }

            int conta = 0;

            foreach (Knot no in Data.nos.Values)
                foreach (bool trava in no.travas)
                    if (trava)
                        conta++;

            if (conta + Data.barras.Values.Count != 2 * Data.nos.Count)
            {
                MessageBox.Show("\tEsta treliça é indeterminada estaticamente, e o algoritmo utilizado é projetado para calcular apenas treliças estaticamente determinadas. Para ser estaticamente determinada a treliça deve obedecer o seguinte critéio:" +
                    "\n\n(Qtd. de Barras) + (Qtd. de Reações de Apoio) = 2 * (Qtd. de Nós)" +
                    $"\n\nEsta treliça tem: \n{Data.barras.Values.Count} Barras\n{Data.nos.Values.Count(x => x.travas.Any(y => y == true))} Reações de apoio\n{Data.nos.Count} Nós");
                return;
            }

            /*
            Thread calculo = new Thread(MetodoStiffEmBarra);
            calculo.Start();

            calculo.Join();
            */

            MetodoStiffEmBarra();

            tela.Redesenhar();
        }

        private static List<Knot> IdentificarIncognitas(List<Knot> nos)
        {
            List<Knot> incognitas = new List<Knot>();

            foreach (Knot no in nos)
            {
                if (no.travas[0])
                {
                    /*
                    Force temp = new Force();
                    temp.Nome = $"iV_{no.ToString()}";
                    temp.angulo = 270;
                    temp.posX = no.valorX;
                    temp.calculado = false;
                    no.forcas.Add(temp);
                    */

                    if (!incognitas.Contains(no))
                        incognitas.Add(no);
                }
                if (no.travas[1])
                {
                    /*
                    Force temp = new Force();
                    temp.Nome = $"iH_{no.ToString()}";
                    temp.angulo = 180;
                    temp.posX = no.valorY;
                    temp.calculado = false;
                    no.forcas.Add(temp);
                    */

                    if (!incognitas.Contains(no))
                        incognitas.Add(no);
                }
            }

            return incognitas;
        }

        #region Calculo de ditâncias
        private static int CalcularDistanciaX(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorX - secundario.valorX);
        }

        private static int CalcularDistanciaY(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorY - secundario.valorY);
        }

        public static double CalcularHip(Bar barra)
        {
            double hip = Math.Sqrt(Math.Pow(CalcularDistanciaX(barra.knots[0], barra.knots[1]), 2) + Math.Pow(CalcularDistanciaY(barra.knots[0], barra.knots[1]), 2));
            return hip;
        }
        #endregion

        private static void MetodoStiffEmBarra()
        {
            List<List<double>> matrizN = MontarMatrizNos(Data.barras, Data.nos); // Matriz N para Nós

            List<double> matrizF = MontarMatrizForcas(Data.nos); //Matriz F para Forças

            //EscreverMatriz(matrizN, "0 - Matriz Principal");

            List<List<double>> invertida = CalcularMatrizInversa(matrizN);

            //EscreverMatriz(invertida, "1 - Matriz Invertida");

            List<double> resultado = CalcularProdutoMatrizes(invertida, matrizF);
            //EscreverMatriz(resultado, "2 - Matriz Multiplicada");

            int i = 0;
            foreach (Bar barra in Data.barras.Values)
                barra.Force = resultado[i++];

            Port.ImportarCalculos();
        }

        private static List<List<double>> MontarMatrizNos(Dictionary<string, Bar> dictB, Dictionary<byte, Knot> dictN)
        {
            List<Knot> incognitas = IdentificarIncognitas(dictN.Values.ToList());

            //Veja quantas barras conectadas
            int q = dictB.Values.Count;

            List<List<double>> matrizN = new List<List<double>>(); //Matriz N para Nós

            // Monta a matriz de valores
            foreach (Knot no in dictN.Values)
            {
                List<Bar> barrasConectadas = dictB.Values.Where(x => x.knots.Contains(no)).ToList();

                List<double> colunaX = new List<double>();
                List<double> colunaY = new List<double>();

                foreach (Bar barra in dictB.Values)
                {
                    if (barrasConectadas.Contains(barra))
                    {


                        //double vertical = (barra.knots[0].valorY - barra.knots[1].valorY) / CalcularHip(barra);
                        //double horizontal = (barra.knots[0].valorX - barra.knots[1].valorX) / CalcularHip(barra);

                        double vertical = (NoDiferente(no, barra.knots).valorY - no.valorY) / CalcularHip(barra);
                        double horizontal = (NoDiferente(no, barra.knots).valorX - no.valorX) / CalcularHip(barra);

                        colunaX.Add(horizontal);
                        colunaY.Add(vertical);
                    }
                    else
                    {
                        colunaX.Add(0);
                        colunaY.Add(0);
                    }
                }

                foreach (Knot inco in incognitas)
                {
                    if (inco == no)
                    {
                        if (inco.travas[0])
                        {
                            colunaY.Add(1);
                            colunaX.Add(0);
                        }
                        if (inco.travas[1])
                        {
                            colunaY.Add(0);
                            colunaX.Add(1);
                        }
                    }
                    else
                    {
                        if (inco.travas[0])
                        {
                            colunaY.Add(0);
                            colunaX.Add(0);
                        }
                        if (inco.travas[1])
                        {
                            colunaY.Add(0);
                            colunaX.Add(0);
                        }
                    }
                }

                matrizN.Add(colunaX);
                matrizN.Add(colunaY);
            }

            return matrizN;
        }

        private static Knot NoDiferente(Knot principal, Knot[] nos)
        {
            if (principal != nos[0])
                return nos[0];
            else
                return nos[1];
        }

        private static List<double> MontarMatrizForcas(Dictionary<byte, Knot> dict)
        {
            List<double> matrizF = new List<double>(); //Matriz F para Forças

            foreach (Knot no in dict.Values)
            {
                matrizF.Add(no.ForceX * -1);
                matrizF.Add(no.ForceY * -1);
            }

            return matrizF;
        }

        private static double CalcularDeterminante(List<List<double>> matriz)
        {
            List<List<double>> calculo = new List<List<double>>();

            /* Eliminação de Gauss-Jordan
             */

            //Copia a matriz, para não macular a original
            foreach (List<double> lista in matriz)
            {
                List<double> copy = new List<double>();
                copy.AddRange(lista);

                calculo.Add(copy);
            }

            //Pega o tamanho da matriz
            int q0 = calculo.Count;
            int q1 = calculo[0].Count;

            //Para cada linha da matriz
            for (int i = 0; i < q0 - 1; i++)
            {
                //Pega o pivo
                double pivo = calculo[i][i];

                //Se o pivo for zero, troca ele por outra linha
                if (pivo == 0)
                    for (int linha = i + 1; linha < q0; linha++)
                        if (calculo[linha][i] != 0)
                        {
                            List<double> linha_original = new List<double>();
                            linha_original.AddRange(calculo[i]);

                            List<double> linha_nova = new List<double>();
                            linha_nova.AddRange(calculo[linha]);

                            for (int num = 0; num < linha_nova.Count; num++)
                                linha_nova[num] *= -1;

                            calculo[i] = linha_nova;
                            calculo[linha] = linha_original;

                            pivo = calculo[i][i];

                            break;
                        }

                for (int linha = i + 1; linha < q0; linha++)
                {
                    if (calculo[linha][i] == 0)
                        continue;

                    double divisor = calculo[linha][i] / pivo;

                    for (int coluna = 0; coluna < q1; coluna++)
                        calculo[linha][coluna] -= divisor * calculo[i][coluna];
                }
            }

            double valor = 1;
            for (int i = 0; i < q0; i++)
                valor *= calculo[i][i];

            //EscreverMatriz(calculo, "Teste - Determinante");

            return valor;
        }

        private static List<List<double>> CalcularMatrizInversa(List<List<double>> matriz)
        {
            /* 
             * 
             */

            List<List<double>> basica = new List<List<double>>();
            List<List<double>> invertida = new List<List<double>>();

            double det = CalcularDeterminante(matriz);

            if (det == 0)
                throw new Exception("Calculo indevido");

            //Pega o tamanho da matriz
            int q0 = matriz.Count;
            int q1 = matriz[0].Count;


            //Para cada linha da tabela
            for (int linha = 0; linha < q0; linha++)
            {
                //Para cada coluna
                List<double> linha_basica = new List<double>();
                for (int coluna = 0; coluna < q1; coluna++)
                {
                    //
                    List<List<double>> calculo = new List<List<double>>();
                    for (int i = 0; i < q0; i++)
                    {
                        if (i == linha)
                            continue;
                        
                        List<double> linha_valores = new List<double>();
                        for (int j = 0; j < q1; j++)
                        {
                            if (j == coluna)
                                continue;
                            
                            linha_valores.Add(matriz[i][j]);
                        }
                        calculo.Add(linha_valores);
                    }

                    linha_basica.Add(CalcularDeterminante(calculo) * Math.Pow(-1, linha + coluna));
                }
                basica.Add(linha_basica);
            }

            for (int linha = 0; linha < q0; linha++)
            {
                List<double> linha_invertida = new List<double>();
                for (int coluna = 0; coluna < q1; coluna++)
                {
                    linha_invertida.Add(basica[coluna][linha] / det);
                }
                invertida.Add(linha_invertida);
            }

            return invertida;
        }

        private static List<double> CalcularProdutoMatrizes(List<List<double>> matriz, List<double> forcas)
        {
            List<double> multipla = new List<double>();

            //Pega o tamanho da matriz
            int q0 = matriz.Count;
            int q1 = matriz[0].Count;

            for (int linha = 0; linha < q0; linha++)
            {
                double linha_for = 0;
                for (int coluna = 0; coluna < q1; coluna++)
                {
                    linha_for += matriz[linha][coluna] * forcas[coluna];
                }
                multipla.Add(linha_for);
            }

            return multipla;
        }

        #region Escrita
        private static void EscreverMatriz(List<List<double>> matriz, string nome)
        {
            string texto = ";";

            List<Knot> abc = Data.nos.Values.ToList();

            foreach (Bar barra in Data.barras.Values)
                texto += barra.ID + ";";

            foreach (Knot no in IdentificarIncognitas(Data.nos.Values.ToList()))
            {
                if (no.travas[0])
                    texto += no.Nome + "_Y;";

                if (no.travas[1])
                    texto += no.Nome + "_X;";
            }

            texto += Environment.NewLine;

            for (int linha = 0; linha < matriz.Count; linha++)
            {
                if (linha % 2 == 0)
                    texto += abc[linha / 2].Nome + "_X;";
                else
                    texto += abc[linha / 2].Nome + "_Y;";


                for (int coluna = 0; coluna < matriz[linha].Count; coluna++)
                    texto += $"{matriz[linha][coluna]};";

                texto += Environment.NewLine;
            }

            File.WriteAllText($"{nome}.csv", texto);

        }

        private static void EscreverMatriz(List<double> matrizF, string nome)
        {
            string texto = null;
            var barras = Data.barras.Values.ToList();

            for (int linha = 0; linha < matrizF.Count; linha++)
            {
                if (linha < barras.Count)
                    texto += $"{barras[linha].ID};";
                else
                    texto += $";";

                texto += $"{matrizF[linha]};";

                texto += Environment.NewLine;
            }

            File.WriteAllText($"{nome}.csv", texto);

        }
        #endregion
    }
}
