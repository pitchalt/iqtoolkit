using System;
using System.Linq;

using ClickHouse.Ado;

using IQToolkit.Data;
using IQToolkit.Data.ClickHouse;
using IQToolkit.Data.Mapping;
using System.Diagnostics;

namespace Test
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            new TestRunner(args, System.Reflection.Assembly.GetEntryAssembly()).RunTests();
        }

        private static DbEntityProvider CreateNorthwindProvider()
        {
            ClickHouseConnectionSettings set = new ClickHouseConnectionSettings();
            set.Database = "Northwind";
            set.User = "default";
            set.Password = "";
         //   set.Host = "127.0.0.1";
        //    set.Port = 32769;
            set.Host = "10.200.101.163";
            set.Port = 9000;


            ClickHouseConnection clickHouseConnection = new ClickHouseConnection(set);
//            clickHouseConnection.Open();
            var provider = new ClickHouseQueryProvider( clickHouseConnection, 
                new AttributeMapping(typeof(Test.NorthwindWithAttributes)));
            provider.Log = System.Console.Out;


            return provider;
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

            public void TestGroupByFullSum()
            {
                var list = db.OrderDetails.GroupBy(o => "g0").Select(g => g.Sum(o => o.ProductID)).ToList();
                Assert.Equal(1, list.Count);
                Assert.Equal(87909, list[0]);
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