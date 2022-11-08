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

            PainelValoresPos("Posição em X:", 'X');
            PainelValoresPos("Posição em Y:", 'Y');
            PainelValoresForce();
        }

        private FlowLayoutPanel Agrupar()
        {
            FlowLayoutPanel agrupado = new FlowLayoutPanel();

            agrupado.AutoSize = true;
            agrupado.BorderStyle = BorderStyle.FixedSingle;
            agrupado.Margin = new Padding(0, 5, 0, 5);

            return agrupado;
        }

        private void PainelValoresPos(string texto, char tipo)
        {
            FlowLayoutPanel agrupado = Agrupar();

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var numero = CriarValores(letras);

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

        private void PainelValoresForce()
        {
            FlowLayoutPanel agrupado = Agrupar();

            Label textoVetor = Letreiro("Força");
            agrupado.Controls.Add(textoVetor);

            NumericUpDown vetor = CriarValores(textoVetor);
            agrupado.Controls.Add(vetor);


            agrupado.SetFlowBreak(vetor, true);

            Label textoAngulo = Letreiro("Angulo");
            agrupado.Controls.Add(textoAngulo);
            
            NumericUpDown angulo = CriarValores(textoVetor);
            agrupado.Controls.Add(angulo);
            angulo.Maximum = 360;

            /*
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
            */



            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);
        }

        private NumericUpDown CriarValores(Label letreiro)
        {
            NumericUpDown numero = new NumericUpDown();
            numero.Name = letreiro.Text;

            numero.Width = largura - 10;

            numero.BorderStyle = BorderStyle.FixedSingle;

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
