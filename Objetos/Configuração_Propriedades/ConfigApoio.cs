using CalculoTre.Calculos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CalculoTre.Objetos.Configuração_Propriedades
{
    internal class ConfigApoio : ConfigBase
    {
        protected Knot noEscolhido;

        public ConfigApoio(Tela tela, Knot no, TabPage pagina) : base(tela)
        {
            noEscolhido = no;

            pagina.Controls.Add(Controle);

            Trigger.DesenhoAlterado += (s, e) => teste(tela, s, e);

            PainelValoresPos("Posição em X:", 'X');
            PainelValoresPos("Posição em Y:", 'Y');
            PainelValoresForce();
        }

        private void teste(Tela tela, object sender, EventArgs e)
        {
            tela.Limpar();

            tela.Desenhar();

            tela.Esquematizar(noEscolhido, false);
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

        private ComboBox Listar()
        {
            ComboBox lista = new ComboBox();

            lista.Size = new Size(largura - 10, 24);

            lista.Items.Add(new CreateForcePlaceholder());

            lista.SelectedIndexChanged += botoesLista;

            return lista;
        }

        private void atualizarLista(ComboBox lista, int i = 0)
        {
            lista.Items.Clear();

            foreach (Force force in noEscolhido.forcas)
                lista.Items.Add(force);

            lista.Items.Add(new CreateForcePlaceholder());

            lista.SelectedIndex = i;
        }

        private void botoesLista(object sender, EventArgs e)
        {
            ComboBox lista = sender as ComboBox;

            List<Button> botoes = new List<Button>();

            foreach (object item in lista.Parent.Controls)
                if (item is Button)
                    botoes.Add(item as Button);

            foreach (Button bot in botoes)
                lista.Parent.Controls.Remove(bot);


            if (lista.SelectedItem is CreateForcePlaceholder)
            {
                Button adicionar = new Button();

                adicionar.Size = new Size(largura - 10, 24);
                adicionar.Text = "Adicionar";

                adicionar.Click += (sd, ev) =>
                {

                    double vetor = 0;
                    double angulo = 0;

                    foreach (object item in lista.Parent.Controls)
                        if (item is NumericUpDown)
                        {
                            NumericUpDown numeric = item as NumericUpDown;
                            switch (numeric.Name)
                            {
                                case "Vetor":
                                    vetor = (double)numeric.Value;
                                    break;
                                case "Angulo":
                                    angulo = (double)numeric.Value;
                                    break;
                            }
                        }

                    noEscolhido.forcas.Add(new Force(vetor, angulo));
                    atualizarLista(lista);
                };

                lista.Parent.Controls.Add(adicionar);
            }
            else
            {
                foreach (object item in lista.Parent.Controls)
                    if (item is NumericUpDown)
                    {
                        NumericUpDown numeric = item as NumericUpDown;
                        switch (numeric.Name)
                        {
                            case "Vetor":
                                numeric.Value = (decimal)(lista.SelectedItem as Force).Vetor;
                                break;
                            case "Angulo":
                                numeric.Value = (decimal)(lista.SelectedItem as Force).Angulo;
                                break;
                        }
                    }

                Button modificar = new Button();
                modificar.Size = new Size(largura - 10, 24);
                modificar.Text = "Modificar";

                modificar.Click += (se, ev) =>
                {
                    int i = noEscolhido.forcas.IndexOf(lista.SelectedItem as Force);

                    foreach (object item in lista.Parent.Controls)
                        if (item is NumericUpDown)
                        {
                            NumericUpDown numeric = item as NumericUpDown;
                            switch (numeric.Name)
                            {
                                case "Vetor":
                                    noEscolhido.forcas[i].Vetor = (double)numeric.Value;
                                    break;
                                case "Angulo":
                                    noEscolhido.forcas[i].Angulo = (double)numeric.Value;
                                    break;
                            }
                        }



                    atualizarLista(lista);
                };

                Button apagar = new Button();
                apagar.Size = new Size(largura - 10, 24);
                apagar.Text = "Remover";

                apagar.Click += (se, ev) =>
                {
                    noEscolhido.forcas.Remove(lista.SelectedItem as Force);
                    atualizarLista(lista);
                };

                lista.Parent.Controls.Add(modificar);
                lista.Parent.Controls.Add(apagar);


            }
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
                    numero.Maximum = Resolution.EscalaHorizontal;
                    numero.Value = noEscolhido.ValorX;

                    numero.ValueChanged += (s, e) =>
                    {
                        noEscolhido.valorX = (int)numero.Value;
                        Trigger.ForçarRedesenho(e);
                    };
                    break;
                case 'Y':
                    numero.Maximum = Resolution.EscalaVertical;
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

            ComboBox lista = Listar();
            agrupado.Controls.Add(lista);

            #region valores fixos
            Label textoVetor = Letreiro("Vetor");
            agrupado.Controls.Add(textoVetor);

            NumericUpDown vetor = CriarValores(textoVetor);
            agrupado.Controls.Add(vetor);
            vetor.Maximum = decimal.MaxValue;
            vetor.ThousandsSeparator = true;

            agrupado.SetFlowBreak(vetor, true);

            Label textoAngulo = Letreiro("Angulo");
            agrupado.Controls.Add(textoAngulo);

            NumericUpDown angulo = CriarValores(textoAngulo);
            agrupado.Controls.Add(angulo);
            angulo.Maximum = 360;
            #endregion


            //agrupado.Controls.Add(botao);

            agrupado.Padding = new Padding(0);

            Controle.Controls.Add(agrupado);
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
    }
}
