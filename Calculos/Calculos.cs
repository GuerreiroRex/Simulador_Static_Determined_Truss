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
        public static void CalcReaction()
        {
            Knot noDuplo = IdentificarApoio();

            List<Knot> nosLigados = IdetificarAdemais(noDuplo);

            CalcularMomento(noDuplo, nosLigados);
        }

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

        private static List<Knot> IdetificarAdemais(Knot principal)
        {
            List<Knot[]> vetores = Data.barras.Values.Where(x => x.knots.Contains(principal)).Select(x => x.knots).ToList();

            List<Knot> nosLigados = new List<Knot>();

            foreach (Knot[] vetor in vetores)
                foreach (Knot no in vetor)
                    if (no != principal && !nosLigados.Contains(no))
                        nosLigados.Add(no);

            return nosLigados;
        }

        private static void CalcularMomento(Knot principal, List<Knot> ademais)
        {
            List<Force> incognitas = IdentificarIncognitas(ademais);

            List<Knot> nosHorarios = ademais.Where(x => IdentificarHorario(principal, x)).ToList();
            List<Knot> nosAntihorario = ademais.Where(x => IdentificarAntiHorario(principal, x)).ToList();

            /*      HORARIO         -        ANTIHORARIO 
             * (Distância_H * Força_H)  =   (Distância_AH * Força_AH)
             * 
             * Sempre considerar incognita horaria
             * 
             *           HORARIO                 -        ANTIHORARIO 
             * (Distância_In * Incognita) + (Distância_H * Força_H)   =   (Distância_AH * Força_AH)
             * 
             *           HORARIO                 -        ANTIHORARIO 
             * (Distância_In * Incognita) + (Distância_H * Força_H)   =   (Distância_AH * Força_AH)
             * 
             *
             * Incognita = { (Distância_AH * Força_AH) - (Distância_H * Força_H) } / Distância_In
             */

            double distancia_HA = Math.Abs(nosHorarios.Sum(x => CalcularDistanciaX(principal, x) * x.ForceY));
            double distancia_HB = Math.Abs(nosHorarios.Sum(x => CalcularDistanciaY(principal, x) * x.ForceX));


            double distancia_AHA = Math.Abs(nosAntihorario.Sum(x => CalcularDistanciaX(principal, x) * x.ForceY));
            double distancia_AHB = Math.Abs(nosAntihorario.Sum(x => CalcularDistanciaY(principal, x) * x.ForceX));

            Force fixoV = new Force(0, 90, principal);
            Force fixoH = new Force(0, 0, principal);


            if (incognitas[0].posX != 0)
            {
                incognitas[0].vetor = ((distancia_HA + distancia_HB) - (distancia_AHA + distancia_AHB)) / incognitas[0].posX;
            }
            else
            {
                incognitas[0].vetor = ((distancia_HA + distancia_HB) - (distancia_AHA + distancia_AHB)) / incognitas[0].posY;
            }





            double Cima = Math.Abs(ademais.Where(x => x.ForceY > 0).Sum(x => x.ForceY));
            double Baixo = Math.Abs(ademais.Where(x => x.ForceY < 0).Sum(x => x.ForceY));

            fixoV.Vetor = (Baixo - Cima) - incognitas[0].vetor;
            fixoV.Decompor();



            double Esquerda = Math.Abs(ademais.Where(x => x.ForceX > 0).Sum(x => x.ForceX));
            double Direita = Math.Abs(ademais.Where(x => x.ForceX < 0).Sum(x => x.ForceX));

            fixoH.Vetor = (Esquerda - Direita);
            fixoH.Decompor();

            principal.forcas.Add(fixoH);
            principal.forcas.Add(fixoV);

            //------------------------------------------------------------------------------------------------------------------

            CalcularForcaBarras(principal, ademais);
        }

        private static void CalcularForcaBarras(Knot principal, List<Knot> ademais)
        {
            do
            {
                foreach (Bar barra in Data.barras.Values)
                {
                    CalcularEmBarra(barra);
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
            Knot no = barra.knots[0];

            if (barra.ID == "Barra (1)(2)")
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

            int verticaisInc = barrasVerticais.Where(x => x.forceCalc == false).Count();
            foreach (Bar vertical in barrasVerticais)
            {
                if (vertical.forceCalc || verticaisInc != 1)
                    continue;

                vertical.Angulo = CalcularAnguloHip(no, DevolverDiferente(no, vertical.knots)) / (180 / Math.PI);
                var seno = Math.Sin(vertical.Angulo);

                var test = barrasVerticais.Where(x => x.forceCalc == true && x != vertical);

                vertical.Force = ((fVertical * -1) + (test.Sum(x => x.Force * Math.Sin(x.Angulo)))) / seno;
                vertical.forceCalc = true;
            }
            
            int horizontaisInc = barrasHorizontais.Where(x => x.forceCalc == false).Count();
            foreach (Bar horizontal in barrasHorizontais)
            {
                if (horizontal.forceCalc || horizontaisInc != 1)
                    continue;

                double angulo = CalcularAnguloHip(no, DevolverDiferente(no, horizontal.knots)) / (180 / Math.PI);

                horizontal.Force = fHorizontal - barrasVerticais.Sum(x => x.Force * Math.Cos(x.Angulo));
                horizontal.forceCalc = true;
            }
        }

        private static double CalcularAnguloHip(Knot principal, Knot secundario)
        {
            double cateteA = CalcularDistanciaX(principal, secundario);
            double cateteO = CalcularDistanciaY(principal, secundario);

            double angulo = Math.Atan(cateteO/cateteA);
            return (180 / Math.PI) * angulo;
        }

        private static Knot DevolverDiferente(Knot alvo, Knot[]vetor)
        {
            if (vetor[0] != alvo)
                return vetor[0];
            else
                return vetor[1];
        }

        private static int CalcularDistanciaX(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorX - secundario.valorX);
        }

        private static int CalcularDistanciaY(Knot principal, Knot secundario)
        {
            return Math.Abs(principal.valorY - secundario.valorY);
        }

        private static bool IdentificarHorario(Knot principal, Knot secundario)
        {
            if ((secundario.ForceX > 0 && secundario.valorY >= principal.valorY) ||
                (secundario.ForceY < 0 && secundario.valorX >= principal.valorX) ||
                (secundario.ForceX < 0 && secundario.valorY <= principal.valorY) ||
                (secundario.ForceY > 0 && secundario.valorX <= principal.valorX))
                return true;
            else
                return false;
        }

        private static bool IdentificarAntiHorario(Knot principal, Knot secundario)
        {
            if ((secundario.ForceX < 0 && secundario.valorY >= principal.valorY) ||
                (secundario.ForceY > 0 && secundario.valorX >= principal.valorX) ||
                (secundario.ForceX > 0 && secundario.valorY <= principal.valorY) ||
                (secundario.ForceY < 0 && secundario.valorX <= principal.valorX))
                return true;
            else
                return false;
        }

        private static List<Force> IdentificarIncognitas(List<Knot> nos)
        {
            List<Force> incognitas = new List<Force>();

            foreach (Knot no in nos)
            {
                if (no.travas[0])
                {
                    Force temp = new Force();
                    temp.Nome = $"iV_{no.ToString()}";
                    temp.angulo = 90;
                    temp.posX = no.valorX;
                    incognitas.Add(temp);
                    no.forcas.Add(temp);
                }
                if (no.travas[1])
                {
                    Force temp = new Force();
                    temp.Nome = $"iH_{no.ToString()}";
                    temp.angulo = 0;
                    temp.posX = no.valorY;
                    incognitas.Add(temp);
                    no.forcas.Add(temp);
                }
            }

            return incognitas;
        }
    }
}
