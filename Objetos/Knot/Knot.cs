using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public partial class Knot
    {
        static byte quantidade;

        protected byte id;
        private string nome;
        private Button botao;
        private const int tamanho = 24; //Preferível que seja divisível por 2

        private int posX;
        private int posY;
        private int valorX;
        private int valorY;

        private double forçaX;
        private double forçaY;

        public Knot(Panel deTela, int cordX, int cordY)
        {
            id = quantidade++;
            nome = id.ToString();

            posX = cordX;
            posY = cordY;

            valorX = Grid.PosParaValorX(cordX);
            valorY = Grid.PosParaValorY(cordY);
        }

        #region Variaveis
        public int X
        {
            get => posX; 
            
            set
            {
                if (value > 0)
                    posX = value;
                else
                    throw new Exception("Coordenada invalida.");
            }
        }

        public int Y
        {
            get => posY;

            set
            {
                if (value > 0)
                    posY = value;
                else
                    throw new Exception("Coordenada invalida.");
            }
        }

        public Button Botao
        {
            get => botao;
        }

        public string Nome
        {
            get => nome;
        }

        public byte ID
        {
            get => id;
        }

        public Point Ponto
        {
            get => new Point(X, Y);
        }

        public static int Tamanho
        {
            get => tamanho;
        }

        public int ValorX
        {
            get => valorX;

            set => valorX = value;
        }

        public int ValorY
        {
            get => valorY;

            set => valorY = value;
        }
        #endregion
    }
}
