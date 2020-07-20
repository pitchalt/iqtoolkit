// Copyright (c) Microsoft Corporation.  All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (MS-PL)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using ClickHouse.Ado;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using IQToolkit;
using IQToolkit.Data.ClickHouse;
using Test;

namespace Test
{
    using IQToolkit;
    using IQToolkit.Data;
    using IQToolkit.Data.Mapping;

    [Persistent("Customers")]
    public class Customer : XPLiteObject
    {
        [Key] public string CustomerID;
        public string ContactName;
        public string CompanyName;
        public string Phone;
        public string City;
        public string Country;
        public IList<Order> Orders;
    }

    [Persistent("Orders")]
    public class Order
    {
        [Key] public int OrderID;
        public string CustomerID;
        public DateTime OrderDate;
        public Customer Customer;
        [Association] public IList<OrderDetail> Details;
    }

    [Persistent("Order Details")]
    [Table(Name ="Order Details")]
    public class OrderDetail
    {
        public int? OrderID { get; set; }
        public int ProductID { get; set; }

        public Decimal UnitPrice { get; set; }

        public Decimal Quantity { get; set; }

        //[Association]
        //public Product Product;
        //[Association]
        //public Order Order;
    }

    public interface IEntity
    {
        int ID { get; }
    }

    [Persistent("Products")]
    public class Product : IEntity
    {
        [Key] public int ID;
        public string ProductName;
        public bool Discontinued;

        int IEntity.ID
        {
            get { return this.ID; }
        }
    }

