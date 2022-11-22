using CalculoTre.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CalculoTre.Calculos
{
    internal static class Calcular
    {
        public static void InicioCalculo()
        {
            MetodoStiffEmBarra();


            Knot noDuplo = IdentificarApoio();

            List<Knot> nosLigados = IdetificarAdemais(noDuplo);

            CalcularMomento(noDuplo, nosLigados);
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

            if (incognitas.Count != 1)
                throw new Exception("Apoios inválidos");

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


        private static void AcharTravas()
        {
            List<Knot> travas = new List<Knot>();

            foreach (Knot no in Data.nos.Values)
            {
                if (no.travas[0])
                {

                }
                if (no.travas[1])
                {

                }
            }
        }

        private static void MetodoStiffEmBarra()
        {
            int q = Data.barras.Values.Count;
            
            double[,] matrizN = new double[q, q];
            double[] matrizF = new double[q];

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

                        colunaY.Add(vertical);
                        colunaX.Add(horizontal);
                    }
                    else
                    {
                        colunaX.Add(0);
                        colunaY.Add(0);
                    }
                }

                double fAplicadaX = no.ForceX * -1;
                double fAplicadaY = no.ForceY * -1;
            }

            
        }



    }
}
