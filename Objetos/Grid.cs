using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Grid
    {
        public static async void Desenhar(Panel tela, int[] resolucao)
        {
            await Task.Delay(100);
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Black);
            caneta.Width = (float)1.5;

            var a = Knot.Tamanho / 2;
            var b = tela.Height - a;
            var c = tela.Width - a;

            Point superiorEsquerdo =    new Point(a, a);
            Point superiorDireito =     new Point(c, a);
            Point inferiorEsquerdo =    new Point(a, b);
            Point inferiorDireito =     new Point(c, b);

            int corteX = 0;
            int corteY = 0;

            corteX = (tela.Width - 2 * a) / resolucao[0];
            corteY = (tela.Height - 2 * a) / resolucao[1];

            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.DrawLine(caneta, superiorEsquerdo, superiorDireito);
                g.DrawLine(caneta, superiorEsquerdo, inferiorEsquerdo);
                g.DrawLine(caneta, inferiorEsquerdo, inferiorDireito);
                g.DrawLine(caneta, inferiorDireito, superiorDireito);

                for (int i = 1; i <= resolucao[0]; i++)
                    g.DrawLine(caneta, a + corteX * i, a, a + corteX * i, b);

                for (int i = 1; i <= resolucao[1]; i++)
                    g.DrawLine(caneta, a, a + corteY * i, c, a + corteY * i);
            }
        }

        public static void LimparTela(Panel tela)
        {
            Knot.Reiniciar();
            tela.Controls.Clear();
            using (Graphics g = tela.CreateGraphics())
            {
                g.Clear(Color.White);
            }
            Desenhar(tela, Data.Resolucao);
        }

        public static void RedesenharTela(Panel tela)
        {
            tela.Controls.Clear();
            using (Graphics g = tela.CreateGraphics())
            {
                g.Clear(Color.White);
            }

            Joint.AtualizarNos();
            Joint.Esquematizar();
            Desenhar(tela, Data.Resolucao);
            Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);
        }
    }
}
