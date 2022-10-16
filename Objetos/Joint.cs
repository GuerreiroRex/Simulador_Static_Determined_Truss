using System;
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
        public static Panel deTela;
        public static Knot[] tempKnot = new Knot[2];

        public static Dictionary<string, Bar> barras = new Dictionary<string, Bar>();
        public static Dictionary<byte, Knot> nos = new Dictionary<byte, Knot>();

        public static object sender;
        public static MouseEventArgs e;

        public static Knot Apoio;

        //Cria o primeiro nó e desenha ele na tela
        public static void PrimeiroClique(object sender, MouseEventArgs e)
        {
            Joint.sender = sender;
            Joint.e = e;

            //Se o primeiro clique foi um botão use os dados dele
            if (Triggers.JuntarApoios)
            {
                tempKnot[0] = Apoio;
                Triggers.JuntarApoios = false;
            }
            //ou usa as posições do Mouse
            else
            {
                tempKnot[0] = new Knot(deTela, e.Location.X, e.Location.Y);
            }

            //Diz que o primeiro clique foi feito
            Triggers.PrimeiroClique = true;

            //Desenha na tela o nó
            tempKnot[0].Desenhar(deTela);

            //Prepara o programa para receber o segundo clique
            deTela.MouseDown += SegundoClique;
        }

        //Executa quan o osegundo clique foir aplicado
        public static void SegundoClique(object sender, MouseEventArgs e)
        {
            //Se não for dentro do quadro, faz nada
            if (!DentroDoQuadro(sender, e))
                return;

            //Se foi o bottão direito, prossiga adicionando o novo nó
            if (e.Button == MouseButtons.Left)
            {
                //Decide se vai usar um nó já existente ou um novo
                if (Triggers.JuntarApoios)
                {
                    Triggers.JuntarApoios = false;
                    if (Apoio == tempKnot[0])
                        return;

                    tempKnot[1] = Apoio;
                }
                else
                {
                    tempKnot[1] = new Knot(deTela, e.Location.X, e.Location.Y);
                }

                //Desenha o novo nó
                tempKnot[1].Desenhar(deTela);

                //Cria a barra com os dois nós coletados
                Bar barra = new Bar(tempKnot[0], tempKnot[1]);
                barras.Add(barra.ID, barra);

                //Esse aqui tá bem no nome o que faz, e limpa a os valores temporários
                AtualizarNos();
                Array.Clear(tempKnot, 0, tempKnot.Length);

                //Desenha todos os nós e barras
                Esquematizar();

                //Voltar para o estudo de esperar o primeiro clique
                deTela.MouseDown -= SegundoClique;
                Triggers.PrimeiroClique = false;

                //Atualiza os combobox
                Triggers.AtualizarObjeto(Data.deTipo, Data.deObjeto);
            }
        }

        //Atualiza os nós
        public static void AtualizarNos()
        {
            List<Knot> tempNos = new List<Knot>();
            nos.Clear();

            //Para cada valor da barra, adicione os que ainda não forem adicionados
            foreach (var barra in barras.Values)
            {
                if (!tempNos.Contains(barra.knots[0]))
                {
                    nos.Add(barra.knots[0].ID, barra.knots[0]);
                    tempNos.Add(barra.knots[0]);
                }
                if (!tempNos.Contains(barra.knots[1]))
                {
                    nos.Add(barra.knots[1].ID, barra.knots[1]);
                    tempNos.Add(barra.knots[1]);
                }
            }
        }

        //Verifica se o clique foi dentro do quadro
        public static bool DentroDoQuadro(object sender, MouseEventArgs e)
        {
            if (e.X >= Knot.Tamanho / 2 && e.X < deTela.Width - Knot.Tamanho / 2 &&
                    e.Y >= Knot.Tamanho / 2 && e.Y < deTela.Height - Knot.Tamanho / 2)
                return true;
            else
                return false;
        }

        //Desenha todas as barras e nós
        public static void Esquematizar()
        {
            foreach (var barra in barras.Values)
            {
                barra.DrawLine(deTela);
                barra.Pontuar(deTela);
            }
        }
    }
}
