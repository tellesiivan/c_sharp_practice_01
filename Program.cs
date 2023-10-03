using System;
using HelloWorld.Data;
using HelloWorld.Models;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataContextDapper dapper = new();
            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine(rightNow);

            Computer myComputer = new()
            {
                Motherboard = "78934WE",
                CPUCore = 211,
                HasWifi = true,
                Price = 4321.32m,
                HasLTE = true,
                ReleaseDate = DateTime.Now.AddDays(3),
                VideoCard = "34sa-2sds"
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


            // int result = dapper.ExecuteSqlWithRowCount(sqlCommand);
            bool result = dapper.ExecuteSql(sqlCommand);
            Console.WriteLine("Did the query successfully update rows? ==> {0}", result);

            string sqlSelect = @"
            SELECT
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

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