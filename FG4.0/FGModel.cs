using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace FG4._0
{
    public class FGModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private const int STRING_BUFFER = 2048;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public FGModel(string FileDll) 
        {
            //if (FileDll == "HybridAnomalyDetector")
            //{
            //    dll = "HybridAnomalyDetector";
            //}
            //else
            //{
            //    dll = "SimpleAnomalyDetector";
            //}
            //Dll = FileDll;
            //File.Copy(FileDll + ".dll", "test.dll");
        }

        //FGModel() 
        //{
        //    string temp = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        //    CurrPath = temp.Substring(0, temp.Length - 3);
        //}

        TcpClient tcpclnt;
        NetworkStream stm;
        DataTable dataTable;//load csv file
        readonly string Dll;         
        int lines;
        int currentLine = 0;
        //CancellationTokenSource cancellation;
        //bool isPlay;
        /////////////////
        //VideoController
        /////////////////
        private int max;
        public int Max
        {
            get => max;
            set
            {
                max = value;
                NotifyPropertyChanged(nameof(Max));
            }
        }

        private TimeSpan time;
        public TimeSpan Time
        {
            get => time;
            private set
            {
                time = value;
                NotifyPropertyChanged(nameof(Time));
            }
        }

        private double speed;
        public double Speed
        {
            get => speed;
            set
            {
                speed = value;
                NotifyPropertyChanged(nameof(Speed));
            } 
        }

        private bool isPlay = false;
        public bool IsPlay
        {
            get => isPlay;
            set
            {
                isPlay = value;
                NotifyPropertyChanged(nameof(IsPlay));
            }
        }

        /////////////////
        ///  Navigators
        /////////////////

        double aileron;
        public double Aileron
        {
            get => aileron;
            set
            {
                aileron = value;
                NotifyPropertyChanged(nameof(Aileron));
            }
        }

        double elevator;
        public double Elevator
        {
            get => elevator;
            set
            {
                elevator = value;
                NotifyPropertyChanged(nameof(Elevator));
            }
        }

        double altimeter;
        public double Altimeter
        {
            get => altimeter;
            set
            {
                altimeter = value;
                NotifyPropertyChanged(nameof(Altimeter));
            }
        }

        public double airSpeed;
        public double AirSpeed
        {
            get => airSpeed;
            set
            {
                airSpeed = value;
                NotifyPropertyChanged(nameof(AirSpeed));
            }
        }

        public double flightDirection;

        public double FlightDirection
        {
            get => flightDirection;
            set
            {
                flightDirection = value;
                NotifyPropertyChanged(nameof(FlightDirection));
            }
        }

        public double yaw;
        public double Yaw
        {
            get => yaw;
            set
            {
                yaw = value;
                NotifyPropertyChanged(nameof(Yaw));
            }
        }

        public double roll;
        public double Roll
        {
            get => roll;
            set
            {
                roll = value;
                NotifyPropertyChanged(nameof(Roll));
            }
        }

        public double pitch;
        public double Pitch
        {
            get => pitch;
            set
            {
                pitch = value;
                NotifyPropertyChanged(nameof(Pitch));
            }
        }

        public double rudder;
        public double Rudder
        {
            get => rudder;
            set
            {
                rudder = value;
                NotifyPropertyChanged(nameof(Rudder));
            }
        }

        public double throttle;
        public double Throttle
        {
            get => throttle;
            set
            {
                throttle = value;
                NotifyPropertyChanged(nameof(Throttle));
            }
        }

        ///////////////////////
        ///GraphsAndCorrelation
        ///////////////////////

        List<Feature> listOfFeatures;
        public List<Feature> ListOfFeatures
        {
            get => listOfFeatures;
            set
            {
                listOfFeatures = value;
                NotifyPropertyChanged(nameof(ListOfFeatures));
            }
        }

        Dictionary<string, List<double>> vectorsOfFeatures;
        public Dictionary<string, List<double>> VectorsOfFeatures
        {
            get => vectorsOfFeatures;
            set
            {
                vectorsOfFeatures = value;
                NotifyPropertyChanged(nameof(VectorsOfFeatures));
            }
        }

        private int frames;
        public int Frames
        {
            get => frames;
            private set
            {
                frames = value;
                currentLine = value;
                NotifyPropertyChanged(nameof(Frames));
            }
        }

        //graphs and list of features


        //////////////////////
        ///     funcs      ///
        //////////////////////
        ///private void LearnNormalFlight() 
        ///{
        ///    //send to dll file:reg_flight.csv
        ///    //ListOfFeatures = new
        ///    List<Feature> lof = new List<Feature>();
        ///    //Feature f1 = new Feature();
        ///    //Feature f2 = new Feature();
        ///    //Feature f3 = new Feature();
        ///    Feature f1 = new Feature("airspeed-kt");
        ///    Feature f2 = new Feature("glideslope");
        ///    Feature f3 = new Feature("vertical-speed-fps");
        ///    //f1.Name = "airspeed-kt";
        ///    //f2.Name = "glideslope";
        ///    //f3.Name = "vertical-speed-fps";
        ///    f1.cf = f3;
        ///    f2.cf = f1;
        ///    f3.cf = f2;
        ///    lof.Add(f1);
        ///    lof.Add(f2);
        ///    lof.Add(f3);
        ///    ListOfFeatures = lof;
        ///}
        ///
        const string simpleDll = "SimpleAnomalyDetector.dll";
        const string hybridDll = "HybridAnomalyDetector.dll";
        const string dll = hybridDll;
        //const string CurrPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())).Substring(0, Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())).Length - 3);
        //CurrPath = CurrPath.Substring(0, ppp.Length - 3);
        //const string DLL_NAME = @"C:\\Users\\Omer\\source\\repos\\FG4.0\\FG4.0\\" + dll;
        const string DLL_NAME = "Detector.dll";

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void learn_normal(string trainFileName);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int detect(string testFileName);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_detector();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void delete_detector();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_normal_model();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int get_by_index_ar(int i, StringBuilder desc);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_by_index_cf_f1(int i, StringBuilder f);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_by_index_cf_f2(int i, StringBuilder f);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static float get_by_index_cf_cor(int i);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_by_index_cf_lin_reg(int i, ref float a, ref float b);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static float get_by_index_cf_thres(int i);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static float get_by_index_cf_cx(int i);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static float get_by_index_cf_cy(int i);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static int get_normal_model_size();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public extern static float get_y_by_x(int i, float x);

        [StructLayout(LayoutKind.Sequential)]
        public struct SimplePoint
        {
            public float x;
            public float y;
        };

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public unsafe extern static void get_dll_points2(int index, int length, IntPtr[] points);

        correlatedFeatures getFeaturesByIndex(int i)
        {
            var f1 = new StringBuilder(STRING_BUFFER);
            var f2 = new StringBuilder(STRING_BUFFER);
            get_by_index_cf_f1(i, f1);
            get_by_index_cf_f2(i, f2);
            float a = 0;
            float b = 0;
            get_by_index_cf_lin_reg(i, ref a, ref b);
            var feature = new correlatedFeatures(f1.ToString(),            // must
                                          f2.ToString(),            // must
                                          get_by_index_cf_cor(i),   // must
                                          new Line(a, b),           // optional
                                          get_by_index_cf_thres(i), // optinal
                                          get_by_index_cf_cx(i),
                                          get_by_index_cf_cy(i));
            return feature;
        }

        List<correlatedFeatures> initFeatures()
        {
            List<correlatedFeatures> cf = new List<correlatedFeatures>();
            if (get_normal_model_size() == 0)
            {
                get_normal_model();
            }
            for (int i = 0; i < get_normal_model_size(); i++)
            {
                cf.Add(getFeaturesByIndex(i));
            }
            return cf;
        }


        List<Point> getPoints(List<correlatedFeatures> cfs, int i)
        {
            correlatedFeatures cf = cfs[i];
            List<Point> points = new List<Point>();
            int numOfPoints = 3000;
            float d = (float)0.05;
            float y;
            for (float x = 0; x < numOfPoints; x += d)
            {
                y = get_y_by_x(i, x);
                points.Add(new Point(x, y));
            }
            return points;
        }

        unsafe List<Point> getPoints2(correlatedFeatures cf, int index)
        {
            List<Point> points = new List<Point>();
            int numOfPoints = 3000;
            IntPtr[] xy = new IntPtr[numOfPoints];
            for (int k = 0; k < numOfPoints; k++)
            {
                xy[k] = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(SimplePoint)));
            }
            get_dll_points2(index, numOfPoints, xy);
            for (int k = 0; k < numOfPoints; k++)
            {
                var cppPoint = (SimplePoint)Marshal.PtrToStructure(xy[k], typeof(SimplePoint));
                points.Add(new Point(cppPoint.x, cppPoint.y));
            }
            for (int k = 0; k < numOfPoints; k++)
            {
                Marshal.FreeHGlobal(xy[k]);
            }
            return points;
        }

        void detectAnomalies(string fileName, List<int> ts, List<string> desc)
        {
            int hSize = detect(fileName);
            for (int i = 0; i < hSize; i++)
            {
                var d = new StringBuilder(STRING_BUFFER);
                int t = get_by_index_ar(i, d);
                ts.Add(t);
                desc.Add(d.ToString());
            }
        }

        string GetAbsolutePath(string filename)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename);
            return path;
        }
        private void LearnNormalFlight(string path, string[] features)
        {
            //send to dll file:reg_flight.csv
            //ListOfFeatures = new
            //string NormalFlightFileName = "C:\\Users\\roth\\Google Drive\\noam\\barIlan\\89211-advanced2\\Project\\P_dlls\\FG4.0\\reg_flight.csv";
            //string addr = GetAbsolutePath("reg_flight.csv");
            //string addr = @"reg_flight.csv";
            //addr = "reg_flight.csv";
            //string addr = System.Reflection.Assembly.GetEntryAssembly().Location;
            //addr += "\\reg_flight.csv";
            //string ppp = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            //ppp = ppp.Substring(0, ppp.Length - 3);
            //Uri u = new Uri(@"reg_flight.csv", UriKind.Relative);
            //string ppp = u.ToString();
            //learn_normal("C:\\Users\\Omer\\source\\repos\\FG4.0\\FG4.0\\reg_flight.csv");
            //learn_normal("C:\\Users\\Omer\\source\\repos\\FG4.0\\FG4.0\\reg_flight.csv");
            string addr = GetAbsolutePath("reg_flight.csv");
            learn_normal(GetAbsolutePath("reg_flight.csv"));
            List<correlatedFeatures> cf = initFeatures();
            List<int> ts = new List<int>();//time
            List<string> d = new List<string>();//a-b
            detectAnomalies(path, ts, d);
            Dictionary<string, Feature> fs = new Dictionary<string, Feature>();
            for (int i = 0; i < features.Length; i++)
            {
                // set name
                fs.Add(features[i], new Feature(features[i]));
            }
            //FIRST-TIME
            for (int i = 0; i < cf.Count; i++)
            {
                // set Feature() - correlative
                fs[cf[i].Feature1].cf = fs[cf[i].Feature2]; // set per each
                //fs[cf[i].Feature2].cf = fs[cf[i].Feature1];
                // points
                List<Point> p = getPoints2(cf[i], i);     ////////////////////////////////////////////here
                fs[cf[i].Feature1].points.Add(p);
                //fs[cf[i].Feature2].points.Add(p);

                string desc = cf[i].Feature1 + "-" + cf[i].Feature2;
                fs[cf[i].Feature1].anomalies = new Dictionary<int, bool>();
                //fs[cf[i].Feature2].anomalies = new Dictionary<int, bool>();
                for (int j = 0; j < ts.Count; j++)
                {
                    if (d[j].Equals(desc))
                    {
                        fs[cf[i].Feature1].anomalies.Add(ts[j], true);
                        //fs[cf[i].Feature2].anomalies.Add(ts[j], false);
                    }
                }
            }
            //SECOND-TIME
            for (int i = 0; i < cf.Count; i++)
            {
                if (fs[cf[i].Feature2].cf == null)
                {
                    fs[cf[i].Feature1].cf = fs[cf[i].Feature2]; // set per each
                                                                //fs[cf[i].Feature2].cf = fs[cf[i].Feature1];
                                                                // points
                    List<Point> p = getPoints2(cf[i], i);     ////////////////////////////////////////////here
                    //fs[cf[i].Feature1].points.Add(p);
                    fs[cf[i].Feature2].points.Add(p);

                    string desc = cf[i].Feature1 + "-" + cf[i].Feature2;
                    //fs[cf[i].Feature1].anomalies = new Dictionary<int, bool>();
                    fs[cf[i].Feature2].anomalies = new Dictionary<int, bool>();
                    for (int j = 0; j < ts.Count; j++)
                    {
                        if (d[j].Equals(desc))
                        {
                            //fs[cf[i].Feature1].anomalies.Add(ts[j], true);
                            fs[cf[i].Feature2].anomalies.Add(ts[j], false);
                        }
                    }
                }
            }
            List<Feature> lof = new List<Feature>();
            for (int i = 0; i < features.Length; i++)
            {
                lof.Add(fs[features[i]]);
            }
            ListOfFeatures = lof;
            // learnNormal --> getFeatures
            // Feature 2 cTor: name only, anything but name
            /*
            List<Feature> lof = new List<Feature>();
            for (int i = 0; i < features.len; i++)
            {
                lof.Add(null);
            }

            //List<Feature> lof = new List<Feature>();
            /*
            Feature f1 = new Feature();
            Feature f2 = new Feature();
            Feature f3 = new Feature();
            f1.Name = "airspeed-kt";
            f2.Name = "glideslope";
            f3.Name = "vertical-speed-fps";
            f1.cf = f3;
            f2.cf = f1;
            f3.cf = f2;
            lof.Add(f1);
            lof.Add(f2);
            lof.Add(f3);
            ListOfFeatures = lof;
            */
        }

        public void UpdateFile(string path)
        {
            Dictionary<string, List<double>> vof = new Dictionary<string, List<double>>();
            DataTable dt = new DataTable("uploadedFile");
            int counterlines = 0;
            string line;
            System.IO.StreamReader fileReader = new System.IO.StreamReader(path);
            line = fileReader.ReadLine();
            string[] features = line.Split(',');
            LearnNormalFlight(path, features);
            //ListOfFeatures = new List<string>(features);
            //add Columns
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("CSVline", typeof(string));
            for (int i = 0; i < features.Length; i++)
            {
                dt.Columns.Add(features[i],typeof(double));
                vof.Add(features[i], new List<double>());//for the Vectors
                //add Dictionary?
            }
            double[] featuresVals;
            while ((line = fileReader.ReadLine()) != null)
            {
                featuresVals = Array.ConvertAll(line.Split(','), Double.Parse);
                DataRow dr = dt.NewRow();
                dr["id"] = counterlines;
                dr["CSVline"] = line;
                for (int i = 0; i < features.Length; i++)
                {
                    dr[features[i]] = featuresVals[i];
                    vof[features[i]].Add(featuresVals[i]);
                    //add Dictionary?
                }
                //dt.Rows.Add(counterlines, featuresVals);//check it!
                dt.Rows.Add(dr);
                counterlines++;
            }
            VectorsOfFeatures = vof;
            fileReader.Close();
            dataTable = dt;
            Max = lines = counterlines;
            Max = Max - 1;
            //add this at the end^^
            /////////////////////////
            //using(var csvReader = new CsvReader(
            //{
            //    DataTable dt = new DataTable();
            //    dt.Load(csvReader);
            //}

        }


        public void Play() { IsPlay = true; }
        public void Pause() { IsPlay = false; }
        //bool first = true;
        public Thread t;
        public void Start() 
        {
            t = new Thread(() => 
            {
            IsPlay = true;
                //datatable
                try
                {
                    //first = false;
                    tcpclnt = new TcpClient();
                    //Connecting...
                    tcpclnt.Connect("127.0.0.1", 5400);
                    //Connected!
                    string line;
                    stm = tcpclnt.GetStream();
                    while (true)//((line = inFG.ReadLine()) != null) //line of data table
                    {
                        while (IsPlay)
                        {
                            if (frames <= max)
                            {
                                line = (string)dataTable.Rows[currentLine]["CSVline"];
                                byte[] byData = System.Text.Encoding.ASCII.GetBytes(line + "\n");
                                stm.Flush();
                                stm.Write(byData, 0, byData.Length);
                                updateLine(line);
                                System.Threading.Thread.Sleep(Convert.ToInt32(100 / Speed));
                                //currentLine++; // problem with Thread?
                                //if (frames <= max)
                                //{
                                currentLine++; // problem with Thread?
                                Frames = Frames + 1;
                                Time = Time + TimeSpan.FromSeconds(0.1);
                            }
                            //Frames = Frames + 1;
                            //Time = Time + TimeSpan.FromSeconds(0.1);
                            //if IsPlay t.sleep until notify t
                        }
                        //line = (string)dataTable.Rows[currentLine]["CSVline"];
                        //byte[] byData = System.Text.Encoding.ASCII.GetBytes(line + "\n");
                        //stm.Flush();
                        //stm.Write(byData, 0, byData.Length);
                        //updateLine(line);
                        //System.Threading.Thread.Sleep(Convert.ToInt32(100 / Speed));
                        //currentLine++;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error... " + e.StackTrace);
                }
            });
            t.Start();
            //}
                //tcpclnt.Close(); if finish the table do not DC
                ////tcpclnt = new TcpClient();
                //////Connecting...
                ////tcpclnt.Connect("127.0.0.1", 5400);
                //////Connected!
                ////String line;
                ////stm = tcpclnt.GetStream();
                ////while ((line = inFG.ReadLine()) != null) //line of data table
                ////{
                ////    byte[] byData = System.Text.Encoding.ASCII.GetBytes(line + "\n");
                ////    stm.Flush();
                ////    stm.Write(byData, 0, byData.Length);
                ////    System.Threading.Thread.Sleep(Convert.ToInt32(100 * Speed));
                ////    updateLine(line);
                ////}
                //tcpclnt.Close(); if finish the table do not DC
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error... " + e.StackTrace);
            //}
        }
        private void updateLine(string line) 
        {
            //save arr of features names and do for features.len and update each one
            //,,,,,,,,,,,,,,,,,,,,,,,,,,,,encoder_indicated-altitude-ft,encoder_pressure-alt-ft,gps_indicated-altitude-ft,gps_indicated-ground-speed-kt,gps_indicated-vertical-speed,indicated-heading-deg,magnetic-compass_indicated-heading-deg,slip-skid-ball_indicated-slip-skid,turn-indicator_indicated-turn-rate,vertical-speed-indicator_indicated-speed-fpm,engine_rpm
            double[] features = Array.ConvertAll(line.Split(','), Double.Parse);
            Aileron = features[0];
            Elevator = features[1];
            Rudder = features[2];
            //flaps = features[3];
            //slats = features[4];
            //speedbrake = features[5];
            Throttle = features[6];//throttle1
            //throttle2 = features[7];
            //engine - pump1 = features[8];
            //engine - pump2 = features[9];
            //electric - pump1 = features[10];
            //electric - pump2 = features[11];
            //external - power = features[12];
            //APU - generator = features[13];
            //latitude - deg = features[14];
            //longitude - deg = features[15];
            //altitude - ft = features[16];
            Roll = features[17];//roll-deg
            Pitch= features[18];//pitch-deg
            FlightDirection = features[19];//heading - deg
            Yaw = features[20];//side-slip-deg
            AirSpeed = features[21];//airspeed-kt
            //glideslope = features[22];
            //vertical - speed - fps = features[23];
            //airspeed - indicator_indicated - speed - kt = features[24];
            Altimeter = features[25];//altimeter_pressure-alt-ft
            //attitude - indicator_indicated - pitch - deg = features[26];
            //attitude - indicator_internal - pitch - deg,attitude - indicator_internal - roll - deg = features[27];
            
            
            
            //FlightDirection
        }



        public void ChangeTime(int cTime)
        {
            Time = new TimeSpan(0, 0, cTime/10);
            Frames = cTime;
        }

        public void ChangeSpeed(double newSpeed)
        {
            Speed = newSpeed;
        }


    }
}
