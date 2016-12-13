using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace LEDCatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            poort.Open();
            NMCTTimer.Tick += Timer_Tick;
            NMCTTimer.Interval = new TimeSpan(0, 0, 1);
            NMCTTimer.Start();

            puntje = rand.Next(192, 384);
            puntje = rand.Next(1, 3) == 1 ? rand.Next(0, 64) : rand.Next(192, 384);
        }
        private int counter = 0;
        private void ToonNMCT()
        {
            List<int> array = new List<int>();
            switch (counter)
            {
                //N
                case 0:
                    array.AddRange(LetterN);
                    for (int n = 0; n < LetterN.Length; n++)
                    {

                        array.Add(LetterN[n] + 8);
                    }
                    MakeArray(array);
                    counter++;
                    break;
                //M
                case 1:
                    array.AddRange(LetterM);
                    for (int n = 0; n < LetterM.Length; n++)
                    {

                        array.Add(LetterM[n] + 1);
                    }
                    MakeArray(array);
                    counter++;
                    break;
                //C
                case 2:
                    array.AddRange(LetterC);
                    for (int n = 0; n < LetterC.Length; n++)
                    {

                        array.Add(LetterC[n] - 8);
                    }
                    MakeArray(array);
                    counter++;
                    break;
                //T
                case 3:
                    array.AddRange(LetterT);
                    for (int n = 0; n < LetterT.Length; n++)
                    {

                        array.Add(LetterT[n] - 1);
                    }
                    MakeArray(array);
                    counter = 0;
                    break;
            }
            array.Clear();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            ToonNMCT();
        }
        #region poort en timer
        private SerialPort poort = new SerialPort("COM5", 38400);
        private DispatcherTimer NMCTTimer = new DispatcherTimer();
        private DispatcherTimer GameTimer = new DispatcherTimer();
        #endregion
        #region NMCT
        private int[] ventje = { 3, 5, 258, 262, 67, 69, 132, 196, 259, 260, 261, 324, 387, 389, 452, 600 };

        private int[] LetterN = { 1, 2, 5, 6, 65, 66, 69, 70, 129, 130, 131, 133, 134, 193, 194, 195, 196, 197, 198, 257, 258, 260, 261, 262, 321, 322, 325, 326, 385, 386, 389, 390 };
        private int[] LetterM = { 8, 16, 40, 48, 72, 80, 104, 112, 136, 144, 168, 176, 200, 208, 216, 224, 232, 240, 264, 272, 280, 288, 296, 304, 328, 336, 360, 368, 392, 432 };
        private int[] LetterC = { 57, 58, 59, 60, 61, 62, 121, 122, 123, 124, 125, 126, 185, 186, 249, 250, 313, 314, 377, 378, 379, 380, 381, 382, 441, 442, 443, 444, 445, 446, };
        private int[] LetterT = { 31, 39, 95, 103, 159, 167, 223, 231, 287, 295, 335, 343, 351, 359, 367, 375, 399, 407, 415, 423, 431, 439 };
        #endregion
        #region Vlakken
        private int[] AlleVlakken = { 0, 1, 2, 3, 4, 5, 6, 7, 64, 65, 66, 67, 68, 69, 70, 71, 128, 129, 130, 131, 132, 133, 134, 135, 192, 193, 194, 195, 196, 197, 198, 199, 256, 257, 258, 259, 260, 261, 262, 263, 320, 321, 322, 323, 324, 325, 326, 327, 384, 385, 386, 387, 388, 389, 390, 391, 448, 449, 450, 451, 452, 453, 454, 455, 0, 64, 128, 192, 256, 320, 384, 448, 7, 71, 135, 199, 263, 327, 391, 455 };
        private List<int> Voor = new List<int>() { 0,1,2,3,4,5,6,7,64,65,66,67,68,69,70,71,128,129,130,131,132,133,134,135,192,193,
                194,195,196,197,198,199,256,257,258,259,260,261,262,263,320,321,322,323,324,325,326,327,384,385,386,387,388,389,390,391,448,449,450,451,452,453,454,455};
        private List<int> Rechts = new List<int>() { 0,8,16,24,32,40,48,56,64,72,80,88,96,104,112,120,128,136,144,152,160,168,176,184,
                192,200,208,216,224,232,240,248,256,264,272,280,288,296,304,312,320,328,336,344,352,360,368,376,384,392,400,408,416,424,432,440,448,456,464,472,480,488,496,504};
        private List<int> Achter = new List<int> {56,57,58,59,60,61,62,63,120,121,122,123,124,125,126,127,184,185,186,187,188,189,190,
                191,248,249,250,251,252,253,254,255,312,313,314,315,316,317,318,319,376,377,378,379,380,381,382,383,440,441,442,443,444,445,446,447,504,505,506,507,508,509,510,511};
        private List<int> Links = new List<int> {7,15,23,31,39,47,55,63,71,79,87,95,103,111,119,127,135,143,151,159,167,175,183,191,
                199,207,215,223,231,239,247,255,263,271,279,287,295,303,311,319,327,335,343,351,359,367,375,383,391,399,407,415,423,431,439,447,455,463,471,479,487,495,503,511};

        #endregion
        #region kinect var
        private float CurrentPositionZ = 0;
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));
        private const float RenderWidth = 640.0f;
        private const float RenderHeight = 480.0f;
        private KinectSensor sensor;
        
        #endregion
        #region FunctieOmDataTeSturenNaarLEDCube
        private void MakeArray(List<int> data)
        {

            int aantal = 0;
            byte[,] array = new byte[8, 8];
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    byte row = 0;
                    for (int z = 0; z < 8; z++)
                    {
                        if (data.Contains(aantal))
                        {
                            row |= (byte)((byte)1 << z);
                        }
                        else
                        {
                            row |= (byte)((byte)0 << z);
                        }

                        aantal++;
                    }
                    array[x, y] = row;
                }
            }
            WriteFrame(array);
        }
        private void WriteFrame(byte[,] buffer)
        {
            List<byte> flatbuffer = new List<byte>();
            flatbuffer.Add(0xff);
            flatbuffer.Add(0x00);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (buffer[x, y] == 0xff)
                        flatbuffer.Add(buffer[x, y]);
                    flatbuffer.Add(buffer[x, y]);
                }
            }

            poort.Write(flatbuffer.ToArray(), 0, flatbuffer.Count);
        }
        #endregion
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
          
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
            
                this.sensor.SkeletonStream.Enable();
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }



                if (skeletons.Length != 0)
                {
                    foreach(Skeleton skel in skeletons)
                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                        Thread.Sleep(1000);
                            DrawBonesAndJoints(skel);
                        }                
                }
            
        }
        #region functies ventje
        private bool rbeenstrek = false;
        private bool lbeenstrek = false;
        private int linkerarm = 0;
        private int rechterarm = 0;
        private void GaNaarAchteren()
        {
            List<int> newVentje = new List<int>();
            for (int i = 0; i < ventje.Length; i++)
            {
                if (Voor.Contains(ventje[i]))
                {
                    newVentje.Add(ventje[i]);
                }

                else
                {
                    if (i != 15)
                        newVentje.Add(ventje[i] - 8);
                    else
                        newVentje.Add(ventje[i]);

                }

            }
            ventje = newVentje.ToArray();
            MakeArray(newVentje);
        }
        private void GaNaarVoren()
        {
            List<int> newVentje = new List<int>();
            for (int i = 0; i < ventje.Length; i++)
            {
                if (Achter.Contains(ventje[i]))
                {
                    newVentje.Add(ventje[i]);
                }


                else
                {
                    if (i != 15)
                        newVentje.Add(ventje[i] + 8);
                    else
                        newVentje.Add(ventje[i]);
                }

            }
            ventje = newVentje.ToArray();
            MakeArray(newVentje);
        }
        private void GaNaarLinks()
        {
            bool einde = true;
            List<int> newVentje = new List<int>();
            for (int i = 0; i < ventje.Length; i++)
            {
                if (i != 15)
                {
                    if (Links.Contains(ventje[i]))
                        einde = false;
                }

            }
            for (int s = 0; s < ventje.Length; s++)
            {
                if (einde)
                {
                    if (s != 15)
                        newVentje.Add(ventje[s] + 1);
                    else
                        newVentje.Add(ventje[s]);
                }
                else
                {
                    newVentje.Add(ventje[s]);
                }
            }
            ventje = newVentje.ToArray();
            MakeArray(newVentje);

        }
        private void GaNaarRechts()
        {
            bool einde = true;
            List<int> newVentje = new List<int>();
            for (int i = 0; i < ventje.Length; i++)
            {
                if (i != 15)
                {
                    if (Rechts.Contains(ventje[i]))
                        einde = false;
                }

            }
            for (int s = 0; s < ventje.Length; s++)
            {
                if (einde)
                {
                    if (s != 15)
                        newVentje.Add(ventje[s] - 1);
                    else
                        newVentje.Add(ventje[s]);
                }
                else
                {
                    newVentje.Add(ventje[s]);
                }
            }
            ventje = newVentje.ToArray();
            MakeArray(newVentje);
        }
        private void StrekRechterBeen()
        {
            if (!rbeenstrek)
            {
                ventje[0] = ventje[0] - 1;
                MakeArray(ventje.ToList<int>());
                rbeenstrek = !rbeenstrek;
            }
        }
        private void StrekLinkerBeen()
        {
            if (!lbeenstrek)
            {
                ventje[1] = ventje[1] + 1;
                MakeArray(ventje.ToList<int>());
                lbeenstrek = !lbeenstrek;
            }
        }
        private void RechterBeenTerug()
        {
            if (rbeenstrek)
            {
                ventje[0] = ventje[0] + 1;
                MakeArray(ventje.ToList<int>());
                rbeenstrek = !rbeenstrek;
            }
        }
        private void LinkerBeenTerug()
        {
            if (lbeenstrek)
            {
                ventje[1] = ventje[1] - 1;
                MakeArray(ventje.ToList<int>());
                lbeenstrek = !lbeenstrek;
            }
        }
        private void RechterArmOp()
        {
            if (rechterarm == 0)
            {
                ventje[2] = ventje[2] + 64;
                MakeArray(ventje.ToList<int>());
                rechterarm = 1;
            }
            
        }
        private void RechterArmBeneden()
        {
            if(rechterarm ==0)
            {
                ventje[2] = ventje[2] - 64;
                MakeArray(ventje.ToList<int>());
                rechterarm = -1;
            }
                

        }
        private void LinkerArmOp()
        {
            if(linkerarm ==0)
            {
                ventje[3] = ventje[3] + 64;
                MakeArray(ventje.ToList<int>());
                linkerarm = 1;
            }              
            
        }
        private void LinkerArmBeneden()
        {
            if (linkerarm == 0)
            {
                ventje[3] = ventje[3] - 64;
                MakeArray(ventje.ToList<int>());
                linkerarm = -1;
            }
                
        }
        private void LinkerArmGewoon()
        {
            if(linkerarm == 1)
            {
                ventje[3] = ventje[3] - 64;
                MakeArray(ventje.ToList<int>());
                linkerarm = 0;
            }
            if(linkerarm == -1)
            {
                ventje[3] = ventje[3] + 64;
                MakeArray(ventje.ToList<int>());
                linkerarm = 0;
            }
            
        }
        private void RechterArmGewoon()
        {
            if (rechterarm == 1)
            {
                ventje[2] = ventje[2] - 64;
                MakeArray(ventje.ToList<int>());
                rechterarm = 0;
            }
            if (rechterarm == -1)
            {
                ventje[2] = ventje[2] + 64;
                MakeArray(ventje.ToList<int>());
                rechterarm = 0;
            }
            
        }
        private void CheckPositieVoorAchter(int positie)
        {
            if(PositieVoorAchter < positie)
            {
                GaNaarAchteren();
            }
            else if(PositieVoorAchter > positie)
            {
                GaNaarVoren();
            }
            PositieVoorAchter= positie;
        }
        #endregion
        private Random rand = new Random();
        private int puntje;
        bool puntaan = true;
        private int score = 0;


        private int PositieLinksRechts = 1;
        private int PositieVoorAchter;
        private void DrawBonesAndJoints(Skeleton skeleton)
        {


            bool test = true;
            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                //Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    //drawBrush = this.trackedJointBrush;
                    NMCTTimer.Stop();
                    //PuntjeTimer.Start();

                  

                    if (test)
                    {
                        Joint rightankle = skeleton.Joints[JointType.AnkleRight];
                        Joint leftankle = skeleton.Joints[JointType.AnkleLeft];
                        Joint righthand = skeleton.Joints[JointType.HandRight];
                        Joint lefthand = skeleton.Joints[JointType.HandLeft];
                        Joint spine = skeleton.Joints[JointType.Spine];
                        #region voet
                        //if (Math.Round(rightankle.Position.X, 1) >= 0.4)
                        //{
                        //    Console.WriteLine("RIGHT ANKLE: " + Math.Round(rightankle.Position.X, 1));
                        //    StrekRechterBeen();
                        //}
                        //else if (Math.Round(leftankle.Position.X, 1) <= -0.6)
                        //{
                        //    Console.WriteLine("LEFT ANKLE: " + leftankle.Position.X);
                        //    StrekLinkerBeen();
                        //}
                        //else
                        //{
                        //    RechterBeenTerug();
                        //    LinkerBeenTerug();
                        //}
                        #endregion
                        // X IS WANNEER LINKS OF RECHTS
                        // Y IS WANNEER ONDER OF BOVEN
                        // Z IS WANNEER ACHTER OF VOOR
                        
                        #region Arm
                        if (Math.Round(righthand.Position.Y,1) > 0.4 && rechterarm == 0)
                        {
                            Console.WriteLine("HAND OMHOOG");
                            RechterArmOp();
                        }

                        else if ((Math.Round(righthand.Position.Y,1) > 0 || Math.Round(righthand.Position.Y,1) < 0.3)&&(rechterarm==-1 || rechterarm==1))
                        {
                            Console.WriteLine("HAND INT MIDDEN");
                            RechterArmGewoon();
                        }
                        else if (Math.Round(righthand.Position.Y,1) < -0.2 && rechterarm == 0)
                        {
                            Console.WriteLine("HAND BENEDEN");
                            RechterArmBeneden();
                        }

                        if (Math.Round(lefthand.Position.Y,1) > 0.4 && linkerarm == 0)
                        {
                            LinkerArmOp();
                        }
                        else if ((Math.Round(lefthand.Position.Y,1) > 0|| Math.Round(lefthand.Position.Y,1) < 0.3)&&(linkerarm==-1 || linkerarm==1))
                        {
                   
                            LinkerArmGewoon();

                        }
                        else if (Math.Round(lefthand.Position.Y,1) < -0.2 && linkerarm == 0)
                        {
                            LinkerArmBeneden();
                        }

                        #endregion
                        // VOORUIT ACHTERUIT

                        #region Vooruit Achteruit
                        if (spine.Position.Z > 3.4)
                        {
                            Console.WriteLine("Lijn 8 - Hier sta ik achteraan - ");
                            CheckPositieVoorAchter(8);
                        }
                        else if (spine.Position.Z > 3.2 && spine.Position.Z < 3.4)
                        {
                            Console.WriteLine("Lijn 7");
                            CheckPositieVoorAchter(7);

                        }
                        else if (spine.Position.Z > 3.0 && spine.Position.Z < 3.2)
                        {
                            Console.WriteLine("Lijn 6");
                            CheckPositieVoorAchter(6);

                        }
                        else if (spine.Position.Z > 2.8 && spine.Position.Z < 3.0)
                        {
                            Console.WriteLine("Lijn 5");
                            CheckPositieVoorAchter(5);

                        }
                        else if (spine.Position.Z > 2.6 && spine.Position.Z < 2.8)
                        {
                            Console.WriteLine("Lijn 4");
                            CheckPositieVoorAchter(4);

                        }
                        else if (spine.Position.Z > 2.4 && spine.Position.Z < 2.6)
                        {
                            Console.WriteLine("Lijn 3");
                            CheckPositieVoorAchter(3);

                        }
                        else if (spine.Position.Z > 2.2 && spine.Position.Z < 2.4)
                        {
                            Console.WriteLine("Lijn 2");
                            CheckPositieVoorAchter(2);

                        }
                        else if (spine.Position.Z < 2.2)
                        {
                            Console.WriteLine("Lijn 1 - HIER STA IK VOORAAN - ");
                            CheckPositieVoorAchter(1);

                        }
                        #endregion
                        //LINKS RECHT
                        #region links recht                      
                        if (Math.Round(spine.Position.X, 1) > -0.2 && Math.Round(spine.Position.X, 1) < 0.05)
                        {
                            Console.WriteLine("GE STAAT INT MIDDENLINKS");
                            switch (PositieLinksRechts)
                            {
                                case 0:
                                    {
                                        GaNaarRechts();
                                        break;
                                    }
                                case 2:
                                    {
                                        GaNaarLinks();
                                        break;
                                    }
                            }
                            PositieLinksRechts = 1;
                        }
                        else if (Math.Round(spine.Position.X, 1) > 0.05 && Math.Round(spine.Position.X, 1) <= 0.2)
                        {
                            Console.WriteLine("GE STAAT INT MIDDENRECHTS");
                            switch (PositieLinksRechts)
                            {
                                case 1:
                                    {
                                        GaNaarRechts();
                                        break;
                                    }
                                case 3:
                                    {
                                        GaNaarLinks();
                                        break;
                                    }
                            }
                            PositieLinksRechts = 2;
                        }
                        else if (Math.Round(spine.Position.X, 1) < -0.4)
                        {
                            Console.WriteLine("GE STAAT LINKS");
                            GaNaarLinks();
                            PositieLinksRechts = 0;
                        }
                        else if (Math.Round(spine.Position.X, 1) > 0.4)
                        {
                            Console.WriteLine("GE STAAT RECHTS");
                            GaNaarRechts();
                            PositieLinksRechts = 3;
                        }
                        #endregion
                        test = false;
                        #region punt
                        if (puntaan)
                        {
                            ventje[15] = puntje;
                        }
                        else
                        {
                            ventje[15] = 600;
                        }
                        MakeArray(ventje.ToList<int>());
                        puntaan = !puntaan;
                        List<int> v = ventje.ToList<int>();
                        v.Remove(puntje);
                        if (v.Contains(puntje))
                        {
                            score++;
                            puntje = rand.Next(1, 3) == 1 ? rand.Next(0, 64) : rand.Next(192, 384);
                        }
                        #endregion
                    }

                }
                else
                {
                    //PuntjeTimer.Stop();
                    NMCTTimer.Start();
                }
                //else if (joint.TrackingState == JointTrackingState.Inferred)
                //{
                //    drawBrush = this.inferredJointBrush;
                //}

                //if (drawBrush != null)
                //{
                //    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                //}
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowLoaded(sender, e);
        }
    }
}
