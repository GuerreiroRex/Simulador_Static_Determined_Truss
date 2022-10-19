using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static partial class Grid
    {
        public static Panel tela;

        public static int a;
        public static int b;
        public static int c;

        public static double corteX;
        public static double corteY;

        public static int ValorParaPosX(int valor)
        {
            double ajuste = (double)(tela.Width - 2 * a) / (double)tela.Width * (double)tela.Width * valor / Data.EscalaHorizontal;
            return (int)(ajuste + a);
        }

        public static int ValorParaPosY(int valor)
        {
            double ajuste = (double)(tela.Height - 2 * a) / (double)tela.Height * (double)tela.Height * (Data.EscalaVertical - valor) / Data.EscalaVertical;
            return (int)(ajuste + a);
        }

        public static int PosParaValorX(int pos)
        {
            double valor = (double)(pos - a) * Data.EscalaHorizontal / (tela.Width - 2 * a);
            return (int)valor;
        }

        public static int PosParaValorY(int pos)
        {
            double valor = (double)((tela.Height - pos) - a) * Data.EscalaVertical / (tela.Height - 2 * a);
            return (int)valor;
        }
    }
}
