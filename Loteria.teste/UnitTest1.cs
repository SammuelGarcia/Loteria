using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using Loteria.Domain.Map;
using NHibernate.Tool.hbm2ddl;

namespace Loteria.teste
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void a_Criar_Banco_De_Dados_Por_Modelo()
        {
            try
            {
                Fluently.Configure().Database(MsSqlConfiguration.MsSql2005.ConnectionString(c => c
               .FromAppSetting("Conexao")
                ).ShowSql()).Mappings(m => m.FluentMappings.AddFromAssemblyOf<ApostaMap>()).Mappings(m => m.MergeMappings())
                .ExposeConfiguration(BuildSchema).BuildSessionFactory();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BuildSchema(Configuration config)
        {
            //new SchemaExport(config)
            //    .Drop(false, false);

            new SchemaExport(config)
                .Create(true, true);
        }

    }
}
