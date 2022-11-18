using System.Collections.Generic;
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

        public static void Reiniciar()
        {
            nos.Clear();
            barras.Clear();
            Knot.Quantidade = 0;
        }
    }
}
