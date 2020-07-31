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
        protected override PivotGridViewInfoData CreateData()
        {
            return base.CreateData();
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            base.OnGiveFeedback(gfbevent);
        }
        protected override void HookOnLoaded()
        {
            base.HookOnLoaded();
        }

        protected override void OnBindingContextChanged(EventArgs e)
        {
            base.OnBindingContextChanged(e);
        }

        protected override void RaiseExportStarted()
        {
            base.RaiseExportStarted();
        }

        protected override void RaiseBeginRefresh()
        {
            base.RaiseBeginRefresh();
        }

    }
}
