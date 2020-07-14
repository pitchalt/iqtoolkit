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
using DevExpress.Xpo;
using Test;

namespace PivotForm
{
    public partial class FormNorthwindXpo : DevExpress.XtraEditors.XtraForm
    {
//        lineorder_flat_collection col;
        private NorthwindXpo _northwindXpo;
        public FormNorthwindXpo(NorthwindXpo northwindxpo)
        {
            InitializeComponent();
            _northwindXpo = northwindxpo;           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            linqServerModeSource1.QueryableSource = new XPQuery<OrderDetail>(_northwindXpo.CreateSession());

        }
    }
}
