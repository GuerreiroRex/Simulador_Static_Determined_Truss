using CalculoTre.Objetos;
using System;
using System.Windows.Forms;

namespace CalculoTre.Calculos
{
    public class Force
    {
        private string nome;

        public double vetor;
        public double angulo;

        public double valorX;
        public double valorY;

        public double posX;
        public double posY;

        public bool calculado = true;
        public Force () { }

        public Force(double Newton, double Graus, Knot pai)
        {
            vetor = Newton;
            angulo = Graus;

            posX = pai.valorX;
            posY = pai.valorY;

            Decompor();
        }

        public void Decompor()
        {
            double rad = angulo * (Math.PI / 180);

            /* X
             * cossesno = cateto adjacente / hip
             * cateto adjacente = cosseno * hip
             */
            valorX = Math.Round( Math.Cos(rad) * vetor, 7);

            /* Y
             * seno = cateto oposto / hip
             * cateto oposto = seno * hip
             */
            valorY = Math.Round( Math.Sin(rad) * vetor, 7);
        }

        public override string ToString()
        {
            return $"{angulo}º | {Math.Round(vetor, 2)}KN";
        }

        public string Nome { get => nome; set => nome = value; }    

        public double ForcaX
        {
            get
            {
                Decompor();
                return valorX;
            }
        }

        public double ForcaY
        {
            get
            {
                Decompor();
                return valorY;
            }
        }

        public double Vetor { get => vetor; set => vetor = value; }

        public double Angulo { get => angulo; set => angulo = value; }


    }
}
