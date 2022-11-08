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
        
        protected Font fonte = new Font("Arial", 8);

        public ConfigBase (Tela tela)
        {
            Controle = new FlowLayoutPanel();


            Controle.Location = new Point(5, 10);
            Controle.Width = largura;
            Controle.Height = altura;

            Controle.Margin = new Padding(3);
        }
    }
}
