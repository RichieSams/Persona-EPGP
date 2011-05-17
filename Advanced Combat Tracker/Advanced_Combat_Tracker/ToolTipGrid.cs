namespace Advanced_Combat_Tracker
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ToolTipGrid
    {
        private List<ToolTipRect> ttRects = new List<ToolTipRect>();

        public List<int> GetItemIndicesAt(int x, int y)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < this.Items.Count; i++)
            {
                if ((this.Items[i].Active && (this.Items[i].ItemIndex > -1)) && this.Items[i].AreaRect.Contains((float) x, (float) y))
                {
                    list.Add(this.Items[i].ItemIndex);
                }
            }
            return list;
        }

        public string GetToolTipTextAt(int x, int y)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Active && this.Items[i].AreaRect.Contains((float) x, (float) y))
                {
                    builder.AppendLine(this.Items[i].Text);
                }
            }
            return builder.ToString().Trim();
        }

        public List<ToolTipRect> Items
        {
            get
            {
                return this.ttRects;
            }
            set
            {
                this.ttRects = value;
            }
        }
    }
}

