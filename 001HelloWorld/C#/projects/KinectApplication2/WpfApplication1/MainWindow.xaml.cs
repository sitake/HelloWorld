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

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {


        private readonly int bytePerPixel = (PixelFormats.Bgr32.BitsPerPixel + 7) / 8;

        private KinectSensor Kinect = null;

        private CoordinateMapper coordinateMapper = null;

        private MultiSourceFrameReader multiFrameSourceReader = null;

        private WriteableBitmap bitmap = null;

        private ColorSpacePoint[] DepthPointToColorPoints =  null;

        private FrameDescription depthFrameDescription = null;

        private FrameDescription colorFrameDescription = null;

        private WriteableBitmap depthBitmap = null;

        private ushort[] depthPixels = null;

        private const int MapDepthToByte = 8000 / 256;      




        

        public MainWindow()
        {
            InitializeComponent();

            StartKinect();

        }

        private void StartKinect()
        {

            this.Kinect = KinectSensor.GetDefault();

            if (this.Kinect == null) return;

            this.multiFrameSourceReader = this.Kinect.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth);

            this.multiFrameSourceReader.MultiSourceFrameArrived += this.Reader_MultiSouceFrameArrived;

            this.coordinateMapper = this.Kinect.CoordinateMapper;

            this.depthFrameDescription = this.Kinect.DepthFrameSource.FrameDescription;

            this.colorFrameDescription = this.Kinect.ColorFrameSource.FrameDescription;

            
            this.bitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96, 96, PixelFormats.Bgr32, null);

            this.depthBitmap = new WriteableBitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height, 96, 96, PixelFormats.Gray8, null);

            this.depthPixels = new ushort[this.depthFrameDescription.Width * this.depthFrameDescription.Height];

            this.DepthPointToColorPoints = new ColorSpacePoint[this.depthPixels.Length];

            
            
            this.Kinect.Open();






        }

        private void Reader_MultiSouceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {

            DepthFrame depthFrame = null;
            ColorFrame colorFrame = null;


            MultiSourceFrame multiSourceFrame = e.FrameReference.AcquireFrame();

            if (multiSourceFrame == null)
            {
                return;
            }



            using (colorFrame = multiSourceFrame.ColorFrameReference.AcquireFrame())
            {

                if (colorFrame == null)
                {
                    return;
                }

                using (depthFrame = multiSourceFrame.DepthFrameReference.AcquireFrame())
                {

                    if (depthFrame == null)
                    {
                        return;
                    }

                    this.depthFrameDescription = depthFrame.FrameDescription;
                    CreateDepthBitmap(depthFrame);

                }


                this.colorFrameDescription = colorFrame.FrameDescription;

                CreateColorBitmap(colorFrame);
                                
            }
                          
        }

        private void CreateColorBitmap(ColorFrame colorFrame)
        {

            using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
            {

                this.bitmap.Lock();
                colorFrame.CopyConvertedFrameDataToIntPtr(
                   this.bitmap.BackBuffer,
                   (uint)(this.colorFrameDescription.Width*this.colorFrameDescription.Height*4),
                   ColorImageFormat.Bgra
                   );

                this.bitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorFrameDescription.Width, this.colorFrameDescription.Height));



            }

            this.bitmap.Unlock();
            this.imageRgb.Source = bitmap;



        }

        private void CreateDepthBitmap(DepthFrame depthFrame)
        {


            if (this.depthPixels.Length == (depthFrameDescription.Width * depthFrameDescription.Height))
            {
                depthFrame.CopyFrameDataToArray(this.depthPixels);
                this.ProcessDepthFrameDataArray(this.depthPixels, depthFrame.DepthMinReliableDistance, ushort.MaxValue);
                
            }



        }

        private void ProcessDepthFrameDataArray(ushort[] depthFrameData, ushort minDepth, ushort maxDepth)
        {
           byte[] depthFrameDataArray = new byte[depthFrameData.Length];

           this.coordinateMapper.MapDepthFrameToColorSpace(depthPixels, DepthPointToColorPoints);

            for (int i = 0; i < depthFrameData.Length; i++)
            {
                float colorX = DepthPointToColorPoints[i].X;
                float colorY = DepthPointToColorPoints[i].Y;

                if (float.IsNegativeInfinity(colorX) || float.IsNegativeInfinity(colorY))
                {
                    continue;
                }
                int depthX = (int)(colorX + 0.5f);
                int depthY = (int)(colorY + 0.5f);

                if (depthX >= 0 && depthX < this.depthFrameDescription.Width && depthY >= 0 && depthY < this.depthFrameDescription.Height)
                {

                    int depthIndex = ((depthY * depthFrameDescription.Width) + depthX);




                    depthFrameDataArray[i] = (byte)(depthFrameData[depthIndex] >= minDepth && depthFrameData[depthIndex] <= maxDepth ? (depthFrameData[depthIndex] / MapDepthToByte) : 0);

                }
            }


            this.RenderDepthPixelsArray(depthFrameDataArray);
        }



        private void RenderDepthPixelsArray(byte[] pixels)
        {
            this.depthBitmap.WritePixels(
                new Int32Rect(0, 0, this.depthFrameDescription.Width,
                    this.depthFrameDescription.Height),
                    pixels,
                    this.depthBitmap.PixelWidth,
                    0);

            this.imageDepth.Source = this.depthBitmap;
        }       



        private void Window_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            Kinect.Close();
        }



    }
}
