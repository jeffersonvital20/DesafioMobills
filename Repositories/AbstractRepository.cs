using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.Sqlite;

namespace DesafioMobills.Repositories
{
    public abstract class AbstractRepository<T>
    {
        //SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder();
        //connectionStringBuilder.DataSource = ";
        private string _connectionString;
        protected string ConnectionString => _connectionString;
        
        public AbstractRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");//configuration.GetValue<string>("DBInfo:ConnectionString"); 
            DesafioMobills.db.Seed.CreateDb(configuration);

        }
        public abstract bool Add(T item);
        public abstract bool Remove(int id);
        public abstract bool Update(T item);
        public abstract T FindByID(int id);
        public abstract IEnumerable<T> FindAll();
    }
}
