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
    public partial class FormNorthwindIQT : DevExpress.XtraEditors.XtraForm
    {
//        lineorder_flat_collection col;
        private NorthwindIQT _northwindIQT;
        public FormNorthwindIQT(NorthwindIQT northwindiqt)
        {
            InitializeComponent();
            _northwindIQT = northwindiqt;           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            linqServerModeSource1.QueryableSource = _northwindIQT.OrderDetail;

        }
    }
}
