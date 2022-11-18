using CalculoTre.Objetos.Configuração_Propriedades;
using System;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Pages
{
    internal class PaginaApoio : PaginaBase
    {
        protected Knot noEscolhido;

        public PaginaApoio(TabControl tab, Knot no) : base(tab)
        {
            noEscolhido = no;

            pagina.Text = noEscolhido.ToString();

            ConfigApoio configBarra = new ConfigApoio(tela, noEscolhido, pagina);

            tab.Controls.Add(pagina);

            //PrepararTela(new object(), new EventArgs());
        }

        public override void PrepararTela(object sender, EventArgs e)
        {
            base.PrepararTela(sender, e);

            tela.Esquematizar(noEscolhido, false);
        }
    }
}
