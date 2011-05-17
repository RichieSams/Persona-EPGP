namespace Advanced_Combat_Tracker
{
    using System;
    using System.Drawing;

    public class ToolTipRect
    {
        private bool active;
        private int itemIndex;
        private RectangleF rect;
        private string text;

        public ToolTipRect(int ItemIndex, string ToolTipText, float x, float y, float w, float h)
        {
            this.itemIndex = ItemIndex;
            this.text = ToolTipText;
            this.rect = new RectangleF(x, y, w, h);
            this.active = true;
        }

        public bool Active
        {
            get
            {
                return this.active;
            }
            set
            {
                this.active = value;
            }
        }

        public RectangleF AreaRect
        {
            get
            {
                return this.rect;
            }
            set
            {
                this.rect = value;
            }
        }

        public int ItemIndex
        {
            get
            {
                return this.itemIndex;
            }
            set
            {
                this.itemIndex = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
    }
}

