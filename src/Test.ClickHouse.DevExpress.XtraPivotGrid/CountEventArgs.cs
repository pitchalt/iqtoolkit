using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotForm
{
    public class CountEventArgs : EventArgs
    {
        public CountEventArgs(int count, bool canUpload = true)
        {
            Count = count;
            CanUpload = canUpload;
        }
        public int Count { get; }
        public bool CanUpload { get; set; }
      //  public bool NeedToCount { get; set; }
    }
}
