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

        bool needToRestoreLayout;


        public RibbonForm1(StarBench starBench)
        {
            InitializeComponent();
            _starBench = starBench;

            adapter = new ProviderAdapter(_starBench.LineOrder);

            this.linqServerModeSource1.QueryableSource = adapter.GetQueryableSource;

            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            pivotGridControl1.FieldsCustomization(this.panelControl1);

            SetEventFollows();
            SetFieldCaption();

            filteringUIContext1.RetrieveFields();

            SetPreFilters();

            SendCountEvent(true, true);

        }

        private void SetEventFollows()
        {
            RaiseNeedToCountEvent += PivotGridControl1_RaiseNeedToCountEvent;
            adapter.RaiseCountEvent += Adapter_RaiseCountEvent;
            pivotGridControl1.BeginRefresh += PivotGridControl1_BeginRefresh;
            pivotGridControl1.EndRefresh += PivotGridControl1_EndRefresh;
            filteringUIContext1.FieldRetrieving += FilteringUIContext1_FieldRetrieving;
            accordionControl1.ElementClick += AccordionControl1_ElementClick;

        }

        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            SendCountEvent(true, false);
        }

        private void SendCountEvent(bool needToCount, bool needToCreateCheckCountWindow)
        {
            var needtocountevent = new NeedToCountEventArgs();
            needtocountevent.NeedToCount = needToCount;
            needtocountevent.NeedToCreateCheckCountWindow = needToCreateCheckCountWindow;
            OnNeedToCountEvent(needtocountevent);
        }

        private void SetFieldCaption()
        {
            this.fieldCADDRESS1.Caption = "Адрес покупателя";
            this.fieldCADDRESS1.Visible = false;
            this.fieldCNAME1.Caption = "Имя покупателя";
            this.fieldCNAME1.Visible = false;
            this.fieldCPHONE1.Caption = "Номер телефона покупателя";
            this.fieldCPHONE1.Visible = false;
            this.fieldLOCOMMITDATE1.Caption = "Дата завершения заказа";
            this.fieldLOCOMMITDATE1.Visible = false;
            this.fieldLOCUSTKEY1.Caption = "Идентификатор покупателя";
            this.fieldLOCUSTKEY1.Visible = false;
            this.fieldLODISCOUNT1.Caption = "Скидка на заказ";
            this.fieldLODISCOUNT1.Visible = false;
            this.fieldLOEXTENDEDPRICE1.Caption = "Полная цена заказа";
            this.fieldLOEXTENDEDPRICE1.Visible = false;
            this.fieldLOLINENUMBER1.Caption = "Порядковый номер заказа";
            this.fieldLOLINENUMBER1.Visible = false;
            this.fieldLOORDERDATE1.Caption = "Дата заказа";
            this.fieldLOORDERDATE1.Visible = false;
            this.fieldLOORDERKEY1.Caption = "Идентификатор заказа";
            this.fieldLOORDERKEY1.Visible = false;
            this.fieldLOORDTOTALPRICE1.Caption = "Итоговая цена заказа";
            this.fieldLOORDTOTALPRICE1.Visible = false;
            this.fieldLOPARTKEY1.Caption = "Идентификатор партии";
            this.fieldLOPARTKEY1.Visible = false;
            this.fieldLOQUANTITY1.Caption = "Объем заказа";
            this.fieldLOQUANTITY1.Visible = false;
            this.fieldLOREVENUE1.Caption = "Доход заказа";
            this.fieldLOREVENUE1.Visible = false;
            this.fieldLOSHIPPRIORITY1.Caption = "Приоритет доставки заказа";
            this.fieldLOSHIPPRIORITY1.Visible = false;
            this.fieldLOSUPPKEY1.Caption = "Идентификатор поставщика";
            this.fieldLOSUPPKEY1.Visible = false;
            this.fieldLOSUPPLYCOST1.Caption = "Стоимость услуг поставщика";
            this.fieldLOSUPPLYCOST1.Visible = false;
            this.fieldLOTAX1.Caption = "Налог на заказ";
            this.fieldLOTAX1.Visible = false;
            this.fieldPNAME1.Caption = "Наименование партии";
            this.fieldPNAME1.Visible = false;
            this.fieldPSIZE1.Caption = "Объем партии";
            this.fieldPSIZE1.Visible = false;
            this.fieldSADDRESS1.Caption = "Адрес поставщика";
            this.fieldSADDRESS1.Visible = false;
            this.fieldSNAME1.Caption = "Наименование поставщика";
            this.fieldSNAME1.Visible = false;
            this.fieldSPHONE1.Caption = "Номер телефона поставщика";
            this.fieldSPHONE1.Visible = false;


            this.fieldCMKTSEGMENT1.Caption = "Сегмент покупателя";
            this.fieldCMKTSEGMENT1.Visible = false;
            this.fieldCNATION1.Caption = "Страна покупателя";
            this.fieldCNATION1.Visible = false;
            this.fieldPCOLOR1.Caption = "Цвет товара";
            this.fieldPCOLOR1.Visible = false;
            this.fieldPCATEGORY1.Caption = "Категория товара";
            this.fieldPCATEGORY1.Visible = false;
            this.fieldPCONTAINER1.Caption = "Контейнер товара";
            this.fieldPCONTAINER1.Visible = false;
            this.fieldPMFGR1.Caption = "Группа товара";
            this.fieldPMFGR1.Visible = false;
            this.fieldPTYPE1.Caption = "Тип товара";
            this.fieldPTYPE1.Visible = false;
            this.fieldCREGION1.Caption = "Регион покупателя";
            this.fieldCREGION1.Visible = false;
            this.fieldSNATION1.Caption = "Страна поставщика";
            this.fieldSNATION1.Visible = false;
            this.fieldSREGION1.Caption = "Регион поставщика";
            this.fieldSREGION1.Visible = false;
            this.fieldCCITY1.Caption = "Город покупателя";
            this.fieldCCITY1.Visible = false;
            this.fieldLOORDERPRIORITY1.Caption = "Приоритет заказа";
            this.fieldLOORDERPRIORITY1.Visible = false;
            this.fieldLOSHIPMODE1.Caption = "Способ транспортировки";
            this.fieldLOSHIPMODE1.Visible = false;
            this.fieldPBRAND1.Caption = "Брэнд товара";
            this.fieldPBRAND1.Visible = false;
            this.fieldSCITY1.Caption = "Город поставщика";
            this.fieldSCITY1.Visible = false;
        }

        private void SetPreFilters()
        {
            string filterCRegion = this.fieldCREGION1.PrefilterColumnName;
            string filterSRegion = this.fieldSREGION1.PrefilterColumnName;
            string filterLOOrderDate = this.fieldLOORDERDATE1.PrefilterColumnName;
            string filterPMFGR1 = this.fieldPMFGR1.PrefilterColumnName;

            pivotGridControl1.ActiveFilterString = "[" + filterCRegion + "]= 'AMERICA' AND [" + filterSRegion + "]='AMERICA' " +
              //  "AND( " +
             //   "[" + filterLOOrderDate + "] < '01-01-1993')" + 
           $" AND([" + filterPMFGR1 + "] = 'MFGR#1' OR [" + filterPMFGR1 + "] = 'MFGR#2')";

        }

        private void FilteringUIContext1_FieldRetrieving(object sender, DevExpress.Utils.Filtering.FilteringUIFieldRetrievingEventArgs e)
        {
            if (e.PropertyName == "fieldLOORDTOTALPRICE1" || e.PropertyName == "fieldLOORDERDATE1"
               || e.PropertyName == "fieldCCITY1" || e.PropertyName == "fieldCREGION1"
               || e.PropertyName == "fieldPMFGR1" || e.PropertyName == "fieldPBRAND1"
               || e.PropertyName == "fieldSNATION1" || e.PropertyName == "fieldSREGION1"
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
            SendCountEvent(false, true);

            if (needToRestoreLayout)
            {
                pivotGridControl1.RefreshData();
            }

        }

        private void PivotGridControl1_BeginRefresh(object sender, EventArgs e)
        {
            if (needToRestoreLayout)
            {
                SendCountEvent(true, false);
                needToRestoreLayout = false;
            }
            else
                SendCountEvent(true, true);
        }

        private void PivotGridControl1_RaiseNeedToCountEvent(object sender, NeedToCountEventArgs args)
        {
            adapter.NeedToCount = args.NeedToCount;
            adapter.NeedToCreateCheckCountWindow = args.NeedToCreateCheckCountWindow;
        }

        private void Adapter_RaiseCountEvent(object sender, CountEventArgs args)
        {
            if (args.Count <= 1000)
            {
                CanUploadRecords(args);
            }
            else if (args.Count > 1000 && args.Count < 10000)
            {
                if (AskToUploadWindow(args.Count))
                {
                    CanUploadRecords(args);
                }
                else
                {
                    CannotUploadRecords(args);
                }
            }
            else if (args.Count > 10000)
            {
                CreateErrorUploadWindow();
                CannotUploadRecords(args);
            }

        }

        private void CanUploadRecords(CountEventArgs args)
        {
            args.CanUpload = true;
            LayotSave();
        }

        private void CannotUploadRecords(CountEventArgs args)
        {
            args.CanUpload = false;
            LayotRestore();
            needToRestoreLayout = true;
        }

        private void LayotSave()
        {
            pivotGridControl1.SaveLayoutToRegistry("DevExpress\\XtraGrid\\Layouts\\MainLayout");
        }

        private void LayotRestore()
        {
            pivotGridControl1.RestoreLayoutFromRegistry("DevExpress\\XtraGrid\\Layouts\\MainLayout");

        }

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

        private void RibbonForm1_Load(object sender, EventArgs e)
        {
            //  needToRestoreLayout = true;
        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}