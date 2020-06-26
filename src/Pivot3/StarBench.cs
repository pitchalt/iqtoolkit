using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IQToolkit.Data.ClickHouse;
using IQToolkit;
using DevExpress.Xpo;
using IQToolkit.Data;

namespace PivotForm
{
    
    public class lineorder_flat_collection
    {
        private readonly DbEntityProvider provider;

        public lineorder_flat_collection(DbEntityProvider provider)
        {
            this.provider = provider;
        }

        public DbEntityProvider Provider
        {
            get { return this.provider; }        
        }


      //  public IQueryable<lineorder_flat> _flatCollection;

        public IQueryable<lineorder_flat> FlatCollection
        {
            get { return this.provider.GetTable<lineorder_flat>(); }
        }

    }

    public class lineorder_flat //: XPLiteObject
    {
        public String C_NAME;
        public String C_ADDRESS;
        public String C_CITY;
        public String C_NATION;
        public String C_REGION;
        public String C_PHONE;
        public String C_MKTSEGMENT;
        public UInt32 LO_ORDERKEY;
        public UInt16 LO_LINENUMBER;
        public UInt32 LO_CUSTKEY;
        public UInt32 LO_PARTKEY;
        public UInt32 LO_SUPPKEY;
        public DateTime LO_ORDERDATE;
        public String LO_ORDERPRIORITY;
        public UInt16 LO_SHIPPRIORITY;
        public UInt16 LO_QUANTITY;
        public UInt32 LO_EXTENDEDPRICE;
        public UInt32 LO_ORDTOTALPRICE;
        public UInt16 LO_DISCOUNT;
        public UInt32 LO_REVENUE;
        public UInt32 LO_SUPPLYCOST;
        public UInt16 LO_TAX;
        public DateTime LO_COMMITDATE;
        public String LO_SHIPMODE;
        public String P_NAME;
        public String P_MFGR;
        public String P_CATEGORY;
        public String P_BRAND;
        public String P_COLOR;
        public String P_TYPE;
        public UInt16 P_SIZE;
        public String P_CONTAINER;
        public String S_NAME;
        public String S_ADDRESS;
        public String S_CITY;
        public String S_NATION;
        public String S_REGION;
        public String S_PHONE;

        //private readonly ClickHouseQueryProvider provider;

        //public lineorder_flat(ClickHouseQueryProvider provider)
        //{
        //    this.provider = provider;
        //}

        //public ClickHouseQueryProvider Provider
        //{
        //    get { return this.provider; }
        //}

        //public ICollection<customer> _Customers;

        //public ICollection<customer> Customers
        //{
        //    get { return _Customers;}
        //}

        //public ICollection<lineorder> _Lineorders;

        //public ICollection<lineorder> Lineorders
        //{
        //    get { return _Lineorders; }
        //}

        //public ICollection<part> _Parts;

        //public ICollection<part> Parts
        //{
        //    get { return _Parts; }
        //}

        //public ICollection<supplier> _Suppliers;

        //public ICollection<supplier> Suppliers
        //{
        //    get { return _Suppliers; }
        //}

    }


    //public class customer
    //{
    //   // public UInt32 C_CUSTKEY;
    //    public String C_NAME;
    //    public String C_ADDRESS;
    //    public String C_CITY;
    //    public String C_NATION;
    //    public String C_REGION;
    //    public String C_PHONE;
    //    public String C_MKTSEGMENT;

    //}

    //public class lineorder
    //{
    //    public UInt32 LO_ORDERKEY;
    //    public UInt16 LO_LINENUMBER;
    //    public UInt32 LO_CUSTKEY;
    //    public UInt32 LO_PARTKEY;
    //    public UInt32 LO_SUPPKEY;
    //    public DateTime LO_ORDERDATE;
    //    public String LO_ORDERPRIORITY;
    //    public UInt16 LO_SHIPPRIORITY;
    //    public UInt16 LO_QUANTITY;
    //    public UInt32 LO_EXTENDEDPRICE;
    //    public UInt32 LO_ORDTOTALPRICE;
    //    public UInt16 LO_DISCOUNT;
    //    public UInt32 LO_REVENUE;
    //    public UInt32 LO_SUPPLYCOST;
    //    public UInt16 LO_TAX;
    //    public DateTime LO_COMMITDATE;
    //    public String LO_SHIPMODE;
    //}

    //public class part
    //{
    ////    public UInt32 P_PARTKEY;
    //    public String P_NAME;
    //    public String P_MFGR;
    //    public String P_CATEGORY;
    //    public String P_BRAND;
    //    public String P_COLOR;
    //    public String P_TYPE;
    //    public UInt16 P_SIZE;
    //    public String P_CONTAINER;

    //}

    //public class supplier
    //{
    //  //  public UInt32 S_SUPPKEY;
    //    public String S_NAME;
    //    public String S_ADDRESS;
    //    public String S_CITY;
    //    public String S_NATION;
    //    public String S_REGION;
    //    public String S_PHONE;
    //}


}