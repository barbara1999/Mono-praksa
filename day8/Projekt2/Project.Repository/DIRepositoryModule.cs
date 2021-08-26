using Autofac;
using Project.Repository.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository
{
    public class DIRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["bascic"].ConnectionString)).As<SqlConnection>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<CityRepository>().As<ICityRepository>();
            
        }
    }
}
