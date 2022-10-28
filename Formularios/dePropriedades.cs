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
        private Type    tipoEscolhido;

        Tela telaPropriedades;

        public dePropriedades(object item)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;

            telaPropriedades = new Tela(deProjecao);

            tipoEscolhido = item.GetType();

            switch (tipoEscolhido.Name)
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

            Shown += telaPropriedades.Redesenhar;
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
            ConfigBarra configBarra = new ConfigBarra(telaPropriedades, noEscolhido, this);
        }
        #endregion

        private async void DesenharBarra(Bar atual)
        {
            await Task.Yield();
            telaPropriedades.Redesenhar();
        }

        private void deFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void RedesenharTela(object sender, EventArgs e)
        {
            await Task.Yield();

            telaPropriedades.Limpar();
            telaPropriedades.Desenhar(sender, e);

            switch (tipoEscolhido.Name)
            {
                case "Bar":
                    telaPropriedades.Esquematizar(barraEscolhida);
                    break;
                case "Knot":
                    telaPropriedades.Esquematizar(noEscolhido);
                    break;
            }
        }
    }
}
