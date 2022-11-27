using CalculoTre.Objetos.Pages;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigBarra : ConfigBase
    {
        protected Bar barraEscolhida;
        protected TabControl Tab_controle;

        public ConfigBarra(Tela tela, Bar Barra, TabPage pagina, TabControl tab) : base(tela)
        {
            barraEscolhida = Barra;
            Tab_controle = tab;

            pagina.Controls.Add(Controle);

            Trigger.DesenhoAlterado += (s, e) => Remodelagem(tela, s, e);

            PainelRenomear("Nome da barra:");
            PainelForca("Forca aplicada");
        }

        private void Remodelagem(Tela tela, object sender, EventArgs e)
        {
            tela.Limpar();

            tela.Desenhar();

            tela.Esquematizar(barraEscolhida);
        }

        private FlowLayoutPanel Agrupar()
        {
            FlowLayoutPanel agrupado = new FlowLayoutPanel();

            agrupado.AutoSize = true;
            agrupado.Width = Controle.Width;

            agrupado.BorderStyle = BorderStyle.FixedSingle;
            agrupado.Margin = new Padding(0, 5, 0, 5);

            return agrupado;
        }

        private Label Letreiro(string texto)
        {
            Label label = new Label();
            label.Text = texto;
            label.Font = fonte;

            label.AutoSize = true;
            label.MaximumSize = new Size(0, 24);

            label.Location = new Point(0, 0);

            Controle.SetFlowBreak(label, true);

            return label;
        }

        private NumericUpDown CriarValores(Label letreiro)
        {
            NumericUpDown numero = new NumericUpDown();
            numero.Name = letreiro.Text;

            numero.TextAlign = HorizontalAlignment.Right;

            numero.Width = largura - 10;

            numero.BorderStyle = BorderStyle.FixedSingle;

            return numero;
        }

        private TextBox CriarNome(Label letreiro)
        {
            TextBox caixa = new TextBox();
            caixa.Name = letreiro.Text;

            caixa.TextAlign = HorizontalAlignment.Right;

            caixa.Width = largura - 10;

            caixa.BorderStyle = BorderStyle.FixedSingle;

            return caixa;
        }

        private void PainelRenomear(string texto)
        {
            FlowLayoutPanel agrupado = Agrupar();

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var caixa = CriarNome(letras);

            caixa.Text = barraEscolhida.ID;
            caixa.TextAlign = HorizontalAlignment.Center;

            caixa.ReadOnly = true;

            agrupado.Controls.Add(caixa);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);
        }

        private void PainelForca(string texto)
        {
            var letras_prec = Letreiro("Precisão");
            
            NumericUpDown prec = CriarValores(letras_prec);
            prec.Value = 4;
            prec.Maximum = 10;
            prec.Minimum = 0;


            FlowLayoutPanel agrupado = Agrupar();

            var letras = Letreiro(texto);
            agrupado.Controls.Add(letras);

            var sentido = CriarNome(Letreiro("Sentido"));
            sentido.TextAlign = HorizontalAlignment.Center;
            sentido.ReadOnly = true;
            Tab_controle.Selected += (s, e) => DesmostrarValor(sentido, s, e);
            DesmostrarValor(sentido, new object(), new EventArgs());

            var caixa = CriarNome(letras);
            prec.ValueChanged += (s, e) => DesmostrarValor(caixa, (int)prec.Value, s, e);
            DesmostrarValor(caixa, (int)prec.Value, new object(), new EventArgs());

            Trigger.DesenhoAlterado += (s, e) => DesmostrarValor(caixa, (int)prec.Value, s, e);
            caixa.TextAlign = HorizontalAlignment.Center;
            caixa.ReadOnly = true;

            
            agrupado.Controls.Add(sentido);
            agrupado.Controls.Add(caixa);
            agrupado.Controls.Add(letras_prec);
            agrupado.Controls.Add(prec);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);
        }

        private void DesmostrarValor(TextBox caixa, int precisao, object sender, EventArgs e)
        {
            if (Port.CompararCalculos(Data.nos))
                caixa.Text = $"{Math.Round(Math.Abs(barraEscolhida.Force), precisao)} Kn";
            else
                caixa.Text = $"Não calculado";
        }

        private void DesmostrarValor(TextBox sentido, object sender, EventArgs e)
        {
            if (barraEscolhida.Force > 0 && Port.CompararCalculos(Data.nos))
                sentido.Text = "Tração";
            else if (barraEscolhida.Force < 0 && Port.CompararCalculos(Data.nos))
                sentido.Text = "Compressão";
            else
                sentido.Text = "Indefinido";
        }
    }
}
