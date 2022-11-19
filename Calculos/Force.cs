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
            valorX = Math.Cos(rad) * vetor;


            /* Y
             * seno = cateto oposto / hip
             * cateto oposto = seno * hip
             */
            valorY = Math.Sin(rad) * vetor;
        }

        public override string ToString()
        {
            return $"{Math.Round(vetor, 2)}N \t{angulo}º";
        }

        public string Nome { get => nome; set => nome = value; }    

        public double ForcaX { get => valorX; }

        public double ForcaY { get => valorY; }

        public double Vetor { get => vetor; set => vetor = value; }

        public double Angulo { get => angulo; set => angulo = value; }


    }
}
