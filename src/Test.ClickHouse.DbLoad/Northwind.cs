using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ClickHouse.Ado;

namespace Test.ClickHouse.DbLoad {
    
    public partial class Northwind {
        
        
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
        
        
        public void DoReload(ClickHouseConnection clickHouseConnection) {
            var _CustomerDemographicsList = CustomerDemographicsList;
            _CustomerDemographicsList.CreateTable(clickHouseConnection);
            _CustomerDemographicsList.Reload(clickHouseConnection);
            var _RegionList = RegionList;
            _RegionList.CreateTable(clickHouseConnection);
            _RegionList.Reload(clickHouseConnection);
            var _TextEntryList = TextEntryList;
            _TextEntryList.CreateTable(clickHouseConnection);
            _TextEntryList.Reload(clickHouseConnection);
            var _EmployeesList = EmployeesList;
            _EmployeesList.CreateTable(clickHouseConnection);
            _EmployeesList.Reload(clickHouseConnection);
            var _CategoriesList = CategoriesList;
            _CategoriesList.CreateTable(clickHouseConnection);
            _CategoriesList.Reload(clickHouseConnection);
            var _CustomersList = CustomersList;
            _CustomersList.CreateTable(clickHouseConnection);
            _CustomersList.Reload(clickHouseConnection);
            var _ShippersList = ShippersList;
            _ShippersList.CreateTable(clickHouseConnection);
            _ShippersList.Reload(clickHouseConnection);
            var _SuppliersList = SuppliersList;
            _SuppliersList.CreateTable(clickHouseConnection);
            _SuppliersList.Reload(clickHouseConnection);
            var _EmployeeTerritoriesList = EmployeeTerritoriesList;
            _EmployeeTerritoriesList.CreateTable(clickHouseConnection);
            _EmployeeTerritoriesList.Reload(clickHouseConnection);
            var _OrderDetailsList = OrderDetailsList;
            _OrderDetailsList.CreateTable(clickHouseConnection);
            _OrderDetailsList.Reload(clickHouseConnection);
            var _ProductCategoryMapList = ProductCategoryMapList;
            _ProductCategoryMapList.CreateTable(clickHouseConnection);
            _ProductCategoryMapList.Reload(clickHouseConnection);
            var _CustomerCustomerDemoList = CustomerCustomerDemoList;
            _CustomerCustomerDemoList.CreateTable(clickHouseConnection);
            _CustomerCustomerDemoList.Reload(clickHouseConnection);
            var _TerritoriesList = TerritoriesList;
            _TerritoriesList.CreateTable(clickHouseConnection);
            _TerritoriesList.Reload(clickHouseConnection);
            var _OrdersList = OrdersList;
            _OrdersList.CreateTable(clickHouseConnection);
            _OrdersList.Reload(clickHouseConnection);
            var _ProductsList = ProductsList;
            _ProductsList.CreateTable(clickHouseConnection);
            _ProductsList.Reload(clickHouseConnection);
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
        
        int CustomerTypeIDLen;
        public CustomerDemographicsList() { }
        
        public CustomerDemographicsList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerTypeID] from [CustomerDemographics]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new CustomerDemographics(reader);
                if ( CustomerTypeIDLen < (rec.CustomerTypeID ?? String.Empty).Length)
                CustomerTypeIDLen = rec.CustomerTypeID.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into CustomerDemographics (CustomerTypeID) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table CustomerDemographics("
                + "CustomerTypeID char)"
            +" ENGINE = MergeTree"
            +"Order by CustomerTypeID";
            command.ExecuteNonQuery();
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
        
        int RegionDescriptionLen;
        public RegionList() { }
        
        public RegionList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [RegionID],[RegionDescription] from [Region]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Region(reader);
                if ( RegionDescriptionLen < (rec.RegionDescription ?? String.Empty).Length)
                RegionDescriptionLen = rec.RegionDescription.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Region (RegionID, RegionDescription) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Region("
                + "RegionID integer,"
                + "RegionDescription char)"
            +" ENGINE = MergeTree"
            +"Order by RegionID";
            command.ExecuteNonQuery();
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
        
        int titleLen;
        int contentNameLen;
        int contentLen;
        int iconPathLen;
        int lastEditedByLen;
        int externalLinkLen;
        int statusLen;
        int callOutLen;
        int createdByLen;
        int modifiedByLen;
        public TextEntryList() { }
        
        public TextEntryList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [contentID],[contentGUID],[title],[contentName],[content],[iconPath],[dateExpires],[lastEditedBy],[externalLink],[status],[listOrder],[callOut],[createdOn],[createdBy],[modifiedOn],[modifiedBy] from [TextEntry]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new TextEntry(reader);
                if ( titleLen < (rec.title ?? String.Empty).Length)
                titleLen = rec.title.Length;
                if ( contentNameLen < (rec.contentName ?? String.Empty).Length)
                contentNameLen = rec.contentName.Length;
                if ( contentLen < (rec.content ?? String.Empty).Length)
                contentLen = rec.content.Length;
                if ( iconPathLen < (rec.iconPath ?? String.Empty).Length)
                iconPathLen = rec.iconPath.Length;
                if ( lastEditedByLen < (rec.lastEditedBy ?? String.Empty).Length)
                lastEditedByLen = rec.lastEditedBy.Length;
                if ( externalLinkLen < (rec.externalLink ?? String.Empty).Length)
                externalLinkLen = rec.externalLink.Length;
                if ( statusLen < (rec.status ?? String.Empty).Length)
                statusLen = rec.status.Length;
                if ( callOutLen < (rec.callOut ?? String.Empty).Length)
                callOutLen = rec.callOut.Length;
                if ( createdByLen < (rec.createdBy ?? String.Empty).Length)
                createdByLen = rec.createdBy.Length;
                if ( modifiedByLen < (rec.modifiedBy ?? String.Empty).Length)
                modifiedByLen = rec.modifiedBy.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into TextEntry (contentID, contentGUID, title, contentName, content, iconPath, dateExpires, lastEditedBy, externalLink, status, listOrder, callOut, createdOn, createdBy, modifiedOn, modifiedBy) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table TextEntry("
                + "contentID integer,"
                + "contentGUID guid,"
                + "title nvarchar,"
                + "contentName nvarchar,"
                + "content nvarchar,"
                + "iconPath nvarchar,"
                + "dateExpires datetime,"
                + "lastEditedBy nvarchar,"
                + "externalLink nvarchar,"
                + "status nvarchar,"
                + "listOrder int,"
                + "callOut nvarchar,"
                + "createdOn datetime,"
                + "createdBy nvarchar,"
                + "modifiedOn datetime,"
                + "modifiedBy nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by contentID";
            command.ExecuteNonQuery();
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
        
        int LastNameLen;
        int FirstNameLen;
        int TitleLen;
        int TitleOfCourtesyLen;
        int AddressLen;
        int CityLen;
        int RegionLen;
        int PostalCodeLen;
        int CountryLen;
        int HomePhoneLen;
        int ExtensionLen;
        int PhotoPathLen;
        public EmployeesList() { }
        
        public EmployeesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [EmployeeID],[LastName],[FirstName],[Title],[TitleOfCourtesy],[BirthDate],[HireDate],[Address],[City],[Region],[PostalCode],[Country],[HomePhone],[Extension],[ReportsTo],[PhotoPath],[Deleted] from [Employees]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Employees(reader);
                if ( LastNameLen < (rec.LastName ?? String.Empty).Length)
                LastNameLen = rec.LastName.Length;
                if ( FirstNameLen < (rec.FirstName ?? String.Empty).Length)
                FirstNameLen = rec.FirstName.Length;
                if ( TitleLen < (rec.Title ?? String.Empty).Length)
                TitleLen = rec.Title.Length;
                if ( TitleOfCourtesyLen < (rec.TitleOfCourtesy ?? String.Empty).Length)
                TitleOfCourtesyLen = rec.TitleOfCourtesy.Length;
                if ( AddressLen < (rec.Address ?? String.Empty).Length)
                AddressLen = rec.Address.Length;
                if ( CityLen < (rec.City ?? String.Empty).Length)
                CityLen = rec.City.Length;
                if ( RegionLen < (rec.Region ?? String.Empty).Length)
                RegionLen = rec.Region.Length;
                if ( PostalCodeLen < (rec.PostalCode ?? String.Empty).Length)
                PostalCodeLen = rec.PostalCode.Length;
                if ( CountryLen < (rec.Country ?? String.Empty).Length)
                CountryLen = rec.Country.Length;
                if ( HomePhoneLen < (rec.HomePhone ?? String.Empty).Length)
                HomePhoneLen = rec.HomePhone.Length;
                if ( ExtensionLen < (rec.Extension ?? String.Empty).Length)
                ExtensionLen = rec.Extension.Length;
                if ( PhotoPathLen < (rec.PhotoPath ?? String.Empty).Length)
                PhotoPathLen = rec.PhotoPath.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Employees (EmployeeID, LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, ReportsTo, PhotoPath, Deleted) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Employees("
                + "EmployeeID integer,"
                + "LastName nvarchar,"
                + "FirstName nvarchar,"
                + "Title nvarchar,"
                + "TitleOfCourtesy nvarchar,"
                + "BirthDate datetime,"
                + "HireDate datetime,"
                + "Address nvarchar,"
                + "City nvarchar,"
                + "Region nvarchar,"
                + "PostalCode nvarchar,"
                + "Country nvarchar,"
                + "HomePhone nvarchar,"
                + "Extension nvarchar,"
                + "ReportsTo int,"
                + "PhotoPath nvarchar,"
                + "Deleted bit)"
            +" ENGINE = MergeTree"
            +"Order by EmployeeID";
            command.ExecuteNonQuery();
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
        
        int CategoryNameLen;
        public CategoriesList() { }
        
        public CategoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CategoryID],[CategoryName] from [Categories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Categories(reader);
                if ( CategoryNameLen < (rec.CategoryName ?? String.Empty).Length)
                CategoryNameLen = rec.CategoryName.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Categories (CategoryID, CategoryName) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Categories("
                + "CategoryID integer,"
                + "CategoryName nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by CategoryID";
            command.ExecuteNonQuery();
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
        
        int CustomerIDLen;
        int CompanyNameLen;
        int ContactNameLen;
        int ContactTitleLen;
        int AddressLen;
        int CityLen;
        int RegionLen;
        int PostalCodeLen;
        int CountryLen;
        int PhoneLen;
        int FaxLen;
        public CustomersList() { }
        
        public CustomersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [Customers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Customers(reader);
                if ( CustomerIDLen < (rec.CustomerID ?? String.Empty).Length)
                CustomerIDLen = rec.CustomerID.Length;
                if ( CompanyNameLen < (rec.CompanyName ?? String.Empty).Length)
                CompanyNameLen = rec.CompanyName.Length;
                if ( ContactNameLen < (rec.ContactName ?? String.Empty).Length)
                ContactNameLen = rec.ContactName.Length;
                if ( ContactTitleLen < (rec.ContactTitle ?? String.Empty).Length)
                ContactTitleLen = rec.ContactTitle.Length;
                if ( AddressLen < (rec.Address ?? String.Empty).Length)
                AddressLen = rec.Address.Length;
                if ( CityLen < (rec.City ?? String.Empty).Length)
                CityLen = rec.City.Length;
                if ( RegionLen < (rec.Region ?? String.Empty).Length)
                RegionLen = rec.Region.Length;
                if ( PostalCodeLen < (rec.PostalCode ?? String.Empty).Length)
                PostalCodeLen = rec.PostalCode.Length;
                if ( CountryLen < (rec.Country ?? String.Empty).Length)
                CountryLen = rec.Country.Length;
                if ( PhoneLen < (rec.Phone ?? String.Empty).Length)
                PhoneLen = rec.Phone.Length;
                if ( FaxLen < (rec.Fax ?? String.Empty).Length)
                FaxLen = rec.Fax.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Customers("
                + "CustomerID char,"
                + "CompanyName nvarchar,"
                + "ContactName nvarchar,"
                + "ContactTitle nvarchar,"
                + "Address nvarchar,"
                + "City nvarchar,"
                + "Region nvarchar,"
                + "PostalCode nvarchar,"
                + "Country nvarchar,"
                + "Phone nvarchar,"
                + "Fax nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by CustomerID";
            command.ExecuteNonQuery();
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
        
        int CompanyNameLen;
        int PhoneLen;
        public ShippersList() { }
        
        public ShippersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [ShipperID],[CompanyName],[Phone] from [Shippers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Shippers(reader);
                if ( CompanyNameLen < (rec.CompanyName ?? String.Empty).Length)
                CompanyNameLen = rec.CompanyName.Length;
                if ( PhoneLen < (rec.Phone ?? String.Empty).Length)
                PhoneLen = rec.Phone.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Shippers (ShipperID, CompanyName, Phone) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Shippers("
                + "ShipperID integer,"
                + "CompanyName nvarchar,"
                + "Phone nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by ShipperID";
            command.ExecuteNonQuery();
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
        
        int CompanyNameLen;
        int ContactNameLen;
        int ContactTitleLen;
        int AddressLen;
        int CityLen;
        int RegionLen;
        int PostalCodeLen;
        int CountryLen;
        int PhoneLen;
        int FaxLen;
        public SuppliersList() { }
        
        public SuppliersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [SupplierID],[CompanyName],[ContactName],[ContactTitle],[Address],[City],[Region],[PostalCode],[Country],[Phone],[Fax] from [Suppliers]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Suppliers(reader);
                if ( CompanyNameLen < (rec.CompanyName ?? String.Empty).Length)
                CompanyNameLen = rec.CompanyName.Length;
                if ( ContactNameLen < (rec.ContactName ?? String.Empty).Length)
                ContactNameLen = rec.ContactName.Length;
                if ( ContactTitleLen < (rec.ContactTitle ?? String.Empty).Length)
                ContactTitleLen = rec.ContactTitle.Length;
                if ( AddressLen < (rec.Address ?? String.Empty).Length)
                AddressLen = rec.Address.Length;
                if ( CityLen < (rec.City ?? String.Empty).Length)
                CityLen = rec.City.Length;
                if ( RegionLen < (rec.Region ?? String.Empty).Length)
                RegionLen = rec.Region.Length;
                if ( PostalCodeLen < (rec.PostalCode ?? String.Empty).Length)
                PostalCodeLen = rec.PostalCode.Length;
                if ( CountryLen < (rec.Country ?? String.Empty).Length)
                CountryLen = rec.Country.Length;
                if ( PhoneLen < (rec.Phone ?? String.Empty).Length)
                PhoneLen = rec.Phone.Length;
                if ( FaxLen < (rec.Fax ?? String.Empty).Length)
                FaxLen = rec.Fax.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Suppliers (SupplierID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Suppliers("
                + "SupplierID integer,"
                + "CompanyName nvarchar,"
                + "ContactName nvarchar,"
                + "ContactTitle nvarchar,"
                + "Address nvarchar,"
                + "City nvarchar,"
                + "Region nvarchar,"
                + "PostalCode nvarchar,"
                + "Country nvarchar,"
                + "Phone nvarchar,"
                + "Fax nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by SupplierID";
            command.ExecuteNonQuery();
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
        
        int TerritoryIDLen;
        public EmployeeTerritoriesList() { }
        
        public EmployeeTerritoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [EmployeeID],[TerritoryID] from [EmployeeTerritories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new EmployeeTerritories(reader);
                if ( TerritoryIDLen < (rec.TerritoryID ?? String.Empty).Length)
                TerritoryIDLen = rec.TerritoryID.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into EmployeeTerritories (EmployeeID, TerritoryID) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table EmployeeTerritories("
                + "EmployeeID int,"
                + "TerritoryID nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by EmployeeID";
            command.ExecuteNonQuery();
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
                var rec = new OrderDetails(reader);
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into OrderDetails (OrderID, ProductID, UnitPrice, Quantity, Discount) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Order Details("
                + "OrderID int,"
                + "ProductID int,"
                + "UnitPrice numeric,"
                + "Quantity smallint,"
                + "Discount real)"
            +" ENGINE = MergeTree"
            +"Order by OrderID";
            command.ExecuteNonQuery();
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
                var rec = new ProductCategoryMap(reader);
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into ProductCategoryMap (CategoryID, ProductID) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Product_Category_Map("
                + "CategoryID int,"
                + "ProductID int)"
            +" ENGINE = MergeTree"
            +"Order by CategoryID";
            command.ExecuteNonQuery();
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
        
        int CustomerIDLen;
        int CustomerTypeIDLen;
        public CustomerCustomerDemoList() { }
        
        public CustomerCustomerDemoList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [CustomerID],[CustomerTypeID] from [CustomerCustomerDemo]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new CustomerCustomerDemo(reader);
                if ( CustomerIDLen < (rec.CustomerID ?? String.Empty).Length)
                CustomerIDLen = rec.CustomerID.Length;
                if ( CustomerTypeIDLen < (rec.CustomerTypeID ?? String.Empty).Length)
                CustomerTypeIDLen = rec.CustomerTypeID.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into CustomerCustomerDemo (CustomerID, CustomerTypeID) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table CustomerCustomerDemo("
                + "CustomerID char,"
                + "CustomerTypeID char)"
            +" ENGINE = MergeTree"
            +"Order by CustomerID";
            command.ExecuteNonQuery();
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
        
        int TerritoryIDLen;
        int TerritoryDescriptionLen;
        public TerritoriesList() { }
        
        public TerritoriesList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [TerritoryID],[TerritoryDescription],[RegionID] from [Territories]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Territories(reader);
                if ( TerritoryIDLen < (rec.TerritoryID ?? String.Empty).Length)
                TerritoryIDLen = rec.TerritoryID.Length;
                if ( TerritoryDescriptionLen < (rec.TerritoryDescription ?? String.Empty).Length)
                TerritoryDescriptionLen = rec.TerritoryDescription.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Territories (TerritoryID, TerritoryDescription, RegionID) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Territories("
                + "TerritoryID nvarchar,"
                + "TerritoryDescription char,"
                + "RegionID int)"
            +" ENGINE = MergeTree"
            +"Order by TerritoryID";
            command.ExecuteNonQuery();
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
        
        int CustomerIDLen;
        int ShipNameLen;
        int ShipAddressLen;
        int ShipCityLen;
        int ShipRegionLen;
        int ShipPostalCodeLen;
        int ShipCountryLen;
        public OrdersList() { }
        
        public OrdersList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [OrderID],[CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[ShipVia],[Freight],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry] from [Orders]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Orders(reader);
                if ( CustomerIDLen < (rec.CustomerID ?? String.Empty).Length)
                CustomerIDLen = rec.CustomerID.Length;
                if ( ShipNameLen < (rec.ShipName ?? String.Empty).Length)
                ShipNameLen = rec.ShipName.Length;
                if ( ShipAddressLen < (rec.ShipAddress ?? String.Empty).Length)
                ShipAddressLen = rec.ShipAddress.Length;
                if ( ShipCityLen < (rec.ShipCity ?? String.Empty).Length)
                ShipCityLen = rec.ShipCity.Length;
                if ( ShipRegionLen < (rec.ShipRegion ?? String.Empty).Length)
                ShipRegionLen = rec.ShipRegion.Length;
                if ( ShipPostalCodeLen < (rec.ShipPostalCode ?? String.Empty).Length)
                ShipPostalCodeLen = rec.ShipPostalCode.Length;
                if ( ShipCountryLen < (rec.ShipCountry ?? String.Empty).Length)
                ShipCountryLen = rec.ShipCountry.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Orders (OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Orders("
                + "OrderID integer,"
                + "CustomerID char,"
                + "EmployeeID int,"
                + "OrderDate datetime,"
                + "RequiredDate datetime,"
                + "ShippedDate datetime,"
                + "ShipVia int,"
                + "Freight numeric,"
                + "ShipName nvarchar,"
                + "ShipAddress nvarchar,"
                + "ShipCity nvarchar,"
                + "ShipRegion nvarchar,"
                + "ShipPostalCode nvarchar,"
                + "ShipCountry nvarchar)"
            +" ENGINE = MergeTree"
            +"Order by OrderID";
            command.ExecuteNonQuery();
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
        
        int ProductNameLen;
        int QuantityPerUnitLen;
        int AttributeXMLLen;
        int CreatedByLen;
        int ModifiedByLen;
        public ProductsList() { }
        
        public ProductsList(IDbConnection connection) {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "select [ProductID],[ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued],[AttributeXML],[DateCreated],[ProductGUID],[CreatedOn],[CreatedBy],[ModifiedOn],[ModifiedBy],[Deleted] from [Products]";
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var rec = new Products(reader);
                if ( ProductNameLen < (rec.ProductName ?? String.Empty).Length)
                ProductNameLen = rec.ProductName.Length;
                if ( QuantityPerUnitLen < (rec.QuantityPerUnit ?? String.Empty).Length)
                QuantityPerUnitLen = rec.QuantityPerUnit.Length;
                if ( AttributeXMLLen < (rec.AttributeXML ?? String.Empty).Length)
                AttributeXMLLen = rec.AttributeXML.Length;
                if ( CreatedByLen < (rec.CreatedBy ?? String.Empty).Length)
                CreatedByLen = rec.CreatedBy.Length;
                if ( ModifiedByLen < (rec.ModifiedBy ?? String.Empty).Length)
                ModifiedByLen = rec.ModifiedBy.Length;
                
                Add(rec);
            }
        }
        
        public void Reload(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText = "insert into Products (ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, AttributeXML, DateCreated, ProductGUID, CreatedOn, CreatedBy, ModifiedOn, ModifiedBy, Deleted) values @bulk";
            command.Parameters.Add(new ClickHouseParameter { ParameterName = "bulk", Value = this });
            command.ExecuteNonQuery();
        }
        
        public void CreateTable(ClickHouseConnection clickHouseConnection) {
            var command = clickHouseConnection.CreateCommand();
            command.CommandText ="create table Products("
                + "ProductID integer,"
                + "ProductName nvarchar,"
                + "SupplierID int,"
                + "CategoryID int,"
                + "QuantityPerUnit nvarchar,"
                + "UnitPrice numeric,"
                + "UnitsInStock smallint,"
                + "UnitsOnOrder smallint,"
                + "ReorderLevel smallint,"
                + "Discontinued bit,"
                + "AttributeXML varchar,"
                + "DateCreated datetime,"
                + "ProductGUID guid,"
                + "CreatedOn datetime,"
                + "CreatedBy nvarchar,"
                + "ModifiedOn datetime,"
                + "ModifiedBy nvarchar,"
                + "Deleted bit)"
            +" ENGINE = MergeTree"
            +"Order by ProductID";
            command.ExecuteNonQuery();
        }
        
    }

}
