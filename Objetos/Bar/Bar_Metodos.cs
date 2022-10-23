using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Bar
    {
        //Sobrecarrega o método ToString, apenas para facilitar na hora de ler a varíavel
        public override string ToString()
        {
            return $"Barra {ID}";
        }

        //Desenha a linha entre os dois pontos
        public void DrawLine(Tela tela)
        {
            Pen caneta = new Pen(Color.Blue);
            caneta.Width = (float)2;

            Point ponto1 = new Point(tela.ValorParaPosX(knots[0].ValorX), tela.ValorParaPosY(knots[0].ValorY));
            Point ponto2 = new Point(tela.ValorParaPosX(knots[1].ValorX), tela.ValorParaPosY(knots[1].ValorY));

            using (Graphics g = tela.Painel.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(caneta, ponto1, ponto2);
            }
        }
    }
}
