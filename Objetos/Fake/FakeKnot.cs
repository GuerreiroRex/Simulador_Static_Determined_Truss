using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos._0Fake
{
    public class FakeKnot
    {
        private Button botao;
        private const int tamanho = 25;
        private byte id;

        public static Point[] pontos = new Point[2];

        private static byte conta;

        public FakeKnot(Knot no)
        {
            id = no.ID;
        }

        public void Desenhar(Panel tela)
        {
            //Cria o botão
            botao = new Button();

            //Coloca seu fundo preto, letra branca e escreve uma letra
            botao.BackColor = System.Drawing.Color.Black;
            botao.ForeColor = Color.White;

            //botao.Text = nome;
            botao.Text = id.ToString();

            //Define o tamanho do botão
            botao.Height = tamanho;
            botao.Width = tamanho;

            //Define a posição do botão e retira sua borda
            int dv = tamanho / 2;

            botao.Location = new Point((pontos[conta].X - dv), (pontos[conta].Y - dv));
            conta++;

            if (conta > 1)
                conta = 0;

            botao.FlatAppearance.BorderSize = 0;

            tela.Controls.Add(botao);
        }
    }
}
