using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Templates.ActionControls;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.ExpressApp.Win.Templates.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Customization;
using Test;

namespace PivotForm
{
    public partial class DetailRibbonForm1 : RibbonForm, IActionControlsSite, IContextMenuHolder, IWindowTemplate, IBarManagerHolder, ISupportViewChanged, ISupportUpdate, IViewSiteTemplate, ISupportStoreSettings, IViewHolder
    {
        private static readonly object viewChanged = new object();
        private static readonly object settingsReloaded = new object();
        private StatusMessagesHelper statusMessagesHelper;
        private StarBench _starBench;


        protected virtual void RaiseViewChanged(DevExpress.ExpressApp.View view)
        {
            EventHandler<TemplateViewChangedEventArgs> handler = (EventHandler<TemplateViewChangedEventArgs>)Events[viewChanged];
            if (handler != null)
            {
                handler(this, new TemplateViewChangedEventArgs(view));
            }
        }
        protected virtual void RaiseSettingsReloaded()
        {
            EventHandler handler = (EventHandler)Events[settingsReloaded];
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        protected override FormShowMode ShowMode
        {
            get { return FormShowMode.AfterInitialization; }
        }
        public DetailRibbonForm1(StarBench starBench)
        {
            InitializeComponent();
            ribbonControl.Manager.ForceLinkCreate();
            statusMessagesHelper = new StatusMessagesHelper(barContainerStatusMessages);

            _starBench = starBench;            
            this.linqServerModeSource1.QueryableSource = _starBench.LineOrder;
            pivotGridControl1.OptionsCustomization.CustomizationFormStyle = CustomizationFormStyle.Excel2007;
            pivotGridControl1.FieldsCustomization();

            //    pivotGridControl1.CollapseAllColumns();

            //   LetsTrySomeShit();


           //his.checkedListBoxControl1.DataSource = fieldLOTAX1;
          //  string price = this.fieldLOTAX1.PrefilterColumnName;
        //    pivotGridControl1.ActiveFilterString = "([" + price + "] >= 25000 And [" + price + "] <= 80000)";


            //fieldModelPriceFilterUIEditorContainerEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.fieldLOTAX1, "fieldLOTAX1meme", true));
            //this.fieldModelPriceFilterUIEditorContainerEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.fieldModelPriceFilterUIEditorContainerEdit.EditValue = "filterUIEditorContainerEdit1";
            //this.fieldModelPriceFilterUIEditorContainerEdit.Location = new System.Drawing.Point(17, 2);
            //this.fieldModelPriceFilterUIEditorContainerEdit.Name = "fieldModelPriceFilterUIEditorContainerEdit";
            //this.fieldModelPriceFilterUIEditorContainerEdit.Properties.LookupUIEditorType = DevExpress.Utils.Filtering.LookupUIEditorType.Default;
            //this.fieldModelPriceFilterUIEditorContainerEdit.Properties.UseFlags = true;
            //this.fieldModelPriceFilterUIEditorContainerEdit.Size = new System.Drawing.Size(155, 22);
            //this.fieldModelPriceFilterUIEditorContainerEdit.TabIndex = 0;
            //this.fieldModelPriceFilterUIEditorContainerEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.filteringUIContext, "fieldModelPrice", true));

            //string price = _starBench.LineOrder. fieldModelPrice.PrefilterColumnName, body = fieldBodyStyle.PrefilterColumnName;
            //  pivotGridControl.ActiveFilterString = "([" + price + "] >= 25000 And [" + price + "] <= 80000) And [" + body + "] In (6,7)";
            // Hide loading panel
            // pivotGridControl.LoadingPanelVisible = false;
        }


        private void LetsTrySomeShit()
        {

           // var list = pivotGridControl1.GetFieldList();


           // string meme = "meme";
            //    string price = pivotField.PrefilterColumnName;

            //   ivotGridControl1.ActiveFilterString = "([" + price + "] >= 25000 And [" + price + "] <= 80000) And [" + body + "] In (6,7)";

            foreach (var field in pivotGridControl1.Fields)
            {
                if (field.GetType() == typeof(Int32))
                {
                    var pivotField = field as PivotGridField;

                    pivotGridControl1.BeginUpdate();
                    try
                    {
                        pivotField.FilterValues.Clear();
                        //pivotField.FilterValues.FilterType = PivotFilterType.Included;
                        //pivotField.FilterValues.Add("Chevrolet");
                        //pivotField.FilterValues.Add("Chrysler");

                        string price = pivotField.PrefilterColumnName;
                        
                        pivotGridControl1.ActiveFilterString = "([" + price + "] >= 100 And [" + price + "] <= 100000)";
                    }
                    finally
                    {
                        pivotGridControl1.EndUpdate();
                    }

                }
            }



        }

        #region IActionControlsSite Members
        IEnumerable<IActionControlContainer> IActionControlsSite.ActionContainers
        {
            get { return ribbonControl.ActionContainers; }
        }
        IEnumerable<IActionControl> IActionControlsSite.ActionControls
        {
            get { return ribbonControl.ActionControls; }
        }
        IActionControlContainer IActionControlsSite.DefaultContainer
        {
            get { return barActionContainerView; }
        }
        #endregion

        #region IFrameTemplate Members
        void IFrameTemplate.SetView(DevExpress.ExpressApp.View view)
        {
            viewSiteManager.SetView(view);
            RaiseViewChanged(view);
        }
        ICollection<IActionContainer> IFrameTemplate.GetContainers()
        {
            return new IActionContainer[] { };
        }
        IActionContainer IFrameTemplate.DefaultContainer
        {
            get { return null; }
        }
        #endregion

        #region IWindowTemplate Members
        void IWindowTemplate.SetCaption(string caption)
        {
            Text = caption;
        }
        void IWindowTemplate.SetStatus(ICollection<string> statusMessages)
        {
            statusMessagesHelper.SetMessages(statusMessages);
        }
        bool IWindowTemplate.IsSizeable
        {
            get { return FormBorderStyle == FormBorderStyle.Sizable; }
            set { FormBorderStyle = (value ? FormBorderStyle.Sizable : FormBorderStyle.FixedDialog); }
        }
        #endregion

        #region IBarManagerHolder Members
        BarManager IBarManagerHolder.BarManager
        {
            get { return ribbonControl.Manager; }
        }
        event EventHandler IBarManagerHolder.BarManagerChanged
        {
            add { }
            remove { }
        }
        #endregion

        #region IContextMenuHolder
        PopupMenu IContextMenuHolder.ContextMenu
        {
            get { return contextMenu; }
        }
        #endregion

        #region ISupportViewChanged Members
        event EventHandler<TemplateViewChangedEventArgs> ISupportViewChanged.ViewChanged
        {
            add { Events.AddHandler(viewChanged, value); }
            remove { Events.RemoveHandler(viewChanged, value); }
        }
        #endregion

        #region ISupportUpdate Members
        void ISupportUpdate.BeginUpdate()
        {
            if (ribbonControl.Manager != null)
            {
                ribbonControl.Manager.BeginUpdate();
            }
        }
        void ISupportUpdate.EndUpdate()
        {
            if (ribbonControl.Manager != null)
            {
                ribbonControl.Manager.EndUpdate();
            }
        }
        #endregion

        #region IViewSiteTemplate Members
        object IViewSiteTemplate.ViewSiteControl
        {
            get { return viewSiteManager.ViewSiteControl; }
        }
        #endregion

        #region ISupportStoreSettings Members
        void ISupportStoreSettings.SetSettings(IModelTemplate settings)
        {
            IModelTemplateWin templateModel = (IModelTemplateWin)settings;
            TemplatesHelper templatesHelper = new TemplatesHelper(templateModel);
            IModelFormState formState;
            if (viewSiteManager.View != null)
            {
                formState = templatesHelper.GetFormStateNode(viewSiteManager.View.Id);
            }
            else
            {
                formState = templatesHelper.GetFormStateNode();
            }
            formStateModelSynchronizer.Model = formState;
            templatesHelper.SetRibbonSettings(ribbonControl);
        }
        void ISupportStoreSettings.ReloadSettings()
        {
            modelSynchronizationManager.ApplyModel();
            RaiseSettingsReloaded();
        }
        void ISupportStoreSettings.SaveSettings()
        {
            SuspendLayout();
            try
            {
                modelSynchronizationManager.SynchronizeModel();
            }
            finally
            {
                ResumeLayout();
            }
        }
        event EventHandler ISupportStoreSettings.SettingsReloaded
        {
            add { Events.AddHandler(settingsReloaded, value); }
            remove { Events.RemoveHandler(settingsReloaded, value); }
        }
        #endregion

        #region IViewHolder Members
        DevExpress.ExpressApp.View IViewHolder.View
        {
            get { return viewSiteManager.View; }
        }
        #endregion

        private void pivotGridControl1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void filterEditorControl1_Click(object sender, EventArgs e)
        {

        }

        private void rangeTrackBarControl1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void fieldModelPriceFilterUIEditorContainerEdit_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
