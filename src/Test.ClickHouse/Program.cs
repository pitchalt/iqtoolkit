using System;
using System.Linq;

using ClickHouse.Ado;

using IQToolkit.Data;
using IQToolkit.Data.ClickHouse;
using IQToolkit.Data.Mapping;

namespace Test
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //CreateDatabase();
            new TestRunner(args, System.Reflection.Assembly.GetEntryAssembly()).RunTests();
        }

        private static DbEntityProvider CreateNorthwindProvider()
        {
            return new ClickHouseQueryProvider(
                "Async=\"False\";BufferSize=\"4096\";ApacheBufferSize=\"0\";SocketTimeout=\"1000\";" +
                "ConnectionTimeout=\"1000\";DataTransferTimeout=\"1000\";KeepAliveTimeout=\"1000\";TimeToLiveMillis=\"0\";" +
                "DefaultMaxPerRoute=\"0\";MaxTotal=\"0\";Host=\"10.200.101.163\";Database=\"Northwind\";Port=\"9000\";MaxCompressBufferSize=\"0\";" +
                "MaxParallelReplicas=\"0\";Priority=\"0\";Compress=\"True\";CheckCompressedHash=\"True\";Decompress=\"False\";" +
                "Extremes=\"False\";MaxThreads=\"0\";MaxExecutionTime=\"0\";MaxBlockSize=\"0\";MaxRowsToGroupBy=\"0\";" +
                "User=\"default\";Password=\"\";DistributedAggregationMemoryEfficient=\"False\";MaxBytesBeforeExternalGroupBy=\"0\";" +
                "MaxBytesBeforeExternalSort=\"0\"",
                //"Async=\"False\";BufferSize=\"4096\";ApacheBufferSize=\"0\";SocketTimeout=\"1000\";" +
                //"ConnectionTimeout=\"1000\";DataTransferTimeout=\"1000\";KeepAliveTimeout=\"1000\";TimeToLiveMillis=\"0\";"+
                //"DefaultMaxPerRoute=\"0\";MaxTotal=\"0\";Host=\"localhost\";Database=\"Northwind\";Port=\"32769\";MaxCompressBufferSize=\"0\";" +
                //"MaxParallelReplicas=\"0\";Priority=\"0\";Compress=\"True\";CheckCompressedHash=\"True\";Decompress=\"False\";"+
                //"Extremes=\"False\";MaxThreads=\"0\";MaxExecutionTime=\"0\";MaxBlockSize=\"0\";MaxRowsToGroupBy=\"0\";" +
                //"User=\"default\";Password=\"\";DistributedAggregationMemoryEfficient=\"False\";MaxBytesBeforeExternalGroupBy=\"0\";"+
                //"MaxBytesBeforeExternalSort=\"0\"",
                //"Server=localhost;user id='root';password='mypwd';Database=Northwind", 
                new AttributeMapping(typeof(Test.NorthwindWithAttributes)));
        }

        private static DbEntityProvider CreateNorthwindProviderZero()
        {
            return new ClickHouseQueryProvider(
                  "Async=\"False\";BufferSize=\"4096\";ApacheBufferSize=\"0\";SocketTimeout=\"1000\";" +
                "ConnectionTimeout=\"1000\";DataTransferTimeout=\"1000\";KeepAliveTimeout=\"1000\";TimeToLiveMillis=\"0\";" +
                "DefaultMaxPerRoute=\"0\";MaxTotal=\"0\";Host=\"10.200.101.163\";Database=\"Northwind\";Port=\"9000\";MaxCompressBufferSize=\"0\";" +
                "MaxParallelReplicas=\"0\";Priority=\"0\";Compress=\"True\";CheckCompressedHash=\"True\";Decompress=\"False\";" +
                "Extremes=\"False\";MaxThreads=\"0\";MaxExecutionTime=\"0\";MaxBlockSize=\"0\";MaxRowsToGroupBy=\"0\";" +
                "User=\"default\";Password=\"\";DistributedAggregationMemoryEfficient=\"False\";MaxBytesBeforeExternalGroupBy=\"0\";" +
                "MaxBytesBeforeExternalSort=\"0\"",
                //"Async=\"False\";BufferSize=\"4096\";A
                //"Async=\"False\";BufferSize=\"4096\";ApacheBufferSize=\"0\";SocketTimeout=\"1000\";" +
                //"ConnectionTimeout=\"1000\";DataTransferTimeout=\"1000\";KeepAliveTimeout=\"1000\";TimeToLiveMillis=\"0\";"+
                //"DefaultMaxPerRoute=\"0\";MaxTotal=\"0\";Host=\"localhost\";Port=\"32769\";MaxCompressBufferSize=\"0\";" +
                //"MaxParallelReplicas=\"0\";Priority=\"0\";Compress=\"True\";CheckCompressedHash=\"True\";Decompress=\"False\";"+
                //"Extremes=\"False\";MaxThreads=\"0\";MaxExecutionTime=\"0\";MaxBlockSize=\"0\";MaxRowsToGroupBy=\"0\";" +
                //"User=\"default\";Password=\"\";DistributedAggregationMemoryEfficient=\"False\";MaxBytesBeforeExternalGroupBy=\"0\";"+
                //"MaxBytesBeforeExternalSort=\"0\"",
                //"Server=localhost;user id='root';password='mypwd';Database=Northwind", 
                new AttributeMapping(typeof(Test.NorthwindWithAttributes)));
         
        }

        public static void CreateDatabase() {
            var provider = CreateNorthwindProviderZero();
                provider.ExecuteCommand("DROP DATABASE IF EXISTS `Northwind`;");
                provider.ExecuteCommand("CREATE DATABASE `Northwind`;");
                provider.ExecuteCommand("USE `Northwind`;");
                provider.ExecuteCommand(
                    "CREATE TABLE `Northwind`.`Customers` ( CustomerID Int32, ContactName String, Phone String, CompanyName String, Country String, City String ) ENGINE = MergeTree() ORDER BY CustomerID PRIMARY KEY CustomerID;");
                provider.ExecuteCommand(
                    "CREATE TABLE `Northwind`.`Orders` ( OrderID String, CustomerID Int32, OrderDate DATE ) ENGINE = MergeTree() ORDER BY OrderID PRIMARY KEY OrderID;");
                provider.ExecuteCommand(
                    "CREATE TABLE `Northwind`.`Order Details` ( OrderID String, DetailID String, ProductID String ) ENGINE = MergeTree() ORDER BY OrderID PRIMARY KEY OrderID;");
//                provider.ExecuteCommand("INSERT ");
        }

        //public class NorthwindMappingTests : Test.NorthwindMappingTests, IDisposable
        //{

        //    public NorthwindMappingTests() {
        //      //  Setup(new String [0]);
        //      //  CreateDatabase();
        //    }

        //    protected override DbEntityProvider CreateProvider() {
        //        return CreateNorthwindProvider();
        //    }

        //    public void Dispose() {
        //        //Teardown();
        //    }

        //}

        //public class NorthwindTranslationTests : Test.NorthwindTranslationTests, IDisposable
        //{
        //    public NorthwindTranslationTests() {
        //       // CreateDatabase();
        //        // Setup(new String [0]);

        //    }

        //    protected override DbEntityProvider CreateProvider()
        //    {
        //        return CreateNorthwindProvider();
        //    }
        //    public void Dispose() {
        //        //Teardown();
        //    }
        //}

        public class NorthwindExecutionTests : Test.NorthwindExecutionTests
        {
            protected override DbEntityProvider CreateProvider()
            {
                return CreateNorthwindProvider();
            }

            public new void TestOr()
            {
                // difference in collation (mysql is matching "A" and "Å" but the others are not)
              //  var custs = db.Customers.Where(c => c.Country == "USA").Select(c => c).ToList();
            //    var custs = db.Customers.Where(c => c.Country == "USA").Select(c => c).Count();
                //    var custs = db.Customers.Where(c => c.Country == "USA" || c.City.StartsWith("A")).Select(c => new { c.Country, c.City }).ToList();
            //    Assert.Equal(13, custs);
            }

            public override string GetBaseLineFilePath()
            {
                return "NorthwindTranslation_" + this.GetProvider().GetType().Name;
            }
        }

//        public class NorthwindCUDTests : Test.NorthwindCUDTests
//        {
//            protected override DbEntityProvider CreateProvider()
//            {
//                return CreateNorthwindProvider();
//            }
//        }
    }
}