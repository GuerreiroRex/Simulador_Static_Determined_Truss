using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigBarra: ConfigBase
    {
        public ConfigBarra(Tela tela, TabPage pagina) :base(tela)
        {
            pagina.Controls.Add(Controle);
        }
    }
}
