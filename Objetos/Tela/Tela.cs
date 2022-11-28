using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Tela
    {
        private Panel tela;

        public static int[] Resolucao = new int[2] { 1, 1 };
        public const double proporcionalidade = (1280 / 720);

        public int MaximoVertical = 100;
        public int MaximoHorizontal = 100;

        public int MinimoVertical = 0;
        public int MinimoHorizontal = 0;

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

            unidade_CorteX = (double)(c - a) / Resolucao[0];
            unidade_CorteY = (double)(b - a) / Resolucao[1];
        }

        public Panel Painel
        {
            get => tela;
        }

        #region Conversao
        public float ValorParaPosX(int valor)
        {
            double ajuste = (double)(tela.Width - 2 * a) / (double)tela.Width * (double)tela.Width * valor / MaximoHorizontal;
            return (float)(ajuste + a);
            //return (int)(ajuste + a);
        }

        public float ValorParaPosY(int valor)
        {
            double ajuste = (double)(tela.Height - 2 * a) / (double)tela.Height * (double)tela.Height * (MaximoVertical - valor) / MaximoVertical;
            return (float)(ajuste + a);
        }

        public float ValorParaPosX(float valor)
        {
            double ajuste = (double)(tela.Width - 2 * a) / (double)tela.Width * (double)tela.Width * valor / MaximoHorizontal;
            return (float)(ajuste + a);
            //return (int)(ajuste + a);
        }

        public float ValorParaPosY(float valor)
        {
            double ajuste = (double)(tela.Height - 2 * a) / (double)tela.Height * (double)tela.Height * (MaximoVertical - valor) / MaximoVertical;
            return (float)(ajuste + a);
        }

        public int PosParaValorX(int pos)
        {
            double valor = (double)(pos - a) * MaximoHorizontal / (tela.Width - 2 * a);
            return (int)valor;
        }

        public int PosParaValorY(int pos)
        {
            double valor = (double)((tela.Height - pos) - a) * MaximoVertical / (tela.Height - 2 * a);
            return (int)valor;
        }
        #endregion
    }
}
