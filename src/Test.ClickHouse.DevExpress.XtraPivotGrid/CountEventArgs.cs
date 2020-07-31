using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotForm
{
    public class CountEventArgs : EventArgs
    {
        public CountEventArgs(int Count, bool CanUpload = true)
        {
            count = Count;
            canUpload = CanUpload;
        }
        public int count { get; }
        public bool canUpload { get; set; }
        public bool needToCount { get; set; }
    }
}
