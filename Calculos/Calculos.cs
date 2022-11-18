using CalculoTre.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

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

            double distancia_HA = Math.Abs(nosHorarios.Sum(x => CalcularDistanciaX(principal, x) * Math.Round(x.ForceY, 6)));
            double distancia_HB = Math.Abs(nosHorarios.Sum(x => CalcularDistanciaY(principal, x) * Math.Round(x.ForceX, 6)));


            double distancia_AHA = Math.Abs(nosAntihorario.Sum(x => CalcularDistanciaX(principal, x) * Math.Round(x.ForceY, 6)));
            double distancia_AHB = Math.Abs(nosAntihorario.Sum(x => CalcularDistanciaY(principal, x) * Math.Round(x.ForceX, 6)));


            //incognitas[0].vetor = ((distancia_HA + distancia_HB) - (distancia_AHA + distancia_AHB)) / CalcularDistanciaX(principal, incognitas[0]);
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
                    incognitas.Add(temp);
                }
                if (no.travas[1])
                {
                    Force temp = new Force();
                    temp.Nome = $"iH_{no.ToString()}";
                    temp.angulo = 0;
                    incognitas.Add(temp);
                }
            }

            return incognitas;
        }
    }
}
