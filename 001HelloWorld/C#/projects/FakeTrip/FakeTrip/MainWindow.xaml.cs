using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace FakeTrip
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int bytesPerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        private KinectSensor kinect = null;

        private CoordinateMapper coordinateMapper = null;

        private MultiSourceFrameReader multiFrameSourceReader = null;

        private WriteableBitmap colorBitmap = null;

        private FrameDescription colorDSC = null;

        private byte[] colorPixels = null;

        private FrameDescription depthDSC = null;

        private DepthSpacePoint[] depthSpacePoints = null;

        private ushort[] depthFrameData = null;

        





        public MainWindow()
        {
            InitializeComponent();

            StartKinect();
        }


        private void StartKinect()
        {

            this.kinect = KinectSensor.GetDefault();

            if (this.kinect == null) return;

            this.coordinateMapper = this.kinect.CoordinateMapper;

            this.multiFrameSourceReader = this.kinect.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.BodyIndex | FrameSourceTypes.Depth);

            this.colorDSC = this.kinect.ColorFrameSource.FrameDescription;

            this.depthDSC = this.kinect.DepthFrameSource.FrameDescription;

            this.multiFrameSourceReader.MultiSourceFrameArrived += Reader_FrameArrived;

            this.colorPixels = new byte[this.colorDSC.Height * this.colorDSC.Width * this.bytesPerPixel];

            this.depthSpacePoints = new DepthSpacePoint[this.colorDSC.Width * colorDSC.Height];

            this.depthFrameData = new ushort[this.depthDSC.Height * this.depthDSC.Width];

            this.kinect.Open();

            this.colorBitmap = new WriteableBitmap(this.colorDSC.Width, this.colorDSC.Height, 96, 96, PixelFormats.Bgr32, null);





        }

        private void Reader_FrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {


            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame().ColorFrameReference.AcquireFrame())
            {
                if (colorFrame == null)
                {
                    return;
                }

                CreateColorBitmap(colorFrame);

                

            }

            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame().DepthFrameReference.AcquireFrame())
            {
                if (depthFrame == null)
                {
                    return;
                }
                this.depthDSC = depthFrame.FrameDescription;

                depthFrame.CopyFrameDataToArray(depthFrameData);

                coordinateMapper.MapColorFrameToDepthSpace(depthFrameData, this.depthSpacePoints);
            }

            using (BodyIndexFrame bodyIndexFrame = e.FrameReference.AcquireFrame().BodyIndexFrameReference.AcquireFrame())
            {
                if (bodyIndexFrame == null)
                {
                    return;
                }

                MaskColorBitmap(bodyIndexFrame);

            }

            



        }

        private void MaskColorBitmap(BodyIndexFrame bodyIndexFrame)
        {

            byte[] bodyIndexData = new byte[this.depthDSC.Width * this.depthDSC.Height];

            bodyIndexFrame.CopyFrameDataToArray(bodyIndexData);

            Point point;

            for (int i = 0; i < this.depthSpacePoints.Length; i++)
            {

                point = new Point(this.depthSpacePoints[i].X, this.depthSpacePoints[i].Y);
                ResizePoint(ref point);

                int depthX = (int)(point.X + 0.5);
                int depthY = (int)(point.Y + 0.5);
                if (depthX > 0 && depthY > 0 && depthX < depthDSC.Width && depthY < depthDSC.Height)
                {
                    if (bodyIndexData[depthY * depthDSC.Width + depthX] == 0xff)
                    {
                        continue;
                    }
                }
                


            }

            this.colorBitmap.Lock();
            this.colorBitmap.WritePixels(new Int32Rect(0, 0, colorDSC.Width, colorDSC.Height), colorPixels, colorDSC.Width * bytesPerPixel, 0);
            this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, colorDSC.Width, colorDSC.Height));
            this.colorBitmap.Unlock();

            this.imageColor.Source = this.colorBitmap;



        }

        

        

        private void ResizePoint(ref Point point)
        {


            point.X = point.X * (imageColor.ActualWidth / colorDSC.Width);
            point.Y = point.Y * (imageColor.ActualHeight / colorDSC.Height);



        }



        private void CreateColorBitmap(ColorFrame colorFrame)
        {
            this.colorDSC = colorFrame.FrameDescription;

                colorFrame.CopyConvertedFrameDataToArray(colorPixels,ColorImageFormat.Bgra);

        }

    }
}
