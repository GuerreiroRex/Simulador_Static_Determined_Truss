using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigBarra: ConfigBase
    {
        public ConfigBarra(Knot noEscolhido, Form Formulario) :base(noEscolhido)
        {
            Formulario.Controls.Add(Controle);

            PainelValores("Valor em X:", 'X');
            PainelValores("Valor em Y:", 'Y');
        }

        private void PainelValores(string texto, char tipo)
        {
            Panel agrupado = new Panel();
            
            agrupado.AutoSize = true;
            agrupado.BorderStyle = BorderStyle.FixedSingle;

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var numeros = CriarValores(texto);

            switch (tipo)
            {
                case 'X':
                    numeros.Value = noEscolhido.ValorX;
                    break;
                case 'Y':
                    numeros.Value = noEscolhido.ValorY;
                    break;
            }

            
            agrupado.Controls.Add(numeros);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);

        }

        private NumericUpDown CriarValores(string Nome)
        {
            NumericUpDown numero = new NumericUpDown();
            numero.Name = Nome;
            numero.Maximum = Data.EscalaHorizontal;

            numero.Width = largura - 10;
            numero.Height = 24;

            numero.Location = new Point(0, 25);

            numero.BorderStyle = BorderStyle.FixedSingle;

            Controle.SetFlowBreak(numero, true);

            return numero;
        }

        private Label Letreiro(string texto)
        {
            Label label = new Label();
            label.Text = texto;

            label.AutoSize = true;
            label.MaximumSize = new Size(0, 24);

            label.Location = new Point(0 ,0);

            Controle.SetFlowBreak(label, true);

            return label;
        }
    }
}
