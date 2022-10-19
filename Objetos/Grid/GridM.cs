using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static partial class Grid
    {
        public static List<int> valoresTempX = new List<int>();

        public static void AtualizarValoresGrid(Panel atela = null)
        {
            //Calcula as bordas do quarto
            if (atela != null)
                tela = atela;

            a = Knot.Tamanho;
            b = tela.Height - a;
            c = tela.Width - a;

            corteX = (double)(c - a) / Data.Resolucao[0];
            corteY = (double)(b - a) / Data.Resolucao[1];
        }

        //Desenha o quadro e seus quadriculados na tela.
        public static async void Desenhar()
        {
            AtualizarValoresGrid();
            await Task.Delay(100);
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Black);
            caneta.Width = (float)1;

            //Os pontos de cada extremidade
            Point superiorEsquerdo  = new Point(a, a);
            Point superiorDireito   = new Point(c, a);
            Point inferiorEsquerdo  = new Point(a, b);
            Point inferiorDireito   = new Point(c, b);


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
                for (int i = 1; i < Data.Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX((i * Data.EscalaHorizontal / Data.Resolucao[0])), a, (int)(a + corteX * i), b);

                for (int i = 1; i < Data.Resolucao[1]; i++)
                    g.DrawLine(caneta, a, (int)(a + corteY * i), c, (int)(a + corteY * i));

                Letreiro();
            }
        }

        public static void Letreiro()
        {
            #region Codigo de suporte
            /* Marcar coordenadas X no grafico
            foreach (int valor in valoresTempX)
            {
                //Horizontal
                #region talvez uma classe a parte
                Label marca = new Label();

                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);
                #endregion

                marca.TextAlign = ContentAlignment.TopCenter;
                marca.Text = (valor).ToString();
                marca.Location = new Point(valor - a / 2, tela.Height - a / 2 + 1);

                tela.Controls.Add(marca);
            }
            */
            #endregion

            for (int i = 0; i <= Data.Resolucao[0]; i++)
            {
                //Horizontal
                #region talvez uma classe a parte
                Label marca = new Label();

                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);
                #endregion

                marca.TextAlign = ContentAlignment.TopCenter;
                marca.Text = (i * Data.EscalaHorizontal / Data.Resolucao[0]).ToString();
                marca.Location = new Point((int)(corteX * i + a /2), tela.Height - a + 1);

                tela.Controls.Add(marca);
            }

            for (int i = 0; i < Data.Resolucao[1]; i++)
            {
                #region talvez uma classe a parte
                Label marca = new Label();

                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);
                #endregion

                marca.TextAlign = ContentAlignment.MiddleRight;
                marca.Text = ((Data.Resolucao[1] - i) * Data.EscalaVertical / Data.Resolucao[1]).ToString();
                marca.Location = new Point(0, (int)(corteY * i + a - marca.Height / 2));

                tela.Controls.Add(marca);
            }
        }

        //Limpa a tela e apaga todas as informações
        public static void LimparTela(Panel tela)
        {
            tela.MouseDown -= Joint.SegundoClique;
            Knot.Reiniciar();
            tela.Controls.Clear();
            using (Graphics g = tela.CreateGraphics())
            {
                g.Clear(Color.White);
            }
            Desenhar();
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
            Knot.Reposicionar(Joint.nos);

            Joint.Esquematizar();
            Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);

            Desenhar();
        }
    }
}
