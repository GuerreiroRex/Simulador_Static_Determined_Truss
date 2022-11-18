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

        public Tela(Panel painel)
        {
            tela = painel;

            AtualizarDados();
        }

        public void AtualizarDados()
        {
            a = Knot.Tamanho;
            b = tela.Height - a;
            c = tela.Width - a;

            unidade_CorteX = (double)(c - a) / Resolution.Resolucao[0];
            unidade_CorteY = (double)(b - a) / Resolution.Resolucao[1];
        }

        public Panel Painel
        {
            get => tela;
        }

        #region Conversao
        public int ValorParaPosX(int valor)
        {
            double ajuste = (double)(tela.Width - 2 * a) / (double)tela.Width * (double)tela.Width * valor / Resolution.EscalaHorizontal;
            return (int)(ajuste + a);
        }

        public int ValorParaPosY(int valor)
        {
            double ajuste = (double)(tela.Height - 2 * a) / (double)tela.Height * (double)tela.Height * (Resolution.EscalaVertical - valor) / Resolution.EscalaVertical;
            return (int)(ajuste + a);
        }

        public int PosParaValorX(int pos)
        {
            double valor = (double)(pos - a) * Resolution.EscalaHorizontal / (tela.Width - 2 * a);
            return (int)valor;
        }

        public int PosParaValorY(int pos)
        {
            double valor = (double)((tela.Height - pos) - a) * Resolution.EscalaVertical / (tela.Height - 2 * a);
            return (int)valor;
        }
        #endregion
    }
}
