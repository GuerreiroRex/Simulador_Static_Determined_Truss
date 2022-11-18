using System;

namespace CalculoTre.Calculos
{
    public class Force
    {
        protected double vetor;
        protected double angulo;

        protected double valorX;
        protected double valorY;

        public Force(double Newton, double Graus)
        {
            vetor = Newton;
            angulo = Graus;

            Decompor();
        }

        private void Decompor()
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

        public string Nome { get => ToString(); }

        public double ForcaX { get => valorX; }

        public double ForcaY { get => valorY; }

        public double Vetor { get => vetor; set => vetor = value; }

        public double Angulo { get => angulo; set => angulo = value; }
    }
}
