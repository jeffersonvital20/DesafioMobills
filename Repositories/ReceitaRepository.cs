using DesafioMobills.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace DesafioMobills.Repositories
{
    public class ReceitaRepository : AbstractRepository<Receita>
    {
        public ReceitaRepository(IConfiguration configuration) : base(configuration) { }

        public override bool Add(Receita receita)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "INSERT INTO Receita (Descricao, Valor, Data, Recebido)"
                                    + " VALUES(@Descricao, @Valor, @Data, @Recebido)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, receita);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public override IEnumerable<Receita> FindAll()
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Receita>("SELECT * FROM Receita");
            }
        }

        public override Receita FindByID(int id)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Receita"
                            + " WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Receita>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
        public IEnumerable<Receita> FindByRecebido(bool recebido)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Receita"
                            + " WHERE Recebido = @Recebido";
                dbConnection.Open();
                return dbConnection.Query<Receita>(sQuery, new { Recebido = recebido });
            }
        }

        public override bool Remove(int id)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "DELETE FROM Receita"
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

        public override bool Update(Receita receita)
        {
            try
            {
                using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
                {
                    string sQuery = "UPDATE Receita SET Descricao = @Descricao,"
                                + " Valor = @Valor, Data= @Data, Recebido = @Recebido"
                                + " WHERE Id = @Id";
                    dbConnection.Open();
                    dbConnection.Query(sQuery, receita);
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
