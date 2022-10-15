using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Grid
    {
        public static void Desenhar(Panel tela, int resolucao)
        {
            //
        }

        public static void LimparTela(Panel tela)
        {
            Knot.Reiniciar();
            tela.Controls.Clear();
            using (Graphics g = tela.CreateGraphics())
            {
                g.Clear(Color.White);
            }
        }
    }
}
