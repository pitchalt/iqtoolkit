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
    public partial class FormStarBench : DevExpress.XtraEditors.XtraForm
    {
//        lineorder_flat_collection col;
        private StarBench _starBench;
        public FormStarBench(StarBench starBench)
        {
            InitializeComponent();
            _starBench = starBench;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.linqServerModeSource1.QueryableSource = _starBench.LineOrder;
        }
    }
}
