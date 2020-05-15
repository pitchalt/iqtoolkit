using System;
using System.Linq;
using IQToolkit.Data;
using IQToolkit.Data.ClickHouse;
using IQToolkit.Data.Mapping;

namespace Test
{
    public static class Program
    {
//        public static void Main(string[] args)
//        {
//            new TestRunner(args, System.Reflection.Assembly.GetEntryAssembly()).RunTests();
//        }

        private static DbEntityProvider CreateNorthwindProvider()
        {
            return new ClickHouseQueryProvider(
                "Async=\"False\";BufferSize=\"4096\";ApacheBufferSize=\"0\";SocketTimeout=\"1000\";" +
                "ConnectionTimeout=\"1000\";DataTransferTimeout=\"1000\";KeepAliveTimeout=\"1000\";TimeToLiveMillis=\"0\";"+
                "DefaultMaxPerRoute=\"0\";MaxTotal=\"0\";Host=\"localhost\";Port=\"32769\";MaxCompressBufferSize=\"0\";" +
                "MaxParallelReplicas=\"0\";Priority=\"0\";Compress=\"True\";CheckCompressedHash=\"True\";Decompress=\"False\";"+
                "Extremes=\"False\";MaxThreads=\"0\";MaxExecutionTime=\"0\";MaxBlockSize=\"0\";MaxRowsToGroupBy=\"0\";" +
                "User=\"default\";Password=\"\";DistributedAggregationMemoryEfficient=\"False\";MaxBytesBeforeExternalGroupBy=\"0\";"+
                "MaxBytesBeforeExternalSort=\"0\"",
            //"Server=localhost;user id='root';password='mypwd';Database=Northwind", 
                new AttributeMapping(typeof(Test.NorthwindWithAttributes)));
        }

        public class NorthwindMappingTests : Test.NorthwindMappingTests, IDisposable
        {

            public NorthwindMappingTests() {
                Setup(new String [0]);
            }

            protected override DbEntityProvider CreateProvider() {
                return CreateNorthwindProvider();
            }

            public void Dispose() {
                Teardown();
            }

        }

        public class NorthwindTranslationTests : Test.NorthwindTranslationTests
        {
            protected override DbEntityProvider CreateProvider()
            {
                return CreateNorthwindProvider();
            }
        }

        public class NorthwindExecutionTests : Test.NorthwindExecutionTests
        {
            protected override DbEntityProvider CreateProvider()
            {
                return CreateNorthwindProvider();
            }

            public new void TestOr()
            {
                // difference in collation (mysql is matching "A" and "Å" but the others are not)
                var custs = db.Customers.Where(c => c.Country == "USA" || c.City.StartsWith("A")).Select(c => new { c.Country, c.City }).ToList();
                Assert.Equal(15, custs.Count);
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