using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Loteria.Domain.Map;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Test
{
    public class DataSession
    {
        private readonly IPersistenceConfigurer _dbType;

        public DataSession(IPersistenceConfigurer dbType)
        {
            _dbType = dbType;

            CreateSessionFactory();
        }

        private ISessionFactory _sessionFactory;

        private Configuration _configuration;

        public ISessionFactory SessionFactory
        {
            get { return _sessionFactory; }
        }

        public Configuration Configuration
        {
            get { return _configuration; }
        }

        private void CreateSessionFactory()
        {
            _sessionFactory = Fluently
            .Configure()
            .Database(_dbType)
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ApostaMap>())
            .ExposeConfiguration(cfg => _configuration = cfg)
            .BuildSessionFactory();
        }
    }
}
