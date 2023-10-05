using System;
using System.Text.Json;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SystemJsonSerializer = System.Text.Json.JsonSerializer;

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


            // JSON Section --> Parse data and match to models <-- JSON Section
            string computersJson = File.ReadAllText("Computers.json");
            // IEnumerable --> Use when we dont need to add after it has been created
            // List --> We can add after reading from it

            // ==== using System.Text.Json; ====
            // Need them to serialize and deserialize
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // make optional since it can return a null value
            IEnumerable<Computer>? Systemcomputers = SystemJsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            string serialzedComputersSystemCopy = SystemJsonSerializer.Serialize(Systemcomputers, options);
            File.WriteAllText("computersCopySystem.txt", serialzedComputersSystemCopy);

            // ==== using Newtonsoft.Json;  ====
            // using Newtonsoft.Json; --> Need the following when we serialize(ONLY)
            JsonSerializerSettings jsonSerializerSettings = new()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computers != null)
            {
                foreach (Computer computer in computers)
                {
                    string sqlCommand = @"INSERT INTO TutorialAppSchema.Computer (
                        Motherboard,
                        HasWifi,
                        HasLTE,
                        ReleaseDate,
                        Price,
                        VideoCard
                    ) VALUES (
                        '" + EscapeSingleQuote(computer.Motherboard)
                        + "','" + computer.HasWifi
                        + "','" + computer.HasLTE
                        + "','" + computer.ReleaseDate
                        + "','" + computer.Price
                        + "','" + EscapeSingleQuote(computer.VideoCard)
                        + "' )";

                    dapper.ExecuteSql(sqlCommand);
                }
            }

            string serialzedComputersCopy = JsonConvert.SerializeObject(computers, jsonSerializerSettings);
            File.WriteAllText("computersCopyNewtonsoft.txt", serialzedComputersCopy);

            // Entity framework -> Will execute the same as the sqlCommand below and dapper.ExecuteSql(sqlCommand);
            // EntityFW.Add(myComputer);
            // EntityFW.SaveChanges();

            // IEnumerable<Computer>? computersEF = EntityFW.Computer?.ToList<Computer>();

            // string sqlCommand = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES (
            //     '" + myComputer.Motherboard
            //     + "','" + myComputer.HasWifi
            //     + "','" + myComputer.HasLTE
            //     + "','" + myComputer.ReleaseDate
            //     + "','" + myComputer.Price
            //     + "','" + myComputer.VideoCard
            //     + "' )";

            // File read & write
            // File.WriteAllText("log.txt", "\n" + sqlCommand + "\n");

            // // Using stream writer
            // using StreamWriter openFile = new("log.txt", append: true);
            // openFile.WriteLine("\n" + sqlCommand + "\n");
            // openFile.Close();

            // Console.WriteLine(File.ReadAllText("log.txt"));


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

        static string EscapeSingleQuote(string input)
        {
            return input.Replace("'", "''");
        }
    }
}