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

namespace BodyIndexFrameTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        private KinectSensor kinect = null;

        private MultiSourceFrameReader sourceReader = null;

        private WriteableBitmap bodyBitmap = null;

        private FrameDescription bodyIndexDSC;

        private byte[] bodyIndexbuffer = null;

        private uint[] bodyIndexPixels = null;

        private static readonly uint[] BodyColor =
        {
            0x0000FF00,
            0x00FF0000,
            0xFFFF4000,
            0x40FFFF00,
            0xFF40FF00,
            0xFF808000,
        };



        public MainWindow()
        {
            InitializeComponent();

            StartKinect();

        }


        private void  StartKinect(){
            this.kinect = KinectSensor.GetDefault();

            if (this.kinect == null)
            {
                return;
            }

            

            this.sourceReader = this.kinect.OpenMultiSourceFrameReader(FrameSourceTypes.BodyIndex);
            this.bodyIndexDSC = this.kinect.BodyIndexFrameSource.FrameDescription;

            this.bodyIndexbuffer = new byte[this.bodyIndexDSC.Width * this.bodyIndexDSC.Height];

            this.bodyBitmap = new WriteableBitmap(this.bodyIndexDSC.Width, this.bodyIndexDSC.Height, 96, 96, PixelFormats.Bgr32, null);

            this.sourceReader.MultiSourceFrameArrived += Reader_FrameArrived;

            this.bodyIndexPixels = new uint[this.bodyIndexDSC.Width * this.bodyIndexDSC.Height];


            kinect.Open();            


        }

        private void Reader_FrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            using (BodyIndexFrame biFrame = e.FrameReference.AcquireFrame().BodyIndexFrameReference.AcquireFrame())
            {
                if (biFrame != null)
                {
                    bodyIndexDSC = biFrame.FrameDescription;

                    if (this.bodyIndexDSC.Width * this.bodyIndexDSC.Height == this.bodyIndexbuffer.Length)
                    {
                        CreateBodyIndexBitmap(biFrame);
                        
                    }


                }
            }
            RenderBodyIndexPixels();
        }

        private void RenderBodyIndexPixels()
        {
            this.bodyBitmap.WritePixels(new Int32Rect(0, 0, this.bodyBitmap.PixelWidth, this.bodyBitmap.PixelHeight),
                this.bodyIndexPixels,
                this.bodyBitmap.PixelWidth * 4,
                0);

            this.imageBody.Source = this.bodyBitmap;
        }

        private void CreateBodyIndexBitmap(BodyIndexFrame biFrame)
        {
            biFrame.CopyFrameDataToArray(bodyIndexbuffer);

            for (int i=0; i < bodyIndexbuffer.Length; i++)
            {
                if (bodyIndexbuffer[i] < BodyColor.Length)
                {
                    this.bodyIndexPixels[i] = BodyColor[bodyIndexbuffer[i]];
                }
                else
                {
                    this.bodyIndexPixels[i] = 0;
                }



            }



        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            kinect.Close();
        }












        

    }





}
