using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace CalculoTre.Objetos
{
    public partial class Tela
    {
        public void Desenhar()
        {
            AtualizarDados();

            #region Dados
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Black);
            caneta.Width = (float)1;

            //Os pontos de cada extremidade
            Point superiorEsquerdo = new Point(a, a);
            Point superiorDireito = new Point(c, a);
            Point inferiorEsquerdo = new Point(a, b);
            Point inferiorDireito = new Point(c, b);
            #endregion

            //Cria o gráfico para desenhar na  tela
            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //Desenha cada linha da borda do quadro
                g.DrawLine(caneta, superiorEsquerdo, superiorDireito);
                g.DrawLine(caneta, superiorEsquerdo, inferiorEsquerdo);
                g.DrawLine(caneta, inferiorEsquerdo, inferiorDireito);
                g.DrawLine(caneta, inferiorDireito, superiorDireito);

                //Posiciona as linahs em X dos quadriculados
                for (int i = 1; i < Data.Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX(i * Data.EscalaHorizontal / Data.Resolucao[0]), a, ValorParaPosX(i * Data.EscalaHorizontal / Data.Resolucao[0]), b);

                //Posiciona as linahs em Y dos quadriculados
                for (int i = 1; i < Data.Resolucao[1]; i++)
                    g.DrawLine(caneta, a, ValorParaPosY(i * Data.EscalaVertical / Data.Resolucao[1]), c, ValorParaPosY(i * Data.EscalaVertical / Data.Resolucao[1]));

                /*
                PrepararLetras prepararLetras = new PrepararLetras(Letreiro);
                prepararLetras.Invoke();
                */

                Letreiro();

                Esquematizar();

            }
        }

        public void Desenhar(object sender, EventArgs args)
        {
            AtualizarDados();
            #region Dados
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Black);
            caneta.Width = (float)1;

            //Os pontos de cada extremidade
            Point superiorEsquerdo = new Point(a, a);
            Point superiorDireito = new Point(c, a);
            Point inferiorEsquerdo = new Point(a, b);
            Point inferiorDireito = new Point(c, b);
            #endregion

            //Cria o gráfico para desenhar na  tela
            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                //Desenha cada linha da borda do quadro
                g.DrawLine(caneta, superiorEsquerdo, superiorDireito);
                g.DrawLine(caneta, superiorEsquerdo, inferiorEsquerdo);
                g.DrawLine(caneta, inferiorEsquerdo, inferiorDireito);
                g.DrawLine(caneta, inferiorDireito, superiorDireito);

                Letreiro();
            }
        }

        private void Letreiro()
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

            //Letreiros em X
            for (int i = 0; i <= Data.Resolucao[0]; i++)
            {
                #region talvez uma classe a parte
                Label marca = new Label();

                marca.AutoSize = false;
                marca.Height = a;
                marca.Width = a;
                marca.Font = new Font("Arial", 6);
                #endregion

                marca.TextAlign = ContentAlignment.TopCenter;
                marca.Text = (i * Data.EscalaHorizontal / Data.Resolucao[0]).ToString();

                marca.Location = new Point((int)(unidade_CorteX * i + a / 2), Painel.Height - a + 1);

                tela.Controls.Add(marca);
            }

            //Letreiros em Y
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
                marca.Location = new Point(0, (int)(unidade_CorteY * i + a - marca.Height / 2));

                tela.Controls.Add(marca);
            }
        }

        public void Redesenhar()
        {
            Limpar();

            Joint.AtualizarNos();

            Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);

            Desenhar();
            Esquematizar();
        }

        public bool DentroDoQuadro(object sender, MouseEventArgs e)
        {
            if (e.X >= a && e.X < c && e.Y >= a && e.Y < b)
                return true;
            else
                return false;
        }

        //Desenha todas as barras e nós
        public void Esquematizar(Bar barra)
        {
            barra.DrawLine(this);
        }

        public void Esquematizar(bool limpeza = false)
        {
            foreach (var barra in Data.barras.Values)
            {
                barra.DrawLine(this);
                Pontuar(barra.knots[0]);
                Pontuar(barra.knots[1]);
            }
        }

        public void Pontuar(Knot no)
        {
            //Cria o botão
            no.botao = new Button();

            //Aplica-lhe um nome
            no.botao.Name = $"B{no.id}";

            //Coloca seu fundo preto, letra branca e escreve uma letra
            no.botao.BackColor = System.Drawing.Color.Black;
            no.botao.ForeColor = Color.White;

            //botao.Text = nome;
            no.botao.Text = no.id.ToString();

            //Define o tamanho do botão
            no.botao.Height = Knot.tamanho;
            no.botao.Width = Knot.tamanho;

            //Define a posição do botão e retira sua borda
            var dv = Knot.tamanho / 2;
            no.botao.FlatAppearance.BorderSize = 0;
            no.botao.Location = new System.Drawing.Point(ValorParaPosX(no.valorX) - dv, ValorParaPosY(no.valorY) - dv);

            no.botao.AutoSize = true;
            no.botao.AutoSizeMode = AutoSizeMode.GrowOnly;

            //Se o botão for apertado
            no.botao.MouseDown += (s, e) =>
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        Triggers.JuntarApoios = true;
                        Joint.Apoio = no;

                        //Se o primeiro clique aindã não foi dado
                        if (!Triggers.PrimeiroClique)
                            Joint.PrimeiroClique(Joint.sender, Joint.e);
                        else
                            Joint.SegundoClique(Joint.sender, Joint.e);

                        break;

                    case MouseButtons.Right:

                        tela.MouseDown -= Joint.SegundoClique;


                        Dictionary<string, Bar> dictBarraTemp = new Dictionary<string, Bar>();

                        foreach (var barra in Data.barras)
                            if (barra.Value.knots.Contains(no))
                                dictBarraTemp.Add(barra.Key, barra.Value);

                        foreach (var barra in dictBarraTemp)
                            Data.barras.Remove(barra.Key);

                        Redesenhar();

                        break;
                }
            };

            tela.Controls.Add(no.botao);
        }









        public void Limpar()
        {
            tela.MouseDown -= Joint.SegundoClique;

            tela.Controls.Clear();

            using (Graphics g = tela.CreateGraphics())
                g.Clear(Color.White);
        }

    }
}
