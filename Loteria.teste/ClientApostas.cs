using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Loteria.Domain;
using Loteria.Domain.Map;
using Loteria.Domain.repo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.teste
{
    [TestClass]
    public class ClientApostas
    {
        [TestMethod]
        public void a_CriarApostaJogo1777()
        {
            try
            {
                 ProcessaJogos jogos = new MegaSena(TipoJogo.mega, 100);
                 jogos.ProcessarJogos(1777);

                RepositorioAposta repositorio = new RepositorioAposta();

                repositorio.SalvarLista<Aposta>(jogos.Apostas);

             
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestMethod]
        public void a_CriarApostaJogo1775()
        {
            try
            {
                MegaSena jogos = new MegaSena(TipoJogo.mega, 1000);
                jogos.RecriarApostasDoInicio();

              


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
