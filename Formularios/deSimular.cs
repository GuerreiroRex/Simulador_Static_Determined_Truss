using CalculoTre.Objetos;
using CalculoTre.Telas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CalculoTre
{
    public partial class deSimular : Form
    {
        private Tela telaPrincipal;

        public deSimular()
        {
            InitializeComponent();
            Inicializar();

            this.Shown += telaPrincipal.Desenhar;
        }

        public void Inicializar()
        {
            PrepararComboBoxes();
            DimensionarPainel();
            telaPrincipal = new Tela(deTela);
            Data.telas.Add(0, telaPrincipal);

            telaPrincipal.AtualizarDados();
        }   

        private void PrepararComboBoxes()
        {
            Data.deTipo = deTipo;
            Data.deObjeto = deObjeto;

            deTipo.Items.Clear();
            deObjeto.Items.Clear();

            Dictionary<byte, string> comboSource = new Dictionary<byte, string>();
            comboSource.Add(0, "Apoios");
            comboSource.Add(1, "Barras");

            deTipo.DataSource = new BindingSource(comboSource, null);
            deTipo.DisplayMember = "Value";
            deTipo.ValueMember = "Key";
        }

        private void DimensionarPainel()
        {
            deTela.Width = this.Size.Width - deTela.Location.X - 28;
            deTela.Height = (this.Size.Height) - deTela.Location.Y - 51;
        }

        private void CliquePainel(object sender, MouseEventArgs e)
        {
            if (!telaPrincipal.DentroDoQuadro(sender, e))
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    Joint.PrimeiroClique(sender, e);
                    break;

                case MouseButtons.Right:
                    deTela.MouseDown -= Joint.SegundoClique;
                    break;
            }

            AtualizarListaObjetos(sender, new EventArgs());
        }

        public void AtualizarListaObjetos(object sender, EventArgs e)
        {
            Trigger.AtualizarObjeto(deTipo, deObjeto);
        }

        private void deProp_Click(object sender, EventArgs e)
        {
            if (deObjeto.SelectedItem == null)
                return;

            dePropriedades dePropriedades;
            switch (deTipo.SelectedIndex)
            {
                case 0:
                    var apoioTemp = ((KeyValuePair<byte, Knot>)deObjeto.SelectedItem).Value;
                    dePropriedades = new dePropriedades(apoioTemp);
                    dePropriedades.ShowDialog();
                    break;
                case 1:
                    var barraTemp = ((KeyValuePair<string, Bar>)deObjeto.SelectedItem).Value;
                    dePropriedades = new dePropriedades(barraTemp);
                    dePropriedades.ShowDialog();
                    break;
            }

            telaPrincipal.Redesenhar();
        }

        private void deConfigurarTela_Click(object sender, EventArgs e)
        {
            deQuantidadeGrade grade = new deQuantidadeGrade();
            grade.ShowDialog();
            telaPrincipal.Redesenhar();
        }

        private void deSimular_SizeChanged(object sender, EventArgs e)
        {
            DimensionarPainel();

            telaPrincipal.Redesenhar();
        }

        private void deLimpar_Click(object sender, EventArgs e)
        {
            Data.Reiniciar();
            telaPrincipal.Redesenhar();
        }
    }
}
