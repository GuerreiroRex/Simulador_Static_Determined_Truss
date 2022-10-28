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
        public ConfigBarra(Tela tela, Knot noEscolhido, Form Formulario) :base(tela, noEscolhido)
        {
            Formulario.Controls.Add(Controle);

            Trigger.DesenhoAlterado += tela.Redesenhar;

            PainelValores("Valor em X:", 'X', 0);
            PainelValores("Valor em Y:", 'Y', 1);
        }

        private void PainelValores(string texto, char tipo, int i)
        {
            Panel agrupado = new Panel();
            
            agrupado.AutoSize = true;
            agrupado.BorderStyle = BorderStyle.FixedSingle;

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var numero = CriarValores(texto, i);

            switch (tipo)
            {
                case 'X':
                    numero.Value = noEscolhido.ValorX;

                    numero.ValueChanged += (s, e) =>
                    {
                        noEscolhido.valorX = (int)numero.Value;
                        Trigger.ForçarRedesenho(e);
                    };
                    break;
                case 'Y':
                    numero.Value = noEscolhido.ValorY;

                    numero.ValueChanged += (s, e) =>
                    {
                        //Data.nos[noEscolhido.id].valorY = (int)numero.Value;
                        noEscolhido.valorY = (int)numero.Value;
                        Trigger.ForçarRedesenho(e);

                    };
                    break;
            }

            
            agrupado.Controls.Add(numero);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);

        }

        private NumericUpDown CriarValores(string Nome, int i)
        {
            NumericUpDown numero = new NumericUpDown();
            numero.Name = Nome;
            numero.Maximum = Data.EscalaHorizontal;

            numero.Width = largura - 10;
            numero.Height = 24;

            numero.Location = new Point(0, 25);

            numero.BorderStyle = BorderStyle.FixedSingle;

            numero.TabIndex = i;
            
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
