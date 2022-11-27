using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    internal static class Trigger
    {
        //Classe que guarda alguns gatilhos para outras ações
        public static bool PrimeiroClique = false;
        public static bool JuntarApoios = false;

        public static event EventHandler DesenhoAlterado;
        public static void ForçarRedesenho(EventArgs e)
        {
            if (DesenhoAlterado != null)
                DesenhoAlterado(new object(), e);
        }

        //Ativa quando tiver que atualizar os objetos
        public static void AtualizarObjeto(ComboBox deTipo, ComboBox deObjeto)
        {
            object objeto = deObjeto.SelectedValue;

            //Dependendo do tipo selecionado no indice, adiciona cada dicionário em um combo box
            switch (deTipo.SelectedIndex)
            {
                case 0:
                    if (Data.nos.Count <= 0)
                        break;

                    List<Knot> lista_nos = Data.nos.Values.OrderBy(x => x.nome).ToList();
                    deObjeto.DataSource = lista_nos; //new BindingSource(Data.nos, null);
                    deObjeto.DisplayMember = "Value";
                    break;
                case 1:
                    if (Data.barras.Count <= 0)
                        break;

                    List<Bar> lista_barras = Data.barras.Values.OrderBy(x => x.ID).ToList();
                    deObjeto.DataSource = lista_barras;
                    deObjeto.DisplayMember = "Value";

                    if (Data.deObjecto_Index >= lista_barras.Count)
                        Data.deObjecto_Index = lista_barras.Count - 1;
                    break;
            }

            if (objeto != null)
                deObjeto.SelectedIndex = deObjeto.Items.IndexOf(objeto);
        }
    }
}
