using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Loteria.Domain.Map;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Test
{
   
    public class IniciarBancoTest
    {
        [Test]
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
