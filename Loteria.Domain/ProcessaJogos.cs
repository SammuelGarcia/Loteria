using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Domain
{

    public class RegistroExcel
    {
        public int numero1 { get; set; }
        public int numero2 { get; set; }
        public int numero3 { get; set; }
        public int numero4 { get; set; }
        public int numero5 { get; set; }
        public int numero6 { get; set; }
        public int numero7 { get; set; }
        public int numero8 { get; set; }
        public int numero9 { get; set; }
        public int numero10 { get; set; }
        public int numero11 { get; set; }
        public int numero12 { get; set; }
        public int numero13 { get; set; }
        public int numero14 { get; set; }
        public int numero15 { get; set; }
    }



    public abstract class ProcessaJogos:  IAggregateRoot<long>
    {
        public virtual int concurso { get; set; }
        public virtual List<Aposta> Apostas { get; set; }
        public virtual IList<Numero> Numeros { get; set; }
        protected List<RegistroExcel> jogos;
        protected int jogoAtual;
        public virtual TipoJogo Tipo { get; set; }

        [NonSerialized()]
        private long id;
        public virtual long Id
        {
            get { return id; }
            set { id = value; }
        }

        public ProcessaJogos(TipoJogo tipoJogo, int concurso)
        {
            this.Tipo = tipoJogo;
            this.concurso = concurso;
            Apostas = new List<Aposta>();
        }

        public abstract void CarregarArquivo();
        public abstract void ProcessarJogos(int concurso);
        public abstract void ClassificarNumeros();
        public virtual void EscreverNumeros()
        {
            foreach (var item in this.Numeros.OrderByDescending(c => c.possibilidade).ToList())
            {
                Console.WriteLine(item.numero + " Possibilidade: " + item.possibilidade);
            }

        }

    }
}
