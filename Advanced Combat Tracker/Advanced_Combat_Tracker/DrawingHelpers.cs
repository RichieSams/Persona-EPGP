namespace Advanced_Combat_Tracker
{
    using System;
    using System.Drawing;

    public static class DrawingHelpers
    {
        public static double ATan(double x, double y)
        {
            return R2D(Math.Atan2(y, x));
        }

        public static double Cos(double Degrees)
        {
            return Math.Cos(D2R(Degrees));
        }

        public static double D2R(double Degrees)
        {
            return ((Degrees / 360.0) * 6.2831853071795862);
        }

        public static PointF[] GetArrowPolygon(PointF Dest, float SourceWidth, PointF Source)
        {
            return GetArrowPolygon(Dest.X, Dest.Y, SourceWidth, Source.X, Source.Y);
        }

        public static PointF[] GetArrowPolygon(float DestX, float DestY, float SourceWidth, float SourceX, float SourceY)
        {
            float num = SourceX - DestX;
            float num2 = SourceY - DestY;
            float num3 = (float) Math.Sqrt(Math.Pow((double) num, 2.0) + Math.Pow((double) num2, 2.0));
            float num4 = ((float) ATan((double) num, (double) num2)) - 90f;
            float num5 = (((float) Sin((double) (num4 - 90f))) * SourceWidth) / 2f;
            float num6 = (((float) Cos((double) (num4 - 90f))) * SourceWidth) / 2f;
            float num7 = ((float) Sin((double) num4)) * num3;
            float num8 = ((float) Cos((double) num4)) * num3;
            float num9 = (((float) Sin((double) (num4 + 90f))) * SourceWidth) / 2f;
            float num10 = (((float) Cos((double) (num4 + 90f))) * SourceWidth) / 2f;
            return new PointF[] { new PointF(SourceX + num5, SourceY - num6), new PointF(SourceX + num7, SourceY - num8), new PointF(SourceX + num9, SourceY - num10) };
        }

        public static PointF GetRadialLocation(float Angle, float Distance, PointF Source)
        {
            return GetRadialLocation(Angle, Distance, Source.X, Source.Y);
        }

        public static PointF GetRadialLocation(float Angle, float Distance, float SourceX, float SourceY)
        {
            float num = ((float) Sin((double) Angle)) * Distance;
            float num2 = ((float) Cos((double) Angle)) * Distance;
            return new PointF(SourceX + num, SourceY - num2);
        }

        public static double R2D(double Radians)
        {
            return ((Radians / 6.2831853071795862) * 360.0);
        }

        public static int RectCx(Rectangle rect)
        {
            return (rect.Left + (rect.Width / 2));
        }

        public static float RectCx(RectangleF rect)
        {
            return (rect.Left + (rect.Width / 2f));
        }

        public static int RectCy(Rectangle rect)
        {
            return (rect.Top + (rect.Height / 2));
        }

        public static float RectCy(RectangleF rect)
        {
            return (rect.Top + (rect.Height / 2f));
        }

        public static double Sin(double Degrees)
        {
            return Math.Sin(D2R(Degrees));
        }
    }
}

