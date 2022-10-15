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
            botao.Location = new System.Drawing.Point(posX - dv, posY - dv);


            botao.FlatAppearance.BorderSize = 0;

            //Se o botão for apertado
            botao.MouseDown += (s, e) =>
            {
                Triggers.JuntarApoios = true;
                Joint.Apoio = this;
                   

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        
                        //Se o primeiro clique aindã não foi dado
                        if (!Triggers.PrimeiroClique)
                            Joint.PrimeiroClique(Joint.sender, Joint.e);
                        else
                            Joint.SegundoClique(Joint.sender, Joint.e);

                        break;

                    case MouseButtons.Right:

                        tela.Controls.Remove(Botao);

                        foreach (var barra in Joint.barras)
                            if (barra.Value.knots.Contains(this))
                                Joint.barras.Remove(barra.Key);

                        break;
                }
            };

            tela.Controls.Add(botao);
        }

        public void ApagarBotao(Panel tela)
        {
            tela.Controls.Remove(botao);
        }

        public static void Reiniciar()
        {
            Joint.barras.Clear();
            Joint.nos.Clear();

            quantidade = 0;
        }
    }
}
