using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotnetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1;";

            IDbConnection dbConnection = new SqlConnection(connectionString);

            Computer myComputer = new()
            {
                Motherboard = "CAJKDH",
                CPUCore = 212,
                HasWifi = false,
                Price = 21.32m,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                VideoCard = "31-20ej"
            };

            string sqlCommand = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES (
                '" + myComputer.Motherboard
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
                + "' )";


            // int result = dbConnection.Execute(sqlCommand);
            // Console.WriteLine(result);

            string sqlSelect = @"
            SELECT
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);

            foreach (Computer computer in computers)
            {
                Console.WriteLine("'" + computer.Motherboard
                + "','" + computer.HasWifi
                + "','" + computer.HasLTE
                + "','" + computer.ReleaseDate
                + "','" + computer.Price
                + "','" + computer.VideoCard
                + "' ");
            }
        }
    }
}