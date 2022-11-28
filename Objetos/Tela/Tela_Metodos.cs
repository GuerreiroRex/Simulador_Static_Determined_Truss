using CalculoTre.Calculos;
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
                for (int i = 1; i < Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), a, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), b);
                //g.DrawLine(caneta, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), a, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), b);

                //Posiciona as linahs em Y dos quadriculados
                for (int i = 1; i < Resolucao[1]; i++)
                    g.DrawLine(caneta, a, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]), c, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]));
                //g.DrawLine(caneta, a, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]), c, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]));

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

                for (int i = 1; i < Resolucao[0]; i++)
                    g.DrawLine(caneta, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), a, ValorParaPosX((float)i * EscalaHorizontal / Resolucao[0]), b);

                //Posiciona as linahs em Y dos quadriculados
                for (int i = 1; i < Resolucao[1]; i++)
                    g.DrawLine(caneta, a, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]), c, ValorParaPosY((float)i * EscalaVertical / Resolucao[1]));

                Letreiro();
            }
        }

        private void Letreiro()
        {
            using (Graphics g = tela.CreateGraphics())
            {
                int tamanhoFonte = 6;
                Font fonte = new Font("Arial", tamanhoFonte);

                //for (int i = 0; i <= Resolucao[0]; i++)
                for (int i = Resolucao[0]; i > 0; i--)
                {
                    string temp = (i * EscalaHorizontal / Resolucao[0]).ToString();

                    int size = g.MeasureString(temp, fonte).ToSize().Width;

                    g.DrawString(temp,
                                    fonte,
                                    new SolidBrush(Color.Black),
                                    new PointF((float)(unidade_CorteX * i + a - size / 2), Painel.Height - tamanhoFonte * 2));

                }

                for (int i = 0; i < Resolucao[1]; i++)
                {
                    g.DrawString(((Resolucao[1] - i) * EscalaVertical / Resolucao[1]).ToString(),
                                     fonte,
                                     new SolidBrush(Color.Black),
                                     new PointF(0, (float)unidade_CorteY * i + a - fonte.Height / 2));
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
            //EscalaHorizontal, EscalaVertical, Resolucao
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

            PointF ponto1 = new PointF(ValorParaPosX(a.ValorX), ValorParaPosY(a.ValorY));
            PointF ponto2 = new PointF(ValorParaPosX(b.ValorX), ValorParaPosY(b.ValorY));

            using (Graphics g = tela.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(caneta, ponto1, ponto2);

                if (Port.CompararCalculos(Data.nos))
                {
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                    Pen caneta_vermelha = new Pen(Color.FromArgb(150, Color.Red), (float)4);
                    caneta_vermelha.CustomEndCap = bigArrow;
                    
                    double cateto_adjacente = (b.valorX - a.valorX);
                    double cateto_oposto = (b.valorY - a.valorY);
                    double angulo = Math.Atan(cateto_oposto / cateto_adjacente);

                    PointF prim_inicio = new PointF(ValorParaPosX((float)(a.valorX + cateto_adjacente * 2 / 8)), ValorParaPosY((float)(a.valorY + cateto_oposto * 2 / 8)));
                    PointF prim_fim = new PointF(ValorParaPosX((float)(a.valorX + cateto_adjacente * 5/12)), ValorParaPosY((float)(a.valorY + cateto_oposto * 5/12)));

                    PointF prim_inicio2 = new PointF(ValorParaPosX((float)(a.valorX + cateto_adjacente * 6 / 8)), ValorParaPosY((float)(a.valorY + cateto_oposto * 6 / 8)));
                    PointF prim_fim2 = new PointF(ValorParaPosX((float)(a.valorX + cateto_adjacente * 7/12)), ValorParaPosY((float)(a.valorY + cateto_oposto * 7/12)));

                    if (barra.Force < 0)
                    {
                        g.DrawLine(caneta_vermelha, prim_inicio, prim_fim);
                        g.DrawLine(caneta_vermelha, prim_inicio2, prim_fim2);
                    }
                    else if (barra.Force > 0)
                    {
                        g.DrawLine(caneta_vermelha, prim_fim, prim_inicio);
                        g.DrawLine(caneta_vermelha, prim_fim2, prim_inicio2);
                    }


                    int displace_X = (int)(3 * Math.Cos(angulo));
                    int displace_Y = (int)(3 * Math.Sin(angulo));



                    int tamanhoFonte = 10;
                    Font fonte = new Font("Arial", tamanhoFonte);

                    
                    

                    //string valor = $"{barra.ID}: {Math.Round(barra.Force, 4)} Kn";
                    string valor = $"{Math.Round(Math.Abs(barra.Force), 2)}";

                    var pos_x = ValorParaPosX((int)(a.valorX + cateto_adjacente / 2)) - TextRenderer.MeasureText(valor, fonte).Width / 2;
                    var pos_y = ValorParaPosY((int)(a.valorY + cateto_oposto / 2)) - TextRenderer.MeasureText(valor, fonte).Height / 2;
                    PointF centro = new PointF((float)pos_x, (float)pos_y);

                    RectangleF rect = new RectangleF(centro, g.MeasureString(valor, fonte));
                    g.FillRectangle(Brushes.White, rect);

                    /* Continuar daqui,
                     * 
                     * ajustar posicionamento do texto
                     */

                    g.DrawString(valor, 
                                    fonte,
                                    new SolidBrush(Color.FromArgb(250, Color.DarkViolet)),
                                    rect);
                    //new Point(ValorParaPosX((int)(a.valorX + cateto_adjacente / 2) + 3  ), ValorParaPosY((int)(a.valorY + cateto_oposto / 2) + fonte.Height + 3))
                    //Color.FromArgb(200, Color.DarkViolet)

                }
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
            botao.Text = no.Nome;

            //Define o tamanho do botão
            botao.Height = Knot.tamanho;
            botao.Width = Knot.tamanho;

            //Define a posição do botão e retira sua borda
            var dv = Knot.tamanho / 2;
            botao.FlatAppearance.BorderSize = 0;
            botao.Location = new System.Drawing.Point((int)ValorParaPosX(no.valorX) - dv, (int)ValorParaPosY(no.valorY) - dv);

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
                //Se o botão for apertado
                botao.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        dePropriedades pai = tab.Parent as dePropriedades;

                        tab.Controls.Clear();
                        pai.TabApoios(no, true);
                    }
                };
            }

            //tab.Controls.Clear();

            using (Graphics g = tela.CreateGraphics())
            {
                int posX = botao.Location.X + dv;
                int posY = botao.Location.Y + dv;

                #region Seta de Força
                if (no.ForceX != 0 || no.ForceY != 0)
                {
                    AdjustableArrowCap bigArrow = new AdjustableArrowCap(5, 5);
                    Pen p = new Pen(Color.Orange, (float)2.5);
                    p.CustomEndCap = bigArrow;

                    const byte vetor = 50;

                    double deg = no.CalcularAngulo();
                    double rad = deg * (Math.PI / 180);

                    var seno = Math.Sin(rad) * vetor;
                    var cos = Math.Cos(rad) * vetor;


                    int valorX = (int)(posX + cos);
                    int valorY = (int)(posY - seno);

                    Point p1 = new Point(posX, posY);
                    Point p2 = new Point(valorX, valorY);

                    g.DrawLine(p, p1, p2);
                }
                #endregion

                if (no.travas[0])
                {
                    Pen pq = new Pen(Color.Green, 5);
                    Point pq1 = new Point(posX, posY - dv * 2);
                    Point pq2 = new Point(posX, posY + dv * 2);
                    g.DrawLine(pq, pq1, pq2);
                }

                if (no.travas[1])
                {
                    Pen pq = new Pen(Color.Green, 5);
                    Point pq1 = new Point(posX - dv * 2, posY);
                    Point pq2 = new Point(posX + dv * 2, posY);
                    g.DrawLine(pq, pq1, pq2);
                }
            }


            tela.Controls.Add(botao);
        }

        /*
        public async void Refazer(bool valor)
        {
            await Task.Yield();

            do
            {
                await Task.Delay(10000);

                if (valor)
                    Redesenhar();
                else
                    Redesenhar(new object(), new EventArgs());

            } while (tela.Enabled);
        }
        */

        public void Limpar()
        {
            tela.MouseDown -= Joint.SegundoClique;

            tela.Controls.Clear();

            using (Graphics g = tela.CreateGraphics())
                g.Clear(Color.White);
        }
    }
}
