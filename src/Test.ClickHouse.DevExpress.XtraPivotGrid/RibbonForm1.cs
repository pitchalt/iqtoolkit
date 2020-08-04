﻿using System;
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

namespace PivotForm
{
    public partial class RibbonForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        StarBench _starBench;
        PivotLinqAdapter pivotGridControl1;
        ProviderAdapter adapter;
        AccordionControl accordion;
        PanelControl panelControl;
        LayoutControl lc;
        FilteringUIContext filteringUIContext1;


        public RibbonForm1(StarBench starBench)
        {
            InitializeComponent();
            pivotGridControl1 = new PivotLinqAdapter();
            panelControl = new PanelControl();
            filteringUIContext1 = new FilteringUIContext();

            LayoutControl lc = new LayoutControl();
            lc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(lc);
            lc.BeginUpdate();
            try
            {
                lc.Root.GroupBordersVisible = false;

                LayoutControlGroup group1 = new LayoutControlGroup();

                LayoutControlItem item1 = group1.AddItem();
                item1.Control = pivotGridControl1;

                LayoutControlItem item2 = new LayoutControlItem(lc, accordion);

                item2.Move(item1, InsertType.Right);

                LayoutControlItem item3 = new LayoutControlItem(lc, panelControl);
                item3.Move(item2, InsertType.Right);


                lc.Root.Add(group1);
            }
            finally
            {
                lc.EndUpdate();
            }

            this.linqServerModeSource1.ElementType = typeof(lineorder_flat);

            _starBench = starBench;
            adapter = new ProviderAdapter(_starBench.LineOrder);
            adapter.RaiseCountEvent += Adapter_RaiseCountEvent;
            // adapter.RaiseNeedToCountEvent += Adapter_RaiseNeedToCountEvent;

            pivotGridControl1.RaiseNeedToCountEvent += PivotGridControl1_RaiseNeedToCountEvent;

            pivotGridControl1.FieldAreaChanging += PivotGridControl1_FieldAreaChanging;


            this.linqServerModeSource1.QueryableSource = adapter.GetQueryableSource;
            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            pivotGridControl1.FieldsCustomization(this.panelControl);

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
            accordion = new AccordionControl();

            pivotGridControl1.DataSource = linqServerModeSource1;
            pivotGridControl1.RetrieveFields();
            HideFields();

            filteringUIContext1.Control = accordion;
            filteringUIContext1.Client = pivotGridControl1;
            filteringUIContext1.FieldRetrieving += FilteringUIContext1_FieldRetrieving;
            filteringUIContext1.FieldRetrieved += FilteringUIContext1_FieldRetrieved;
            filteringUIContext1.RetrieveFields();
            accordion.Update();
            filteringUIContext1.Control.RetrieveFields(pivotGridControl1, typeof(lineorder_flat));



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
        }

