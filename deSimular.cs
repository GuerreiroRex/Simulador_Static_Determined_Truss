using CalculoTre.Objetos;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalculoTre
{
    public partial class deSimular : Form
    {

        public deSimular()
        {
            InitializeComponent();
            Inicializar();
        }

        public void Inicializar()
        {
            Joint.deTela = deTela;

            Data.deTipo = deTipo;
            Data.deObjeto = deObjeto;

            deTipo.Items.Clear();

            Dictionary<byte, string> comboSource = new Dictionary<byte, string>();
            comboSource.Add(0, "Apoios");
            comboSource.Add(1, "Barras");

            deTipo.DataSource = new BindingSource(comboSource, null);
            deTipo.DisplayMember = "Value";
            deTipo.ValueMember = "Key";
        }

        #region Tela de desenho
        private void BtLimp(object sender, EventArgs e)
        {
            LimparTela();
        }

        private void JointAtualizar(object sender, MouseEventArgs e)
        {
            Joint.sender = sender;
            Joint.e = e;
        }

        private void CliquePainel(object sender, MouseEventArgs e)
        {            
            if (e.Button == MouseButtons.Left)
            {
                Joint.PrimeiroClique(sender, e);
            }

            EventArgs d = new EventArgs();
            AtualizarListaObjetos(sender, d);
        }
        #endregion

        public void AtualizarListaObjetos(object sender, EventArgs e)
        {
            Triggers.AtualizarObjeto(deTipo, deObjeto);
        }

        private void deProp_Click(object sender, EventArgs e)
        {
            propriedadesBarra dePropBarra;
            switch (deTipo.SelectedIndex)
            {
                case 0:
                    var barraTemp = ((KeyValuePair<byte, Knot>)deObjeto.SelectedItem).Value;
                    dePropBarra = new propriedadesBarra(barraTemp);
                    dePropBarra.ShowDialog();
                    break;
                case 1:
                    var apoioTemp = ((KeyValuePair<string, Bar>)deObjeto.SelectedItem).Value;
                    dePropBarra = new propriedadesBarra(apoioTemp);
                    dePropBarra.ShowDialog();
                    break;
            }
        }
    }
}
