using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalayPasilungan
{
    class noBorderTab : System.Windows.Forms.TabControl
    {
        private const int TCM_ADJUSTRECT = 0x1328;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == TCM_ADJUSTRECT && !DesignMode)
            { }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
