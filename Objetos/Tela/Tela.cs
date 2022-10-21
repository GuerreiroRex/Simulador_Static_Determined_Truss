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
    public partial class Tela
    {
        private Panel tela;

        private int a;
        private int b;
        private int c;

        private double unidade_CorteX;
        private double unidade_CorteY;

        private delegate void PrepararLetras();
        private delegate void PrepararEsquema();

        public Tela (Panel painel)
        {
            tela = painel;

            AtualizarDados();
        }

        public void AtualizarDados()
        {
            a = Knot.Tamanho;
            b = tela.Height - a;
            c = tela.Width - a;

            unidade_CorteX = (double)(c - a) / Data.Resolucao[0];
            unidade_CorteY = (double)(b - a) / Data.Resolucao[1];
        }

        public Panel Painel
        {
            get => tela;
        }

        #region Conversao
        public int ValorParaPosX(int valor)
        {
            double ajuste = (double)(tela.Width - 2 * a) / (double)tela.Width * (double)tela.Width * valor / Data.EscalaHorizontal;
            return (int)(ajuste + a);
        }

        public int ValorParaPosY(int valor)
        {
            double ajuste = (double)(tela.Height - 2 * a) / (double)tela.Height * (double)tela.Height * (Data.EscalaVertical - valor) / Data.EscalaVertical;
            return (int)(ajuste + a);
        }

        public int PosParaValorX(int pos)
        {
            double valor = (double)(pos - a) * Data.EscalaHorizontal / (tela.Width - 2 * a);
            return (int)valor;
        }

        public int PosParaValorY(int pos)
        {
            double valor = (double)((tela.Height - pos) - a) * Data.EscalaVertical / (tela.Height - 2 * a);
            return (int)valor;
        }
        #endregion
    }
}
