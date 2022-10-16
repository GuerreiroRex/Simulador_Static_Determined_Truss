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

namespace CalculoTre.Telas
{
    public partial class deQuantidadeGrade : Form
    {
        public deQuantidadeGrade()
        {
            InitializeComponent();

            deHorizontal.Value = Data.Resolucao[0];
            deVertical.Value = Data.Resolucao[1];

            deValorHorizontal.Value = Data.EscalaHorizontal;
            deValorVertical.Value = Data.EscalaVertical;
        }

        private void deConfirmar_Click(object sender, EventArgs e)
        {
            Data.Resolucao[0] = (int)deHorizontal.Value;
            Data.Resolucao[1] = (int)deVertical.Value;

            Data.EscalaVertical = (int)deValorVertical.Value;
            Data.EscalaHorizontal = (int)deValorHorizontal.Value;

            this.Close();
        }
    }
}
