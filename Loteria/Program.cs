using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria
{
    public class RegistroExcel
    {
        public int numero1 { get; set; }
        public int numero2 { get; set; }
        public int numero3 { get; set; }
        public int numero4 { get; set; }
        public int numero5 { get; set; }
        public int numero6 { get; set; }


    }

    public class Jogo
    {
        public List<Numero> Numeros { get; set; }
        public bool Valido { get { return Numeros.All(n => n.similaridade < 3); } }
        public Jogo()
        {
            Numeros = new List<Numero>();
        }

        public void ValidarJogo()
        {
            ConsiderarSimilaridade();
        }

        private void ConsiderarSimilaridade()
        {
            for (int i = 0; i < Numeros.Count; i++)
            {
                if (i != 0)
                    if (Numeros.Any(n => Numeros[i].numero + 1 == n.numero))
                        Numeros[i].similaridade++;
            }
        }
    }

    public class Aposta
    {
        public List<Jogo> Jogos { get; set; }
        public List<int> quadra { get; set; }
        public List<int> quina { get; set; }
        public List<int> sena { get; set; }
        public List<Numero> NumerosFiltradosPorPossibilidade { get; set; }
        public List<Numero> vintePrimeirosJogos { get { return NumerosFiltradosPorPossibilidade.Take(20).ToList(); } }
        public List<Numero> jogosEntreVinteEQuarenta { get { return NumerosFiltradosPorPossibilidade.Skip(20).Take(20).ToList(); } }
        public List<Numero> ultimosVinteJogos { get { return NumerosFiltradosPorPossibilidade.Skip(40).Take(20).ToList(); } }


        public List<Numero> dezUltimos { get { return NumerosFiltradosPorPossibilidade.Take(10).ToList(); } }
        public List<Numero> vinteUltimos { get { return NumerosFiltradosPorPossibilidade.Skip(10).Take(10).ToList(); } }
        public List<Numero> trintaUltimos { get { return NumerosFiltradosPorPossibilidade.Skip(20).Take(10).ToList(); } }
        public List<Numero> quarentaUltimos { get { return NumerosFiltradosPorPossibilidade.Skip(30).Take(10).ToList(); } }
        public List<Numero> cinquentaUltimos { get { return NumerosFiltradosPorPossibilidade.Skip(40).Take(10).ToList(); } }
        public List<Numero> sessentaUltimos { get { return NumerosFiltradosPorPossibilidade.Skip(50).Take(10).ToList(); } }


        public Aposta(List<Numero> NumerosFiltradosPorPossibilidade)
        {
            quadra = new List<int>();
            quina = new List<int>();
            sena = new List<int>();

            this.NumerosFiltradosPorPossibilidade = NumerosFiltradosPorPossibilidade;
            Jogos = new List<Jogo>();
        }

        public void Escreverjogos()
        {
            foreach (var jogo in Jogos)
            {
                Console.WriteLine("/br");
                jogo.Numeros.ForEach(n => Console.Write(n.numero + "-"));
            }
        }
        public void AdicionarJogo(Jogo jogo)
        {
            Jogos.Add(jogo);
        }

        public void AdicionarJogos(int numeroDeApostas, int quantidadeDeNumeros)
        {
            Random rand = new Random();
            for (int i = 0; i < numeroDeApostas; i++)
            {
                Jogo jogo = new Jogo();

                while (jogo.Numeros.Count() < quantidadeDeNumeros)
                {

                    var escolha = rand.Next(4);

                    switch (escolha)
                    {
                        case 1:
                            int numeroEscolhidoUm = rand.Next(vintePrimeirosJogos.Count());

                            if (jogo.Numeros.All(n => n.numero != vintePrimeirosJogos.ToList()[numeroEscolhidoUm].numero))
                                jogo.Numeros.Add(new Numero() { numero = vintePrimeirosJogos.ToList()[numeroEscolhidoUm].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;
                            break;
                        case 2:

                            int numeroEscolhido2 = rand.Next(jogosEntreVinteEQuarenta.Count());

                            if (jogo.Numeros.All(n => n.numero != jogosEntreVinteEQuarenta.ToList()[numeroEscolhido2].numero))
                                jogo.Numeros.Add(new Numero() { numero = jogosEntreVinteEQuarenta.ToList()[numeroEscolhido2].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;

                            break;
                        case 3:

                            int numeroEscolhido3 = rand.Next(ultimosVinteJogos.Count());

                            if (jogo.Numeros.All(n => n.numero != ultimosVinteJogos.ToList()[numeroEscolhido3].numero))
                                jogo.Numeros.Add(new Numero() { numero = ultimosVinteJogos.ToList()[numeroEscolhido3].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;
                            break;

                        case 0:
                            break;

                        default:
                            break;
                    }
                }

                //int incluir = 0;
                //foreach (var provavel in jogosMaisProvaveis)
                //{
                //    int numerosIguais = 0;
                //    foreach (var numeroProvavel in provavel.Numeros)
                //    {
                //        if (jogo.Numeros.Any(n => n.numero == numeroProvavel.numero))
                //            numerosIguais++;
                //    }

                //    if (numerosIguais > 9)
                //    {
                //        incluir++;
                //        return;
                //    }
                //}

                //if (incluir > 0)
                //{
                //    numeroDeApostas--;
                //}
                //else
                //{
                int contador = 0;

                if (jogo.Numeros.Any(j => j.numero == 2))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero ==  18))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 31))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 42))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 51))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 56))
                    contador++;


                switch (contador)
                {
                    case 4:
                        quadra.Add(1);
                        break;
                    case 5:
                        quina.Add(1);
                        break;
                    case 6:
                        sena.Add(1);
                        break;
                    default:
                        break;
                }

                AdicionarJogo(jogo);

            }
        }

        public void AdicionarJogosOtimistas(int numeroDeApostas, int quantidadeDeNumeros)
        {
            int numeroEscolhido = 0;
            Random rand = new Random();

            for (int i = 1; i < numeroDeApostas; i++)
            {

                Jogo jogo = new Jogo();

                while (jogo.Numeros.Count() < quantidadeDeNumeros)
                {

                    var escolha = rand.Next(24);

                    switch (escolha)
                    {
                        case 1:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 2:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 3:
                            numeroEscolhido = rand.Next(trintaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 4:
                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 5:
                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 6:
                            numeroEscolhido = rand.Next(sessentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != sessentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = sessentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        default:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 7:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 8:

                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 9:


                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 10:


                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 11:

                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 20:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 12:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;

                        case 13:

                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 23:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;

                        case 14:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 15:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 19:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 22:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 16:
                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 17:

                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 24:

                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 18:


                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 21:
                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                    }
                }

                int contador = 0;

                if (jogo.Numeros.Any(j => j.numero == 2))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 18))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 31))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 42))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 51))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 56))
                    contador++;


                switch (contador)
                {
                    case 4:
                        quadra.Add(1);
                        break;
                    case 5:
                        quina.Add(1);
                        break;
                    case 6:
                        sena.Add(1);
                        break;
                    default:
                        break;
                }

                AdicionarJogo(jogo);
            }
        }

    }

    public class Numero
    {
        public int numero { get; set; }
        public int possibilidade { get; set; }
        public int similaridade { get; set; }


    }

    class Program
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


            ProcessaJogos();

        }

        public static void ProcessaJogos()
        {
            string _conectionstring;
            _conectionstring = @"Provider=Microsoft.Jet.OLEDB.4.0;";
            _conectionstring += String.Format("Data Source={0};", "C:\\users\\Sammuel\\Documents\\mega.xls");
            _conectionstring += "Extended Properties='Excel 8.0;HDR=NO;'";

            OleDbConnection cn = new OleDbConnection(_conectionstring);
            OleDbCommand cmd = new OleDbCommand("Select * from [tabe$]", cn);
            cn.Open();
            OleDbDataReader reader = cmd.ExecuteReader();

            List<RegistroExcel> jogos = new List<RegistroExcel>();

            int i = 0;

            while (reader.Read())
            {
                if (reader[0] == DBNull.Value)
                    continue;

                if (i == 2000)
                    break;

                RegistroExcel registroExcel = new RegistroExcel()
                {
                    numero1 = Convert.ToInt32((double)reader[0]),
                    numero2 = Convert.ToInt32((double)reader[1]),
                    numero3 = Convert.ToInt32((double)reader[2]),
                    numero4 = Convert.ToInt32((double)reader[3]),
                    numero5 = Convert.ToInt32((double)reader[4]),
                    numero6 = Convert.ToInt32((double)reader[5])
                };

                jogos.Add(registroExcel);
            }

            var numeros = new List<Numero>();
            for (int i2 = 1; i2 < 61; i2++)
            {
                numeros.Add(new Numero() { numero = i2, possibilidade = 1000 });
            }


            //Números improvaveis
            var ultimoTresJogos = jogos.Skip(jogos.Count - 3);
            foreach (var numero in numeros)
            {
                //Nùmero Saiu nos ultimos 3 jogos
                if (ultimoTresJogos.Where(c => c.numero1 == numero.numero ||
                 c.numero2 == numero.numero || c.numero3 == numero.numero || c.numero4 == numero.numero
                 || c.numero5 == numero.numero || c.numero6 == numero.numero).Count() == 3)
                {
                    numero.possibilidade = numero.possibilidade - 500;
                }

                //Nùmero Saiu nos ultimos 2 jogos
                else if (ultimoTresJogos.Where(c => c.numero1 == numero.numero ||
                c.numero2 == numero.numero || c.numero3 == numero.numero || c.numero4 == numero.numero
                || c.numero5 == numero.numero || c.numero6 == numero.numero).Count() == 2)
                {
                    numero.possibilidade = numero.possibilidade - 250;
                }

                //Nùmero Saiu nos ultimo jogo
                else if (ultimoTresJogos.Where(c => c.numero1 == numero.numero ||
                c.numero2 == numero.numero || c.numero3 == numero.numero || c.numero4 == numero.numero
                || c.numero5 == numero.numero || c.numero6 == numero.numero).Count() == 1)
                {
                    numero.possibilidade = numero.possibilidade - 150;
                }


                //Proximo jogo a sair na mega
                var jogoAtual = jogos.Count + 1;


                //Nùmero não saiu nos ultimos 21 jogos em diante
                for (int i4 = (jogoAtual - 60); i4 < jogoAtual; i4++)
                {

                    var jogosSkip = jogos.Skip(i4);
                    var filtro = ((jogoAtual - i4) - 1);

                    if (filtro == 0)
                        continue;

                    if (filtro > 20)
                    {
                        if (jogosSkip.Where(c => c.numero1 != numero.numero &&
                          c.numero2 != numero.numero && c.numero3 != numero.numero && c.numero4 != numero.numero
                          && c.numero5 != numero.numero && c.numero6 != numero.numero).Count() == filtro)
                        {
                            numero.possibilidade = numero.possibilidade + ((jogoAtual - i4) * 5);
                        }

                        //if (jogosSkip.Where(c => c.numero1 != numero.numero &&
                        //  c.numero2 != numero.numero && c.numero3 != numero.numero && c.numero4 != numero.numero
                        //  && c.numero5 != numero.numero && c.numero6 != numero.numero).Count() > 0)
                        //{
                        //    numero.possibilidade = numero.possibilidade + ((jogoAtual - i4) * 5);
                        //}
                    }
                }

                //Nùmero não saiu nos ultimos 10 jogos em diante
                for (int i4 = (jogoAtual - 60); i4 < jogoAtual; i4++)
                {

                    var jogosSkip = jogos.Skip(i4);
                    var filtro = ((jogoAtual - i4) - 1);

                    if (filtro == 0)
                        continue;

                    if (filtro < 20)
                    {
                        if (jogosSkip.Where(c => c.numero1 != numero.numero &&
                          c.numero2 != numero.numero && c.numero3 != numero.numero && c.numero4 != numero.numero
                          && c.numero5 != numero.numero && c.numero6 != numero.numero).Count() == filtro)
                        {
                            numero.possibilidade = numero.possibilidade + ((jogoAtual - i4) * 3);
                        }

                        //if (jogosSkip.Where(c => c.numero1 != numero.numero &&
                        //  c.numero2 != numero.numero && c.numero3 != numero.numero && c.numero4 != numero.numero
                        //  && c.numero5 != numero.numero && c.numero6 != numero.numero).Count() > 0)
                        //{
                        //    numero.possibilidade = numero.possibilidade + ((jogoAtual - i4) * 5);
                        //}
                    }
                }

                //Nùmero que saiu nos ultimos 10 jogos em diante
                for (int i4 = (jogoAtual - 60); i4 < jogoAtual; i4++)
                {

                    var jogosSkip = jogos.Skip(i4);
                    var filtro = ((jogoAtual - i4) - 1);
                    if (filtro == 0)
                        continue;

                    if (jogosSkip.Where(c => c.numero1 == numero.numero ||
                      c.numero2 == numero.numero || c.numero3 == numero.numero || c.numero4 == numero.numero
                      || c.numero5 == numero.numero || c.numero6 == numero.numero).Count() > 0)
                    {
                        numero.possibilidade = numero.possibilidade - ((i4 - 1680) * 5);
                    }

                    if (jogosSkip.Where(c => c.numero1 == numero.numero ||
                  c.numero2 == numero.numero || c.numero3 == numero.numero || c.numero4 == numero.numero
                  || c.numero5 == numero.numero || c.numero6 == numero.numero).Count() == filtro)
                    {
                        numero.possibilidade = numero.possibilidade - ((i4 - 1680) * 5);
                    }
                }

            }

            //implementar a loogica das 6 linhas.
            //verificar qual a média de numeros sai por linha em um range de 10 em 10 jogos.
            //Criar premissa de não poder ter mais de tres numeros em uma linha.
            var JogosFiltradosPorPossibilidade = numeros.OrderByDescending(c => c.possibilidade).ToList();
            var jogosMaisProvaveis = new List<Jogo>();

            //Implementar a logica das 3 colunas com 20 números mais provaveis
            var vintePrimeirosJogos = JogosFiltradosPorPossibilidade.Take(20);
            var jogosEntreVinteEQuarenta = JogosFiltradosPorPossibilidade.Skip(20).Take(20);
            var ultimosVinteJogos = JogosFiltradosPorPossibilidade.Skip(40).Take(20);


            var dezUltimos = JogosFiltradosPorPossibilidade.Take(10);
            var vinteUltimos = JogosFiltradosPorPossibilidade.Skip(10).Take(10);
            var trintaUltimos = JogosFiltradosPorPossibilidade.Skip(20).Take(10);
            var quarentaUltimos = JogosFiltradosPorPossibilidade.Skip(30).Take(10);
            var cinquentaUltimos = JogosFiltradosPorPossibilidade.Skip(40).Take(10);
            var sessentaUltimos = JogosFiltradosPorPossibilidade.Skip(50).Take(10);

            //for (int iDois = 0; iDois < 100; iDois++)
            //{
            Aposta apostaDeNoveNormal = new Aposta(JogosFiltradosPorPossibilidade);
            apostaDeNoveNormal.AdicionarJogos(4, 9);

            Aposta apostaDeNoveOtimista = new Aposta(JogosFiltradosPorPossibilidade);
            apostaDeNoveOtimista.AdicionarJogos(4, 9);
            //  }

            Aposta apostaUm = new Aposta(JogosFiltradosPorPossibilidade);
            apostaUm.AdicionarJogos(2, 9);
            apostaUm.Escreverjogos();

            Aposta apostaDois = new Aposta(JogosFiltradosPorPossibilidade);
            apostaDois.AdicionarJogos(10, 7);
            apostaDois.Escreverjogos();

            Aposta apostaTres = new Aposta(JogosFiltradosPorPossibilidade);
            apostaTres.AdicionarJogos(102, 6);
            apostaTres.Escreverjogos();

            Aposta apostaUmOtimista = new Aposta(JogosFiltradosPorPossibilidade);
            apostaUmOtimista.AdicionarJogosOtimistas(2, 9);
            apostaUmOtimista.Escreverjogos();

            Aposta apostaDoisOtimista = new Aposta(JogosFiltradosPorPossibilidade);
            apostaDoisOtimista.AdicionarJogosOtimistas(10, 7);
            apostaDoisOtimista.Escreverjogos();

            Aposta apostaTresOtimista = new Aposta(JogosFiltradosPorPossibilidade);
            apostaTresOtimista.AdicionarJogosOtimistas(102, 6);
            apostaTresOtimista.Escreverjogos();

            List<Jogo> jogosSorteados = new List<Jogo>();
            List<int> quadra = new List<int>();
            List<int> quina = new List<int>();
            List<int> sena = new List<int>();

            Random rand = new Random();
            for (int numeroDeApostas = 1; numeroDeApostas < 100; numeroDeApostas++)
            {
                Jogo jogo = new Jogo();

                while (jogo.Numeros.Count() < 12)
                {

                    var escolha = rand.Next(4);

                    switch (escolha)
                    {
                        case 1:
                            int numeroEscolhidoUm = rand.Next(vintePrimeirosJogos.Count());

                            if (jogo.Numeros.All(n => n.numero != vintePrimeirosJogos.ToList()[numeroEscolhidoUm].numero))
                                jogo.Numeros.Add(new Numero() { numero = vintePrimeirosJogos.ToList()[numeroEscolhidoUm].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;
                            break;
                        case 2:

                            int numeroEscolhido2 = rand.Next(jogosEntreVinteEQuarenta.Count());

                            if (jogo.Numeros.All(n => n.numero != jogosEntreVinteEQuarenta.ToList()[numeroEscolhido2].numero))
                                jogo.Numeros.Add(new Numero() { numero = jogosEntreVinteEQuarenta.ToList()[numeroEscolhido2].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;

                            break;
                        case 3:

                            int numeroEscolhido3 = rand.Next(ultimosVinteJogos.Count());

                            if (jogo.Numeros.All(n => n.numero != ultimosVinteJogos.ToList()[numeroEscolhido3].numero))
                                jogo.Numeros.Add(new Numero() { numero = ultimosVinteJogos.ToList()[numeroEscolhido3].numero });

                            if (jogo.Numeros.Count() >= 10)
                                continue;
                            break;

                        case 0:
                            break;

                        default:
                            break;
                    }
                }

                //int incluir = 0;
                //foreach (var provavel in jogosMaisProvaveis)
                //{
                //    int numerosIguais = 0;
                //    foreach (var numeroProvavel in provavel.Numeros)
                //    {
                //        if (jogo.Numeros.Any(n => n.numero == numeroProvavel.numero))
                //            numerosIguais++;
                //    }

                //    if (numerosIguais > 9)
                //    {
                //        incluir++;
                //        return;
                //    }
                //}

                //if (incluir > 0)
                //{
                //    numeroDeApostas--;
                //}
                //else
                //{
                int contador = 0;

                if (jogo.Numeros.Any(j => j.numero == 2))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 18))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 31))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 42))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 51))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 56))
                    contador++;


                switch (contador)
                {
                    case 4:
                        quadra.Add(1);
                        break;
                    case 5:
                        quina.Add(1);
                        break;
                    case 6:
                        sena.Add(1);
                        break;
                    default:
                        break;
                }

                jogosMaisProvaveis.Add(jogo);
                //}


            }


            var jogosMaisProvaveisDois = new List<Jogo>();
            List<int> quadraDois = new List<int>();
            List<int> quinaDois = new List<int>();
            List<int> senaDois = new List<int>();
            int numeroEscolhido = 0;
            for (int numeroDeApostas = 1; numeroDeApostas < 20000; numeroDeApostas++)
            {

                Jogo jogo = new Jogo();

                while (jogo.Numeros.Count() < 7)
                {

                    var escolha = rand.Next(24);

                    switch (escolha)
                    {
                        case 1:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 2:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 3:
                            numeroEscolhido = rand.Next(trintaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 4:
                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 5:
                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 6:
                            numeroEscolhido = rand.Next(sessentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != sessentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = sessentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        default:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 7:
                            numeroEscolhido = rand.Next(dezUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 8:

                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 9:


                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 10:


                            numeroEscolhido = rand.Next(dezUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != dezUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = dezUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 11:

                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 20:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 12:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;

                        case 13:

                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 23:
                            numeroEscolhido = rand.Next(vinteUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != vinteUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = vinteUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;

                        case 14:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 15:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 19:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 22:

                            numeroEscolhido = rand.Next(trintaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != trintaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = trintaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 16:
                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 17:

                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 24:

                            numeroEscolhido = rand.Next(quarentaUltimos.Count());

                            if (jogo.Numeros.All(n => n.numero != quarentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = quarentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 18:


                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                        case 21:
                            numeroEscolhido = rand.Next(cinquentaUltimos.Count());


                            if (jogo.Numeros.All(n => n.numero != cinquentaUltimos.ToList()[numeroEscolhido].numero))
                                jogo.Numeros.Add(new Numero() { numero = cinquentaUltimos.ToList()[numeroEscolhido].numero });

                            if (jogo.Numeros.Count() >= 6)
                                continue;
                            break;
                    }
                }

                int contador = 0;

                if (jogo.Numeros.Any(j => j.numero == 2))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 18))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 31))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 42))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 51))
                    contador++;
                if (jogo.Numeros.Any(j => j.numero == 56))
                    contador++;


                switch (contador)
                {
                    case 4:
                        quadraDois.Add(1);
                        break;
                    case 5:
                        quinaDois.Add(1);
                        break;
                    case 6:
                        senaDois.Add(1);
                        break;
                    default:
                        break;
                }
                jogosMaisProvaveisDois.Add(jogo);
            }
            //Montar Jogos:
            //Tipos de jogos:
            //sequenciais mais  possiveis
            //Divergentes mais possiveis

            Console.WriteLine(quadra.Count());
            Console.WriteLine(quina.Count());
            Console.WriteLine(sena.Count());
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(quadraDois.Count());
            Console.WriteLine(quinaDois.Count());
            Console.WriteLine(senaDois.Count());






            JogosFiltradosPorPossibilidade.ForEach(f => Console.WriteLine(f.numero + "&&&" + f.possibilidade));

            List<string> lines = new List<string>();
            foreach (var jogo in jogosMaisProvaveis)
            {
                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                //lines.Add(jogo.Numeros[jogo.Numeros.);
                //jogo.Numeros.ForEach(n=> n.numero)

                //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
                Console.WriteLine("/br");
                jogo.Numeros.ForEach(n => Console.Write(n.numero + "-"));
            }

            foreach (var jogo in jogosMaisProvaveisDois)
            {
                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                //lines.Add(jogo.Numeros[jogo.Numeros.);
                //jogo.Numeros.ForEach(n=> n.numero)

                //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
                Console.WriteLine("/br");
                jogo.Numeros.ForEach(n => Console.Write(n.numero + "-"));
            }



            cn.Close();
            cn.Dispose();
            cmd.Dispose();


        }
    }
}
