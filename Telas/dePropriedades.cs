using CalculoTre.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre
{
    public partial class dePropriedades : Form
    {
        private static Bar barraEscolhida;
        private static Knot noEscolhido;

        public dePropriedades(object item)
        {
            InitializeComponent();

            switch (item.GetType().Name)
            {
                case "Bar":
                    barraEscolhida = item as Bar;
                    Barras();
                    break;

                case "Knot":
                    noEscolhido = item as Knot;
                    Apoios();
                    break;
            }
        }

        private void Barras()
        {
            deTitulo.Text = barraEscolhida.ToString();
            DesenharBarra(barraEscolhida);
        }

        private void Apoios()
        {
            deTitulo.Text = noEscolhido.ToString();

            foreach (var barra in Joint.barras.Values)
                if (barra.knots.Contains(noEscolhido))
                    DesenharBarra(barra);
        }

        private async void DesenharBarra(Bar atual)
        {
            await Task.Delay(500);
            atual.DrawLineRelative(deProjecao);
            atual.Pontuar(deProjecao, true);
        }

        private void deFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
