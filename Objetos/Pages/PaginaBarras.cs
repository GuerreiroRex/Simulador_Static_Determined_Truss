using CalculoTre.Objetos.Configuração_Propriedades;
using System;
using System.Linq;
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

            int maior;
            int valor_tela;

            int maior_x = barraEscolhida.knots.Max(x => x.ValorX);
            int maior_y = barraEscolhida.knots.Max(y => y.ValorY);

            if (maior_x > maior_y)
                maior = maior_x;
            else
                maior = maior_y;

            valor_tela = (int)(maior * Tela.proporcionalidade);
            //int min_x = barraEscolhida.knots.Min(x => x.ValorX);
            //int min_y = barraEscolhida.knots.Min(x => x.ValorY);


            tela.MaximoHorizontal = maior + (maior / Tela.Resolucao[0]);
            tela.MaximoVertical = valor_tela + (valor_tela / Tela.Resolucao[1]);

            ConfigBarra configBarra = new ConfigBarra(tela, barra, pagina, tab);

            tab.Controls.Add(pagina);

            //PrepararTela(new object(), new EventArgs());
        }

        public override void PrepararTela(object sender, EventArgs e)
        {
            base.PrepararTela(sender, e);

            tela.Esquematizar(barraEscolhida);
        }
    }
}