        private void FilteringUIContext1_FieldRetrieved(object sender, FilteringUIFieldRetrievedEventArgs e)
        {
            if (e.PropertyName  == "fieldLOORDTOTALPRICE" || e.PropertyName == "fieldLOORDERDATE"
          || e.PropertyName == "fieldCCITY" || e.PropertyName == "fieldCREGION"
          || e.PropertyName == "fieldPMFGR" || e.PropertyName == "fieldPBRAND"
          || e.PropertyName == "fieldSNATION" || e.PropertyName == "fieldPCATEGORY"
          || e.PropertyName == "fieldSCITY" || e.PropertyName == "fieldLOREVENUE"
          || e.PropertyName == "fieldLOSUPPLYCOST" || e.PropertyName == "fieldCNATION"
           )
            {
                AccordionControlElement element = e.Item as AccordionControlElement;

                element.Text = e.PropertyName;

                filteringUIContext1.AddField("", e.Item.GetType());
            }

          
        }
        private void InitAccordionControl()
        {
            acControl.BeginUpdate();
            AccordionControlElement acRootGroupHome = new AccordionControlElement();
            AccordionControlElement acItemActivity = new AccordionControlElement();
            AccordionControlElement acItemNews = new AccordionControlElement();
            AccordionControlElement acRootItemSettings = new AccordionControlElement();

            acControl.ElementClick += new ElementClickEventHandler(this.accordionControl1_ElementClick);

            // 
            // Root Group 'Home'
            // 
            acRootGroupHome.Elements.AddRange(new AccordionControlElement[] {
            acItemActivity,
            acItemNews});
            acRootGroupHome.Expanded = true;
            acRootGroupHome.ImageOptions.ImageUri.Uri = "Home;Office2013";
            acRootGroupHome.Name = "acRootGroupHome";
            acRootGroupHome.Text = "Home";
            // 
            // Child Item 'Activity'
            // 
            acItemActivity.Name = "acItemActivity";
            acItemActivity.Style = ElementStyle.Item;
            acItemActivity.Tag = "idActivity";
            acItemActivity.Text = "Activity";
            // 
            // Child Item 'News'
            // 
            acItemNews.Name = "acItemNews";
            acItemNews.Style = ElementStyle.Item;
            acItemNews.Tag = "idNews";
            acItemNews.Text = "News";
            // 
            // Root Item 'Settings' with ContentContainer
            // 
            acRootItemSettings.ImageOptions.ImageUri.Uri = "Customization;Office2013";
            acRootItemSettings.Name = "acRootItemSettings";
            acRootItemSettings.Style = ElementStyle.Item;
            acRootItemSettings.Text = "Settings";
            // 
            // itemSettingsControlContainer
            // 
            AccordionContentContainer itemSettingsControlContainer = new AccordionContentContainer();
            HyperlinkLabelControl hyperlinkLabelControl1 = new HyperlinkLabelControl();
            ToggleSwitch toggleSwitch1 = new ToggleSwitch();
            acControl.Controls.Add(itemSettingsControlContainer);
            acRootItemSettings.ContentContainer = itemSettingsControlContainer;
            itemSettingsControlContainer.Controls.Add(hyperlinkLabelControl1);
            itemSettingsControlContainer.Controls.Add(toggleSwitch1);
            itemSettingsControlContainer.Appearance.BackColor = System.Drawing.SystemColors.Control;
            itemSettingsControlContainer.Appearance.Options.UseBackColor = true;
            itemSettingsControlContainer.Height = 60;
            // 
            // hyperlinkLabelControl1
            // 
            hyperlinkLabelControl1.Location = new System.Drawing.Point(26, 33);
            hyperlinkLabelControl1.Size = new System.Drawing.Size(107, 13);
            hyperlinkLabelControl1.Text = "www.devexpress.com";
            hyperlinkLabelControl1.HyperlinkClick += new DevExpress.Utils.HyperlinkClickEventHandler(this.hyperlinkLabelControl1_HyperlinkClick);
            // 
            // toggleSwitch1
            // 
            toggleSwitch1.EditValue = true;
            toggleSwitch1.Location = new System.Drawing.Point(24, 3);
            toggleSwitch1.Properties.AllowFocused = false;
            toggleSwitch1.Properties.AutoWidth = true;
            toggleSwitch1.Properties.OffText = "Offline Mode";
            toggleSwitch1.Properties.OnText = "Onlne Mode";
            toggleSwitch1.Size = new System.Drawing.Size(134, 24);
            toggleSwitch1.Toggled += new System.EventHandler(this.toggleSwitch1_Toggled);

            acControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
                acRootGroupHome,
                acRootItemSettings});

            acRootItemSettings.Expanded = true;

            acControl.EndUpdate();
        }

        private void accordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            if (e.Element.Style == DevExpress.XtraBars.Navigation.ElementStyle.Group) return;
            if (e.Element.Tag == null) return;
            string itemID = e.Element.Tag.ToString();
            if (itemID == "idNews")
            {
                //...
            }
            listBoxControl1.Items.Add(itemID + " clicked");
        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }
    }
}