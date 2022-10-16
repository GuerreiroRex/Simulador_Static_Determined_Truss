using CalculoTre.Objetos._0Fake;
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
        public void DrawLine(Panel painel)
        {
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Blue);
            caneta.Width = 1;

            //Desenha a linha usando as coordenadas de cada nó
            using (Graphics g = painel.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(caneta, knots[0].Ponto, knots[1].Ponto);
            }
        }

        //Desenha a linha com uam relação de tela reduzida
        public void DrawLineRelative(Panel painel)
        {
            //Cria uma caneta preta
            Pen caneta = new Pen(Color.Blue);
            caneta.Width = 1;

            //Variaveis de relação
            float xa = (float)(painel.Width) / (float)(Joint.deTela.Width);
            float ya = (float)(painel.Height) / (float)(Joint.deTela.Height);

            //Os novos pontos com as posições relativas calculadas
            Point X = new Point(Convert.ToInt16(knots[0].X * xa), Convert.ToInt16(knots[0].Y * ya));
            Point Y = new Point(Convert.ToInt16(knots[1].X * xa), Convert.ToInt16(knots[1].Y * ya));

            FakeKnot.pontos[0] = X;
            FakeKnot.pontos[1] = Y;

            //Desenha a linha usando as coordenadas de cada nó
            using (Graphics g = painel.CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.DrawLine(caneta, X, Y);
            }
        }

        //Cria o botão em cada ponta da linha
        public void Pontuar(Panel tela, bool Relative = false)
        {
            if (Relative)
                foreach (var knot in knots)
                {
                    FakeKnot noFalso = new FakeKnot(knot);
                    noFalso.Desenhar(tela);
                }
            else
                foreach (var knot in knots)
                    knot.Desenhar(tela);
        }
    }
}
