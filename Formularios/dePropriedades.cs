using CalculoTre.Objetos;
using CalculoTre.Objetos.Configuração_Propriedades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre
{
    public partial class dePropriedades : Form
    {
        private Bar     barraEscolhida;
        private Knot    noEscolhido;
        private Type    Escolhido;

        Tela telaPropriedades;

        public dePropriedades(object item)
        {
            InitializeComponent();

            this.Shown += RedesenharTela;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            telaPropriedades = new Tela(deProjecao);

            Escolhido = item.GetType();

            switch (Escolhido.Name)
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
            //deTitulo.Text = barraEscolhida.ToString();
            
            telaPropriedades.Painel.Controls.Clear();

            //telaPropriedades.Esquematizar(barraEscolhida);
        }
        #endregion

        #region Apoios
        private void Apoios()
        {
            ConfigBarra configBarra = new ConfigBarra(noEscolhido, this);

            
        }
        #endregion

        private async void DesenharBarra(Bar atual)
        {
            await Task.Delay(500);
            telaPropriedades.Redesenhar();
        }

        private void deFechar_Click(object sender, EventArgs e)
        {
            //noEscolhido.Reposicionar();
            this.Close();
        }

        private async void RedesenharTela(object sender, EventArgs e)
        {
            while (true)
            {
                await Task.Delay(100);
                telaPropriedades.Desenhar(sender, e);
                telaPropriedades.Esquematizar(false);
            }
        }
    }
}
