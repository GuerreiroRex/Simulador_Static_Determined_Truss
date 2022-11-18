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
            painel.Size = new Size(450, 300);
            painel.BorderStyle = BorderStyle.FixedSingle;

            //painel.Visible = false;
            painel.BackColor = Color.White;

            tela = new Tela(painel);

            tab.Selected += PrepararTela;
            //tab.Selected += tela.Redesenhar;
        }

        public virtual void PrepararTela(object sender, EventArgs e)
        {
            tela.Painel.Location = new Point(pagina.Width - tela.Painel.Width, 0);

            pagina.Controls.Add(tela.Painel);

            tela.Desenhar();
        }

        public TabPage Valor { get => pagina; }

        public Tela Tela { get => tela; }
    }
}
