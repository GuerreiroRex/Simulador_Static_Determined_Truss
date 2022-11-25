using CalculoTre.Objetos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace CalculoTre.Calculos
{
    internal static class Calcular
    {
        public static void InicioCalculo()
        {
            if (Data.barras.Values.Count == 0)
            {
                MessageBox.Show("Sem barras");
                return;
            }

            #region Matrizes de testes
            List<double> linha1 = new List<double>();
            linha1.Add(1);
            linha1.Add(0);
            linha1.Add(1);

            List<double> linha2 = new List<double>();
            linha2.Add(2);
            linha2.Add(-2);
            linha2.Add(-1);

            List<double> linha3 = new List<double>();
            linha3.Add(3);
            linha3.Add(0);
            linha3.Add(0);


            List<List<double>> matrizL = new List<List<double>>();
            matrizL.Add(linha1);
            matrizL.Add(linha2);
            matrizL.Add(linha3);
            //double det = CalcularDeterminante(matrizL);
            #endregion

            //var a = CalcularMatrizInversa(matrizL);

            Thread calculo = new Thread(MetodoStiffEmBarra);
            calculo.Start();

            //MetodoStiffEmBarra();

            /*
            Knot noDuplo = IdentificarApoio();

            List<Knot> nosLigados = IdetificarAdemais(noDuplo);

            CalcularMomento(noDuplo, nosLigados);
            */
        }

        #region Metodo antigo
        #region Pacote
        private static Knot IdentificarApoio()
        {
            List<Knot> noDuploTemp = Data.nos.Values.Where(x => x.travas[0] == true && x.travas[1] == true).ToList();
            Knot noDuplo;

            if (noDuploTemp.Count > 1)
                throw new System.Exception("Quantidade de apoios duplos excede 1 (um). Programa incapaz de calcular.");
            else
                noDuplo = noDuploTemp[0];

            return noDuplo;
        }

        private static List<Knot> IdetificarAdemais(Knot principal, bool shift = false)
        {
            if (shift)
            {
                List<Knot[]> vetores = Data.barras.Values.Where(x => x.knots.Contains(principal)).Select(x => x.knots).ToList();

                List<Knot> nosLigados = new List<Knot>();

                foreach (Knot[] vetor in vetores)
                    foreach (Knot no in vetor)
                        if (no != principal && !nosLigados.Contains(no))
                            nosLigados.Add(no);

                return nosLigados;
            } else
            {
                List<Knot> nosLigados = new List<Knot>();

                foreach (Knot no in Data.nos.Values)
                    if (no != principal && !nosLigados.Contains(no))
                        nosLigados.Add(no);

                return nosLigados;
            }
        }
        #endregion

        private static void CalcularMomento(Knot principal, List<Knot> ademais)
        {
            Knot incognitasDeMomento = IdentificarIncognitas(ademais).First();

            // Separando forças que giram o momento no sentido horário do antihorario
            List<Force> Horarias = ademais.SelectMany(x => x.forcas).Where(f => IdentificarHorario(principal, f)).ToList();
            List<Force> AntiHorarias = ademais.SelectMany(x => x.forcas).Where(f => IdentificarAntiHorario(principal, f)).ToList();

            // Forças verticais tem seu valor medido pelo eixo X, vice-versa
            double somaHorariaX = Horarias.Sum(x => x.ForcaX * CalcularDistanciaY(principal, x.posY));
            double somaHorariaY = Horarias.Sum(y => y.ForcaY * CalcularDistanciaX(principal, y.posX));
            double somaHoraria = Math.Abs(somaHorariaX) + Math.Abs(somaHorariaY);

            double somaAntiHorariaX = AntiHorarias.Sum(x => x.ForcaX * CalcularDistanciaY(principal, x.posY));
            double somaAntiHorariaY = AntiHorarias.Sum(y => y.ForcaY * CalcularDistanciaX(principal, y.posX));
            double somaAntiHoraria = Math.Abs(somaAntiHorariaX) + Math.Abs(somaAntiHorariaY);

            
            double somaMomento;
            // Se a posição for negativa, é anti-horario
            if (incognitasDeMomento.valorX < principal.valorX)    
                somaMomento = somaHoraria - somaAntiHoraria;
            else
                somaMomento = somaAntiHoraria - somaHoraria;

            incognitasDeMomento.forcas[0].vetor = somaMomento / CalcularDistanciaX(principal, incognitasDeMomento);

            Force fixoV = new Force(0, 270, principal);
            Force fixoH = new Force(0, 180, principal);

            /* Lógica do equilibrio Cima/Baixo
             * 
             * Cima = Baixo
             * Cima = Baixo + fixoV
             * fixoV = Cima - Baixo
             */
            double Cima = Math.Abs(ademais.Where(x => x.ForceY > 0).Sum(x => x.ForceY));
            double Baixo = Math.Abs(ademais.Where(x => x.ForceY < 0).Sum(x => x.ForceY));

            fixoV.vetor = Math.Abs ( Cima - Baixo );
            

            /* Lógica do equilibrio Esquerda/Diereita
             * 
             * Esquerda = Direita
             * Esquerda + ficoH = Direita
             * fixoV = Direita - Esquerda
             */
            double Esquerda = Math.Abs(ademais.Where(x => x.ForceX < 0).Sum(x => x.ForceX));
            double Direita = Math.Abs(ademais.Where(x => x.ForceX > 0).Sum(x => x.ForceX));

            fixoH.vetor = Math.Abs ( Direita - Esquerda );
            

            principal.forcas.Add(fixoH);
            principal.forcas.Add(fixoV);
            //------------------------------------------------------------------------------------------------------------------

            CalcularForcaBarras(principal, ademais);
        }

        private static void CalcularForcaBarras(Knot principal, List<Knot> ademais)
        {
            /*
             * 
             * CONTINUAR DAQUI
             * 
             * 
             */

            CalcularEmNo(principal);

            int quantidade = 0;

            do
            {
                /*
                foreach (Bar barra in Data.barras.Values)
                {
                    CalcularEmBarra(barra);  
                }
                 */

                foreach (Knot n in Data.nos.Values)
                    CalcularEmNo(n);

                if (quantidade++ >= Math.Pow(Data.nos.Values.Count, 2))
                {
                    MessageBox.Show("Não foi possível");
                    break;
                }

            } while (Data.barras.Values.Any(x => !x.forceCalc));

            string valor = null;
            foreach (Bar barra in Data.barras.Values)
            {
                valor += $"{barra.ID}:\t{barra.Force}\n";
            }

            MessageBox.Show(valor);

        }

        private static void CalcularEmBarra(Bar barra)
        {
            foreach (Knot no in barra.knots)
            {
                if (no.id == 3)
                {
                    bool a = false;
                }

                double fVertical = no.ForceY;
                double fHorizontal = no.ForceX;

                List<Bar> barrasConectadas = Data.barras.Values.Where(x => x.knots.Contains(no)).ToList();

                List<Bar> barrasVerticais = barrasConectadas.Where(b =>
                CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 0 &&
                CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 180).ToList();

                List<Bar> barrasHorizontais = barrasConectadas.Where(b =>
                CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 90 &&
                CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 270).ToList();

                List<Bar> verticaisInc = barrasVerticais.Where(x => x.forceCalc == false).ToList();
                if (verticaisInc.Count == 1)
                {
                    Bar vertical = verticaisInc.First();
                    
                    double angulo = CalcularAnguloHip(no, DevolverDiferente(no, vertical.knots));

                    vertical.Angulo = angulo / (180 / Math.PI);
                    var seno = Math.Sin(vertical.Angulo);


                    List<Bar> barrasCalculadas = barrasVerticais.Where(x => x.forceCalc == true && x != vertical).ToList();

                    double fInfluenciaC = barrasCalculadas.Where(x => x.Force > 0).Sum(y => y.Force * Math.Sin(y.Angulo));
                    double fInfluenciaB = barrasCalculadas.Where(x => x.Force < 0).Sum(y => y.Force * Math.Sin(y.Angulo));

                    /*
                     * Cima = Baixo
                     * 
                     *  fInfluenciaC = Fbarra + FInfluenciaB
                     * 
                     */
                    vertical.Force = Math.Round( (fVertical + fInfluenciaC - fInfluenciaB) / seno, 2);
                    vertical.forceCalc = true;
                }

                List<Bar> horizontaisInc = barrasHorizontais.Where(x => x.forceCalc == false).ToList();
                if (horizontaisInc.Count == 1)
                {
                    Bar horizontal = horizontaisInc.First();

                    double angulo = CalcularAnguloHip(no, DevolverDiferente(no, horizontal.knots)) / (180 / Math.PI);

                    horizontal.Angulo = angulo / (180 / Math.PI);
                    var cos = Math.Sin(horizontal.Angulo);

                    double fInfluenciaE = barrasVerticais.Where(x => x.Force < 0).Sum(y => y.Force * Math.Cos(y.Angulo));
                    double fInfluenciaD = barrasVerticais.Where(x => x.Force > 0).Sum(y => y.Force * Math.Cos(y.Angulo));


                    horizontal.Force = Math.Round( fHorizontal + fInfluenciaE - fInfluenciaD, 2 );
                    horizontal.forceCalc = true;
                }
            }

            
        }

        private static void CalcularEmNo(Knot no)
        {
            List<Bar> barrasConectadas = Data.barras.Values.Where(x => x.knots.Contains(no)).ToList();

            List<Bar> barrasVerticais = barrasConectadas.Where(b =>
            CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 0 &&
            CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 180).ToList();

            List<Bar> barrasHorizontais = barrasConectadas.Where(b =>
            CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 90 &&
            CalcularAnguloHip(no, DevolverDiferente(no, b.knots)) != 270).ToList();

            /* Continuar daqui
             * 
             * Nenhum calculo vai ser feito porque nenhuma barra tem apenas uma variável
             * 
             * Pedir resolução para um colega
             * 
             */

            foreach (Bar barra in barrasConectadas)
            {
                double fVertical = no.ForceY;
                double fHorizontal = no.ForceX;

                List<Bar> verticaisInc = barrasVerticais.Where(x => x.forceCalc == false).ToList();
                if (verticaisInc.Count == 1)
                {
                    Bar vertical = verticaisInc.First();

                    double angulo = CalcularAnguloHip(no, DevolverDiferente(no, vertical.knots));

                    vertical.Angulo = angulo / (180 / Math.PI);
                    var seno = Math.Sin(vertical.Angulo);


                    List<Bar> barrasCalculadas = barrasVerticais.Where(x => x.forceCalc == true && x != vertical).ToList();

                    double fInfluenciaC = barrasCalculadas.Where(x => x.Force > 0).Sum(y => y.Force * Math.Sin(y.Angulo));
                    double fInfluenciaB = barrasCalculadas.Where(x => x.Force < 0).Sum(y => y.Force * Math.Sin(y.Angulo));

                    /*
                     * Cima = Baixo
                     * 
                     *  fInfluenciaC = Fbarra + FInfluenciaB
                     * 
                     */
                    vertical.Force = Math.Round((fVertical + fInfluenciaC - fInfluenciaB) / seno, 2);
                    vertical.forceCalc = true;
                }

                List<Bar> horizontaisInc = barrasHorizontais.Where(x => x.forceCalc == false).ToList();
                if (horizontaisInc.Count == 1)
                {
                    Bar horizontal = horizontaisInc.First();

                    double angulo = CalcularAnguloHip(no, DevolverDiferente(no, horizontal.knots)) / (180 / Math.PI);

                    horizontal.Angulo = angulo / (180 / Math.PI);
                    var cos = Math.Sin(horizontal.Angulo);

                    double fInfluenciaE = barrasVerticais.Where(x => x.Force < 0).Sum(y => y.Force * Math.Cos(y.Angulo));
                    double fInfluenciaD = barrasVerticais.Where(x => x.Force > 0).Sum(y => y.Force * Math.Cos(y.Angulo));


                    horizontal.Force = Math.Round(fHorizontal + fInfluenciaE - fInfluenciaD, 2) * -1;
                    horizontal.forceCalc = true;
                }
            }


        }

        private static double CalcularAnguloHip(Knot principal, Knot secundario)
        {
            double cateteA = CalcularDistanciaX(principal, secundario);
            double cateteO = CalcularDistanciaY(principal, secundario);

            double angulo = Math.Atan2(cateteO, cateteA);
            return (180 / Math.PI) * angulo; //Retorna o valor em Graus
        }

        #region Manipulação de listas
        private static Knot DevolverDiferente(Knot alvo, Knot[]vetor)
        {
            if (vetor[0] != alvo)
                return vetor[0];
            else
                return vetor[1];
        }

        private static List<Knot> IdentificarIncognitas(List<Knot> nos)
        {
            List<Knot> incognitas = new List<Knot>();

            foreach (Knot no in nos)
            {
                if (no.travas[0])
                {
                    Force temp = new Force();
                    temp.Nome = $"iV_{no.ToString()}";
                    temp.angulo = 270;
                    temp.posX = no.valorX;
                    temp.calculado = false;
                    no.forcas.Add(temp);

                    if (!incognitas.Contains(no))
                        incognitas.Add(no);
                }
                if (no.travas[1])
                {
                    Force temp = new Force();
                    temp.Nome = $"iH_{no.ToString()}";
                    temp.angulo = 180;
                    temp.posX = no.valorY;
                    temp.calculado = false;
                    no.forcas.Add(temp);

                    if (!incognitas.Contains(no))
                        incognitas.Add(no);
                }
            }

            return incognitas;
        }
        #endregion

        #region Calculo de ditâncias
        private static int CalcularDistanciaX(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorX - secundario.valorX);
        }

        private static int CalcularDistanciaX(Knot principal, double pos)
        {
            return Math.Abs(principal.valorX - (int)pos);
        }

        private static int CalcularDistanciaY(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorY - secundario.valorY);
        }

        private static int CalcularDistanciaY(Knot principal, double pos)
        {
            var a = Math.Abs(principal.valorY - (int)pos);
            return a;
        }
        #endregion

        #region Identificação de Rotação
        private static bool IdentificarHorario(Knot principal, Force force)
        {
            if ((force.ForcaX > 0 && force.posY > principal.valorY) ||
                (force.ForcaY < 0 && force.posX > principal.valorX) ||
                (force.ForcaX < 0 && force.posY < principal.valorY) ||
                (force.ForcaY > 0 && force.posX < principal.valorX))
                return true;
            else
                return false;
        }

        private static bool IdentificarAntiHorario(Knot principal, Force force)
        {
            if ((force.ForcaX < 0 && force.posY > principal.valorY) ||
                (force.ForcaY > 0 && force.posX > principal.valorX) ||
                (force.ForcaX > 0 && force.valorY < principal.valorY) ||
                (force.ForcaY < 0 && force.posY < principal.valorX))
                return true;
            else
                return false;
        }
        #endregion


        #endregion

        private static double CalcularHip(Bar barra)
        {
            double hip = Math.Sqrt(Math.Pow(CalcularDistanciaX(barra.knots[0], barra.knots[1]), 2) + Math.Pow(CalcularDistanciaY(barra.knots[0], barra.knots[1]), 2));
            return hip;
        }

        private static void MetodoStiffEmBarra()
        {
            List<Knot> incognitas = IdentificarIncognitas(Data.nos.Values.ToList());

            //Veja quantas barras conectadas
            int q = Data.barras.Values.Count;

            List<List<double>> matrizN = new List<List<double>>(); //Matriz N para Nós
            List<double> matrizF = new List<double>(); //Matriz F para Forças   

            // Monta a matriz de valores
            foreach (Knot no in Data.nos.Values)
            {
                List<Bar> barrasConectadas = Data.barras.Values.Where(x => x.knots.Contains(no)).ToList();

                List<double> colunaX = new List<double>();
                List<double> colunaY = new List<double>();

                foreach (Bar barra in Data.barras.Values)
                {
                    if (barrasConectadas.Contains(barra))
                    {
                        double vertical = (barra.knots[0].valorY - barra.knots[1].valorY) / CalcularHip(barra);
                        double horizontal = (barra.knots[0].valorX - barra.knots[1].valorX) / CalcularHip(barra);

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
                            //colunaY.Add(-1);
                            //colunaX.Add(0);

                            colunaY.Add(-1);
                            colunaX.Add(0);
                        }
                        if (inco.travas[1])
                        {
                            //colunaY.Add(0);
                            //colunaX.Add(-1);

                            colunaY.Add(0);
                            colunaX.Add(-1);
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

                matrizF.Add(no.ForceX * -1);
                matrizF.Add(no.ForceY * -1);
            }


            EscreverMatriz(matrizN, "Matriz Principal");

            var det = CalcularDeterminante(matrizN);

            List<List<double>> invertida = CalcularMatrizInversa(matrizN);

            EscreverMatriz(invertida, "Matriz Invertida");

            MessageBox.Show("Concluido");
        }

        private static bool VerificarMatriz(List<List<double>> matriz)
        {
            int quantidade = matriz[0].Count;

            EscreverMatriz(matriz, "dadinhos");

            foreach (List<double> linha in matriz)
                if (linha.Count != quantidade)
                    return false;

            return true;

        }

        //private static double CalcularDeterminante(double[,] matriz)
        private static double CalcularDeterminante(List<List<double>> matriz)
        {
            /*
            if (!VerificarMatriz(matriz))
                throw new Exception("Matriz não é exata");
            */
            EscreverMatriz(matriz, "Matriz Recebida para Calcular a Determinante");

            //Pega o tamanho da matriz
            int q0 = matriz.Count;
            int q1 = matriz[0].Count;

            #region Matriz de calculo
            //Cria uma matriz separada, para não macular a original
            List<List<double>> calculo = new List<List<double>>();

            /* Isso é necessário porque o Add Range normal copiaria o endereço das tabelas, não nova
             * não novas cópias
             */
            foreach (List<double> lista in matriz)
            {
                List<double> copy = new List<double>();
                copy.AddRange(lista);

                calculo.Add(copy);
            }
            #endregion

            double valor = 0;

            List<double> determinantes = new List<double>();
            if (q0 > 2 || q1 > 2)
            {
                for (int j = 0; j < q1; j++)
                {
                    double pivo = calculo[0][j];
                    List<List<double>> reduzida = new List<List<double>>();

                    for (int ri = 1; ri < q0; ri++)
                    {
                        List<double> linha = new List<double>();

                        for (int rj = 0; rj < q0; rj++)
                            if (rj != j)
                                linha.Add(calculo[ri][rj]);

                        reduzida.Add(linha);
                    }

                    double calc2 = CalcularDeterminante(reduzida);
                    double calc = pivo * calc2 * Math.Pow(-1, 2 + j);

                    determinantes.Add(calc);
                }
            }
            else
            {
                valor = (calculo[0][0] * calculo[1][1]) - (calculo[1][0] * calculo[0][1]);
                return valor;
            }

            valor = determinantes.Sum();
            return valor;
        }

        //private static double[,] CalcularMatrizInversa(double[,] matriz)
        private static List<List<double>> CalcularMatrizInversa(List<List<double>> matriz)
        {
            List<List<double>> basica = new List<List<double>>();
            List<List<double>> invertida = new List<List<double>>();
            double det = CalcularDeterminante(matriz);

            //Pega o tamanho da matriz
            int q0 = matriz.Count;
            int q1 = matriz[0].Count;

            for (int linha = 0; linha < q0; linha++)
            {
                List<double> linha_basica = new List<double>();
                for (int coluna = 0; coluna < q1; coluna++)
                {
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

        private static void EscreverMatriz(List<List<double>> matriz, string nome)
        {
            string texto = null;

            for (int linha = 0; linha < matriz.Count; linha++)
            {
                for (int coluna = 0; coluna < matriz[linha].Count; coluna++)
                    texto += $"{matriz[linha][coluna]};";

                texto += Environment.NewLine;
            }

            string caminho = @"C:\Users\User\Desktop\dados\" + $"{nome}.csv";
            File.WriteAllText(caminho, texto);

        }
    }
}
