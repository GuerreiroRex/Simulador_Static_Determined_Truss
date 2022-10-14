using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    internal static class Triggers
    {
        public static bool PrimeiroClique = false;
        public static bool JuntarApoios = false;

        public static void AtualizarObjeto(ComboBox deTipo, ComboBox deObjeto)
        {
            switch (deTipo.SelectedIndex)
            {
                case 0:
                    if (Joint.nos.Count <= 0)
                        break;

                    deObjeto.DataSource = new BindingSource(Joint.nos, null);
                    deObjeto.DisplayMember = "Value";
                    deObjeto.ValueMember = "Key";
                    break;
                case 1:
                    if (Joint.barras.Count <= 0)
                        break;

                    deObjeto.DataSource = new BindingSource(Joint.barras, null);
                    deObjeto.DisplayMember = "Value";
                    deObjeto.ValueMember = "Key";
                    break;
            }
        }

        //public static event EventHandler Foo;
    }
}
