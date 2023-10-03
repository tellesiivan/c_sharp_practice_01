using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HelloWorld.Data
{
    enum DataContextDapperType
    {
        SingleQuery,
        MultipleQuery
    }
    public class DataContextDapper
    {
        // use _ to prevent having another variable that could have the same name.
        private string _connectionString = "Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1;";

        //
        // Summary:
        //    Makes a query request based on the passed sqlQuery passed.
        //
        // Type parameters:
        //   T:
        //     The type of objects to enumerate.
        public IEnumerable<T> LoadData<T>(string sqlQuery)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Query<T>(sqlQuery);
        }

        public T LoadDataSingle<T>(string sqlQuery)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.QuerySingle<T>(sqlQuery);
        }

        public bool ExecuteSql(string sqlQuery)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            int executedQuery = dbConnection.Execute(sqlQuery);
            return executedQuery > 0;
        }

        public int ExecuteSqlWithRowCount(string sqlQuery)
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);
            return dbConnection.Execute(sqlQuery);
        }

    }
}