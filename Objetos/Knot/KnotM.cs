using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Knot
    {
        public override string ToString()
        {
            return $"Apoio ({nome})";
        }

        //Cria o botão para ser desenhado
        public void Desenhar(Panel tela)
        {
            //Cria o botão
            botao = new Button();

            //Aplica-lhe um nome
            botao.Name = $"B{id}";

            //Coloca seu fundo preto, letra branca e escreve uma letra
            botao.BackColor = System.Drawing.Color.Black;
            botao.ForeColor = Color.White;

            //botao.Text = nome;
            botao.Text = id.ToString();

            //Define o tamanho do botão
            botao.Height = tamanho;
            botao.Width = tamanho;

            //Define a posição do botão e retira sua borda
            var dv = tamanho / 2;
            botao.FlatAppearance.BorderSize = 0;
            botao.Location = new System.Drawing.Point(posX - dv, posY - dv);

            botao.AutoSize = true;
            botao.AutoSizeMode = AutoSizeMode.GrowOnly;

            //Se o botão for apertado
            botao.MouseDown += (s, e) =>
            {
                
                switch (e.Button)
                {
                    case MouseButtons.Left:

                        Triggers.JuntarApoios = true;
                        Joint.Apoio = this;

                        //Se o primeiro clique aindã não foi dado
                        if (!Triggers.PrimeiroClique)
                            Joint.PrimeiroClique(Joint.sender, Joint.e);
                        else
                            Joint.SegundoClique(Joint.sender, Joint.e);

                        break;

                    case MouseButtons.Right:

                        tela.MouseDown -= Joint.SegundoClique;


                        Dictionary<string, Bar> dictBarraTemp = new Dictionary<string, Bar>();

                        foreach (var barra in Joint.barras)
                            if (barra.Value.knots.Contains(this))
                                dictBarraTemp.Add(barra.Key, barra.Value);

                        foreach (var barra in dictBarraTemp)
                            Joint.barras.Remove(barra.Key);

                        Grid.RedesenharTela(tela);

                        break;
                }
            };

            tela.Controls.Add(botao);
        }

        //Apaga todas as informações armazenadas de nós e barras
        public static void Reiniciar()
        {
            Joint.barras.Clear();
            Joint.nos.Clear();

            quantidade = 0;
        }
    }
}
