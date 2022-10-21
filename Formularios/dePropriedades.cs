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

        Tela telaPropriedades;

        public dePropriedades(object item)
        {
            InitializeComponent();

            telaPropriedades = new Tela(deProjecao);
            telaPropriedades.Redesenhar();

            deValorX.Maximum = int.MaxValue;
            deValorY.Maximum = int.MaxValue;

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

        #region Barras
        private void Barras()
        {
            deTitulo.Text = barraEscolhida.ToString();
            DesenharBarra(barraEscolhida);
        }
        #endregion

        #region Apoios
        private void Apoios()
        {
            deTitulo.Text = noEscolhido.ToString();

            deValorX.Value = noEscolhido.ValorX;
            deValorY.Value = noEscolhido.ValorY;

            foreach (var barra in Data.barras.Values)
                if (barra.knots.Contains(noEscolhido))
                    DesenharBarra(barra);

            ApoiosValor();
        }

        private void ApoiosValor()
        {
            Label valorX = new Label();
            valorX.Text = $"Valor em X:\t{noEscolhido.ValorX}";
            valorX.Location = new Point(0, 30);

            Label valorY = new Label();
            valorY.Text = $"Valor em Y:\t{noEscolhido.ValorY}";
            valorY.Location = new Point(0, 60);

            deMenu.Controls.Add(valorX);
            deMenu.Controls.Add(valorY);
        }
        #endregion

        private async void DesenharBarra(Bar atual)
        {
            await Task.Delay(500);
            //atual.DrawLineRelative(telaPropriedades);
            //atual.Pontuar(telaPropriedades, true);
        }

        private void deFechar_Click(object sender, EventArgs e)
        {
            noEscolhido.ValorX = (int)deValorX.Value;
            noEscolhido.ValorY = (int)deValorY.Value;

            //noEscolhido.Reposicionar();
            this.Close();
        }
    }
}
