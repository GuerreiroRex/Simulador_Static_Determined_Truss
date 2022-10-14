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

                foreach (var no in tempKnot)
                {
                    try
                    {
                        nos.Add(no.ID, no);
                    } catch { }
                }

                Array.Clear(tempKnot, 0, tempKnot.Length);

                Esquematizar();

                deTela.MouseDown -= SegundoClique;
                Triggers.PrimeiroClique = false;

                Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);
            }
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
