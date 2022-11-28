using CalculoTre.Objetos;
using System;
using System.Windows.Forms;

namespace CalculoTre.Telas
{
    public partial class deQuantidadeGrade : Form
    {
        protected Tela tela;
        public deQuantidadeGrade(Tela tela_recebida)
        {
            tela = tela_recebida;
            InitializeComponent();

            deHorizontal.Value = Tela.Resolucao[0];
            deVertical.Value = Tela.Resolucao[1];

            deValorHorizontal.Value = tela.MaximoHorizontal;
            deValorVertical.Value = tela.MaximoVertical;
        }

        private void deConfirmar_Click(object sender, EventArgs e)
        {
            Tela.Resolucao[0] = (int)deHorizontal.Value;
            Tela.Resolucao[1] = (int)deVertical.Value;

            tela.MaximoVertical = (int)deValorVertical.Value;
            tela.MaximoHorizontal = (int)deValorHorizontal.Value;

            this.Close();
        }
    }
}
