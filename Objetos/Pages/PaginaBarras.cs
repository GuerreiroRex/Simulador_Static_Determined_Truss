using System;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Pages
{
    internal class PaginaBarra : PaginaBase
    {
        Bar barraEscolhida;

        public PaginaBarra(TabControl tab, Bar barra) : base(tab)
        {
            barraEscolhida = barra;

            pagina.Text = barraEscolhida.ID;

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
