using CalculoTre.Calculos;
using CalculoTre.Objetos;
using CalculoTre.Telas;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            //telaPrincipal.Refazer(true);
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
                    //var apoioTemp = ((KeyValuePair<byte, Knot>)deObjeto.SelectedItem).Value;
                    var apoioTemp = deObjeto.SelectedValue as Knot;
                    dePropriedades = new dePropriedades(apoioTemp);
                    dePropriedades.ShowDialog();
                    break;
                case 1:
                    //var barraTemp = ((KeyValuePair<string, Bar>)deObjeto.SelectedItem).Value;
                    var barraTemp = deObjeto.SelectedValue as Bar;
                    dePropriedades = new dePropriedades(barraTemp);
                    dePropriedades.ShowDialog();
                    break;
            }

            telaPrincipal.Redesenhar();
        }

        private void deConfigurarTela_Click(object sender, EventArgs e)
        {
            deQuantidadeGrade grade = new deQuantidadeGrade(telaPrincipal);
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

        private void deSalvar_Click(object sender, EventArgs e)
        {
            Port.SalvarDados(Data.barras, telaPrincipal);
            MessageBox.Show("Feito");
        }

        private void deCarregar_Click(object sender, EventArgs e)
        {
            Port.CarregarDados(ref Data.barras, telaPrincipal);

            telaPrincipal.Redesenhar(new object(), new EventArgs());
        }

        private void deCalcular_Click(object sender, EventArgs e)
        { 
            Calcular.InicioCalculo(telaPrincipal);
        }
    }
}
