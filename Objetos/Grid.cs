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
        //Desenha o quadro e seus quadriculados na tela.
        public static async void Desenhar(Panel tela, int[] resolucao)
        {
            await Task.Delay(100);
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Black);
            caneta.Width = (float)1;

            //Calcula as bordas do quarto
            var a = Knot.Tamanho;
            var b = tela.Height - a;
            var c = tela.Width - a;

            //Os pontos de cada extremidade
            Point superiorEsquerdo =    new Point(a, a);
            Point superiorDireito =     new Point(c, a);
            Point inferiorEsquerdo =    new Point(a, b);
            Point inferiorDireito =     new Point(c, b);

            //Define a distância e posição das linhas do quadriculado
            int corteX = (tela.Width - 2 * a) / resolucao[0];
            int corteY = (tela.Height - 2 * a) / resolucao[1];

            //Cria o gráfico para desenhar na  tela
            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //Desenha cada linha da borda do quadro
                g.DrawLine(caneta, superiorEsquerdo, superiorDireito);
                g.DrawLine(caneta, superiorEsquerdo, inferiorEsquerdo);
                g.DrawLine(caneta, inferiorEsquerdo, inferiorDireito);
                g.DrawLine(caneta, inferiorDireito, superiorDireito);

                //Posiciona as linahs dos quadriculados
                for (int i = 1; i < resolucao[0]; i++)
                    g.DrawLine(caneta, a + corteX * i, a, a + corteX * i, b);

                for (int i = 1; i < resolucao[1]; i++)
                    g.DrawLine(caneta, a, a + corteY * i, c, a + corteY * i);

                Letreiro(tela, resolucao);
            }
        }

        //Limpa a tela e apaga todas as informações
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

        //Apaga e tela e desenha ela novamente
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

        public static void Letreiro(Panel tela, int[] resolucao)
        {
            var a = Knot.Tamanho;
            var b = tela.Height - a;
            var c = tela.Width - a;

            var ev = Data.EscalaVertical;
            var eh = Data.EscalaHorizontal;

            for (int i = 0; i <= resolucao[0]; i++)
            {
                int corteX = (tela.Width - 2 * a) / resolucao[0];

                Label marca = new Label();
                marca.Text = (i * eh / resolucao[0]).ToString();

                marca.TextAlign = ContentAlignment.TopCenter;
                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);

                marca.Location = new Point(corteX * i + a /2, tela.Height - a + 1);

                tela.Controls.Add(marca);
            }

            for (int i = 0; i < resolucao[1]; i++)
            {
                int corteY = (tela.Height - 2 * a) / resolucao[1];

                Label marca = new Label();
                marca.Text = ((resolucao[1] - i) * ev / resolucao[1]).ToString();

                marca.TextAlign = ContentAlignment.MiddleRight;
                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);

                marca.Location = new Point(0, corteY * i + a - marca.Height / 2);

                tela.Controls.Add(marca);
            }
        }
    }
}
