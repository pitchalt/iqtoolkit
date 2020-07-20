namespace PivotForm
{
    partial class RibbonForm1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.linqServerModeSource1 = new DevExpress.Data.Linq.LinqServerModeSource();
            this.fieldCADDRESS1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCNAME1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldCPHONE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOCOMMITDATE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOCUSTKEY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLODISCOUNT1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOEXTENDEDPRICE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOLINENUMBER1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOORDERDATE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOORDERKEY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOORDTOTALPRICE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOPARTKEY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOQUANTITY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOREVENUE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOSHIPPRIORITY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOSUPPKEY1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOSUPPLYCOST1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldLOTAX1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldPNAME1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldPSIZE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldSADDRESS1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldSNAME1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldSPHONE1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.filteringUIContext1 = new DevExpress.Utils.Filtering.FilteringUIContext(this.components);
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linqServerModeSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filteringUIContext1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.Controller = this.barAndDockingController1;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(1268, 143);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 617);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1268, 31);
            this.ribbonStatusBar.Click += new System.EventHandler(this.ribbonStatusBar_Click);
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.DataSource = this.linqServerModeSource1;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {
            this.fieldCADDRESS1,
            this.fieldCNAME1,
            this.fieldCPHONE1,
            this.fieldLOCOMMITDATE1,
            this.fieldLOCUSTKEY1,
            this.fieldLODISCOUNT1,
            this.fieldLOEXTENDEDPRICE1,
            this.fieldLOLINENUMBER1,
            this.fieldLOORDERDATE1,
            this.fieldLOORDERKEY1,
            this.fieldLOORDTOTALPRICE1,
            this.fieldLOPARTKEY1,
            this.fieldLOQUANTITY1,
            this.fieldLOREVENUE1,
            this.fieldLOSHIPPRIORITY1,
            this.fieldLOSUPPKEY1,
            this.fieldLOSUPPLYCOST1,
            this.fieldLOTAX1,
            this.fieldPNAME1,
            this.fieldPSIZE1,
            this.fieldSADDRESS1,
            this.fieldSNAME1,
            this.fieldSPHONE1});
            this.pivotGridControl1.Location = new System.Drawing.Point(12, 12);
            this.pivotGridControl1.MenuManager = this.ribbon;
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(733, 426);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // linqServerModeSource1
            // 
            this.linqServerModeSource1.ElementType = typeof(Test.lineorder_flat);
            // 
            // fieldCADDRESS1
            // 
            this.fieldCADDRESS1.AreaIndex = 0;
            this.fieldCADDRESS1.Caption = "Адрес покупателя";
            this.fieldCADDRESS1.FieldName = "C_ADDRESS";
            this.fieldCADDRESS1.Name = "fieldCADDRESS1";
            this.fieldCADDRESS1.Visible = false;
            // 
            // fieldCNAME1
            // 
            this.fieldCNAME1.AreaIndex = 0;
            this.fieldCNAME1.Caption = "Имя покупателя";
            this.fieldCNAME1.FieldName = "C_NAME";
            this.fieldCNAME1.Name = "fieldCNAME1";
            this.fieldCNAME1.Visible = false;
            // 
            // fieldCPHONE1
            // 
            this.fieldCPHONE1.AreaIndex = 0;
            this.fieldCPHONE1.Caption = "Customer Phone";
            this.fieldCPHONE1.FieldName = "C_PHONE";
            this.fieldCPHONE1.Name = "fieldCPHONE1";
            // 
            // fieldLOCOMMITDATE1
            // 
            this.fieldLOCOMMITDATE1.AreaIndex = 1;
            this.fieldLOCOMMITDATE1.Caption = "Order Commit Date";
            this.fieldLOCOMMITDATE1.FieldName = "LO_COMMITDATE";
            this.fieldLOCOMMITDATE1.Name = "fieldLOCOMMITDATE1";
            // 
            // fieldLOCUSTKEY1
            // 
            this.fieldLOCUSTKEY1.AreaIndex = 2;
            this.fieldLOCUSTKEY1.Caption = "Order CustomerKey";
            this.fieldLOCUSTKEY1.FieldName = "LO_CUSTKEY";
            this.fieldLOCUSTKEY1.Name = "fieldLOCUSTKEY1";
            // 
            // fieldLODISCOUNT1
            // 
            this.fieldLODISCOUNT1.AreaIndex = 3;
            this.fieldLODISCOUNT1.Caption = "Order Discount";
            this.fieldLODISCOUNT1.FieldName = "LO_DISCOUNT";
            this.fieldLODISCOUNT1.Name = "fieldLODISCOUNT1";
            // 
            // fieldLOEXTENDEDPRICE1
            // 
            this.fieldLOEXTENDEDPRICE1.AreaIndex = 4;
            this.fieldLOEXTENDEDPRICE1.Caption = "Order Extended Price";
            this.fieldLOEXTENDEDPRICE1.FieldName = "LO_EXTENDEDPRICE";
            this.fieldLOEXTENDEDPRICE1.Name = "fieldLOEXTENDEDPRICE1";
            // 
            // fieldLOLINENUMBER1
            // 
            this.fieldLOLINENUMBER1.AreaIndex = 5;
            this.fieldLOLINENUMBER1.Caption = "Order Line Number";
            this.fieldLOLINENUMBER1.FieldName = "LO_LINENUMBER";
            this.fieldLOLINENUMBER1.Name = "fieldLOLINENUMBER1";
            // 
            // fieldLOORDERDATE1
            // 
            this.fieldLOORDERDATE1.AreaIndex = 6;
            this.fieldLOORDERDATE1.Caption = "Order Date";
            this.fieldLOORDERDATE1.FieldName = "LO_ORDERDATE";
            this.fieldLOORDERDATE1.Name = "fieldLOORDERDATE1";
            // 
            // fieldLOORDERKEY1
            // 
            this.fieldLOORDERKEY1.AreaIndex = 7;
            this.fieldLOORDERKEY1.Caption = "Order Key";
            this.fieldLOORDERKEY1.FieldName = "LO_ORDERKEY";
            this.fieldLOORDERKEY1.Name = "fieldLOORDERKEY1";
            // 
            // fieldLOORDTOTALPRICE1
            // 
            this.fieldLOORDTOTALPRICE1.AreaIndex = 8;
            this.fieldLOORDTOTALPRICE1.Caption = "Order Total Price";
            this.fieldLOORDTOTALPRICE1.FieldName = "LO_ORDTOTALPRICE";
            this.fieldLOORDTOTALPRICE1.Name = "fieldLOORDTOTALPRICE1";
            // 
            // fieldLOPARTKEY1
            // 
            this.fieldLOPARTKEY1.AreaIndex = 9;
            this.fieldLOPARTKEY1.Caption = "Order PartKey";
            this.fieldLOPARTKEY1.FieldName = "LO_PARTKEY";
            this.fieldLOPARTKEY1.Name = "fieldLOPARTKEY1";
            // 
            // fieldLOQUANTITY1
            // 
            this.fieldLOQUANTITY1.AreaIndex = 10;
            this.fieldLOQUANTITY1.Caption = "Order Quantity";
            this.fieldLOQUANTITY1.FieldName = "LO_QUANTITY";
            this.fieldLOQUANTITY1.Name = "fieldLOQUANTITY1";
            // 
            // fieldLOREVENUE1
            // 
            this.fieldLOREVENUE1.AreaIndex = 11;
            this.fieldLOREVENUE1.Caption = "Order Revenue";
            this.fieldLOREVENUE1.FieldName = "LO_REVENUE";
            this.fieldLOREVENUE1.Name = "fieldLOREVENUE1";
            // 
            // fieldLOSHIPPRIORITY1
            // 
            this.fieldLOSHIPPRIORITY1.AreaIndex = 12;
            this.fieldLOSHIPPRIORITY1.Caption = "Order Ship Priority";
            this.fieldLOSHIPPRIORITY1.FieldName = "LO_SHIPPRIORITY";
            this.fieldLOSHIPPRIORITY1.Name = "fieldLOSHIPPRIORITY1";
            // 
            // fieldLOSUPPKEY1
            // 
            this.fieldLOSUPPKEY1.AreaIndex = 13;
            this.fieldLOSUPPKEY1.Caption = "Order SupplierKey";
            this.fieldLOSUPPKEY1.FieldName = "LO_SUPPKEY";
            this.fieldLOSUPPKEY1.Name = "fieldLOSUPPKEY1";
            // 
            // fieldLOSUPPLYCOST1
            // 
            this.fieldLOSUPPLYCOST1.AreaIndex = 14;
            this.fieldLOSUPPLYCOST1.Caption = "Order Supplier Cost";
            this.fieldLOSUPPLYCOST1.FieldName = "LO_SUPPLYCOST";
            this.fieldLOSUPPLYCOST1.Name = "fieldLOSUPPLYCOST1";
            // 
            // fieldLOTAX1
            // 
            this.fieldLOTAX1.AreaIndex = 15;
            this.fieldLOTAX1.Caption = "Order Tax";
            this.fieldLOTAX1.FieldName = "LO_TAX";
            this.fieldLOTAX1.Name = "fieldLOTAX1";
            // 
            // fieldPNAME1
            // 
            this.fieldPNAME1.AreaIndex = 16;
            this.fieldPNAME1.Caption = "Part Name";
            this.fieldPNAME1.FieldName = "P_NAME";
            this.fieldPNAME1.Name = "fieldPNAME1";
            // 
            // fieldPSIZE1
            // 
            this.fieldPSIZE1.AreaIndex = 17;
            this.fieldPSIZE1.Caption = "Part Size";
            this.fieldPSIZE1.FieldName = "P_SIZE";
            this.fieldPSIZE1.Name = "fieldPSIZE1";
            // 
            // fieldSADDRESS1
            // 
            this.fieldSADDRESS1.AreaIndex = 18;
            this.fieldSADDRESS1.Caption = "Supplier Address";
            this.fieldSADDRESS1.FieldName = "S_ADDRESS";
            this.fieldSADDRESS1.Name = "fieldSADDRESS1";
            // 
            // fieldSNAME1
            // 
            this.fieldSNAME1.AreaIndex = 19;
            this.fieldSNAME1.Caption = "Supplier Name";
            this.fieldSNAME1.FieldName = "S_NAME";
            this.fieldSNAME1.Name = "fieldSNAME1";
            // 
            // fieldSPHONE1
            // 
            this.fieldSPHONE1.AreaIndex = 20;
            this.fieldSPHONE1.Caption = "Supplier Phone";
            this.fieldSPHONE1.FieldName = "S_PHONE";
            this.fieldSPHONE1.Name = "fieldSPHONE1";
            // 
            // accordionControl1
            // 
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(749, 12);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(286, 426);
            this.accordionControl1.StyleController = this.layoutControl1;
            this.accordionControl1.TabIndex = 0;
            this.accordionControl1.Text = "accordionControl1";
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.flowLayoutPanel1);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.accordionControl1);
            this.layoutControl1.Controls.Add(this.pivotGridControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 143);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1244, 264, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1268, 474);
            this.layoutControl1.TabIndex = 8;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(1039, 12);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(217, 426);
            this.panelControl1.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(1268, 474);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.pivotGridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(737, 430);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.accordionControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(737, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(290, 430);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.panelControl1;
            this.layoutControlItem3.Location = new System.Drawing.Point(1027, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(221, 430);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // filteringUIContext1
            // 
            this.filteringUIContext1.Client = this.pivotGridControl1;
            this.filteringUIContext1.Control = this.accordionControl1;
            // 
            // dockManager1
            // 
            this.dockManager1.Controller = this.barAndDockingController1;
            this.dockManager1.Form = this;
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(5, 23);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(199, 386);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(108, 442);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1148, 20);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.flowLayoutPanel1;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 430);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(1248, 24);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(93, 13);
            // 
            // RibbonForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 648);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "RibbonForm1";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "RibbonForm1";
            this.Load += new System.EventHandler(this.RibbonForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linqServerModeSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filteringUIContext1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;
        private DevExpress.Data.Linq.LinqServerModeSource linqServerModeSource1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCADDRESS1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCNAME1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldCPHONE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOCOMMITDATE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOCUSTKEY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLODISCOUNT1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOEXTENDEDPRICE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOLINENUMBER1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOORDERDATE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOORDERKEY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOORDTOTALPRICE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOPARTKEY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOQUANTITY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOREVENUE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOSHIPPRIORITY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOSUPPKEY1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOSUPPLYCOST1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldLOTAX1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldPNAME1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldPSIZE1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldSADDRESS1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldSNAME1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldSPHONE1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.Utils.Filtering.FilteringUIContext filteringUIContext1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}