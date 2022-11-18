using CalculoTre.Objetos;
using System;
using System.Windows.Forms;

namespace CalculoTre.Telas
{
    public partial class deQuantidadeGrade : Form
    {
        public deQuantidadeGrade()
        {
            InitializeComponent();

            deHorizontal.Value = Resolution.Resolucao[0];
            deVertical.Value = Resolution.Resolucao[1];

            deValorHorizontal.Value = Resolution.EscalaHorizontal;
            deValorVertical.Value = Resolution.EscalaVertical;
        }

        private void deConfirmar_Click(object sender, EventArgs e)
        {
            Resolution.Resolucao[0] = (int)deHorizontal.Value;
            Resolution.Resolucao[1] = (int)deVertical.Value;

            Resolution.EscalaVertical = (int)deValorVertical.Value;
            Resolution.EscalaHorizontal = (int)deValorHorizontal.Value;

            this.Close();
        }
    }
}
