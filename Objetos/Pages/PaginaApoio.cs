using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Pages
{
    internal class PaginaApoio : PaginaBase
    {

        public PaginaApoio (TabControl tab, Knot noEscolhido) : base(tab)
        {
            pagina.Text = noEscolhido.ToString();
            tab.Controls.Add(pagina);
        }
    }
}
