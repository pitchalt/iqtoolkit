using System;
using System.Data.SQLite;
using System.IO;
using ClickHouse.Ado;

namespace Test.ClickHouse.DbLoad {

    class Program {

        static void Main(string[] args) {
            var dbfilename = Path.GetFullPath("Northwind.db3");

            
            ClickHouseConnectionSettings set = new ClickHouseConnectionSettings();
          //  set.Database = "Northwind";
            set.User = "default";
            set.Password = "";
            set.Host = "10.200.101.163";
            set.Port = 9000;


            ClickHouseConnection clickHouseConnection = new ClickHouseConnection(set);
            clickHouseConnection.Open();

            using (var connection = new SQLiteConnection($"Data Source={dbfilename};Pooling=True")) {
                connection.Open();
                var northdb = new Northwind(connection);
                northdb.DoReload(clickHouseConnection);
                foreach (var emp in northdb.EmployeesList)
                {
                    Console.WriteLine($"{emp.EmployeeID} {emp.Country} {emp.Title}");
                }

                connection.Close();
            }
        }

    }

}