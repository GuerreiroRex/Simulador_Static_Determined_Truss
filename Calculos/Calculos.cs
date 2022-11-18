using CalculoTre.Objetos;
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
            double reactNoDuplo;

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
            double momento;
            List<Force> incognitas = IdentificarIncognitas(ademais);

            
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

        private static List<Force> IdentificarIncognitas(List<Knot> nos)
        {
            List<Force> incognitas = new List<Force>();

            foreach (Knot no in nos)
            {
                if (no.travas[0])
                {
                    Force temp = new Force();
                    temp.Nome = $"iV_{no.ToString()}";
                    incognitas.Add(temp);
                }
                if (no.travas[1])
                {
                    Force temp = new Force();
                    temp.Nome = $"iH_{no.ToString()}";
                    incognitas.Add(temp);
                }
            }

            return incognitas;
        }
    }
}
