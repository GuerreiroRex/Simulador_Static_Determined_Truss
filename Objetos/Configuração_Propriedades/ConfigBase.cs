using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal abstract class ConfigBase
    {
        protected FlowLayoutPanel Controle;
        protected const int largura = 90;
        protected const int altura = 260;
        protected Knot noEscolhido;

        public ConfigBase (Knot no)
        {
            noEscolhido = no;

            Controle = new FlowLayoutPanel ();
            Controle.Location = new Point(10, 10);
            Controle.Width = largura;
            Controle.Height = altura;

            Controle.Margin = new Padding(3);

            Label Titulo = new Label();
            Titulo.Width = Controle.Width;
            Titulo.Location = new Point(0, 0);
            Titulo.Text = noEscolhido.ToString();

            Controle.Controls.Add(Titulo);
        }
    }
}
