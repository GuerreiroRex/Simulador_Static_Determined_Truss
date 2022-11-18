﻿using System;
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
            //Dependendo do tipo selecionado no indice, adiciona cada dicionário em um combo box
            switch (deTipo.SelectedIndex)
            {
                case 0:
                    if (Data.nos.Count <= 0)
                        break;

                    deObjeto.DataSource = new BindingSource(Data.nos, null);
                    deObjeto.DisplayMember = "Value";
                    deObjeto.ValueMember = "Key";
                    break;
                case 1:
                    if (Data.barras.Count <= 0)
                        break;

                    deObjeto.DataSource = new BindingSource(Data.barras, null);
                    deObjeto.DisplayMember = "Value";
                    deObjeto.ValueMember = "Key";
                    break;
            }
        }
    }
}
