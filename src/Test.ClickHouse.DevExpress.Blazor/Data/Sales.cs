using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.ClickHouse.DevExpress.Blazor.Data
{
public class Sales {
    static IList<SaleInfo> dataSource;
    static Sales() {
        CreateDataSource();
    }

    static void CreateDataSource() {
        dataSource = new List<SaleInfo> {
            new SaleInfo {
                OrderId = 10248,
                Region = "North America",
                Country = "United States",
                City = "New York",
                Amount = 1740,
                Date = DateTime.Parse("2017/01/06")
            },
            new SaleInfo {
                OrderId = 10249,
                Region = "North America",
                Country = "United States",
                City = "Los Angeles",
                Amount = 850,
                Date = DateTime.Parse("2017/01/13")
            },
            new SaleInfo {
                OrderId = 10250,
                Region = "North America",
                Country = "United States",
                City = "Denver",
                Amount = 2235,
                Date = DateTime.Parse("2017/01/07")
            },
            new SaleInfo {
                OrderId = 10251,
                Region = "North America",
                Country = "Canada",
                City = "Vancouver",
                Amount = 1965,
                Date = DateTime.Parse("2017/01/03")
            },
            new SaleInfo {
                OrderId = 10252,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 880,
                Date = DateTime.Parse("2017/01/10")
            },
            new SaleInfo {
                OrderId = 10253,
                Region = "South America",
                Country = "Brazil",
                City = "Rio de Janeiro",
                Amount = 5260,
                Date = DateTime.Parse("2017/01/17")
            },
            new SaleInfo {
                OrderId = 10254,
                Region = "South America",
                Country = "Argentina",
                City = "Buenos Aires",
                Amount = 2790,
                Date = DateTime.Parse("2017/01/21")
            },
            new SaleInfo {
                OrderId = 10255,
                Region = "South America",
                Country = "Paraguay",
                City = "Asuncion",
                Amount = 3140,
                Date = DateTime.Parse("2017/01/01")
            },
            new SaleInfo {
                OrderId = 10256,
                Region = "Europe",
                Country = "United Kingdom",
                City = "London",
                Amount = 6175,
                Date = DateTime.Parse("2017/01/24")
            },
            new SaleInfo {
                OrderId = 10257,
                Region = "Europe",
                Country = "Germany",
                City = "Berlin",
                Amount = 4575,
                Date = DateTime.Parse("2017/01/11")
            },
            new SaleInfo {
                OrderId = 10258,
                Region = "Europe",
                Country = "Spain",
                City = "Madrid",
                Amount = 3680,
                Date = DateTime.Parse("2017/01/12")
            },
            new SaleInfo {
                OrderId = 10259,
                Region = "Europe",
                Country = "Russian Federation",
                City = "Moscow",
                Amount = 2260,
                Date = DateTime.Parse("2017/01/01")
            },
            new SaleInfo {
                OrderId = 10260,
                Region = "Asia",
                Country = "China",
                City = "Beijing",
                Amount = 2910,
                Date = DateTime.Parse("2017/01/26")
            },
            new SaleInfo {
                OrderId = 10261,
                Region = "Asia",
                Country = "Japan",
                City = "Tokyo",
                Amount = 8400,
                Date = DateTime.Parse("2017/01/05")
            },
            new SaleInfo {
                OrderId = 10262,
                Region = "Asia",
                Country = "Republic of Korea",
                City = "Seoul",
                Amount = 1325,
                Date = DateTime.Parse("2017/01/14")
            },
            new SaleInfo {
                OrderId = 10263,
                Region = "Australia",
                Country = "Australia",
                City = "Sydney",
                Amount = 3920,
                Date = DateTime.Parse("2017/01/05")
            },
            new SaleInfo {
                OrderId = 10264,
                Region = "Australia",
                Country = "Australia",
                City = "Melbourne",
                Amount = 2220,
                Date = DateTime.Parse("2017/01/15")
            },
            new SaleInfo {
                OrderId = 10265,
                Region = "Africa",
                Country = "South Africa",
                City = "Pretoria",
                Amount = 940,
                Date = DateTime.Parse("2017/01/01")
            },
            new SaleInfo {
                OrderId = 10266,
                Region = "Africa",
                Country = "Egypt",
                City = "Cairo",
                Amount = 1630,
                Date = DateTime.Parse("2017/01/10")
            },
            new SaleInfo {
                OrderId = 10267,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 2910,
                Date = DateTime.Parse("2017/01/23")
            },
            new SaleInfo {
                OrderId = 10268,
                Region = "North America",
                Country = "United States",
                City = "Los Angeles",
                Amount = 2600,
                Date = DateTime.Parse("2017/01/14")
            },
            new SaleInfo {
                OrderId = 10269,
                Region = "Europe",
                Country = "Spain",
                City = "Madrid",
                Amount = 4340,
                Date = DateTime.Parse("2017/01/26")
            },
            new SaleInfo {
                OrderId = 10270,
                Region = "Europe",
                Country = "United Kingdom",
                City = "London",
                Amount = 6650,
                Date = DateTime.Parse("2017/01/24")
            },
            new SaleInfo {
                OrderId = 10271,
                Region = "North America",
                Country = "Canada",
                City = "Edmonton",
                Amount = 490,
                Date = DateTime.Parse("2017/01/22")
            }
        };
    }
    public static Task<IQueryable<SaleInfo>> Load() {
        return Task.FromResult(dataSource.AsQueryable());
    }
   }
}