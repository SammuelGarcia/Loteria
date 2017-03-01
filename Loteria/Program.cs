using Loteria.Domain;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Regra
            //Numeros Repetidos nos jogos [quantidate] jogos indica a porcentagem <
            //Numeros Repetidos nos ultimos jogos consecutivos . <
            //Numero de pares, inferiores ou superiores nos ultimos jogos >
            //Numero de pares, inferiores e superiores nos ultimos jogos >
            //Numero de pares, inferiores ou superiores nos jogos >
            //Numero de pares, inferiores e superiores nos jogos >
            //probabilidade de mais registros de numero impar ou par  >


            //ProcessaJogos mega = new MegaSena(TipoJogo.mega, 1906);
            //mega.ProcessarJogos(1906);

                ProcessaJogos dupla = new DuplaSena(TipoJogo.duplaSena, 1613);

                dupla.ProcessarJogos(1613);


            //    ProcessaJogos loto = new LotoFacil(TipoJogo.lotoFacil, 1408);

            //    loto.ProcessarJogos(1408);
            //
        }


    }
}
