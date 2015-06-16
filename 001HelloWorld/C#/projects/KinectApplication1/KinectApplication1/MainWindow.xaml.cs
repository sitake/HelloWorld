using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace KinectApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        private KinectSensor Kinect = null;
        private FrameDescription colorFrameDescription = null;
        private ColorFrameReader CReader = null;
        private WriteableBitmap colorBitmap = null;

        private DepthFrameReader DReader = null;
        private WriteableBitmap depthBitmap = null;
        private FrameDescription depthFrameDescription = null;
        private byte[] depthPixels = null;
        private const int MapDepthToByte = 8000 / 256;


        //private byte[] ColorImagePixelData;
        //private WriteableBitmap ColorImageBitmap;
        //private Int32Rect ColorImageBitmapRect;
        //private int ColorImageStride;


        public MainWindow()
        {
            InitializeComponent();

            StartKinect();

            



        }

        private void StartKinect()
        {

            this.Kinect = KinectSensor.GetDefault();

            if (this.Kinect == null) return;

            this.Kinect.Open();

            this.CReader = Kinect.ColorFrameSource.OpenReader();

            this.DReader= this.Kinect.DepthFrameSource.OpenReader();


            this.colorFrameDescription = this.Kinect.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
            this.depthFrameDescription = this.Kinect.DepthFrameSource.FrameDescription;

            colorBitmap = new WriteableBitmap(colorFrameDescription.Width, colorFrameDescription.Height, 96.0, 96.0, PixelFormats.Bgr32, null);

            this.CReader.FrameArrived+=this.ColorFrameArrived;

            depthBitmap = new WriteableBitmap(this.depthFrameDescription.Width, this.depthFrameDescription.Height,
                96, 96, PixelFormats.Gray8, null);

            this.DReader.FrameArrived += this.DepthFrameArrived;

            this.depthPixels = new byte[this.depthFrameDescription.Width * this.depthFrameDescription.Height];
        }

       

        private void ColorFrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            var frameReference = e.FrameReference;
            using(ColorFrame frame = frameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    CreateBitmapSource(frame);
                }
            }

        }

        private void CreateBitmapSource(ColorFrame frame)
        {

            this.colorFrameDescription = frame.FrameDescription;

            using(KinectBuffer colorBuffer = frame.LockRawImageBuffer()){

                this.colorBitmap.Lock();
                if (colorFrameDescription.Width == this.colorBitmap.PixelWidth && colorFrameDescription.Height == this.colorBitmap.PixelHeight)
                {
                    frame.CopyConvertedFrameDataToIntPtr(
                           
                        this.colorBitmap.BackBuffer,
                        (uint)(colorFrameDescription.Height*colorFrameDescription.Width*4),
                        ColorImageFormat.Bgra


                        );

                    this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, colorFrameDescription.Width, colorFrameDescription.Height));

                }
                this.colorBitmap.Unlock();
                this.imageRGB.Source = this.colorBitmap;
            
            }
        }

        private void DepthFrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            bool depthFrameProcessed = false;

            using (DepthFrame depthFrame = e.FrameReference.AcquireFrame())
            {

                if (depthFrame != null)
                {
                    using (Microsoft.Kinect.KinectBuffer depthBuffer = depthFrame.LockImageBuffer())
                    {
                        if ((this.depthFrameDescription.Width * this.depthFrameDescription.Height) == (depthBuffer.Size / this.depthFrameDescription.BytesPerPixel)&&
                            (this.depthFrameDescription.Width == depthFrame.FrameDescription.Width)&&(this.depthFrameDescription.Height == depthFrame.FrameDescription.Height))
                        {
                            ushort maxDepth = ushort.MaxValue;

                            this.ProcessDepthFrameData(depthBuffer.UnderlyingBuffer, depthBuffer.Size, depthFrame.DepthMinReliableDistance, maxDepth);
                            depthFrameProcessed = true;
                        }
                    }
                }


            }

            if (depthFrameProcessed)
            {
                this.RenderDepthPixels();
            }


        }

        private unsafe void ProcessDepthFrameData(IntPtr depthFrameData, uint depthFrameDataSize, ushort minDepth, ushort maxDepth)
        {

            ushort* frameData = (ushort*)depthFrameData;

            for (int i = 0; i < (int)(depthFrameDataSize / this.depthFrameDescription.BytesPerPixel); ++i)
            {
                ushort depth = frameData[i];

                this.depthPixels[i] = (byte)(depth >= minDepth && depth <= maxDepth ? (depth / MapDepthToByte) : 0);
            }
        }


        private void RenderDepthPixels()
        {


            this.depthBitmap.WritePixels(
                new Int32Rect(0, 0, this.depthFrameDescription.Width,
                    this.depthFrameDescription.Height),
                    this.depthPixels,
                    this.depthBitmap.PixelWidth,
                    0
                    );

            this.imageDepth.Source = this.depthBitmap;

        }



        private  void Window_Closing(object sender,System.ComponentModel.CancelEventArgs e)
        {
            if (this.CReader != null)
            {
                this.CReader.Dispose();
                this.CReader = null;
            }
            if (this.Kinect != null)
            {
                this.Kinect.Close();
                this.Kinect = null;
            }
            
                
        }

        public ImageSource ImageSource
        {
            get
            {
                return colorBitmap;
            }
        }

    }


}
