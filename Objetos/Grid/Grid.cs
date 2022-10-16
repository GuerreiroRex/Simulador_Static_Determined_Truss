using System;
using System.Collections.Generic;
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

        public static int corteX;
        public static int corteY;

        public static int ValorParaPosX(int valor)
        {
            return (valor * (c - a) / Data.EscalaHorizontal) + a;
        }

        public static int ValorParaPosY(int valor)
        {
            return ((Data.EscalaVertical - valor) * (b - a) / Data.EscalaVertical) + a;
        }

        public static int PosParaValorX(int pos)
        {
            //Imprecisso?
            return pos * Data.EscalaHorizontal / (c - a);
        }

        public static int PosParaValorY(int pos)
        {
            //Imprecisso?
            return ((b - a) - pos) * Data.EscalaVertical / (b - a);
        }
    }
}
