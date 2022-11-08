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
        public PaginaBarra(TabControl tab, Bar barraEscolhida) : base(tab)
        {
            pagina.Text = barraEscolhida.ToString();
            
            PrepararTela();

            tab.Controls.Add(pagina);            
        }

        public async void PrepararTela()
        {
            await Task.Yield();

            tela.Painel.Location = new Point(pagina.Width - tela.Painel.Width, 0);

            pagina.Controls.Add(tela.Painel);

            ConfigBarra configBarra = new ConfigBarra(tela, pagina);

            tela.Redesenhar();
        }
    }
}
