using CalculoTre.Objetos.Configuração_Propriedades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            int maior;
            int valor_tela;

            List<Bar> barrasConectadas = Data.barras.Values.Where(x => x.knots.Contains(no)).ToList();

            int maior_x = barrasConectadas.Max(x => x.knots.Max(y => y.ValorX));
            int maior_y = barrasConectadas.Max(x => x.knots.Max(y => y.ValorY));

            if (maior_x > maior_y)
                maior = maior_x;
            else
                maior = maior_y;


            valor_tela = (int)(maior * Tela.proporcionalidade);
            //int min_x = barrasConectadas.Max(x => x.knots.Min(y => y.ValorX));
            //int min_y = barrasConectadas.Max(x => x.knots.Min(y => y.ValorY));

            tela.MaximoHorizontal = maior + (maior / Tela.Resolucao[0]);
            tela.MaximoVertical = valor_tela + (valor_tela / Tela.Resolucao[1]);

            ConfigApoio configApoio = new ConfigApoio(tela, noEscolhido, pagina);

            tab.Controls.Add(pagina);
        }

        public override async void PrepararTela(object sender, EventArgs e)
        {
            await Task.Delay(500);
            base.PrepararTela(sender, e);

            tela.Esquematizar(noEscolhido);
        }
    }
}
