using System;
using System.Collections;
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
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using DevExpress.Utils.Filtering;
using System.Linq.Expressions;

namespace PivotForm
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        StarBench _starBench;
        ProviderAdapter adapter;

        public event NeedToCountEventHandler RaiseNeedToCountEvent;
        public delegate void NeedToCountEventHandler(object sender, NeedToCountEventArgs args);


        public RibbonForm1(StarBench starBench)
        {
            InitializeComponent();
            _starBench = starBench;

            adapter = new ProviderAdapter(_starBench.LineOrder);
            adapter.RaiseCountEvent += Adapter_RaiseCountEvent;


            this.linqServerModeSource1.QueryableSource = adapter.GetQueryableSource;

            RaiseNeedToCountEvent += PivotGridControl1_RaiseNeedToCountEvent;
            pivotGridControl1.FieldAreaChanging += PivotGridControl1_FieldAreaChanging;

            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            pivotGridControl1.FieldsCustomization(this.panelControl1);

     
            pivotGridControl1.BeginRefresh += PivotGridControl1_BeginRefresh;
            pivotGridControl1.EndRefresh += PivotGridControl1_EndRefresh;

            string filterCRegion = this.fieldCREGION1.PrefilterColumnName;
            string filterSRegion = this.fieldSREGION1.PrefilterColumnName;
            string filterLOOrderDate = this.fieldLOORDERDATE1.PrefilterColumnName;
            string filterPMFGR1 = this.fieldPMFGR1.PrefilterColumnName;

            //  pivotGridControl1.ActiveFilterString = "([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '02.01.1993')";
            pivotGridControl1.ActiveFilterString = "[" + filterCRegion + "]= 'AMERICA' AND [" + filterSRegion + "]='AMERICA' AND( ([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '01.01.1994') " +
              $"OR ([" + filterLOOrderDate + "] >= '01.01.1998' and[" + filterLOOrderDate + "] < '01.01.1999' ))  AND([" + filterPMFGR1 + "] = 'MFGR#1' OR [" + filterPMFGR1 + "] = 'MFGR#2')";


        }

        private void OnNeedToCountEvent(NeedToCountEventArgs e)
        {
            NeedToCountEventHandler needToCountEventHandler = RaiseNeedToCountEvent;
            if (needToCountEventHandler != null)
            {
                needToCountEventHandler(this, e);
            }
        }

        private void PivotGridControl1_EndRefresh(object sender, EventArgs e)
        {
            var needtocountevent = new NeedToCountEventArgs();
            needtocountevent.needToCount = false;
            OnNeedToCountEvent(needtocountevent);
        }

        private void PivotGridControl1_BeginRefresh(object sender, EventArgs e)
        {
            var needtocountevent = new NeedToCountEventArgs();
            needtocountevent.needToCount = true;
            OnNeedToCountEvent(needtocountevent);
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
        //    LayotSave();
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
        // записаться к врачу
        //
        // сделать фильтры
        //


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

        private void HideFields()
        {
            foreach (PivotGridField field in pivotGridControl1.Fields)
            {
                if (field.Name == "fieldLOORDTOTALPRICE" || field.Name == "fieldLOORDERDATE"
           || field.Name == "fieldCCITY" || field.Name == "fieldCREGION"
           || field.Name == "fieldPMFGR" || field.Name == "fieldPBRAND"
           || field.Name == "fieldSNATION" || field.Name == "fieldPCATEGORY"
           || field.Name == "fieldSCITY" || field.Name == "fieldLOREVENUE"
           || field.Name == "fieldLOSUPPLYCOST" || field.Name == "fieldCNATION"
            )
                {
                    continue;
                }
                else
                {
                    field.Visible = false;
                }
            }
        }

        private void RibbonForm1_Load(object sender, EventArgs e)
        {      
                  

        

        }

      //  private void CreatePreFilters()
      //  {
      //      string filterCRegion = this.pivotGridControl1.Fields.GetFieldByName("fieldCREGION").PrefilterColumnName;
      //      string filterSRegion = this.pivotGridControl1.Fields.GetFieldByName("fieldSREGION").PrefilterColumnName;
      //      string filterLOOrderDate = this.pivotGridControl1.Fields.GetFieldByName("fieldLOORDERDATE").PrefilterColumnName;
      //      string filterPMFGR1 = this.pivotGridControl1.Fields.GetFieldByName("fieldPMFGR").PrefilterColumnName;

      //      pivotGridControl1.ActiveFilterString = "[" + filterCRegion + "]= 'AMERICA' AND [" + filterSRegion + "]='AMERICA' AND( ([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '01.01.1994') " +
      //        $"OR ([" + filterLOOrderDate + "] >= '01.01.1998' and[" + filterLOOrderDate + "] < '01.01.1999' ))  AND([" + filterPMFGR1 + "] = 'MFGR#1' OR [" + filterPMFGR1 + "] = 'MFGR#2')";

      ////      pivotGridControl1.Prefilter.CriteriaString = "[" + filterCRegion + "]= 'AMERICA' AND [" + filterSRegion + "]='AMERICA' AND( ([" + filterLOOrderDate + "] >= '01.01.1993' And [" + filterLOOrderDate + "] < '01.01.1994') " +
      ////     $"OR ([" + filterLOOrderDate + "] >= '01.01.1998' and[" + filterLOOrderDate + "] < '01.01.1999' ))  AND([" + filterPMFGR1 + "] = 'MFGR#1' OR [" + filterPMFGR1 + "] = 'MFGR#2')";

      //  }

        //private void RenameFields()
        //{
        //    string name = String.Empty;
        //    foreach (PivotGridField el in pivotGridControl1.Fields)
        //    {
        //        el.Name = el.Name.Remove(0, 5);
        //        if (el.Name.StartsWith("C"))
        //        {
        //            if (el.Name.Contains("ADDRESS"))
        //                name += "Адрес ";

        //            if (el.Name.Contains("CITY"))
        //                name += "Город ";

        //            if (el.Name.Contains("SEGMENT"))
        //                name += "Сегмент ";

        //            if (el.Name.Contains("NAME"))
        //                name += "Имя ";

        //            if (el.Name.Contains("NATION"))
        //                name += "Страна ";

        //            if (el.Name.Contains("PHONE"))
        //                name += "Номер телефона ";

        //            if (el.Name.Contains("REGION"))
        //                name += "Регион ";



        //            name += "покупателя";

        //        }
        //        else if (el.Name.StartsWith("S"))
        //        {

        //            if (el.Name.Contains("ADDRESS"))
        //                name += "Адрес ";

        //            if (el.Name.Contains("CITY"))
        //                name += "Город ";

        //            if (el.Name.Contains("SEGMENT"))
        //                name += "Сегмент ";

        //            if (el.Name.Contains("NAME"))
        //                name += "Наименование ";

        //            if (el.Name.Contains("NATION"))
        //                name += "Страна ";

        //            if (el.Name.Contains("PHONE"))
        //                name += "Номер телефона ";

        //            if (el.Name.Contains("REGION"))
        //                name += "Регион ";

        //            name += "поставщика";
        //        }
        //        else if (el.Name.StartsWith("LO"))
        //        {
        //            if (el.Name.Contains("DATE"))
        //            {
        //                name += "Дата  ";
        //                if (el.Name.Contains("COMMIT"))
        //                    name += "завершения  ";
        //                name += "заказа  ";
        //            }

        //            if (el.Name.Contains("KEY"))
        //            {
        //                name += "Идентификатор  ";

        //                if (el.Name.Contains("CUST"))
        //                    name += "покупателя  ";

        //                if (el.Name.Contains("SUP"))
        //                    name += "поставщика  ";

        //                if (el.Name.Contains("ORDER"))
        //                    name += "заказа  ";

        //                if (el.Name.Contains("PART"))
        //                    name += "партии  ";
        //            }
        //            if (el.Name.Contains("DISCOUNT"))
        //            {
        //                name += "Скидка на заказ ";
        //            }

        //            if (el.Name.Contains("PRICE"))
        //            {

        //                if (el.Name.Contains("EXTENDED"))
        //                {
        //                    name += "Расширенная ";
        //                }

        //                if (el.Name.Contains("TOTAL"))
        //                {
        //                    name += "Полная ";
        //                }

        //                name += "цена заказ ";
        //            }

        //            if (el.Name.Contains("LINENUMBER"))
        //            {
        //                name += "Порядковый номер заказа";
        //            }


        //            if (el.Name.Contains("PRIORITY"))
        //            {
        //                name += "Приоритет ";

        //                if (el.Name.Contains("SHIP"))
        //                {
        //                    name += "транспортировки ";
        //                }

        //                name += "заказа";
        //            }

        //            if (el.Name.Contains("QUANTITY"))
        //            {
        //                name += "Объем заказа";
        //            }

        //            if (el.Name.Contains("REVENUE"))
        //            {
        //                name += "Доход заказа";
        //            }

        //            if (el.Name.Contains("SHIP"))
        //            {

        //                if (el.Name.Contains("MODE"))
        //                {
        //                    name += "Способ ";
        //                }

        //                name += "транспортировки заказа";
        //            }

        //            if (el.Name.Contains("COST"))
        //            {
        //                name += "Стоимость услуг поставщика";
        //            }

        //            if (el.Name.Contains("TAX"))
        //            {
        //                name += "Налог на заказ";
        //            }

        //        }

        //        else if (el.Name.StartsWith("P"))
        //        {
        //            if (el.Name.Contains("BRAND"))
        //            {
        //                name += "Брэнд товара";
        //            }

        //            if (el.Name.Contains("CATEGORY"))
        //            {
        //                name += "Категория товара";
        //            }

        //            if (el.Name.Contains("COLOR"))
        //            {
        //                name += "Цвет товара";
        //            }
        //            if (el.Name.Contains("CONTAINER"))
        //            {
        //                name += "Контейнер товара";
        //            }

        //            if (el.Name.Contains("MFGR"))
        //            {
        //                name += "Группа товара";
        //            }

        //            if (el.Name.Contains("NAME"))
        //                name += "Наименование товара ";

        //            if (el.Name.Contains("SIZE"))
        //                name += "Объем партии";

        //            if (el.Name.Contains("TYPE"))
        //                name += "Тип товара";
        //        }
        //        el.Caption = name;
        //        name = String.Empty;
        //    }

        //}

        //private void Accordion_ElementClick(object sender, ElementClickEventArgs e)
        //{
        //    if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
        //    if (e.Element.Text == null) return;
        //    string itemName = e.Element.Text;
        //    if (itemName == "C_CITY")
        //    {
        //        //var query = "select " + itemName + " from lineorder_flat";
        //        var items = _starBench.LineOrder.Select(x => x.C_CITY).ToList();
        //        var el = this.element.Elements.Single(x => x.Name == itemName);
        //        el.BindCommand(items);
        //        //  el.so
        //        //e.Element.
        //    }
        //    else if (itemName == "P_MFGR")
        //    {
        //      //  var prefilter = pivotGridControl1.ActiveFilterString as Expression;
        //        var items = _starBench.LineOrder.Select(x => x.P_MFGR).ToList();
        //        var el = this.element.Elements.Single(x => x.Name == itemName);
        //        el.BindCommand(items);
        //    }
        //}

        //private void FilteringUIContext1_FieldRetrieving(object sender, DevExpress.Utils.Filtering.FilteringUIFieldRetrievingEventArgs e)
        //{
        //    if (e.PropertyName == "fieldLOORDTOTALPRICE" || e.PropertyName == "fieldLOORDERDATE"
        //       || e.PropertyName == "fieldCCITY" || e.PropertyName == "fieldCREGION"
        //       || e.PropertyName == "fieldPMFGR" || e.PropertyName == "fieldPBRAND"
        //       || e.PropertyName == "fieldSNATION" || e.PropertyName == "fieldPCATEGORY"
        //       || e.PropertyName == "fieldSCITY" || e.PropertyName == "fieldLOREVENUE"
        //       || e.PropertyName == "fieldLOSUPPLYCOST" || e.PropertyName == "fieldCNATION"
        //        )
        //    {
        //        e.Cancel = false;             
        //    }
        //    else
        //    {
        //        e.Cancel = true;
        //    }
        //}

        //private void InitAccordionControl()
        //{
        //   accordion.BeginUpdate();
           
        //    this.accordion.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
        //    this.element});
        //    this.accordion.Name = "accordionControl1";
        //    this.accordion.Text = "accordionControl1";     
        //    this.element.Expanded = true;
        //    this.element.Name = "accordionControlElement1";
        //    this.element.Text = "Фильтры";

        //    accordion.EndUpdate();
        //}


        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // class MySelector : IChildrenSelector
    }
}