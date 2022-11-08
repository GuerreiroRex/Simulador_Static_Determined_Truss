using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Bar
    {
        //Sobrecarrega o método ToString, apenas para facilitar na hora de ler a varíavel
        public override string ToString()
        {
            return $"Barra {ID}";
        }
    }
}
