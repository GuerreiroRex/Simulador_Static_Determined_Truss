using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigBarra : ConfigBase
    {
        public ConfigBarra(Tela tela, TabPage pagina) : base(tela)
        {
            pagina.Controls.Add(Controle);
        }
    }
}
