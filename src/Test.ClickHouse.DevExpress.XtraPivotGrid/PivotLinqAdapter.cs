using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PivotForm
{
    public class PivotLinqAdapter : PivotGridControl
    {
        private bool isFirstQuery = true;

        public bool IsFirstQuery 
        {
            get { return isFirstQuery; }
        }

        protected override void RaiseBeginRefresh()
        {
            isFirstQuery = false;
            base.RaiseBeginRefresh();
        }

        protected override void RaiseEndRefresh()
        {
            base.RaiseEndRefresh();
            isFirstQuery = true;
        }


    }
}
