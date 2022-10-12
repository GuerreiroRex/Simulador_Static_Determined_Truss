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
        private string id;

        public  Knot[] knots = new Knot[2];
        private double força;

        public Bar(Knot primario, Knot secundario)
        {
            knots[0] = primario;
            knots[1] = secundario;

            var temp = knots.ToList().OrderBy(x => x.ID).ToArray(); ;

            id = $"({temp[0].ID})({temp[1].ID})";
        }

        #region Variaveis
        public string ID
        {
            get => id;
        }
        #endregion
    }
}
