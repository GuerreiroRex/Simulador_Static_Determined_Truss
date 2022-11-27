using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Pages
{
    internal abstract class PaginaBase
    {
        protected TabControl tab;
        protected TabPage pagina = new TabPage();
        protected Tela tela;

        public PaginaBase(TabControl tabControl)
        {
            tab = tabControl;

            Panel painel = new Panel();
            //painel.Size = new Size(550, 350);
            painel.Size = new Size(711, 400);
            painel.BorderStyle = BorderStyle.FixedSingle;

            //painel.Visible = false;
            painel.BackColor = Color.White;

            tela = new Tela(painel);

            tab.Selected += PrepararTela;
            //tela.Refazer(false);
        }

        public virtual void PrepararTela(object sender, EventArgs e)
        {
            tela.Painel.Location = new Point(pagina.Width - tela.Painel.Width, 0);

            pagina.Controls.Add(tela.Painel);

            tela.Desenhar();
        }
    }
}
