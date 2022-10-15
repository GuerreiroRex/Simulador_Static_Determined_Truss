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
        public static Dictionary<byte, Knot> nos = new Dictionary<byte, Knot>();

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
                tempKnot[0] = new Knot(deTela, e.Location.X, e.Location.Y);
            }

            Triggers.PrimeiroClique = true;

            tempKnot[0].Desenhar(deTela);

            deTela.MouseDown += SegundoClique;
        }

        public static void SegundoClique(object sender, MouseEventArgs e)
        {
            if (!DentroDoQuadro(sender, e))
                return;

            if (e.Button == MouseButtons.Left)
            {
                if (Triggers.JuntarApoios)
                {
                    Triggers.JuntarApoios = false;
                    if (Apoio == tempKnot[0])
                        return;

                    tempKnot[1] = Apoio;
                }
                else
                {
                    tempKnot[1] = new Knot(deTela, e.Location.X, e.Location.Y);
                }
                tempKnot[1].Desenhar(deTela);

                Bar barra = new Bar(tempKnot[0], tempKnot[1]);
                
                barras.Add(barra.ID, barra);

                AtualizarNos();

                Array.Clear(tempKnot, 0, tempKnot.Length);

                Esquematizar();

                deTela.MouseDown -= SegundoClique;
                Triggers.PrimeiroClique = false;

                Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);
            }
        }

        public static void AtualizarNos()
        {
            List<Knot> tempNos = new List<Knot>();
            nos.Clear();

            foreach (var barra in barras.Values)
            {
                if (!tempNos.Contains(barra.knots[0]))
                {
                    nos.Add(barra.knots[0].ID, barra.knots[0]);
                    tempNos.Add(barra.knots[0]);
                }
                if (!tempNos.Contains(barra.knots[1]))
                {
                    nos.Add(barra.knots[1].ID, barra.knots[1]);
                    tempNos.Add(barra.knots[1]);
                }
            }
        }

        public static bool DentroDoQuadro(object sender, MouseEventArgs e)
        {
            if (e.X >= Knot.Tamanho / 2 && e.X < deTela.Width - Knot.Tamanho / 2 &&
                    e.Y >= Knot.Tamanho / 2 && e.Y < deTela.Height - Knot.Tamanho / 2)
                return true;
            else
                return false;
        }

        public static void Esquematizar()
        {
            foreach (var barra in barras.Values)
            {
                barra.DrawLine(deTela);
                barra.Pontuar(deTela);
            }
        }
    }
}