    [Persistent("Employees")]
    public class Employee
    {
        [Key] public int EmployeeID;
        public string LastName;
        public string FirstName;
        public string Title;
        public Address Address;
    }

    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }

        public Address(string street, string city, string region, string postalCode)
        {
            this.Street = street;
            this.City = city;
            this.Region = region;
            this.PostalCode = postalCode;
        }
    }

    public class NorthwindXpo
    {
        private IDataLayer _DataLayer;

        public IDataLayer DataLayer
        {
            get
            {
                if (_DataLayer == null)
                {
                    _DataLayer = XpoDefault.GetDataLayer(SQLiteConnectionProvider.GetConnectionString("Northwind.db3"),
                        AutoCreateOption.None);
                }

                return _DataLayer;
            }
        }

        public Session CreateSession()
        {
            return new Session(DataLayer);
        }
    }

    
    public class NorthwindIQT
    {
        ClickHouseQueryProvider provider;

        public IQueryable<OrderDetail> OrderDetail
        {
            get { return new Query<OrderDetail>(provider);}
        }

        class DiagnosticWriter : TextWriter {
            public override Encoding Encoding {
                get { throw new NotImplementedException(); }
            }

            public override void Write(Char value) {
                System.Diagnostics.Debug.Write(value);
            }

            public override void Write(String value) {
                System.Diagnostics.Debug.Write(value);
            }

            public override void WriteLine(String value) {
                System.Diagnostics.Debug.WriteLine(value);
            }
        }

        public NorthwindIQT()
        {
            provider = new ClickHouseQueryProvider(CreateConnection(), new AttributeMapping(), null);
            provider.Log = new DiagnosticWriter();
        }

        public ClickHouseConnection CreateConnection()
        {
            ClickHouseConnectionSettings set = new ClickHouseConnectionSettings();

            set.Host = "10.200.101.163";
            set.Port = 9000;
//            set.Host = "172.16.170.2";
//            set.Port = 32769;
            set.Compress = true;
            set.User = "default";
            set.Password = "";
            set.Database = "Northwind";

            var connection = new ClickHouseConnection(set);
            //     connection.Open();
            return connection;
        }
    }

    public class  lineorder_flat
    {

        public UInt32? LO_ORDERKEY { get; set; }
        public UInt16? LO_LINENUMBER   { get; set; }
        public UInt32? LO_CUSTKEY   { get; set; }
        public UInt32? LO_PARTKEY   { get; set; }
        public UInt32? LO_SUPPKEY   { get; set; }
        public DateTime? LO_ORDERDATE   { get; set; }
     //   public String LO_ORDERPRIORITY   { get; set; }
        public UInt16? LO_SHIPPRIORITY  { get; set; }
        public UInt16? LO_QUANTITY  { get; set; }
        public UInt32? LO_EXTENDEDPRICE   { get; set; }
        public UInt32? LO_ORDTOTALPRICE   { get; set; }
        public UInt16? LO_DISCOUNT  { get; set; }
        public UInt32? LO_REVENUE   { get; set; }
        public UInt32? LO_SUPPLYCOST      { get; set; }
        public UInt16? LO_TAX  { get; set; }
        public DateTime? LO_COMMITDATE  { get; set; }
    //    public String LO_SHIPMODE { get; set; }
        public String C_NAME  { get; set; }
        public String C_ADDRESS  { get; set; }
      //  public String C_CITY { get; set; }
     //   public String C_NATION  { get; set; }
     //   public String C_REGION  { get; set; }
        public String C_PHONE   { get; set; }
  //      public String C_MKTSEGMENT  { get; set; }
        public String S_NAME  { get; set; }
        public String S_ADDRESS  { get; set; }
//        public String S_CITY  { get; set; }
 //       public String S_NATION { get; set; }
  //      public String S_REGION { get; set; }
        public String S_PHONE  { get; set; }
        public String P_NAME  { get; set; }
  //      public String P_MFGR { get; set; }
 //       public String P_CATEGORY{ get; set; }
 //       public String P_BRAND { get; set; }
 //       public String P_COLOR { get; set; }
   //     public String P_TYPE   { get; set; }
        public UInt16? P_SIZE  { get; set; }
   //     public String P_CONTAINER { get; set; }
    }


    public class StarBench
    {
        ClickHouseQueryProvider provider;

        public IQueryable<lineorder_flat> LineOrder
        {
            get { return new Query<lineorder_flat>(provider); }
        }

        class DiagnosticWriter : TextWriter
        {
           
            public override Encoding Encoding
            {
                get { throw new NotImplementedException(); }
            }
            public override void Write(char[] buffer)
            {
                System.Diagnostics.Debug.Write(buffer);
            }

            public override void Write(Char value)
            {
                System.Diagnostics.Debug.Write(value);
            }

            public override void Write(String value)
            {
                System.Diagnostics.Debug.Write(value);
            }

            public override void WriteLine(String value)
            {
                System.Diagnostics.Debug.WriteLine(value);
            }
        }

        public StarBench()
        {
            provider = new ClickHouseQueryProvider(CreateConnection(), new AttributeMapping(), null);
            provider.Log = new DiagnosticWriter();
        }


        public ClickHouseConnection CreateConnection()
        {
            ClickHouseConnectionSettings set = new ClickHouseConnectionSettings();

            set.Host = "10.200.101.163";
            set.Port = 9000;
            //            set.Host = "172.16.170.2";
            //            set.Port = 32769;
            set.Compress = true;
            set.User = "default";
            set.Password = "";
            set.Database = "PivotTest";

            var connection = new ClickHouseConnection(set);
            //     connection.Open();
            return connection;
        }
    }


    public class Northwind
    {
        private readonly IEntityProvider provider;

        public Northwind(IEntityProvider provider)
        {
            this.provider = provider;
        }

        public IEntityProvider Provider
        {
            get { return this.provider; }
        }

        public virtual IEntityTable<Customer> Customers
        {
            get { return this.provider.GetTable<Customer>(); }
        }

        public virtual IEntityTable<Order> Orders
        {
            get { return this.provider.GetTable<Order>(); }
        }

        public virtual IEntityTable<OrderDetail> OrderDetails
        {
            get { return this.provider.GetTable<OrderDetail>(); }
        }

        public virtual IEntityTable<Product> Products
        {
            get { return this.provider.GetTable<Product>(); }
        }

        public virtual IEntityTable<Employee> Employees
        {
            get { return this.provider.GetTable<Employee>(); }
        }
    }
}