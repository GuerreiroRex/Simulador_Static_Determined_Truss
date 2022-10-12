using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Joint
    {
        public static Panel deTela;
        public static Knot[] tempKnot = new Knot[2];
        public static Dictionary<string, Bar> barras = new Dictionary<string, Bar>();

        public static object sender;
        public static MouseEventArgs e;

        public static Knot Apoio;

        public static void PrimeiroClique(object sender, MouseEventArgs e)
        {
            Joint.sender = sender;
            Joint.e = e;

            if (Triggers.JuntarApoios)
            {
                tempKnot[0] = Apoio;
                Triggers.JuntarApoios = false;
            }
            else
            {
                tempKnot[0] = new Knot(deTela, "A", e.Location.X, e.Location.Y);
            }

            Triggers.PrimeiroClique = true;

            tempKnot[0].Desenhar();

            deTela.MouseDown += SegundoClique;
        }

        public static void SegundoClique(object sender, MouseEventArgs e)
        {
            if (Triggers.JuntarApoios)
            {
                tempKnot[1] = Apoio;
                Triggers.JuntarApoios = false;
            }
            else
            {
                tempKnot[1] = new Knot(deTela, "B", e.Location.X, e.Location.Y);
            }
            tempKnot[1].Desenhar();

            Bar barra = new Bar(tempKnot[0], tempKnot[1]);
            barras.Add(barra.ID, barra);

            Array.Clear(tempKnot, 0, tempKnot.Length);

            Esquematizar();

            deTela.MouseDown -= SegundoClique;
            Triggers.PrimeiroClique = false;
        }

        public static void Esquematizar()
        {
            foreach (var barra in barras.Values)
            {
                barra.DrawLine(deTela);
                barra.Pontuar();
            }
        }
    }
}
