using CalculoTre.Objetos.Configuração_Propriedades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalculoTre.Objetos.Pages
{
    internal class PaginaBarra : PaginaBase
    {
        Bar barraEscolhida;

        public PaginaBarra(TabControl tab, Bar barra) : base(tab)
        {
            barraEscolhida = barra; 

            pagina.Text = barraEscolhida.ToString();

            tab.Controls.Add(pagina);

            PrepararTela(new object(), new EventArgs());
        }

        public override void PrepararTela(object sender, EventArgs e)
        {
            base.PrepararTela(sender, e);

            tela.Esquematizar(barraEscolhida);
        }
    }
}
