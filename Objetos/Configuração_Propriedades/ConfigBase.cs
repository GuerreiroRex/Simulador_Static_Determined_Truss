using System.Drawing;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal abstract class ConfigBase
    {
        protected FlowLayoutPanel Controle;
        protected const int largura = 90;
        protected int altura = 430;

        protected Font fonte = new Font("Arial", 8);

        public ConfigBase(Tela tela_recebida)
        {
            Controle = new FlowLayoutPanel();

            Controle.Location = new Point(5, 10);
            Controle.Width = largura;
            Controle.Height = altura;

            Controle.Margin = new Padding(3);
        }
    }
}
