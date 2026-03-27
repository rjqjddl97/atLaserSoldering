using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Basler.Pylon;
using LogLibrary;

namespace Basler
{
    public class Utils
    {
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static void CrossMarkPosition(PointF origin, out PointF x1, out PointF x2, out PointF y1, out PointF y2, float scale, float hScroll, float vScroll, float crossMarkOffset)
        {
            // Align Mark 1
            x1 = new PointF((float)(origin.X * scale - hScroll - crossMarkOffset),
                 (float)(origin.Y * scale - vScroll));

            x2 = new PointF((float)(origin.X * scale - hScroll + crossMarkOffset + 1),
                 (float)(origin.Y * scale - vScroll));

            y1 = new PointF((float)(origin.X * scale - hScroll),
                 (float)(origin.Y * scale - vScroll - crossMarkOffset));

            y2 = new PointF((float)(origin.X * scale - hScroll),
                 (float)(origin.Y * scale - vScroll + crossMarkOffset + 1));
        }

        public static PointF PointRealToDraw(PointF fptReal, float fScale, float fHScroll, float fVScroll)
        {
            PointF fptConvert = new PointF();

            fptConvert.X = fptReal.X * fScale - fHScroll;
            fptConvert.Y = fptReal.Y * fScale - fVScroll;

            return fptConvert;
        }

        public static PointF PointDrawToReal(PointF fptPaint, float fScale, float fHScroll, float fVScroll)
        {
            PointF fptConvert = new PointF();

            fptConvert.X = (fptPaint.X + fHScroll) / fScale;
            fptConvert.Y = (fptPaint.Y + fVScroll) / fScale;

            return fptConvert;
        }

        public static RectangleF RectRealToDraw(RectangleF rtReal, float fScale, float fHScroll, float fVScroll)
        {
            float left, top, right, bottom;

            left = rtReal.Left * fScale - fHScroll;
            top = rtReal.Top * fScale - fVScroll;
            right = rtReal.Right * fScale - fHScroll;
            bottom = rtReal.Bottom * fScale - fVScroll;

            return new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }

        public static RectangleF RectDrawToReal(RectangleF rtReal, float fScale, float fHScroll, float fVScroll)
        {
            float left, top, right, bottom;

            left = (rtReal.Left + fHScroll) / fScale;
            top = (rtReal.Top + fVScroll) / fScale;
            right = (rtReal.Right + fHScroll) / fScale;
            bottom = (rtReal.Bottom + fVScroll) / fScale;

            return new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }

        public static RectangleF RectRealToDraw(PointF fptStart, PointF fptEnd, float fScale, float fHScroll, float fVScroll)
        {
            float left, top, right, bottom;

            left = fptStart.X * fScale - fHScroll;
            top = fptStart.Y * fScale - fVScroll;
            right = fptEnd.X * fScale - fHScroll;
            bottom = fptEnd.Y * fScale - fVScroll;

            if (left > right)
            {
                float temp = right;
                right = left;
                left = temp;
            }

            if (top > bottom)
            {
                float temp = bottom;

                bottom = top;
                top = temp;
            }

            return new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }

        public static RectangleF RectDrawToReal(PointF fptStart, PointF fptEnd, float fScale, float fHScroll, float fVScroll)
        {
            float left, top, right, bottom;

            left = (fptStart.X + fHScroll) / fScale;
            top = (fptStart.Y + fVScroll) / fScale;
            right = (fptEnd.X + fHScroll) / fScale;
            bottom = (fptEnd.Y + fVScroll) / fScale;

            if (left > right)
            {
                float temp = right;
                right = left;
                left = temp;
            }

            if (top > bottom)
            {
                float temp = bottom;

                bottom = top;
                top = temp;
            }

            return new RectangleF(left, top, Math.Abs(right - left), Math.Abs(bottom - top));
        }

        public static void SaveImage(Bitmap tempImage, RectangleF rtCrop, string strFilePath)
        {
            if (tempImage != null)
            {
                //Mat src = OpenCvSharp.Extensions.BitmapConverter.ToMat(Utils.Clone<Bitmap>(tempImage));
                //Rect rect = new Rect((int)rtCrop.Left, (int)rtCrop.Top, (int)rtCrop.Width, (int)rtCrop.Height);

                //Mat subRegion = src.SubMat(rect);

                //subRegion.SaveImage(strFilePath);

                //if (src != null)
                //    src.Dispose();

                //if (subRegion != null)
                //    src.Dispose();
            }
        }
    }
}
