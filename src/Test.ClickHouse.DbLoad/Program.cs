using System;
using System.Data.SQLite;
using System.IO;

namespace Test.ClickHouse.DbLoad {

    class Program {

        static void Main(string[] args) {
            var dbfilename = Path.GetFullPath("Northwind.db3");
            using (var connection = new SQLiteConnection($"Data Source={dbfilename};Pooling=True")) {
                connection.Open();
                var northdb = new Northwind(connection);
                northdb.DoReload(null);
                foreach (var emp in northdb.EmployeesList)
                {
                    Console.WriteLine($"{emp.EmployeeID} {emp.Country} {emp.Title}");
                }

                connection.Close();
            }
        }

    }

}