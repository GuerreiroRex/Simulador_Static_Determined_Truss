using Newtonsoft.Json;
using System.Linq;

namespace CalculoTre.Objetos
{
    public partial class Bar
    {
        private string id;

        private double force;

        public Knot[] knots = new Knot[2];

        // Esse construtor padrão existe por motivos de Json
        public Bar() { }
        
        public Bar(Knot primario, Knot secundario)
        {
            knots[0] = primario;
            knots[1] = secundario;

            var temp = knots.ToList().OrderBy(x => x.ID).ToArray();

            id = $"({knots[0].ID})({knots[1].ID})";
        }

        #region Variaveis
        public string ID
        {
            get
            {
                id = $"({knots[0].ID})({knots[1].ID})";
                return $"Barra {id}";
            }
        }

        public double Force { get => force; }
        #endregion
    }
}
