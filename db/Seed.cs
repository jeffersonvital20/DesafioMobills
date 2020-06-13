using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMobills.db
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./mydb.db";
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbFilePath = configuration.GetConnectionString("DefaultConnection");//connectionStringBuilder.ConnectionString;  //configuration.GetValue<string>("DBInfo:ConnectionString");
            if (!File.Exists(dbFilePath))
            {
                _dbConnection = new SqliteConnection(connectionString);
                _dbConnection.Open();

                // Create a Despesa table
                _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [Despesa] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Descricao] NVARCHAR(128) NOT NULL,
                        [Valor] Decimal NULL,
                        [Data] datetime NULL,
                        [Pago] Bollean NOT NULL
                    )");

                // Create a Receita table
                _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [Receita] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Descricao] NVARCHAR(128) NOT NULL,
                        [Valor] Decimal NULL,
                        [Data] datetime NULL,
                        [Recebido] Bollean NOT NULL
                    )");
                //Adicionando um valor a despesa
                //_dbConnection.Execute(@"
                //    INSERT INTO Despesa (Descricao,Valor,Data,Pago) Values('Energia',125.0,'12-06-2020',True);
                //    INSERT INTO Despesa (Descricao,Valor,Data,Pago) Values('Agua',75.0,'12-06-2020',True);
                 
                //");
                // _dbConnection.Execute(@"
                //    INSERT INTO Receita (Descricao,Valor,Data,Recebido) Values('Freela',300.0,'12-06-2020',True)
                 
                //");


                _dbConnection.Close();
            }

        }
    }
}
