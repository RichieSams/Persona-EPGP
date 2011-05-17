namespace Advanced_Combat_Tracker
{
    using GammaJul.LgLcd;
    using System;
    using System.Drawing;

    public class LcdUpdateEventArgs : EventArgs
    {
        private int bitmapYOffset;
        private Bitmap lcdBitmap;
        public LcdDeviceType LcdType = LcdDeviceType.Monochrome;

        public void SetLcdBitmapView(Bitmap bmp, int yOffset)
        {
            if (bmp.Width == 160)
            {
                if (bmp.Height < 0x2b)
                {
                    throw new ArgumentException("The supplied bitmap is not 43px or greater height.");
                }
                if ((bmp.Height - yOffset) < 0x2b)
                {
                    throw new ArgumentException("The Y-Offset is too great for this bitmap's height.  The resulting image must be 160x43.");
                }
                if (yOffset < 0)
                {
                    throw new ArgumentException("The Y-Offset cannot be negative.");
                }
            }
            else
            {
                if (bmp.Height < 240)
                {
                    throw new ArgumentException("The supplied bitmap is not 240px or greater height.");
                }
                if ((bmp.Height - yOffset) < 240)
                {
                    throw new ArgumentException("The Y-Offset is too great for this bitmap's height.  The resulting image must be 320x240.");
                }
                if (yOffset < 0)
                {
                    throw new ArgumentException("The Y-Offset cannot be negative.");
                }
            }
            this.lcdBitmap = bmp;
            this.bitmapYOffset = yOffset;
        }

        public int BitmapYOffset
        {
            get
            {
                return this.bitmapYOffset;
            }
        }

        public Bitmap LcdBitmap
        {
            get
            {
                return this.lcdBitmap;
            }
        }
    }
}

