using CalculoTre.Objetos;
using CalculoTre.Objetos.Pages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CalculoTre
{
    public partial class dePropriedades : Form
    {
        private Type tipoEscolhido;

        private TabControl tab;

        public dePropriedades(object item)
        {
            InitializeComponent();

            OrganizarTab();

            FormBorderStyle = FormBorderStyle.FixedSingle;

            tipoEscolhido = item.GetType();

            switch (tipoEscolhido.Name)
            {
                case "Bar":
                    TabBarras(item as Bar);
                    break;

                case "Knot":
                    TabApoios(item as Knot);
                    break;
            }
        }

        private void OrganizarTab()
        {
            tab = new TabControl();

            tab.SizeMode = TabSizeMode.Fixed;
            tab.ItemSize = new Size(80, 20);

            tab.Width = this.Width - 15;
            tab.Height = this.Height - tab.ItemSize.Height * 2;
        }

        #region Barras
        private void TabBarras(Bar barraEscolhida)
        {
            List<TabPage> lista = new List<TabPage>();

            PaginaBarra pagina = new PaginaBarra(tab, barraEscolhida);

            PaginaApoio paginaApoio;

            foreach (Knot no in barraEscolhida.knots)
                paginaApoio = new PaginaApoio(tab, no);

            this.Controls.Add(tab);

            this.Shown += pagina.PrepararTela;
        }
        #endregion

        #region Apoios
        private void TabApoios(Knot noEscolhido)
        {
            List<TabPage> lista = new List<TabPage>();

            PaginaApoio pagina = new PaginaApoio(tab, noEscolhido);

            PaginaBarra paginaBarra;

            foreach (Bar barra in Data.barras.Values)
            {
                if (barra.knots.Contains(noEscolhido))
                    paginaBarra = new PaginaBarra(tab, barra);
            }

            this.Controls.Add(tab);

            this.Shown += pagina.PrepararTela;
        }
        #endregion
    }
}
