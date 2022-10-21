using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    //Classe criada para guardar algumas informações entra formulários.
    public static class Data
    {
        public static Dictionary<byte, Tela> telas = new Dictionary<byte, Tela>();

        public static Dictionary<string, Bar> barras = new Dictionary<string, Bar>();
        public static Dictionary<byte, Knot> nos = new Dictionary<byte, Knot>();

        public static ComboBox deTipo;
        public static ComboBox deObjeto;
        public static int[] Resolucao = new int[2] { 1, 1 };

        public static int EscalaVertical = 100;
        public static int EscalaHorizontal = 100;

        public static void Reiniciar()
        {
            nos.Clear();
            barras.Clear();
            Knot.Quantidade = 0;
        }
    }
}
