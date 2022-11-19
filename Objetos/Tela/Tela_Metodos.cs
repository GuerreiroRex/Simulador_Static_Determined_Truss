using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Tela
    {
        public async void Desenhar()
        {
            await Task.Yield();
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
                for (int i = 1; i < Resolution.Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX(i * Resolution.EscalaHorizontal / Resolution.Resolucao[0]), a, ValorParaPosX(i * Resolution.EscalaHorizontal / Resolution.Resolucao[0]), b);

                //Posiciona as linahs em Y dos quadriculados
                for (int i = 1; i < Resolution.Resolucao[1]; i++)
                    g.DrawLine(caneta, a, ValorParaPosY(i * Resolution.EscalaVertical / Resolution.Resolucao[1]), c, ValorParaPosY(i * Resolution.EscalaVertical / Resolution.Resolucao[1]));

                /*
                PrepararLetras prepararLetras = new PrepararLetras(Letreiro);
                prepararLetras.Invoke();
                */

                Letreiro();
            }
        }

        public async void Desenhar(object sender, EventArgs args)
        {
            await Task.Yield();

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

                for (int i = 1; i < Resolution.Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX(i * Resolution.EscalaHorizontal / Resolution.Resolucao[0]), a, ValorParaPosX(i * Resolution.EscalaHorizontal / Resolution.Resolucao[0]), b);

                //Posiciona as linahs em Y dos quadriculados
                for (int i = 1; i < Resolution.Resolucao[1]; i++)
                    g.DrawLine(caneta, a, ValorParaPosY(i * Resolution.EscalaVertical / Resolution.Resolucao[1]), c, ValorParaPosY(i * Resolution.EscalaVertical / Resolution.Resolucao[1]));

                Letreiro();
            }
        }

        private void Letreiro()
        {
            using (Graphics g = tela.CreateGraphics())
            {
                int tamanhoFonte = 6;
                Font fonte = new Font("Arial", tamanhoFonte);

                //for (int i = 0; i <= Resolution.Resolucao[0]; i++)
                for (int i = Resolution.Resolucao[0]; i > 0; i--)
                {
                    string temp = (i * Resolution.EscalaHorizontal / Resolution.Resolucao[0]).ToString();

                    int size = g.MeasureString(temp, fonte).ToSize().Width;

                    g.DrawString(temp,
                                    fonte,
                                    new SolidBrush(Color.Black),
                                    new Point((int)(unidade_CorteX * i + a - size / 2), Painel.Height - tamanhoFonte * 2));

                }

                for (int i = 0; i < Resolution.Resolucao[1]; i++)
                {
                    g.DrawString(((Resolution.Resolucao[1] - i) * Resolution.EscalaVertical / Resolution.Resolucao[1]).ToString(),
                                     new Font("Arial", 6),
                                     new SolidBrush(Color.Black),
                                     new Point(0, (int)unidade_CorteY * i + a));
                }

                g.DrawString("0", fonte, new SolidBrush(Color.Black), new Point(0, tela.Height - tamanhoFonte * 2));
            }
        }

        public async void Redesenhar()
        {
            await Task.Yield();

            Limpar();

            Joint.AtualizarNos();

            Trigger.AtualizarObjeto(Data.deTipo, Data.deObjeto);

            Desenhar();
            Esquematizar();
        }

        public async void Redesenhar(object sender, EventArgs e)
        {
            await Task.Yield();

            using (Graphics g = tela.CreateGraphics())
            {
                g.Clear(Color.White);
                tela.Controls.Clear();
            }

            Joint.AtualizarNos();

            Trigger.AtualizarObjeto(Data.deTipo, Data.deObjeto);

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
        public void Esquematizar(bool gatilho = true)
        {
            if (gatilho)
            {
                foreach (var barra in Data.barras.Values)
                {
                    Tracejar(barra);
                    Pontuar(barra.knots[0], gatilho);
                    Pontuar(barra.knots[1], gatilho);
                }
            }
            else
            {
                foreach (var barra in Data.barras.Values)
                {
                    Tracejar(barra);
                    Pontuar(barra.knots[0], true);
                    Pontuar(barra.knots[1], true);
                }
            }
        }

        public void Esquematizar(Bar barra, bool gatilho = false)
        {
            Tracejar(barra);
            Pontuar(barra.knots[0], gatilho);
            Pontuar(barra.knots[1], gatilho);
        }

        public void Esquematizar(Knot no, bool gatilho = false)
        {
            List<Bar> barras = Data.barras.Values.Where(x => x.knots.Contains(no)).ToList();

            foreach (Bar barra in barras)
            {
                Pontuar(barra.knots[0], gatilho);
                Pontuar(barra.knots[1], gatilho);
                Tracejar(barra);
            }
        }

        private async void Tracejar(Bar barra)
        {
            await Task.Yield();

            Pen caneta = new Pen(Color.Blue);
            caneta.Width = (float)2;

            var a = Data.nos.Values.ToList().Where<Knot>(x => x.ID == barra.knots[0].ID).First();
            var b = Data.nos.Values.ToList().Where<Knot>(x => x.ID == barra.knots[1].ID).First();

            Point ponto1 = new Point(ValorParaPosX(a.ValorX), ValorParaPosY(a.ValorY));
            Point ponto2 = new Point(ValorParaPosX(b.ValorX), ValorParaPosY(b.ValorY));

            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(caneta, ponto1, ponto2);
            }

        }

        public void Pontuar(Knot no, bool gatilho)
        {
            //Cria o botão
            //botao
            Button botao = new Button();

            //Aplica-lhe um nome
            botao.Name = $"B{no.id}";


            //Coloca seu fundo preto, letra branca e escreve uma letra
            botao.BackColor = System.Drawing.Color.Black;
            botao.ForeColor = Color.White;

            //botao.Text = nome;
            botao.Text = no.id.ToString();

            //Define o tamanho do botão
            botao.Height = Knot.tamanho;
            botao.Width = Knot.tamanho;

            //Define a posição do botão e retira sua borda
            var dv = Knot.tamanho / 2;
            botao.FlatAppearance.BorderSize = 0;
            botao.Location = new System.Drawing.Point(ValorParaPosX(no.valorX) - dv, ValorParaPosY(no.valorY) - dv);

            botao.AutoSize = true;
            botao.AutoSizeMode = AutoSizeMode.GrowOnly;

            if (gatilho)
            {
                //Se o botão for apertado
                botao.MouseDown += (s, e) =>
                {
                    switch (e.Button)
                    {
                        case MouseButtons.Left:

                            Trigger.JuntarApoios = true;
                            Joint.Apoio = no;

                            //Se o primeiro clique aindã não foi dado
                            if (!Trigger.PrimeiroClique)
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
            }
            else
            {
                using (Graphics g = tela.CreateGraphics())
                {
                    if (no.ForceX != 0 || no.ForceY != 0)
                    {
                        AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                        Pen p = new Pen(Color.Orange, (float)2.5);
                        p.CustomEndCap = bigArrow;

                        Point p1 = new Point(botao.Location.X + dv, botao.Location.Y + dv);

                        byte vetor = 10;

                        double rad = no.Angulo * (Math.PI / 180);

                        double valorX = Math.Cos(rad) * vetor;

                        double valorY = Math.Sin(rad) * vetor;

                        Point p2 = new Point((int)(botao.Location.X + dv + valorX), (int)(botao.Location.Y + dv + valorY));

                        g.DrawLine(p, botao.Location, p2);
                    }
                }
            }



            tela.Controls.Add(botao);
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
