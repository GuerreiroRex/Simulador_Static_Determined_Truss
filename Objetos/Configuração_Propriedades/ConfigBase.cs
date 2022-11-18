using System.Drawing;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal abstract class ConfigBase
    {
        protected FlowLayoutPanel Controle;
        protected const int largura = 90;
        protected const int altura = 320;

        protected Font fonte = new Font("Arial", 8);

        public ConfigBase(Tela tela)
        {
            Controle = new FlowLayoutPanel();
            //Controle.BackColor = Color.LightGreen;

            Controle.Location = new Point(5, 10);
            Controle.Width = largura;
            Controle.Height = altura;

            Controle.Margin = new Padding(3);
        }
    }
}
