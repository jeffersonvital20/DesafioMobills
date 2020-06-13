using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DesafioMobills.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace DesafioMobills.Repositories
{
    public class DespesaRepository : AbstractRepository<Despesa>
    {
        public DespesaRepository(IConfiguration configuration) : base(configuration) { }

        public override bool Add(Despesa despesa)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "INSERT INTO Despesa (Descricao, Valor, Data, Pago)"
                                    + " VALUES(@Descricao, @Valor, @Data, @Pago)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, despesa);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public override IEnumerable<Despesa> FindAll()
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Despesa>("SELECT * FROM Despesa");
            }
        }

        public override Despesa FindByID(int id)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Despesa"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Despesa>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
        public IEnumerable<Despesa> FindByPago(bool pago)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Despesa"
                            + " WHERE Pago = @Pago";
                dbConnection.Open();
                return dbConnection.Query<Despesa>(sQuery, new { Pago = pago });
            }
        }

        public override bool Remove(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "DELETE FROM Despesa"
                                + " WHERE Id = @Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Id = id });
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public override bool Update(Despesa despesa)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "UPDATE Despesa SET Descricao = @Descricao,"
                                + " Valor = @Valor, Data= @Data, Pago = @Pago"
                                + " WHERE Id = @Id";
                    dbConnection.Open();
                    dbConnection.Query(sQuery, despesa);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}
