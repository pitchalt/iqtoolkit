using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Test;
using DevExpress.XtraPivotGrid.Customization;

namespace PivotForm
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        StarBench _starBench;
        public RibbonForm1(StarBench starBench)
        {
            InitializeComponent();


            _starBench = starBench;
            this.linqServerModeSource1.QueryableSource = _starBench.LineOrder;
            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            pivotGridControl1.FieldsCustomization(this.sidePanel1);


            string price = this.fieldLOEXTENDEDPRICE1.PrefilterColumnName;
            pivotGridControl1.ActiveFilterString = "([" + price + "] >= 50000 And [" + price + "] <= 20000)";

            string discount = this.fieldLODISCOUNT1.PrefilterColumnName;
            pivotGridControl1.ActiveFilterString = "([" + discount + "] >= 5 And [" + discount + "] <= 10)";

           // string custAddress = this.fieldCADDRESS1.PrefilterColumnName;
          //  pivotGridControl1.ActiveFilterString = "([" + discount + "] == 'qlq9IMZJy2'";

        }
    }
}