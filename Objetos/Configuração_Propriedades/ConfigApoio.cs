using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigApoio: ConfigBase
    {
        protected Knot noEscolhido;
        
        public ConfigApoio(Tela tela, Knot no, TabPage pagina) :base(tela)
        {
            noEscolhido = no;

            pagina.Controls.Add(Controle);

            Trigger.DesenhoAlterado += tela.Redesenhar;

            PainelValoresPos("Posição em X:", 'X', 0);
            PainelValoresPos("Posição em Y:", 'Y', 1);
            PainelValoresForce("Força em X:", 'X', 2);
            PainelValoresForce("Força em Y:", 'Y', 3);
        }

        private void PainelValoresPos(string texto, char tipo, int i)
        {
            Panel agrupado = new Panel();
            
            agrupado.AutoSize = true;
            agrupado.BorderStyle = BorderStyle.FixedSingle;

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var numero = CriarValores(letras, i);

            switch (tipo)
            {
                case 'X':
                    numero.Maximum = Data.EscalaHorizontal;
                    numero.Value = noEscolhido.ValorX;

                    numero.ValueChanged += (s, e) =>
                    {
                        noEscolhido.valorX = (int)numero.Value;
                        Trigger.ForçarRedesenho(e);
                    };
                    break;
                case 'Y':
                    numero.Maximum = Data.EscalaVertical;
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

        private void PainelValoresForce(string texto, char tipo, int i)
        {
            Panel agrupado = new Panel();

            agrupado.AutoSize = true;
            agrupado.BorderStyle = BorderStyle.FixedSingle;

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var numero = CriarValores(letras, i);
            numero.Maximum = int.MaxValue;

            switch (tipo)
            {
                case 'X':
                    numero.Value = noEscolhido.ValorX;

                    numero.ValueChanged += (s, e) =>
                    {
                        noEscolhido.forçaX = (int)numero.Value;
                    };
                    break;
                case 'Y':
                    numero.Value = noEscolhido.ValorY;

                    numero.ValueChanged += (s, e) =>
                    {
                        //Data.nos[noEscolhido.id].valorY = (int)numero.Value;
                        noEscolhido.forçaY = (int)numero.Value;
                    };
                    break;
            }


            agrupado.Controls.Add(numero);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);
        }

        private NumericUpDown CriarValores(Label letreiro, int i)
        {
            NumericUpDown numero = new NumericUpDown();
            numero.Name = letreiro.Text;

            numero.Width = largura - 10;
            numero.Height = 24;

            numero.Location = new Point(0, letreiro.Height + letreiro.Location.Y);

            numero.BorderStyle = BorderStyle.FixedSingle;

            numero.TabIndex = i;
            
            Controle.SetFlowBreak(numero, true);

            return numero;
        }

        private Label Letreiro(string texto)
        {
            Label label = new Label();
            label.Text = texto;
            label.Font = fonte;

            label.AutoSize = true;
            label.MaximumSize = new Size(0, 24);

            label.Location = new Point(0 ,0);

            Controle.SetFlowBreak(label, true);

            return label;
        }
    }
}
