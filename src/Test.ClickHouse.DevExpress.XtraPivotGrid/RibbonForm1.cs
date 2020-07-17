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
             pivotGridControl1.FieldsCustomization(this.panelControl1);

            this.fieldCADDRESS1.Caption = "Адрес покупателя";
            this.fieldCADDRESS1.Visible = false;            
            this.fieldCNAME1.Caption = "Имя покупателя";
            this.fieldCNAME1.Visible = false;
            this.fieldCPHONE1.Caption = "Номер телефона покупателя";
            this.fieldCPHONE1.Visible = false;
            this.fieldLOCOMMITDATE1.Caption = "Дата завершения заказа";
            this.fieldLOCOMMITDATE1.Visible = true;
            this.fieldLOCUSTKEY1.Caption = "Идентификатор покупателя";
            this.fieldLOCUSTKEY1.Visible = false;
            this.fieldLODISCOUNT1.Caption = "Скидка на заказ";
            this.fieldLODISCOUNT1.Visible = true;
            this.fieldLOEXTENDEDPRICE1.Caption = "Полная цена заказа";
            this.fieldLOEXTENDEDPRICE1.Visible = false;
            this.fieldLOLINENUMBER1.Caption = "Порядковый номер заказа";
            this.fieldLOLINENUMBER1.Visible = false;
            this.fieldLOORDERDATE1.Caption = "Дата заказа";
            this.fieldLOORDERDATE1.Visible = true;
            this.fieldLOORDERKEY1.Caption = "Идентификатор заказа";
            this.fieldLOORDERKEY1.Visible = false;
            this.fieldLOORDTOTALPRICE1.Caption = "Итоговая цена заказа";
            this.fieldLOORDTOTALPRICE1.Visible = true;
            this.fieldLOPARTKEY1.Caption = "Идентификатор партии";
            this.fieldLOPARTKEY1.Visible = false;
            this.fieldLOQUANTITY1.Caption = "Объем заказа";
            this.fieldLOQUANTITY1.Visible = true;
            this.fieldLOREVENUE1.Caption = "Доход заказа";
            this.fieldLOREVENUE1.Visible = false;
            this.fieldLOSHIPPRIORITY1.Caption = "Приоритет доставки заказа";
            this.fieldLOSHIPPRIORITY1.Visible = false;
            this.fieldLOSUPPKEY1.Caption = "Идентификатор поставщика";
            this.fieldLOSUPPKEY1.Visible = false;
            this.fieldLOSUPPLYCOST1.Caption = "Стоимость услуг поставщика";
            this.fieldLOSUPPLYCOST1.Visible = false;
            this.fieldLOTAX1.Caption = "Налог на заказ";
            this.fieldLOTAX1.Visible = true;
            this.fieldPNAME1.Caption = "Наименование партии";
            this.fieldPNAME1.Visible = false;
            this.fieldPSIZE1.Caption = "Объем партии";
            this.fieldPSIZE1.Visible = true;
            this.fieldSADDRESS1.Caption = "Адрес поставщика";
            this.fieldSADDRESS1.Visible = false;
            this.fieldSNAME1.Caption = "Наименование поставщика";
            this.fieldSNAME1.Visible = false;
            this.fieldSPHONE1.Caption = "Номер телефона поставщика";
            this.fieldSPHONE1.Visible = false;


            filteringUIContext1.FieldRetrieving += FilteringUIContext1_FieldRetrieving;
            filteringUIContext1.RetrieveFields();


            
            string commitdate  = this.fieldLOCOMMITDATE1.PrefilterColumnName;
            String commitdateString = "([" + commitdate + "] >= '01.01.1993' And [" + commitdate + "] < '02.01.1993')";

            string date = this.fieldLOORDERDATE1.PrefilterColumnName;
            String dateString = "([" + date + "] >= '01.01.1993' And [" + date + "] <= '02.01.1994')";

            string price = this.fieldLOORDTOTALPRICE1.PrefilterColumnName;
            String priceString = "([" + price + "] >= 50000 And [" + price + "] <= 100000)";

            string discount = this.fieldLODISCOUNT1.PrefilterColumnName;
           String discountString = "([" + discount + "] >= 5 And [" + discount + "] < 6)";

            string quantity = this.fieldLOQUANTITY1.PrefilterColumnName;
            String quantityString = "([" + quantity + "] <= 25)";

            string tax = this.fieldLOTAX1.PrefilterColumnName;
            String taxString= "([" + tax + "] >= 3 And [" + tax + "] <= 4)";

            string size = this.fieldPSIZE1.PrefilterColumnName;
            String sizeString= "([" + size + "] >= 25 And [" + size + "] <= 26)";


            pivotGridControl1.ActiveFilterString = commitdateString + " And " + dateString + " And " + priceString + " And " + discountString + " And " +
                quantityString + " And " + taxString + " And " + sizeString;

         //   pivotGridControl1.ActiveFilterString = dateString + " and ([" + discount + "] > 1 And [" + discount + "] < 3) and [" + quantity + "] < 25";


            /*
             
SELECT C_NATION, S_NATION, toYear(LO_ORDERDATE) AS year, sum(LO_REVENUE) AS revenue FROM lineorder_flat WHERE C_REGION = 'ASIA' AND S_REGION = 'ASIA' AND year >= 1992 AND year <= 1997 GROUP BY C_NATION, S_NATION, year ORDER BY year ASC, revenue DESC;
SELECT C_CITY, S_CITY, toYear(LO_ORDERDATE) AS year, sum(LO_REVENUE) AS revenue FROM lineorder_flat WHERE C_NATION = 'UNITED STATES' AND S_NATION = 'UNITED STATES' AND year >= 1992 AND year <= 1997 GROUP BY C_CITY, S_CITY, year ORDER BY year ASC, revenue DESC;
SELECT C_CITY, S_CITY, toYear(LO_ORDERDATE) AS year, sum(LO_REVENUE) AS revenue FROM lineorder_flat WHERE (C_CITY = 'UNITED KI1' OR C_CITY = 'UNITED KI5') AND (S_CITY = 'UNITED KI1' OR S_CITY = 'UNITEDKI5') AND year >= 1992 AND year <= 1997 GROUP BY C_CITY, S_CITY, year ORDER BY year ASC, revenue DESC;
SELECT C_CITY, S_CITY, toYear(LO_ORDERDATE) AS year, sum(LO_REVENUE) AS revenue FROM lineorder_flat WHERE (C_CITY = 'UNITED KI1' OR C_CITY = 'UNITED KI5') AND (S_CITY = 'UNITED KI1' OR S_CITY = 'UNITEDKI5') AND toYYYYMM(LO_ORDERDATE) = 199712 GROUP BY C_CITY, S_CITY, year ORDER BY year ASC, revenue DESC;

SELECT toYear(LO_ORDERDATE) AS year, C_NATION, sum(LO_REVENUE - LO_SUPPLYCOST) AS profit FROM lineorder_flat WHERE C_REGION = 'AMERICA' AND S_REGION = 'AMERICA' AND (P_MFGR = 'MFGR#1' OR P_MFGR = 'MFGR#2') GROUP BY year, C_NATION ORDER BY year ASC, C_NATION ASC;
SELECT toYear(LO_ORDERDATE) AS year, S_NATION, P_CATEGORY, sum(LO_REVENUE - LO_SUPPLYCOST) AS profit FROM lineorder_flat WHERE C_REGION = 'AMERICA' AND S_REGION = 'AMERICA' AND (year = 1997 OR year = 1998) AND (P_MFGR = 'MFGR#1' OR P_MFGR = 'MFGR#2') GROUP BY year, S_NATION, P_CATEGORY ORDER BY year ASC, S_NATION ASC, P_CATEGORY ASC;
SELECT toYear(LO_ORDERDATE) AS year, S_CITY, P_BRAND, sum(LO_REVENUE - LO_SUPPLYCOST) AS profit FROM lineorder_flat WHERE S_NATION = 'UNITED STATES' AND (year = 1997 OR year = 1998) AND P_CATEGORY = 'MFGR#14' GROUP BY year, S_CITY, P_BRAND ORDER BY year ASC, S_CITY ASC, P_BRAND ASC;





             */
        }


        private void FilteringUIContext1_FieldRetrieving(object sender, DevExpress.Utils.Filtering.FilteringUIFieldRetrievingEventArgs e)
        {
            if(e.PropertyName == "fieldCADDRESS1" || e.PropertyName == "fieldCNAME1"
               || e.PropertyName == "fieldCPHONE1" || e.PropertyName == "fieldLOCUSTKEY1"
               || e.PropertyName == "fieldLOEXTENDEDPRICE1" || e.PropertyName == "fieldLOLINENUMBER1"
               || e.PropertyName == "fieldLOORDERKEY1" || e.PropertyName == "fieldLOPARTKEY1"
               || e.PropertyName == "fieldLOREVENUE1" || e.PropertyName == "fieldLOSHIPPRIORITY1"
               || e.PropertyName == "fieldLOSUPPKEY1" || e.PropertyName == "fieldLOSUPPLYCOST1"
               || e.PropertyName == "fieldPNAME1" || e.PropertyName == "fieldSADDRESS1"
               || e.PropertyName == "fieldSNAME1" || e.PropertyName == "fieldSPHONE1"
                )
            {
                e.Cancel = true;                        
            }
        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {

        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }
    }
}