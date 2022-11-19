﻿using CalculoTre.Calculos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculoTre.Objetos
{
    public partial class Knot
    {
        private static byte quantidade;

        public byte id;
        public string nome;

        public static int tamanho = 24;

        public int valorX;
        public int valorY;

        public List<Force> forcas = new List<Force>();


        /* Travas representão os apoios em um nó
         * 
         * trava[0] representa as travas verticais
         * 
         * trava[1] representa as travas horizontais         * 
         */
        public bool[] travas = new bool[2] { false, false };

        public Knot()
        {
            id = Quantidade++;
            nome = id.ToString();
        }
        public void AtualizarValores(Tela tela, int coordX, int coordY)
        {
            valorX = tela.PosParaValorX(coordX);
            valorY = tela.PosParaValorY(coordY);
        }

        public Knot Copiar()
        {
            return this;
        }

        #region Propriedades

        public byte ID { get => id; }

        public string Nome { get => nome; set => nome = value; }

        public static int Tamanho { get => tamanho; set => tamanho = value; }

        public int ValorX { get => valorX; set => valorX = value; }

        public int ValorY { get => valorY; set => valorY = value; }

        public static byte Quantidade { get => quantidade; set => quantidade = value; }

        public double ForceX
        {
            get
            {
                double valor = 0;

                foreach (Force f in forcas)
                {
                    valor += f.ForcaX;
                }

                return valor;
            }
        }

        public double ForceY
        {
            get
            {
                double valor = 0;

                foreach (Force f in forcas)
                {
                    valor += f.ForcaY;
                }

                return valor;
            }
        }

        public double Angulo
        {
            get
            {
                double angulo = Math.Atan(ForceX / ForceY);
                return (180 / Math.PI) * angulo;
            }
        }
        #endregion
    }
}
