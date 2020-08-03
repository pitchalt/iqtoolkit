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
using DevExpress.XtraPivotGrid;
using DevExpress.XtraLayout;
using DevExpress.XtraBars.Navigation;

namespace PivotForm
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        StarBench _starBench;
        PivotLinqAdapter pivotGridControl1;
        ProviderAdapter adapter;
        AccordionControl accordion;


        public RibbonForm1(StarBench starBench)
        {
            InitializeComponent();
            pivotGridControl1 = new PivotLinqAdapter();
            LayoutControlItem lay = new LayoutControlItem();
            lay.Control = pivotGridControl1;
            lay.Parent = this.Root;

            LayoutControlItem layacc = new LayoutControlItem();
            layacc.Control = accordion;
            layacc.Parent = this.Root;

            this.Root.AddItem("", lay, DevExpress.XtraLayout.Utils.InsertType.Right);
            this.Root.AddItem("", layacc, DevExpress.XtraLayout.Utils.InsertType.Left);
            // this.layoutControl1.AddItem(lay, DevExpress.XtraLayout.Utils.InsertType.Left);          


            _starBench = starBench;
            adapter = new ProviderAdapter(_starBench.LineOrder);
            adapter.RaiseCountEvent += Adapter_RaiseCountEvent;
            // adapter.RaiseNeedToCountEvent += Adapter_RaiseNeedToCountEvent;

            pivotGridControl1.RaiseNeedToCountEvent += PivotGridControl1_RaiseNeedToCountEvent;

            pivotGridControl1.FieldAreaChanging += PivotGridControl1_FieldAreaChanging;


            var control = pivotGridControl1.Parent;
            

            this.linqServerModeSource1.QueryableSource = adapter.GetQueryableSource;
            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            // pivotGridControl1.FieldsCustomization(this.panelControl1);

            //LayotSave();


            //this.fieldCADDRESS1.Caption = "Адрес покупателя";
            //this.fieldCADDRESS1.Visible = false;
            //this.fieldCNAME1.Caption = "Имя покупателя";
            //this.fieldCNAME1.Visible = false;
            //this.fieldCPHONE1.Caption = "Номер телефона покупателя";
            //this.fieldCPHONE1.Visible = false;
            //this.fieldLOCOMMITDATE1.Caption = "Дата завершения заказа";
            //this.fieldLOCOMMITDATE1.Visible = false;
            //this.fieldLOCUSTKEY1.Caption = "Идентификатор покупателя";
            //this.fieldLOCUSTKEY1.Visible = false;
            //this.fieldLODISCOUNT1.Caption = "Скидка на заказ";
            //this.fieldLODISCOUNT1.Visible = false;
            //this.fieldLOEXTENDEDPRICE1.Caption = "Полная цена заказа";
            //this.fieldLOEXTENDEDPRICE1.Visible = false;
            //this.fieldLOLINENUMBER1.Caption = "Порядковый номер заказа";
            //this.fieldLOLINENUMBER1.Visible = false;
            //this.fieldLOORDERDATE1.Caption = "Дата заказа";
            //this.fieldLOORDERDATE1.Visible = false;
            //this.fieldLOORDERKEY1.Caption = "Идентификатор заказа";
            //this.fieldLOORDERKEY1.Visible = false;
            //this.fieldLOORDTOTALPRICE1.Caption = "Итоговая цена заказа";
            //this.fieldLOORDTOTALPRICE1.Visible = false;
            //this.fieldLOPARTKEY1.Caption = "Идентификатор партии";
            //this.fieldLOPARTKEY1.Visible = false;
            //this.fieldLOQUANTITY1.Caption = "Объем заказа";
            //this.fieldLOQUANTITY1.Visible = false;
            //this.fieldLOREVENUE1.Caption = "Доход заказа";
            //this.fieldLOREVENUE1.Visible = false;
            //this.fieldLOSHIPPRIORITY1.Caption = "Приоритет доставки заказа";
            //this.fieldLOSHIPPRIORITY1.Visible = false;
            //this.fieldLOSUPPKEY1.Caption = "Идентификатор поставщика";
            //this.fieldLOSUPPKEY1.Visible = false;
            //this.fieldLOSUPPLYCOST1.Caption = "Стоимость услуг поставщика";
            //this.fieldLOSUPPLYCOST1.Visible = false;
            //this.fieldLOTAX1.Caption = "Налог на заказ";
            //this.fieldLOTAX1.Visible = false;
            //this.fieldPNAME1.Caption = "Наименование партии";
            //this.fieldPNAME1.Visible = false;
            //this.fieldPSIZE1.Caption = "Объем партии";
            //this.fieldPSIZE1.Visible = false;
            ////this.fieldPSIZE1.TotalsVisibility = PivotTotalsVisibility.None;
            //this.fieldSADDRESS1.Caption = "Адрес поставщика";
            //this.fieldSADDRESS1.Visible = false;
            //this.fieldSNAME1.Caption = "Наименование поставщика";
            //this.fieldSNAME1.Visible = false;
            //this.fieldSPHONE1.Caption = "Номер телефона поставщика";
            //this.fieldSPHONE1.Visible = false;


            //this.fieldCMKTSEGMENT1.Caption = "Сегмент покупателя";
            //this.fieldCMKTSEGMENT1.Visible = false;
            //this.fieldCNATION1.Caption = "Страна покупателя";
            //this.fieldCNATION1.Visible = false;
            //this.fieldPCOLOR1.Caption = "Цвет товара";
            //this.fieldPCOLOR1.Visible = false;
            //this.fieldPCATEGORY1.Caption = "Категория товара";
            //this.fieldPCATEGORY1.Visible = false;
            //this.fieldPCONTAINER1.Caption = "Контейнер товара";
            //this.fieldPCONTAINER1.Visible = false;
            //this.fieldPMFGR1.Caption = "Группа товара";
            //this.fieldPMFGR1.Visible = false;
            //this.fieldPTYPE1.Caption = "Тип товара";
            //this.fieldPTYPE1.Visible = false;
            //this.fieldCREGION1.Caption = "Регион покупателя";
            //this.fieldCREGION1.Visible = false;
            //this.fieldSNATION1.Caption = "Страна поставщика";
            //this.fieldSNATION1.Visible = false;
            //this.fieldSREGION1.Caption = "Регион поставщика";
            //this.fieldSREGION1.Visible = false;
            //this.fieldCCITY1.Caption = "Город покупателя";
            //this.fieldCCITY1.Visible = false;
            //this.fieldLOORDERPRIORITY1.Caption = "Приоритет заказа";
            //this.fieldLOORDERPRIORITY1.Visible = false;
            //this.fieldLOSHIPMODE1.Caption = "Способ транспортировки";
            //this.fieldLOSHIPMODE1.Visible = false;
            //this.fieldPBRAND1.Caption = "Брэнд товара";
            //this.fieldPBRAND1.Visible = false;
            //this.fieldSCITY1.Caption = "Город поставщика";
            //this.fieldSCITY1.Visible = false;

            filteringUIContext1.Control = accordion;
          //  filteringUIContext1.cl
            filteringUIContext1.FieldRetrieving += FilteringUIContext1_FieldRetrieving;
            filteringUIContext1.RetrieveFields();

            



            //string commitdate = this.fieldLOCOMMITDATE1.PrefilterColumnName;
            //String commitdateString = "([" + commitdate + "] >= '01.01.1993' And [" + commitdate + "] < '02.01.1993')";

            //string date = this.fieldLOORDERDATE1.PrefilterColumnName;
            //String dateString = "([" + date + "] >= '01.01.1993' And [" + date + "] <= '02.01.1994')";

            //string price = this.fieldLOORDTOTALPRICE1.PrefilterColumnName;
            //String priceString = "([" + price + "] >= 50000 And [" + price + "] <= 100000)";

            //string discount = this.fieldLODISCOUNT1.PrefilterColumnName;
            //String discountString = "([" + discount + "] >= 5 And [" + discount + "] < 6)";

            //string quantity = this.fieldLOQUANTITY1.PrefilterColumnName;
            //String quantityString = "([" + quantity + "] <= 25)";

            //string tax = this.fieldLOTAX1.PrefilterColumnName;
            //String taxString = "([" + tax + "] >= 3 And [" + tax + "] <= 4)";

            //string size = this.fieldPSIZE1.PrefilterColumnName;
            //String sizeString = "([" + size + "] >= 25 And [" + size + "] <= 26)";


            //pivotGridControl1.ActiveFilterString = commitdateString + " And " + dateString + " And " + priceString + " And " + discountString + " And " +
            //    quantityString + " And " + taxString + " And " + sizeString;

            //pivotGridControl1.ActiveFilterString = dateString + " and ([" + discount + "] > 1 And [" + discount + "] < 3) and [" + quantity + "] < 25";


            //string filterCRegion = this.fieldCREGION1.PrefilterColumnName;
            //string filterSRegion = this.fieldSREGION1.PrefilterColumnName;
            //string filterLOOrderDate = this.fieldLOORDERDATE1.PrefilterColumnName;
            //string filterPMFGR1 = this.fieldPMFGR1.PrefilterColumnName;

            ////  pivotGridControl1.ActiveFilterString = "([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '02.01.1993')";
            //pivotGridControl1.ActiveFilterString = "[" + filterCRegion + "]= 'AMERICA' AND [" + filterSRegion + "]='AMERICA' AND( ([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '01.01.1994') " +
            //  $"OR ([" + filterLOOrderDate + "] >= '01.01.1998' and[" + filterLOOrderDate + "] < '01.01.1999' ))  AND([" + filterPMFGR1 + "] = 'MFGR#1' OR [" + filterPMFGR1 + "] = 'MFGR#2')";


        }

        private void PivotGridControl1_RaiseNeedToCountEvent(object sender, NeedToCountEventArgs args)
        {
            adapter.NeedToCount = args.needToCount;
        }

        private void Adapter_RaiseCountEvent(object sender, CountEventArgs args)
        {
            if (args.count <= 1000)
            {

                //  LayotSave();
            }
            else if (args.count > 1000 && args.count < 10000)
            {
                if (AskToUploadWindow(args.count))
                {
                    args.canUpload = true;
                    //      LayotSave();
                }
                else
                {
                    args.canUpload = false;
                    //   LayotRestore();
                }
            }
            else if (args.count > 10000)
            {
                CreateErrorUploadWindow();
                args.canUpload = false;
                //     LayotRestore();
            }
        }

        private void Adapter_RaiseNeedToCountEvent(object sender, NeedToCountEventArgs args)
        {
          
        }

        private void PivotGridControl1_FieldAreaChanging(object sender, DevExpress.XtraPivotGrid.PivotAreaChangingEventArgs e)
        {
            LayotSave();
        }

        private void LayotSave()
        {
            pivotGridControl1.SaveLayoutToRegistry("DevExpress\\XtraGrid\\Layouts\\MainLayout");
        }

        private void LayotRestore()
        {
            pivotGridControl1.RestoreLayoutFromRegistry("DevExpress\\XtraGrid\\Layouts\\MainLayout");
           // stream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        // иногда падает из-за пустого списка, потестить
        // убрать из провайдера типы object
        // отследить первое выполнение запроса
        // сохранить лайаут при неудачном запросе
        // записаться к врачу, голова болит
     

        private void CreateErrorUploadWindow()
        {
            string caption = "Upload error";
            string message = "The number of records is above 1 000 000, please change filters";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool AskToUploadWindow(int count)
        {
            string caption = "Upload error";
            string message = $"The number of records is {count}, are you sure to upload records?";
            DialogResult result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo);
            return result == DialogResult.Yes ? true : false;
        }

        private void FilteringUIContext1_FieldRetrieving(object sender, DevExpress.Utils.Filtering.FilteringUIFieldRetrievingEventArgs e)
        {
            if (e.PropertyName == "fieldLOORDTOTALPRICE1" || e.PropertyName == "fieldLOORDERDATE1"
               || e.PropertyName == "fieldCCITY1" || e.PropertyName == "fieldCREGION1"
               || e.PropertyName == "fieldPMFGR1" || e.PropertyName == "fieldPBRAND1"
               || e.PropertyName == "fieldSNATION1" || e.PropertyName == "fieldPCATEGORY1"
               || e.PropertyName == "fieldSCITY1" || e.PropertyName == "fieldLOREVENUE1"
               || e.PropertyName == "fieldLOSUPPLYCOST1" || e.PropertyName == "fieldCNATION1"
                )
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {
            pivotGridControl1.DataSource = linqServerModeSource1;
            pivotGridControl1.RetrieveFields();

            string name = String.Empty;
            foreach (PivotGridField el in pivotGridControl1.Fields)
            {
                el.Name = el.Name.Remove(0, 5);
                if (el.Name.StartsWith("C"))
                {
                    if (el.Name.Contains("ADDRESS"))
                        name += "Адрес ";

                    if (el.Name.Contains("CITY"))
                        name += "Город ";

                    if (el.Name.Contains("SEGMENT"))
                        name += "Сегмент ";

                    if (el.Name.Contains("NAME"))
                        name += "Имя ";

                    if (el.Name.Contains("NATION"))
                        name += "Страна ";

                    if (el.Name.Contains("PHONE"))
                        name += "Номер телефона ";

                    if (el.Name.Contains("REGION"))
                        name += "Регион ";



                    name += "покупателя";

                }
                else if (el.Name.StartsWith("S"))
                {

                    if (el.Name.Contains("ADDRESS"))
                        name += "Адрес ";

                    if (el.Name.Contains("CITY"))
                        name += "Город ";

                    if (el.Name.Contains("SEGMENT"))
                        name += "Сегмент ";

                    if (el.Name.Contains("NAME"))
                        name += "Наименование ";

                    if (el.Name.Contains("NATION"))
                        name += "Страна ";

                    if (el.Name.Contains("PHONE"))
                        name += "Номер телефона ";

                    if (el.Name.Contains("REGION"))
                        name += "Регион ";

                    name += "поставщика";
                }
                else if (el.Name.StartsWith("LO"))
                {
                    if (el.Name.Contains("DATE"))
                    {
                        name += "Дата  ";
                        if (el.Name.Contains("COMMIT"))
                            name += "завершения  ";
                        name += "заказа  ";
                    }

                    if (el.Name.Contains("KEY"))
                    {
                        name += "Идентификатор  ";

                        if (el.Name.Contains("CUST"))
                            name += "покупателя  ";

                        if (el.Name.Contains("SUP"))
                            name += "поставщика  ";

                        if (el.Name.Contains("ORDER"))
                            name += "заказа  ";

                        if (el.Name.Contains("PART"))
                            name += "партии  ";
                    }
                    if (el.Name.Contains("DISCOUNT"))
                    {
                        name += "Скидка на заказ ";
                    }

                    if (el.Name.Contains("PRICE"))
                    {

                        if (el.Name.Contains("EXTENDED"))
                        {
                            name += "Расширенная ";
                        }

                        if (el.Name.Contains("TOTAL"))
                        {
                            name += "Полная ";
                        }

                        name += "цена заказ ";
                    }

                    if (el.Name.Contains("LINENUMBER"))
                    {
                        name += "Порядковый номер заказа";
                    }


                    if (el.Name.Contains("PRIORITY"))
                    {
                        name += "Приоритет ";

                        if (el.Name.Contains("SHIP"))
                        {
                            name += "транспортировки ";
                        }

                        name += "заказа";
                    }

                    if (el.Name.Contains("QUANTITY"))
                    {
                        name += "Объем заказа";
                    }

                    if (el.Name.Contains("REVENUE"))
                    {
                        name += "Доход заказа";
                    }

                    if (el.Name.Contains("SHIP"))
                    {

                        if (el.Name.Contains("MODE"))
                        {
                            name += "Способ ";
                        }

                        name += "транспортировки заказа";
                    }

                    if (el.Name.Contains("COST"))
                    {
                        name += "Стоимость услуг поставщика";
                    }

                    if (el.Name.Contains("TAX"))
                    {
                        name += "Налог на заказ";
                    }

                }

                else if (el.Name.StartsWith("P"))
                {
                    if (el.Name.Contains("BRAND"))
                    {
                        name += "Брэнд товара";
                    }

                    if (el.Name.Contains("CATEGORY"))
                    {
                        name += "Категория товара";
                    }

                    if (el.Name.Contains("COLOR"))
                    {
                        name += "Цвет товара";
                    }
                    if (el.Name.Contains("CONTAINER"))
                    {
                        name += "Контейнер товара";
                    }

                    if (el.Name.Contains("MFGR"))
                    {
                        name += "Группа товара";
                    }

                    if (el.Name.Contains("NAME"))
                        name += "Наименование товара ";

                    if (el.Name.Contains("SIZE"))
                        name += "Объем партии";

                    if (el.Name.Contains("TYPE"))
                        name += "Тип товара";
                }
                el.Caption = name;
                name = String.Empty;
            }

            //this.fieldCADDRESS1.Caption = "Адрес покупателя";
            //this.fieldCADDRESS1.Visible = false;
            //this.fieldCNAME1.Caption = "Имя покупателя";
            //this.fieldCNAME1.Visible = false;
            //this.fieldCPHONE1.Caption = "Номер телефона покупателя";
            //this.fieldCPHONE1.Visible = false;
            //this.fieldLOCOMMITDATE1.Caption = "Дата завершения заказа";
            //this.fieldLOCOMMITDATE1.Visible = false;
            //this.fieldLOCUSTKEY1.Caption = "Идентификатор покупателя";
            //this.fieldLOCUSTKEY1.Visible = false;
            //this.fieldLODISCOUNT1.Caption = "Скидка на заказ";
            //this.fieldLODISCOUNT1.Visible = false;
            //this.fieldLOEXTENDEDPRICE1.Caption = "Полная цена заказа";
            //this.fieldLOEXTENDEDPRICE1.Visible = false;
            //this.fieldLOLINENUMBER1.Caption = "Порядковый номер заказа";
            //this.fieldLOLINENUMBER1.Visible = false;
            //this.fieldLOORDERDATE1.Caption = "Дата заказа";
            //this.fieldLOORDERDATE1.Visible = false;
            //this.fieldLOORDERKEY1.Caption = "Идентификатор заказа";
            //this.fieldLOORDERKEY1.Visible = false;
            //this.fieldLOORDTOTALPRICE1.Caption = "Итоговая цена заказа";
            //this.fieldLOORDTOTALPRICE1.Visible = false;
            //this.fieldLOPARTKEY1.Caption = "Идентификатор партии";
            //this.fieldLOPARTKEY1.Visible = false;
            //this.fieldLOQUANTITY1.Caption = "Объем заказа";
            //this.fieldLOQUANTITY1.Visible = false;
            //this.fieldLOREVENUE1.Caption = "Доход заказа";
            //this.fieldLOREVENUE1.Visible = false;
            //this.fieldLOSHIPPRIORITY1.Caption = "Приоритет доставки заказа";
            //this.fieldLOSHIPPRIORITY1.Visible = false;
            //this.fieldLOSUPPKEY1.Caption = "Идентификатор поставщика";
            //this.fieldLOSUPPKEY1.Visible = false;
            //this.fieldLOSUPPLYCOST1.Caption = "Стоимость услуг поставщика";
            //this.fieldLOSUPPLYCOST1.Visible = false;
            //this.fieldLOTAX1.Caption = "Налог на заказ";
            //this.fieldLOTAX1.Visible = false;
            //this.fieldPNAME1.Caption = "Наименование партии";
            //this.fieldPNAME1.Visible = false;
            //this.fieldPSIZE1.Caption = "Объем партии";
            //this.fieldPSIZE1.Visible = false;
            ////this.fieldPSIZE1.TotalsVisibility = PivotTotalsVisibility.None;
            //this.fieldSADDRESS1.Caption = "Адрес поставщика";
            //this.fieldSADDRESS1.Visible = false;
            //this.fieldSNAME1.Caption = "Наименование поставщика";
            //this.fieldSNAME1.Visible = false;
            //this.fieldSPHONE1.Caption = "Номер телефона поставщика";
            //this.fieldSPHONE1.Visible = false;


            //this.fieldCMKTSEGMENT1.Caption = "Сегмент покупателя";
            //this.fieldCMKTSEGMENT1.Visible = false;
            //this.fieldCNATION1.Caption = "Страна покупателя";
            //this.fieldCNATION1.Visible = false;
            //this.fieldPCOLOR1.Caption = "Цвет товара";
            //this.fieldPCOLOR1.Visible = false;
            //this.fieldPCATEGORY1.Caption = "Категория товара";
            //this.fieldPCATEGORY1.Visible = false;
            //this.fieldPCONTAINER1.Caption = "Контейнер товара";
            //this.fieldPCONTAINER1.Visible = false;
            //this.fieldPMFGR1.Caption = "Группа товара";
            //this.fieldPMFGR1.Visible = false;
            //this.fieldPTYPE1.Caption = "Тип товара";
            //this.fieldPTYPE1.Visible = false;
            //this.fieldCREGION1.Caption = "Регион покупателя";
            //this.fieldCREGION1.Visible = false;
            //this.fieldSNATION1.Caption = "Страна поставщика";
            //this.fieldSNATION1.Visible = false;
            //this.fieldSREGION1.Caption = "Регион поставщика";
            //this.fieldSREGION1.Visible = false;
            //this.fieldCCITY1.Caption = "Город покупателя";
            //this.fieldCCITY1.Visible = false;
            //this.fieldLOORDERPRIORITY1.Caption = "Приоритет заказа";
            //this.fieldLOORDERPRIORITY1.Visible = false;
            //this.fieldLOSHIPMODE1.Caption = "Способ транспортировки";
            //this.fieldLOSHIPMODE1.Visible = false;
            //this.fieldPBRAND1.Caption = "Брэнд товара";
            //this.fieldPBRAND1.Visible = false;
            //this.fieldSCITY1.Caption = "Город поставщика";
            //this.fieldSCITY1.Visible = false;



            //  pivotGridControl1.Update();
        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }
    }
}