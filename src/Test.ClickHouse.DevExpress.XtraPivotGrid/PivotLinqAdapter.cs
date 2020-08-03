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
        public event NeedToCountEventHandler RaiseNeedToCountEvent;
        public delegate void NeedToCountEventHandler(object sender, NeedToCountEventArgs args);

    
        private void OnNeedToCountEvent(NeedToCountEventArgs e)
        {
            NeedToCountEventHandler needToCountEventHandler = RaiseNeedToCountEvent;
            if (needToCountEventHandler != null)
            {
                needToCountEventHandler(this, e);
            }
        }

        protected override void RaiseBeginRefresh()
        {
            var needtocountevent = new NeedToCountEventArgs();
            needtocountevent.needToCount = true;
            OnNeedToCountEvent(needtocountevent);

            base.RaiseBeginRefresh();
        }

        protected override void RaiseEndRefresh()
        {        
            base.RaiseEndRefresh();

            var needtocountevent = new NeedToCountEventArgs();
            needtocountevent.needToCount = false;
            OnNeedToCountEvent(needtocountevent);


        }


    }
}
