using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Data
    {
        //Classe criada para guardar algumas informações entra formulários.

        public static ComboBox deTipo;
        public static ComboBox deObjeto;
        public static int[] Resolucao = new int[2] { 1, 1 };

        public static int EscalaVertical = 100;
        public static int EscalaHorizontal = 100;
    }
}
