﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculoTre.Objetos
{
    public static class Joint
    {
        //Informações para serem guardadas dentre formularios
        private static Knot[] tempKnot = new Knot[2];

        public static object sender;
        public static MouseEventArgs e;

        public static Knot Apoio;










        //Atualiza os nós
        public static void AtualizarNos()
        {
            List<Knot> tempNos = new List<Knot>();
            Data.nos.Clear();

            //Para cada valor da barra, adicione os que ainda não forem adicionados
            foreach (var barra in Data.barras.Values)
            {
                if (!tempNos.Contains(barra.knots[0]))
                {
                    Data.nos.Add(barra.knots[0].ID, barra.knots[0]);
                    tempNos.Add(barra.knots[0]);
                }
                if (!tempNos.Contains(barra.knots[1]))
                {
                    Data.nos.Add(barra.knots[1].ID, barra.knots[1]);
                    tempNos.Add(barra.knots[1]);
                }
            }
        }






        //Cria o primeiro nó e desenha ele na tela
        public static void PrimeiroClique(object sender, MouseEventArgs e)
        {
            Joint.sender = sender;
            Joint.e = e;

            //Se o primeiro clique foi um botão use os dados dele
            if (Triggers.JuntarApoios)
            {
                Triggers.JuntarApoios = false;
                tempKnot[0] = Apoio;
            }
            //ou usa as posições do Mouse
            else
            {
                tempKnot[0] = new Knot();
                tempKnot[0].AtualizarValores(Data.telas[0], e.X, e.Y);
            }

            //Diz que o primeiro clique foi feito
            Triggers.PrimeiroClique = true;

            Data.telas[0].Pontuar(tempKnot[0]);

            //Prepara o programa para receber o segundo clique
            Data.telas[0].Painel.MouseDown += SegundoClique;
        }

        //Executa quan o osegundo clique foir aplicado
        public static void SegundoClique(object sender, MouseEventArgs e)
        {
            if (!Data.telas[0].DentroDoQuadro(sender, e))
                return;

            //Se foi o bottão direito, prossiga adicionando o novo nó
            if (e.Button == MouseButtons.Left)
            {
                //Decide se vai usar um nó já existente ou um novo
                if (Triggers.JuntarApoios)
                {
                    if (Apoio == tempKnot[0])
                        return;

                    Triggers.JuntarApoios = false;
                    tempKnot[1] = Apoio;
                }
                else
                {
                    tempKnot[1] = new Knot();
                    tempKnot[1].AtualizarValores(Data.telas[0], e.X, e.Y);
                }                

                Data.telas[0].Pontuar(tempKnot[1]);

                //Cria a barra com os dois nós coletados
                Bar barra = new Bar(tempKnot[0], tempKnot[1]);
                Data.barras.Add(barra.ID, barra);

                //Esse aqui tá bem no nome o que faz, e limpa a os valores temporários
                AtualizarNos();

                Array.Clear(tempKnot, 0, tempKnot.Length);

                //Voltar para o estudo de esperar o primeiro clique
                Data.telas[0].Painel.MouseDown -= SegundoClique;
                Triggers.PrimeiroClique = false;

                //Atualiza os combobox
                Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);
                Data.telas[0].Esquematizar();
            }
        }
    }
}
