namespace PivotForm
{
    partial class FormNorthwindIQT
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
        private void InitializeComponent() {
            this.pivotGridControl1 = new DevExpress.XtraPivotGrid.PivotGridControl();
            this.linqServerModeSource1 = new DevExpress.Data.Linq.LinqServerModeSource();
            this.fieldOrderID1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldProductID1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldQuantity1 = new DevExpress.XtraPivotGrid.PivotGridField();
            this.fieldUnitPrice1 = new DevExpress.XtraPivotGrid.PivotGridField();
            ((System.ComponentModel.ISupportInitialize) (this.pivotGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize) (this.linqServerModeSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pivotGridControl1
            // 
            this.pivotGridControl1.DataSource = this.linqServerModeSource1;
            this.pivotGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pivotGridControl1.Fields.AddRange(new DevExpress.XtraPivotGrid.PivotGridField[] {this.fieldOrderID1, this.fieldProductID1, this.fieldQuantity1, this.fieldUnitPrice1});
            this.pivotGridControl1.Location = new System.Drawing.Point(0, 0);
            this.pivotGridControl1.Name = "pivotGridControl1";
            this.pivotGridControl1.Size = new System.Drawing.Size(632, 278);
            this.pivotGridControl1.TabIndex = 0;
            // 
            // linqServerModeSource1
            // 
            this.linqServerModeSource1.ElementType = typeof(Test.OrderDetail);
            // 
            // fieldOrderID1
            // 
            this.fieldOrderID1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldOrderID1.AreaIndex = 1;
            this.fieldOrderID1.Caption = "Order ID";
            this.fieldOrderID1.FieldName = "OrderID";
            this.fieldOrderID1.Name = "fieldOrderID1";
            // 
            // fieldProductID1
            // 
            this.fieldProductID1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldProductID1.AreaIndex = 0;
            this.fieldProductID1.Caption = "Product ID";
            this.fieldProductID1.FieldName = "ProductID";
            this.fieldProductID1.Name = "fieldProductID1";
            // 
            // fieldQuantity1
            // 
            this.fieldQuantity1.AreaIndex = 0;
            this.fieldQuantity1.Caption = "Quantity";
            this.fieldQuantity1.FieldName = "Quantity";
            this.fieldQuantity1.Name = "fieldQuantity1";
            // 
            // fieldUnitPrice1
            // 
            this.fieldUnitPrice1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldUnitPrice1.AreaIndex = 0;
            this.fieldUnitPrice1.Caption = "Unit Price";
            this.fieldUnitPrice1.FieldName = "UnitPrice";
            this.fieldUnitPrice1.Name = "fieldUnitPrice1";
            // 
            // FormNorthwindIQT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 278);
            this.Controls.Add(this.pivotGridControl1);
            this.Name = "FormNorthwindIQT";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize) (this.pivotGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize) (this.linqServerModeSource1)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraPivotGrid.PivotGridField fieldOrderID1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldProductID1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldQuantity1;
        private DevExpress.XtraPivotGrid.PivotGridField fieldUnitPrice1;
        private DevExpress.Data.Linq.LinqServerModeSource linqServerModeSource1;
        private DevExpress.XtraPivotGrid.PivotGridControl pivotGridControl1;

        #endregion
    }
}

