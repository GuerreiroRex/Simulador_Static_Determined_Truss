using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Pages
{
    internal abstract class PaginaBase
    {
        protected TabControl tab;
        protected TabPage pagina = new TabPage();
        protected Tela tela;

        public PaginaBase (TabControl tabControl)
        {
            tab = tabControl;
            
            Panel painel = new Panel();
            painel.Name = "telaPagina";
            painel.Size = new Size(300, 200);
            painel.BorderStyle = BorderStyle.FixedSingle;

            tela = new Tela(painel);
        }

        public TabPage Valor { get => pagina; }

        public Tela Tela { get => tela;  }
    }
}
