using System.Drawing;
using System.Windows.Forms;

namespace qPlanner.UserControls
{
    public partial class cboColored : ComboBox
    {
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.MaxDropDownItems = 100;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            e.DrawBackground();
            if (e.Index >= 0)
            {
                object current = this.Items[e.Index];
                using (Brush brush = new SolidBrush(e.ForeColor))
                {
                    e.Graphics.DrawString(current.ToString(), e.Font, brush, e.Bounds.Left + 2, e.Bounds.Top);
                }
                e.DrawFocusRectangle();
            }

        }
    }
}