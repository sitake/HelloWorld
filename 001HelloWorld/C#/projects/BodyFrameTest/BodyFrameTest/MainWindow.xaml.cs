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

namespace BodyFrameTest
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {


        private KinectSensor kinect = null;

        private CoordinateMapper coordinateMapper = null;

        private MultiSourceFrameReader multiFrameSourceReader = null;

        private WriteableBitmap colorBitmap = null;



        private FrameDescription colorDSC = null;

        private Body[] bodies = null;

        private List<Tuple<JointType, JointType>> bones;








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

            this.multiFrameSourceReader = this.kinect.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Body);

            this.colorDSC = this.kinect.ColorFrameSource.FrameDescription;

            this.multiFrameSourceReader.MultiSourceFrameArrived += Reader_FrameArrived;

            this.bodies = new Body[this.kinect.BodyFrameSource.BodyCount];




            this.bones = new List<Tuple<JointType, JointType>>();

            // Torso
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            // Right Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // Left Arm
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // Right Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // Left Leg
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));








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

            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame().BodyFrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                {
                    return;
                }


                DrawBodies(bodyFrame);


            }



        }

        private void DrawBodies(BodyFrame bodyFrame)
        {

            bodyFrame.GetAndRefreshBodyData(bodies);

            this.CanvasBody.Children.Clear();

            foreach (Body body in bodies.Where(b => b.IsTracked))
            {

                foreach (var bone in this.bones)
                {
                    this.DrawBone(body.Joints,bone.Item1,bone.Item2);
                }


                foreach (var joint in body.Joints)
                {
                    if (joint.Value.TrackingState == TrackingState.Tracked)
                    {
                        DrawEllipse(joint.Value,10,Brushes.Blue);

                    

                      if (joint.Value.JointType == JointType.HandLeft)
                         {
                          DrawHandState(body.Joints[JointType.HandLeft], body.HandLeftConfidence,body.HandLeftState);
                           }

                      if (joint.Value.JointType == JointType.HandRight)
                          {
                             DrawHandState(body.Joints[JointType.HandRight], body.HandRightConfidence,body.HandRightState);
    
                        }

                    }


                    else if (joint.Value.TrackingState == TrackingState.Inferred)
                    {

                        DrawEllipse(joint.Value, 10, Brushes.Yellow);

                    }

                }


            }

        }

        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, JointType jointType0, JointType jointType1)
        {

            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            if(joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            var colorPoint0 = coordinateMapper.MapCameraPointToColorSpace(joint0.Position);
            var colorPoint1 = coordinateMapper.MapCameraPointToColorSpace(joint1.Position);

            if (colorPoint0.X < 0 || colorPoint0.Y < 0 || colorPoint1.X < 0 || colorPoint1.Y < 0)
            {
                return;
            }

            Point point0 = new Point(colorPoint0.X, colorPoint0.Y);
            Point point1 = new Point(colorPoint1.X, colorPoint1.Y);

            ResizePoint(ref point0);
            ResizePoint(ref point1);


            Line line = new Line()
            {
                X1 = point0.X,
                X2 = point1.X,
                Y1 = point0.Y,
                Y2 = point1.Y,
                Stroke = new SolidColorBrush(new Color(){

                    R = 255,
                    G = 64,
                    B = 64,
                    A = 128

                }),
                StrokeThickness = 7,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round

            };

            CanvasBody.Children.Add(line);


        }

        private void DrawHandState(Joint joint, TrackingConfidence trackingConfidence,HandState handState)
        {

            if (trackingConfidence != TrackingConfidence.High)
            {
                return;
            }


            switch (handState)
            {
                case HandState.Open:

                    DrawEllipse(joint, 40, new SolidColorBrush(new Color()
                    {
                        R = 255,
                        G = 255,
                        A = 128
                    }));
                    
                    break;
                case HandState.Closed:

                    DrawEllipse(joint, 40, new SolidColorBrush(new Color()
                    {
                        R = 255,
                        B = 255,
                        A = 128
                    }));

                    break;
                case HandState.Lasso:

                    DrawEllipse(joint, 40, new SolidColorBrush(new Color()
                    {
                        G = 255,
                        B = 255,
                        A = 128
                    }));
                    
                    break;
            }


        }

        private void DrawEllipse(Joint joint, int r, Brush brush)
        {
            var ellipse = new Ellipse()
            {
                Width = r,
                Height = r,
                Fill = brush
                
            };

            ColorSpacePoint colorPoint = this.coordinateMapper.MapCameraPointToColorSpace(joint.Position);

            Point point = new Point();

            point.X = colorPoint.X;
            point.Y = colorPoint.Y;

            
            
            

            


            if ((point.X < 0) || (point.Y < 0))
            {
                return;
            }

            this.ResizePoint(ref point);

            Canvas.SetLeft(ellipse, point.X - (r / 2));
            Canvas.SetTop(ellipse,point.Y - (r/2));

            CanvasBody.Children.Add(ellipse);


        }

        private void ResizePoint(ref Point point)
        {


            point.X = point.X * (imageColor.ActualWidth / colorDSC.Width);
            point.Y = point.Y * (imageColor.ActualHeight / colorDSC.Height);



        }



        private void CreateColorBitmap(ColorFrame colorFrame)
        {
            this.colorDSC = colorFrame.FrameDescription;

            using (KinectBuffer colorBuffer = colorFrame.LockRawImageBuffer())
            {

                this.colorBitmap.Lock();
                colorFrame.CopyConvertedFrameDataToIntPtr(
                   this.colorBitmap.BackBuffer,
                   (uint)(this.colorDSC.Width * this.colorDSC.Height * 4),
                   ColorImageFormat.Bgra
                   );

                this.colorBitmap.AddDirtyRect(new Int32Rect(0, 0, this.colorDSC.Width, this.colorDSC.Height));



            }

            this.colorBitmap.Unlock();
            this.imageColor.Source = colorBitmap;



        }

    }
}
