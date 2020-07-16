namespace PivotForm
{
    partial class FormStarBench
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
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
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
            this.linqServerModeSource1 = new DevExpress.Data.Linq.LinqServerModeSource();
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.linqServerModeSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.DataSource = this.linqServerModeSource1;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(617, 291);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // fieldCADDRESS1
            // 
            this.fieldCADDRESS1.AreaIndex = 0;
            this.fieldCADDRESS1.Caption = "C_ADDRESS";
            this.fieldCADDRESS1.FieldName = "C_ADDRESS";
            this.fieldCADDRESS1.Name = "fieldCADDRESS1";
            // 
            // fieldCNAME1
            // 
            this.fieldCNAME1.AreaIndex = 1;
            this.fieldCNAME1.Caption = "C_NAME";
            this.fieldCNAME1.FieldName = "C_NAME";
            this.fieldCNAME1.Name = "fieldCNAME1";
            // 
            // fieldCPHONE1
            // 
            this.fieldCPHONE1.AreaIndex = 2;
            this.fieldCPHONE1.Caption = "C_PHONE";
            this.fieldCPHONE1.FieldName = "C_PHONE";
            this.fieldCPHONE1.Name = "fieldCPHONE1";
            // 
            // fieldLOCOMMITDATE1
            // 
            this.fieldLOCOMMITDATE1.AreaIndex = 3;
            this.fieldLOCOMMITDATE1.Caption = "LO_COMMITDATE";
            this.fieldLOCOMMITDATE1.FieldName = "LO_COMMITDATE";
            this.fieldLOCOMMITDATE1.Name = "fieldLOCOMMITDATE1";
            // 
            // fieldLOCUSTKEY1
            // 
            this.fieldLOCUSTKEY1.AreaIndex = 4;
            this.fieldLOCUSTKEY1.Caption = "LO_CUSTKEY";
            this.fieldLOCUSTKEY1.FieldName = "LO_CUSTKEY";
            this.fieldLOCUSTKEY1.Name = "fieldLOCUSTKEY1";
            // 
            // fieldLODISCOUNT1
            // 
            this.fieldLODISCOUNT1.AreaIndex = 5;
            this.fieldLODISCOUNT1.Caption = "LO_DISCOUNT";
            this.fieldLODISCOUNT1.FieldName = "LO_DISCOUNT";
            this.fieldLODISCOUNT1.Name = "fieldLODISCOUNT1";
            // 
            // fieldLOEXTENDEDPRICE1
            // 
            this.fieldLOEXTENDEDPRICE1.AreaIndex = 6;
            this.fieldLOEXTENDEDPRICE1.Caption = "LO_EXTENDEDPRICE";
            this.fieldLOEXTENDEDPRICE1.FieldName = "LO_EXTENDEDPRICE";
            this.fieldLOEXTENDEDPRICE1.Name = "fieldLOEXTENDEDPRICE1";
            // 
            // fieldLOLINENUMBER1
            // 
            this.fieldLOLINENUMBER1.AreaIndex = 7;
            this.fieldLOLINENUMBER1.Caption = "LO_LINENUMBER";
            this.fieldLOLINENUMBER1.FieldName = "LO_LINENUMBER";
            this.fieldLOLINENUMBER1.Name = "fieldLOLINENUMBER1";
            // 
            // fieldLOORDERDATE1
            // 
            this.fieldLOORDERDATE1.AreaIndex = 8;
            this.fieldLOORDERDATE1.Caption = "LO_ORDERDATE";
            this.fieldLOORDERDATE1.FieldName = "LO_ORDERDATE";
            this.fieldLOORDERDATE1.Name = "fieldLOORDERDATE1";
            // 
            // fieldLOORDERKEY1
            // 
            this.fieldLOORDERKEY1.AreaIndex = 9;
            this.fieldLOORDERKEY1.Caption = "LO_ORDERKEY";
            this.fieldLOORDERKEY1.FieldName = "LO_ORDERKEY";
            this.fieldLOORDERKEY1.Name = "fieldLOORDERKEY1";
            // 
            // fieldLOORDTOTALPRICE1
            // 
            this.fieldLOORDTOTALPRICE1.AreaIndex = 10;
            this.fieldLOORDTOTALPRICE1.Caption = "LO_ORDTOTALPRICE";
            this.fieldLOORDTOTALPRICE1.FieldName = "LO_ORDTOTALPRICE";
            this.fieldLOORDTOTALPRICE1.Name = "fieldLOORDTOTALPRICE1";
            // 
            // fieldLOPARTKEY1
            // 
            this.fieldLOPARTKEY1.AreaIndex = 11;
            this.fieldLOPARTKEY1.Caption = "LO_PARTKEY";
            this.fieldLOPARTKEY1.FieldName = "LO_PARTKEY";
            this.fieldLOPARTKEY1.Name = "fieldLOPARTKEY1";
            // 
            // fieldLOQUANTITY1
            // 
            this.fieldLOQUANTITY1.AreaIndex = 12;
            this.fieldLOQUANTITY1.Caption = "LO_QUANTITY";
            this.fieldLOQUANTITY1.FieldName = "LO_QUANTITY";
            this.fieldLOQUANTITY1.Name = "fieldLOQUANTITY1";
            // 
            // fieldLOREVENUE1
            // 
            this.fieldLOREVENUE1.AreaIndex = 13;
            this.fieldLOREVENUE1.Caption = "LO_REVENUE";
            this.fieldLOREVENUE1.FieldName = "LO_REVENUE";
            this.fieldLOREVENUE1.Name = "fieldLOREVENUE1";
            // 
            // fieldLOSHIPPRIORITY1
            // 
            this.fieldLOSHIPPRIORITY1.AreaIndex = 14;
            this.fieldLOSHIPPRIORITY1.Caption = "LO_SHIPPRIORITY";
            this.fieldLOSHIPPRIORITY1.FieldName = "LO_SHIPPRIORITY";
            this.fieldLOSHIPPRIORITY1.Name = "fieldLOSHIPPRIORITY1";
            // 
            // fieldLOSUPPKEY1
            // 
            this.fieldLOSUPPKEY1.AreaIndex = 15;
            this.fieldLOSUPPKEY1.Caption = "LO_SUPPKEY";
            this.fieldLOSUPPKEY1.FieldName = "LO_SUPPKEY";
            this.fieldLOSUPPKEY1.Name = "fieldLOSUPPKEY1";
            // 
            // fieldLOSUPPLYCOST1
            // 
            this.fieldLOSUPPLYCOST1.AreaIndex = 16;
            this.fieldLOSUPPLYCOST1.Caption = "LO_SUPPLYCOST";
            this.fieldLOSUPPLYCOST1.FieldName = "LO_SUPPLYCOST";
            this.fieldLOSUPPLYCOST1.Name = "fieldLOSUPPLYCOST1";
            // 
            // fieldLOTAX1
            // 
            this.fieldLOTAX1.AreaIndex = 17;
            this.fieldLOTAX1.Caption = "LO_TAX";
            this.fieldLOTAX1.FieldName = "LO_TAX";
            this.fieldLOTAX1.Name = "fieldLOTAX1";
            // 
            // fieldPNAME1
            // 
            this.fieldPNAME1.AreaIndex = 18;
            this.fieldPNAME1.Caption = "P_NAME";
            this.fieldPNAME1.FieldName = "P_NAME";
            this.fieldPNAME1.Name = "fieldPNAME1";
            // 
            // fieldPSIZE1
            // 
            this.fieldPSIZE1.AreaIndex = 19;
            this.fieldPSIZE1.Caption = "P_SIZE";
            this.fieldPSIZE1.FieldName = "P_SIZE";
            this.fieldPSIZE1.Name = "fieldPSIZE1";
            // 
            // fieldSADDRESS1
            // 
            this.fieldSADDRESS1.AreaIndex = 20;
            this.fieldSADDRESS1.Caption = "S_ADDRESS";
            this.fieldSADDRESS1.FieldName = "S_ADDRESS";
            this.fieldSADDRESS1.Name = "fieldSADDRESS1";
            // 
            // fieldSNAME1
            // 
            this.fieldSNAME1.AreaIndex = 21;
            this.fieldSNAME1.Caption = "S_NAME";
            this.fieldSNAME1.FieldName = "S_NAME";
            this.fieldSNAME1.Name = "fieldSNAME1";
            // 
            // fieldSPHONE1
            // 
            this.fieldSPHONE1.AreaIndex = 22;
            this.fieldSPHONE1.Caption = "S_PHONE";
            this.fieldSPHONE1.FieldName = "S_PHONE";
            this.fieldSPHONE1.Name = "fieldSPHONE1";
            // 
            // linqServerModeSource1
            // 
            this.linqServerModeSource1.ElementType = typeof(Test.lineorder_flat);
            // 
            // FormStarBench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 291);
            this.Controls.Add(this.pivotGridControl1);
            this.Name = "FormStarBench";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.linqServerModeSource1)).EndInit();
            this.ResumeLayout(false);

        }
        private DevExpress.Data.Linq.LinqServerModeSource linqServerModeSource1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;

        #endregion

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
    }
}

