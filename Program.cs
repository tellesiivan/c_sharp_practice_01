using System;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new(configuration);
            DataContextEF EntityFW = new(configuration);

            Computer myComputer = new()
            {
                Motherboard = "dppr",
                CPUCores = 54,
                HasWifi = false,
                Price = 543.32m,
                HasLTE = true,
                ReleaseDate = DateTime.Now.AddDays(3),
                VideoCard = "fdwl-2sds"
            };

            // Entity framework -> Will execute the same as the sqlCommand below and dapper.ExecuteSql(sqlCommand);
            // EntityFW.Add(myComputer);
            // EntityFW.SaveChanges();

            // IEnumerable<Computer>? computersEF = EntityFW.Computer?.ToList<Computer>();

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

            // File read & write
            File.WriteAllText("log.txt", "\n" + sqlCommand + "\n");

            // Using stream writer
            using StreamWriter openFile = new("log.txt", append: true);
            openFile.WriteLine("\n" + sqlCommand + "\n");
            openFile.Close();


            Console.WriteLine(File.ReadAllText("log.txt"));


            // int result = dapper.ExecuteSqlWithRowCount(sqlCommand);
            // bool result = dapper.ExecuteSql(sqlCommand);
            // Console.WriteLine("Did the query successfully update rows? ==> {0}", result);

            // string sqlSelect = @"
            // SELECT
            //     Computer.Motherboard,
            //     Computer.HasWifi,
            //     Computer.HasLTE,
            //     Computer.ReleaseDate,
            //     Computer.Price,
            //     Computer.VideoCard
            //     FROM TutorialAppSchema.Computer";

            // IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            // foreach (Computer computer in computers)
            // {
            //     Console.WriteLine("'" + computer.Motherboard
            //     + "','" + computer.HasWifi
            //     + "','" + computer.HasLTE
            //     + "','" + computer.ReleaseDate
            //     + "','" + computer.Price
            //     + "','" + computer.VideoCard
            //     + "' ");
            // }
        }
    }
}