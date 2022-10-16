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
            double temp = (double)valor * tela.Width / (double)Data.EscalaHorizontal + a / 2;
            return (int)temp;
        }

        public static int ValorParaPosY(int valor)
        {
            double temp = (double)(Data.EscalaVertical - valor) * tela.Height / (double)Data.EscalaVertical + a / 2;
            return (int)temp;
        }

        public static int PosParaValorX(int pos)
        {
            double temp = (double)(pos - a / 2) * Data.EscalaHorizontal / (double)tela.Width;
            return (int)temp;
        }

        public static int PosParaValorY(int pos)
        {
            double temp = (double)(tela.Height - (pos - a / 2)) * (double)Data.EscalaVertical / (double)tela.Height;
            //Imprecisso?
            return (int)temp;
        }
    }
}
