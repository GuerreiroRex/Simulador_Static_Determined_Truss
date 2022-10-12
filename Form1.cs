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

namespace CalculoTre
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Joint.deTela = deTela;
        }

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
            } else
            {
                LimparTela();
            }
        }

        public void LimparTela()
        {
            deTela.Controls.Clear();
            using (Graphics g = deTela.CreateGraphics())
            {
                g.Clear(Color.White);
            }
            Joint.barras.Clear();
        }
    }
}
