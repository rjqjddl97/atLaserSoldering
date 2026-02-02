using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager
{
    public class Blob
    {
        int _label;
        int _left;
        int _top;
        int _width;
        int _height;
        int _area;

        float _centerx;
        float _centery;

        int _pixelPeakxindex;
        int _pixelPeakyindex;
        int _pixelPeak;

        public int Label
        {
            set { _label = value; }
            get { return _label; }
        }

        public int Left
        {
            set { _left = value; }
            get { return _left; }
        }

        public int Top
        {
            set { _top = value; }
            get { return _top; }
        }

        public int Width
        {
            set { _width = value; }
            get { return _width; }
        }

        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        public float CenterX
        {
            set { _centerx = value; }
            get { return _centerx; }
        }

        public float CenterY
        {
            set { _centery = value; }
            get { return _centery; }
        }

        public int Area
        {
            set { _area = value; }
            get { return _area; }
        }
        public int PixelPeakXIndex
        {
            set { _pixelPeakxindex = value; }
            get { return _pixelPeakxindex; }
        }
        public int PixelPeakYIndex
        {
            set { _pixelPeakyindex = value; }
            get { return _pixelPeakyindex; }
        }
        public int PixelPeak
        {
            set { _pixelPeak = value; }
            get { return _pixelPeak; }
        }
        public Blob()
        {

        }

        public Blob(int label, int left, int top, int width, int height, int area, float centerx, float centery)
        {
            _label = label;
            _left = left;
            _top = top;
            _width = width;
            _height = height;
            _area = area;
            _centerx = centerx;
            _centery = centery;
        }
        public Blob(int label, int left, int top, int width, int height, int area, float centerx, float centery,int peakindexx,int peakindexy,int peak)
        {
            _label = label;
            _left = left;
            _top = top;
            _width = width;
            _height = height;
            _area = area;
            _centerx = centerx;
            _centery = centery;
            _pixelPeakxindex = peakindexx;
            _pixelPeakyindex = peakindexy;
            _pixelPeak = peak;
        }
    }

    public class AreaCompare : IComparer<Blob>
    {
        public int Compare(Blob a, Blob b)
        {
            if (a.Area < b.Area)
                return 1;
            else if (a.Area > b.Area)
                return -1;
            else
                return 0;
        }
    }

    public class LeftCompare : IComparer<Blob>
    {
        public int Compare(Blob a, Blob b)
        {
            if (a.CenterX > b.CenterX)
                return 1;
            else if (a.CenterX < b.CenterX)
                return -1;
            else
                return 0;
        }
    }

    public class RightCompare : IComparer<Blob>
    {
        public int Compare(Blob a, Blob b)
        {
            if (a.CenterX < b.CenterX)
                return 1;
            else if (a.CenterX > b.CenterX)
                return -1;
            else
                return 0;
        }
    }

    public class TopCompare : IComparer<Blob>
    {
        public int Compare(Blob a, Blob b)
        {
            if (a.CenterY > b.CenterY)
                return 1;
            else if (a.CenterY < b.CenterY)
                return -1;
            else
                return 0;
        }
    }

    public class BottomCompare : IComparer<Blob>
    {
        public int Compare(Blob a, Blob b)
        {
            if(a.CenterY < b.CenterY)
                return 1;
            else if (a.CenterY > b.CenterY)
                return -1;
            else
                return 0;
        }
    }

}
