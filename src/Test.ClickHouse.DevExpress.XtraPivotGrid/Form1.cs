using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IQToolkit.Data.ClickHouse;
using ClickHouse.Ado;

namespace PivotForm
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        lineorder_flat_collection col;

        public Form1()
        {
            InitializeComponent();
            
            ClickHouseQueryProvider provider =// new ClickHouseQueryProvider(CreateConnection(), null, null);

            new ClickHouseQueryProvider(CreateConnection(), null, null);
            col = new lineorder_flat_collection(provider);
            //     var query = (IQueryable<lineorder_flat>)
            //     from lineOrder in flat_Collection.FlatCollection
            //   select lineOrder;

            //var me = this
            //var fact_code =  .GetObjectsQuery<lineorder_flat_collection>(false);
            //// ObjectsQuery fact_code = new ObjectsQuery()
            //var query =
            //    from fact in fact_code
            //    select new FactLS
            //    {
            //        Date = fact.Date,
            //        Contract = fact.Contract,
            //        Summ = fact.Summ,
            //        Order = fact.Order,
            //        Article = fact.Article
            //    };

            // linqServerModeSource1.KeyExpression = "LO_ORDERKEY";

          //  this.pivotGridControl1.OptionsData.DataProcessingEngine = DevExpress.XtraPivotGrid.PivotDataProcessingEngine.Legacy;

        }


        public ClickHouseConnection CreateConnection()
        {
            var connection = new ClickHouseConnection(CreateConnectionSettings());
       //     connection.Open();
            return connection;
        }

        public ClickHouseConnectionSettings CreateConnectionSettings()
        {
            ClickHouseConnectionSettings set = new ClickHouseConnectionSettings();

            set.Host = "10.200.101.163";
            set.Port = 9000;
            set.Compress = true;
            set.User = "default";
            set.Password = "";
            set.Database = "default";
            //   var con_str = set.ToString();
            //   System.Console.WriteLine(con_str);
            return set;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            linqServerModeSource1.QueryableSource = col.FlatCollection;

        }
    }
}
