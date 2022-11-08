using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            angulo = Graus * (Math.PI / 180);

            Decompor();
        }

        private void Decompor()
        {
            /* X
             * cossesno = cateto adjacente / hip
             * cateto adjacente = cosseno * hip
             */
            valorX = Math.Cos(angulo) * vetor;


            /* Y
             * seno = cateto oposto / hip
             * cateto oposto = seno * hip
             */
            valorY = Math.Sin(angulo) * vetor;
        }

        public double ForcaX { get => valorX; }

        public double ForcaY { get => valorY; }
    }
}
