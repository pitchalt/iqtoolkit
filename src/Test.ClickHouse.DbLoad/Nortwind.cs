using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace Test.ClickHouse.DbLoad {
    
    public class Northwind {
        
        
        public CustomerDemographicsList CustomerDemographicsList {
             get { return new CustomerDemographicsList(Connection); }
        }
        
        public RegionList RegionList {
             get { return new RegionList(Connection); }
        }
        
        public TextEntryList TextEntryList {
             get { return new TextEntryList(Connection); }
        }
        
        public EmployeesList EmployeesList {
             get { return new EmployeesList(Connection); }
        }
        
        public CategoriesList CategoriesList {
             get { return new CategoriesList(Connection); }
        }
        
        public CustomersList CustomersList {
             get { return new CustomersList(Connection); }
        }
        
        public ShippersList ShippersList {
             get { return new ShippersList(Connection); }
        }
        
        public SuppliersList SuppliersList {
             get { return new SuppliersList(Connection); }
        }
        
        public EmployeeTerritoriesList EmployeeTerritoriesList {
             get { return new EmployeeTerritoriesList(Connection); }
        }
        
        public OrderDetailsList OrderDetailsList {
             get { return new OrderDetailsList(Connection); }
        }
        
        public ProductCategoryMapList ProductCategoryMapList {
             get { return new ProductCategoryMapList(Connection); }
        }
        
        public CustomerCustomerDemoList CustomerCustomerDemoList {
             get { return new CustomerCustomerDemoList(Connection); }
        }
        
        public TerritoriesList TerritoriesList {
             get { return new TerritoriesList(Connection); }
        }
        
        public OrdersList OrdersList {
             get { return new OrdersList(Connection); }
        }
        
        public ProductsList ProductsList {
             get { return new ProductsList(Connection); }
        }
        
        public IDbConnection Connection { get; protected set;}
        
        public Northwind(IDbConnection connection) {
            Connection = connection;
        }
        
        
        public void DoReload() {
            CustomerDemographicsList.Reload();
            RegionList.Reload();
            TextEntryList.Reload();
            EmployeesList.Reload();
            CategoriesList.Reload();
            CustomersList.Reload();
            ShippersList.Reload();
            SuppliersList.Reload();
            EmployeeTerritoriesList.Reload();
            OrderDetailsList.Reload();
            ProductCategoryMapList.Reload();
            CustomerCustomerDemoList.Reload();
            TerritoriesList.Reload();
            OrdersList.Reload();
            ProductsList.Reload();
        }
        
    }
    
    public class CustomerDemographics: IEnumerable {
        
        public const String TableName = "CustomerDemographics";
        
        public String CustomerTypeID { get; set; }
        
        public CustomerDemographics() { }
        
        public CustomerDemographics(IDataReader reader) {
            CustomerTypeID = reader.GetString(0);
        }
        
        public IEnumerator GetEnumerator() {
            yield return CustomerTypeID;
        }
    }
    
    public class CustomerDemographicsList: List<CustomerDemographics> {
        
        public CustomerDemographicsList() { }
        
        public CustomerDemographicsList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerTypeID] from [CustomerDemographics]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new CustomerDemographics(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Region: IEnumerable {
        
        public const String TableName = "Region";
        
        public Int32 RegionID { get; set; }
        public String RegionDescription { get; set; }
        
        public Region() { }
        
        public Region(IDataReader reader) {
            RegionID = reader.GetInt32(0);
            RegionDescription = reader.GetString(1);
        }
        
        public IEnumerator GetEnumerator() {
            yield return RegionID;
            yield return RegionDescription;
        }
    }
    
    public class RegionList: List<Region> {
        
        public RegionList() { }
        
        public RegionList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [RegionID],[RegionDescription] from [Region]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Region(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class TextEntry: IEnumerable {
        
        public const String TableName = "TextEntry";
        
        public Int32 contentID { get; set; }
        public Guid contentGUID { get; set; }
        public String title { get; set; }
        public String contentName { get; set; }
        public String content { get; set; }
        public String iconPath { get; set; }
        public DateTime? dateExpires { get; set; }
        public String lastEditedBy { get; set; }
        public String externalLink { get; set; }
        public String status { get; set; }
        public Int32 listOrder { get; set; }
        public String callOut { get; set; }
        public DateTime? createdOn { get; set; }
        public String createdBy { get; set; }
        public DateTime? modifiedOn { get; set; }
        public String modifiedBy { get; set; }
        
        public TextEntry() { }
        
        public TextEntry(IDataReader reader) {
            contentID = reader.GetInt32(0);
            contentGUID = reader.GetGuid(1);
            title = reader.IsDBNull(2) ?  null : reader.GetString(2);
            contentName = reader.GetString(3);
            content = reader.IsDBNull(4) ?  null : reader.GetString(4);
            iconPath = reader.IsDBNull(5) ?  null : reader.GetString(5);
            dateExpires = reader.IsDBNull(6) ? (DateTime?) null : reader.GetDateTime(6);
            lastEditedBy = reader.IsDBNull(7) ?  null : reader.GetString(7);
            externalLink = reader.IsDBNull(8) ?  null : reader.GetString(8);
            status = reader.IsDBNull(9) ?  null : reader.GetString(9);
            listOrder = reader.GetInt32(10);
            callOut = reader.IsDBNull(11) ?  null : reader.GetString(11);
            createdOn = reader.IsDBNull(12) ? (DateTime?) null : reader.GetDateTime(12);
            createdBy = reader.IsDBNull(13) ?  null : reader.GetString(13);
            modifiedOn = reader.IsDBNull(14) ? (DateTime?) null : reader.GetDateTime(14);
            modifiedBy = reader.IsDBNull(15) ?  null : reader.GetString(15);
        }
        
        public IEnumerator GetEnumerator() {
            yield return contentID;
            yield return contentGUID;
            yield return title;
            yield return contentName;
            yield return content;
            yield return iconPath;
            yield return dateExpires;
            yield return lastEditedBy;
            yield return externalLink;
            yield return status;
            yield return listOrder;
            yield return callOut;
            yield return createdOn;
            yield return createdBy;
            yield return modifiedOn;
            yield return modifiedBy;
        }
    }
    
    public class TextEntryList: List<TextEntry> {
        
        public TextEntryList() { }
        
        public TextEntryList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [contentID],[contentGUID],[title],[contentName],[content],[iconPath],[dateExpires],[lastEditedBy],[externalLink],[status],[listOrder],[callOut],[createdOn],[createdBy],[modifiedOn],[modifiedBy] from [TextEntry]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new TextEntry(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Employees: IEnumerable {
        
        public const String TableName = "Employees";
        
        public Int32 EmployeeID { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String Title { get; set; }
        public String TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String HomePhone { get; set; }
        public String Extension { get; set; }
        public Int32? ReportsTo { get; set; }
        public String PhotoPath { get; set; }
        public Boolean Deleted { get; set; }
        
        public Employees() { }
        
        public Employees(IDataReader reader) {
            EmployeeID = reader.GetInt32(0);
            LastName = reader.GetString(1);
            FirstName = reader.GetString(2);
            Title = reader.IsDBNull(3) ?  null : reader.GetString(3);
            TitleOfCourtesy = reader.IsDBNull(4) ?  null : reader.GetString(4);
            BirthDate = reader.IsDBNull(5) ? (DateTime?) null : reader.GetDateTime(5);
            HireDate = reader.IsDBNull(6) ? (DateTime?) null : reader.GetDateTime(6);
            Address = reader.IsDBNull(7) ?  null : reader.GetString(7);
            City = reader.IsDBNull(8) ?  null : reader.GetString(8);
            Region = reader.IsDBNull(9) ?  null : reader.GetString(9);
            PostalCode = reader.IsDBNull(10) ?  null : reader.GetString(10);
            Country = reader.IsDBNull(11) ?  null : reader.GetString(11);
            HomePhone = reader.IsDBNull(12) ?  null : reader.GetString(12);
            Extension = reader.IsDBNull(13) ?  null : reader.GetString(13);
            ReportsTo = reader.IsDBNull(14) ? (Int32?) null : reader.GetInt32(14);
            PhotoPath = reader.IsDBNull(15) ?  null : reader.GetString(15);
            Deleted = reader.GetBoolean(16);
        }
        
        public IEnumerator GetEnumerator() {
            yield return EmployeeID;
            yield return LastName;
            yield return FirstName;
            yield return Title;
            yield return TitleOfCourtesy;
            yield return BirthDate;
            yield return HireDate;
            yield return Address;
            yield return City;
            yield return Region;
            yield return PostalCode;
            yield return Country;
            yield return HomePhone;
            yield return Extension;
            yield return ReportsTo;
            yield return PhotoPath;
            yield return Deleted;
        }
    }
    
    public class EmployeesList: List<Employees> {
        
        public EmployeesList() { }
        
        public EmployeesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [EmployeeID],[LastName],[FirstName],[Title],[TitleOfCourtesy],[BirthDate],[HireDate],[Address],[City],[Region],[PostalCode],[Country],[HomePhone],[Extension],[ReportsTo],[PhotoPath],[Deleted] from [Employees]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Employees(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Categories: IEnumerable {
        
        public const String TableName = "Categories";
        
        public Int32 CategoryID { get; set; }
        public String CategoryName { get; set; }
        
        public Categories() { }
        
        public Categories(IDataReader reader) {
            CategoryID = reader.GetInt32(0);
            CategoryName = reader.GetString(1);
        }
        
        public IEnumerator GetEnumerator() {
            yield return CategoryID;
            yield return CategoryName;
        }
    }
    
    public class CategoriesList: List<Categories> {
        
        public CategoriesList() { }
        
        public CategoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CategoryID],[CategoryName] from [Categories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Categories(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Customers: IEnumerable {
        
        public const String TableName = "Customers";
        
        public String CustomerID { get; set; }
        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String ContactTitle { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        
        public Customers() { }
        
        public Customers(IDataReader reader) {
            CustomerID = reader.GetString(0);
            CompanyName = reader.GetString(1);
            ContactName = reader.IsDBNull(2) ?  null : reader.GetString(2);
            ContactTitle = reader.IsDBNull(3) ?  null : reader.GetString(3);
            Address = reader.IsDBNull(4) ?  null : reader.GetString(4);
            City = reader.IsDBNull(5) ?  null : reader.GetString(5);
            Region = reader.IsDBNull(6) ?  null : reader.GetString(6);
            PostalCode = reader.IsDBNull(7) ?  null : reader.GetString(7);
            Country = reader.IsDBNull(8) ?  null : reader.GetString(8);
            Phone = reader.IsDBNull(9) ?  null : reader.GetString(9);
            Fax = reader.IsDBNull(10) ?  null : reader.GetString(10);
        }
        
        public IEnumerator GetEnumerator() {
            yield return CustomerID;
            yield return CompanyName;
            yield return ContactName;
            yield return ContactTitle;
            yield return Address;
            yield return City;
            yield return Region;
            yield return PostalCode;
            yield return Country;
            yield return Phone;
            yield return Fax;
        }
    }
    
    public class CustomersList: List<Customers> {
        
        public CustomersList() { }
        
        public CustomersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [Customers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Customers(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Shippers: IEnumerable {
        
        public const String TableName = "Shippers";
        
        public Int32 ShipperID { get; set; }
        public String CompanyName { get; set; }
        public String Phone { get; set; }
        
        public Shippers() { }
        
        public Shippers(IDataReader reader) {
            ShipperID = reader.GetInt32(0);
            CompanyName = reader.GetString(1);
            Phone = reader.IsDBNull(2) ?  null : reader.GetString(2);
        }
        
        public IEnumerator GetEnumerator() {
            yield return ShipperID;
            yield return CompanyName;
            yield return Phone;
        }
    }
    
    public class ShippersList: List<Shippers> {
        
        public ShippersList() { }
        
        public ShippersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [ShipperID],[CompanyName],[Phone] from [Shippers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Shippers(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Suppliers: IEnumerable {
        
        public const String TableName = "Suppliers";
        
        public Int32 SupplierID { get; set; }
        public String CompanyName { get; set; }
        public String ContactName { get; set; }
        public String ContactTitle { get; set; }
        public String Address { get; set; }
        public String City { get; set; }
        public String Region { get; set; }
        public String PostalCode { get; set; }
        public String Country { get; set; }
        public String Phone { get; set; }
        public String Fax { get; set; }
        
        public Suppliers() { }
        
        public Suppliers(IDataReader reader) {
            SupplierID = reader.GetInt32(0);
            CompanyName = reader.GetString(1);
            ContactName = reader.IsDBNull(2) ?  null : reader.GetString(2);
            ContactTitle = reader.IsDBNull(3) ?  null : reader.GetString(3);
            Address = reader.IsDBNull(4) ?  null : reader.GetString(4);
            City = reader.IsDBNull(5) ?  null : reader.GetString(5);
            Region = reader.IsDBNull(6) ?  null : reader.GetString(6);
            PostalCode = reader.IsDBNull(7) ?  null : reader.GetString(7);
            Country = reader.IsDBNull(8) ?  null : reader.GetString(8);
            Phone = reader.IsDBNull(9) ?  null : reader.GetString(9);
            Fax = reader.IsDBNull(10) ?  null : reader.GetString(10);
        }
        
        public IEnumerator GetEnumerator() {
            yield return SupplierID;
            yield return CompanyName;
            yield return ContactName;
            yield return ContactTitle;
            yield return Address;
            yield return City;
            yield return Region;
            yield return PostalCode;
            yield return Country;
            yield return Phone;
            yield return Fax;
        }
    }
    
    public class SuppliersList: List<Suppliers> {
        
        public SuppliersList() { }
        
        public SuppliersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [Suppliers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Suppliers(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class EmployeeTerritories: IEnumerable {
        
        public const String TableName = "EmployeeTerritories";
        
        public Int32 EmployeeID { get; set; }
        public String TerritoryID { get; set; }
        
        public EmployeeTerritories() { }
        
        public EmployeeTerritories(IDataReader reader) {
            EmployeeID = reader.GetInt32(0);
            TerritoryID = reader.GetString(1);
        }
        
        public IEnumerator GetEnumerator() {
            yield return EmployeeID;
            yield return TerritoryID;
        }
    }
    
    public class EmployeeTerritoriesList: List<EmployeeTerritories> {
        
        public EmployeeTerritoriesList() { }
        
        public EmployeeTerritoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [EmployeeID],[TerritoryID] from [EmployeeTerritories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new EmployeeTerritories(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class OrderDetails: IEnumerable {
        
        public const String TableName = "Order Details";
        
        public Int32 OrderID { get; set; }
        public Int32 ProductID { get; set; }
        public Decimal UnitPrice { get; set; }
        public Int16 Quantity { get; set; }
        public Double Discount { get; set; }
        
        public OrderDetails() { }
        
        public OrderDetails(IDataReader reader) {
            OrderID = reader.GetInt32(0);
            ProductID = reader.GetInt32(1);
            UnitPrice = reader.GetDecimal(2);
            Quantity = reader.GetInt16(3);
            Discount = reader.GetDouble(4);
        }
        
        public IEnumerator GetEnumerator() {
            yield return OrderID;
            yield return ProductID;
            yield return UnitPrice;
            yield return Quantity;
            yield return Discount;
        }
    }
    
    public class OrderDetailsList: List<OrderDetails> {
        
        public OrderDetailsList() { }
        
        public OrderDetailsList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [OrderID],[ProductID],[UnitPrice],[Quantity],[Discount] from [Order Details]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new OrderDetails(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class ProductCategoryMap: IEnumerable {
        
        public const String TableName = "Product_Category_Map";
        
        public Int32 CategoryID { get; set; }
        public Int32 ProductID { get; set; }
        
        public ProductCategoryMap() { }
        
        public ProductCategoryMap(IDataReader reader) {
            CategoryID = reader.GetInt32(0);
            ProductID = reader.GetInt32(1);
        }
        
        public IEnumerator GetEnumerator() {
            yield return CategoryID;
            yield return ProductID;
        }
    }
    
    public class ProductCategoryMapList: List<ProductCategoryMap> {
        
        public ProductCategoryMapList() { }
        
        public ProductCategoryMapList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CategoryID],[ProductID] from [Product_Category_Map]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new ProductCategoryMap(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class CustomerCustomerDemo: IEnumerable {
        
        public const String TableName = "CustomerCustomerDemo";
        
        public String CustomerID { get; set; }
        public String CustomerTypeID { get; set; }
        
        public CustomerCustomerDemo() { }
        
        public CustomerCustomerDemo(IDataReader reader) {
            CustomerID = reader.GetString(0);
            CustomerTypeID = reader.GetString(1);
        }
        
        public IEnumerator GetEnumerator() {
            yield return CustomerID;
            yield return CustomerTypeID;
        }
    }
    
    public class CustomerCustomerDemoList: List<CustomerCustomerDemo> {
        
        public CustomerCustomerDemoList() { }
        
        public CustomerCustomerDemoList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerID],[CustomerTypeID] from [CustomerCustomerDemo]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new CustomerCustomerDemo(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Territories: IEnumerable {
        
        public const String TableName = "Territories";
        
        public String TerritoryID { get; set; }
        public String TerritoryDescription { get; set; }
        public Int32 RegionID { get; set; }
        
        public Territories() { }
        
        public Territories(IDataReader reader) {
            TerritoryID = reader.GetString(0);
            TerritoryDescription = reader.GetString(1);
            RegionID = reader.GetInt32(2);
        }
        
        public IEnumerator GetEnumerator() {
            yield return TerritoryID;
            yield return TerritoryDescription;
            yield return RegionID;
        }
    }
    
    public class TerritoriesList: List<Territories> {
        
        public TerritoriesList() { }
        
        public TerritoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [TerritoryID],[TerritoryDescription],[RegionID] from [Territories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Territories(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Orders: IEnumerable {
        
        public const String TableName = "Orders";
        
        public Int32 OrderID { get; set; }
        public String CustomerID { get; set; }
        public Int32? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public Int32? ShipVia { get; set; }
        public Decimal? Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddress { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }
        
        public Orders() { }
        
        public Orders(IDataReader reader) {
            OrderID = reader.GetInt32(0);
            CustomerID = reader.IsDBNull(1) ?  null : reader.GetString(1);
            EmployeeID = reader.IsDBNull(2) ? (Int32?) null : reader.GetInt32(2);
            OrderDate = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3);
            RequiredDate = reader.IsDBNull(4) ? (DateTime?) null : reader.GetDateTime(4);
            ShippedDate = reader.IsDBNull(5) ? (DateTime?) null : reader.GetDateTime(5);
            ShipVia = reader.IsDBNull(6) ? (Int32?) null : reader.GetInt32(6);
            Freight = reader.IsDBNull(7) ? (Decimal?) null : reader.GetDecimal(7);
            ShipName = reader.IsDBNull(8) ?  null : reader.GetString(8);
            ShipAddress = reader.IsDBNull(9) ?  null : reader.GetString(9);
            ShipCity = reader.IsDBNull(10) ?  null : reader.GetString(10);
            ShipRegion = reader.IsDBNull(11) ?  null : reader.GetString(11);
            ShipPostalCode = reader.IsDBNull(12) ?  null : reader.GetString(12);
            ShipCountry = reader.IsDBNull(13) ?  null : reader.GetString(13);
        }
        
        public IEnumerator GetEnumerator() {
            yield return OrderID;
            yield return CustomerID;
            yield return EmployeeID;
            yield return OrderDate;
            yield return RequiredDate;
            yield return ShippedDate;
            yield return ShipVia;
            yield return Freight;
            yield return ShipName;
            yield return ShipAddress;
            yield return ShipCity;
            yield return ShipRegion;
            yield return ShipPostalCode;
            yield return ShipCountry;
        }
    }
    
    public class OrdersList: List<Orders> {
        
        public OrdersList() { }
        
        public OrdersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [OrderID],[CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[ShipVia],[Freight],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry] from [Orders]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Orders(reader));
            }
        }
        
        public void Reload() {
        }
        
    }
    
    public class Products: IEnumerable {
        
        public const String TableName = "Products";
        
        public Int32 ProductID { get; set; }
        public String ProductName { get; set; }
        public Int32? SupplierID { get; set; }
        public Int32? CategoryID { get; set; }
        public String QuantityPerUnit { get; set; }
        public Decimal? UnitPrice { get; set; }
        public Int16? UnitsInStock { get; set; }
        public Int16? UnitsOnOrder { get; set; }
        public Int16? ReorderLevel { get; set; }
        public Boolean Discontinued { get; set; }
        public String AttributeXML { get; set; }
        public DateTime? DateCreated { get; set; }
        public Guid? ProductGUID { get; set; }
        public DateTime CreatedOn { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String ModifiedBy { get; set; }
        public Boolean Deleted { get; set; }
        
        public Products() { }
        
        public Products(IDataReader reader) {
            ProductID = reader.GetInt32(0);
            ProductName = reader.GetString(1);
            SupplierID = reader.IsDBNull(2) ? (Int32?) null : reader.GetInt32(2);
            CategoryID = reader.IsDBNull(3) ? (Int32?) null : reader.GetInt32(3);
            QuantityPerUnit = reader.IsDBNull(4) ?  null : reader.GetString(4);
            UnitPrice = reader.IsDBNull(5) ? (Decimal?) null : reader.GetDecimal(5);
            UnitsInStock = reader.IsDBNull(6) ? (Int16?) null : reader.GetInt16(6);
            UnitsOnOrder = reader.IsDBNull(7) ? (Int16?) null : reader.GetInt16(7);
            ReorderLevel = reader.IsDBNull(8) ? (Int16?) null : reader.GetInt16(8);
            Discontinued = reader.GetBoolean(9);
            AttributeXML = reader.IsDBNull(10) ?  null : reader.GetString(10);
            DateCreated = reader.IsDBNull(11) ? (DateTime?) null : reader.GetDateTime(11);
            ProductGUID = reader.IsDBNull(12) ? (Guid?) null : reader.GetGuid(12);
            CreatedOn = reader.GetDateTime(13);
            CreatedBy = reader.IsDBNull(14) ?  null : reader.GetString(14);
            ModifiedOn = reader.GetDateTime(15);
            ModifiedBy = reader.IsDBNull(16) ?  null : reader.GetString(16);
            Deleted = reader.GetBoolean(17);
        }
        
        public IEnumerator GetEnumerator() {
            yield return ProductID;
            yield return ProductName;
            yield return SupplierID;
            yield return CategoryID;
            yield return QuantityPerUnit;
            yield return UnitPrice;
            yield return UnitsInStock;
            yield return UnitsOnOrder;
            yield return ReorderLevel;
            yield return Discontinued;
            yield return AttributeXML;
            yield return DateCreated;
            yield return ProductGUID;
            yield return CreatedOn;
            yield return CreatedBy;
            yield return ModifiedOn;
            yield return ModifiedBy;
            yield return Deleted;
        }
    }
    
    public class ProductsList: List<Products> {
        
        public ProductsList() { }
        
        public ProductsList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [ProductID],[ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued],[AttributeXML],[DateCreated],[ProductGUID],[CreatedOn],[CreatedBy],[ModifiedOn],[ModifiedBy],[Deleted] from [Products]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                Add(new Products(reader));
            }
        }
        
        public void Reload() {
        }
        
    }

}
