using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    public class GraphsAndCorrelationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GraphsAndCorrelationModel Model { get; private set; }
        public GraphsAndCorrelationViewModel(GraphsAndCorrelationModel model)
        {
            Model = model;
            Model.PropertyChanged += TheModel_PropertyChanged;
            graphFeature1 = new PlotModel();
            graphFeature2 = new PlotModel();
            graphCorrelatedFeatures = new PlotModel();
        }

        private int frames;
        public int Frames
        {
            get => frames;
            private set
            {
                frames = value;
                //makeGraphs();
            }
        }
        //private void makeGraphs() 
        //{
        //    if (isSelected)
        //    {
        //        if (feature2 == null)
        //        {
        //            PlotModel gr1 = new PlotModel();
        //            gr1.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Bottom,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Maximum = Frames,
        //                Title = "Time (0.1 sec)"
        //            });
        //            gr1.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Left,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Title = feature1
        //            });
        //            LineSeries gf1 = new LineSeries();
        //            gr1.Series.Add(gf1);//
        //            for (int i = 0; i < frames; i++)
        //            {
        //                gf1.Points.Add(new DataPoint(i, vectorsOfFeatures[feature1][i]));
        //            }
        //            GraphFeature1 = gr1;
        //            GraphFeature2 = new PlotModel();
        //            GraphCorrelatedFeatures = new PlotModel();
        //        }
        //        else
        //        {
        //            PlotModel gr1 = new PlotModel();
        //            PlotModel gr2 = new PlotModel();
        //            PlotModel gcf = new PlotModel();
        //            gr1.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Bottom,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Maximum = Frames,
        //                Title = "Time (0.1 sec)"
        //            });
        //            gr1.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Left,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Title = feature1
        //            });
        //            gr2.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Bottom,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Maximum = Frames,
        //                Title = "Time (0.1 sec)"
        //            });
        //            gr2.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Left,
        //                IsZoomEnabled = false,
        //                Minimum = 0,
        //                Title = feature2
        //            });
        //            LineSeries gf1 = new LineSeries();
        //            LineSeries gf2 = new LineSeries();
        //            ScatterSeries gc = new ScatterSeries();//gray
        //            gc.MarkerFill = OxyColor.FromRgb(128, 128, 128);
        //            ScatterSeries fgc = new ScatterSeries();//red
        //            fgc.MarkerFill = OxyColor.FromRgb(255, 0, 0);
        //            if (feature.points != null)
        //            {
        //                foreach (var lists in feature.points)
        //                {
        //                    LineSeries drawOver = new LineSeries();
        //                    foreach (var p in lists)
        //                    {
        //                        drawOver.Points.Add(new DataPoint(p.X, p.Y));//black
        //                    }
        //                    gcf.Series.Add(drawOver);//
        //                }
        //            }
        //            gr1.Series.Add(gf1);//
        //            gr2.Series.Add(gf2);//
        //            gcf.Series.Add(gc);//
        //            gcf.Series.Add(fgc);//
        //            for (int i = 0; i < frames; i++)
        //            {
        //                gf1.Points.Add(new DataPoint(i, vectorsOfFeatures[feature1][i]));
        //                gf2.Points.Add(new DataPoint(i, vectorsOfFeatures[feature2][i]));
        //            }
        //            int j = frames - 30;// last 30 samples
        //            if (j < 0)
        //            {
        //                j = 0;
        //            }
        //            double maxX = double.MinValue ,maxY = double.MinValue, minX = double.MaxValue, minY = double.MaxValue;
        //
        //            for (; j <= frames; j++)
        //            {
        //                maxX = Math.Max(maxX, vectorsOfFeatures[feature1][j]);
        //                maxY = Math.Max(maxY, vectorsOfFeatures[feature2][j]);
        //                minX = Math.Max(minX, vectorsOfFeatures[feature1][j]);
        //                minY = Math.Max(minY, vectorsOfFeatures[feature2][j]);
        //                if (feature.anomalies != null && feature.anomalies.ContainsKey(j))
        //                {
        //                    fgc.Points.Add(new ScatterPoint(vectorsOfFeatures[feature1][j], vectorsOfFeatures[feature2][j]));
        //
        //                }
        //                gc.Points.Add(new ScatterPoint(vectorsOfFeatures[feature1][j], vectorsOfFeatures[feature2][j]));
        //            }
        //            gcf.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Bottom,
        //                IsZoomEnabled = false,
        //                Title = feature1
        //            });
        //            gcf.Axes.Add(new LinearAxis
        //            {
        //                Position = AxisPosition.Left,
        //                IsZoomEnabled = false,
        //                Title = feature2
        //            });
        //            GraphFeature1 = gr1;
        //            GraphFeature2 = gr2;
        //            GraphCorrelatedFeatures = gcf;
        //        }
        //    }
        //}



        //Dictionary<string, LineSeries> VectorLineSeries;
        //private void MakeVectorLineSeries() 
        //{
        //    Dictionary<string, LineSeries> vls = new Dictionary<string, LineSeries>();
        //    foreach (KeyValuePair<string, List<double>> entry in vectorsOfFeatures)
        //    {
        //        LineSeries ls = new LineSeries();
        //        int i = 0;
        //        foreach (double x in entry.Value)
        //        {
        //            ls.Points.Add(new DataPoint(i, x));
        //            i++;
        //        }
        //        vls.Add(entry.Key, ls);
        //    }
        //    VectorLineSeries = vls;
        //}

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

        string feature1;
        PlotModel graphFeature1;
        public PlotModel GraphFeature1
        {
            get => graphFeature1;
            set
            {
                graphFeature1 = value;
                NotifyPropertyChanged(nameof(GraphFeature1));
            }
        }

        string feature2;
        PlotModel graphFeature2;
        public PlotModel GraphFeature2
        {
            get => graphFeature2;
            set
            {
                graphFeature2 = value;
                NotifyPropertyChanged(nameof(GraphFeature2));
            }
        }

        PlotModel graphCorrelatedFeatures;
        public PlotModel GraphCorrelatedFeatures
        {
            get => graphCorrelatedFeatures;
            set
            {
                graphCorrelatedFeatures = value;
                NotifyPropertyChanged(nameof(GraphCorrelatedFeatures));
            }
        }


        Dictionary<string, List<double>> vectorsOfFeatures;
        public Dictionary<string, List<double>> VectorsOfFeatures
        {
            get => vectorsOfFeatures;
            set
            {
                vectorsOfFeatures = value;
                //MakeVectorLineSeries();
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //bool isSelected = false;
        //Feature feature;
        public void ChangeGraphs(Feature f)
        {
            Model.ChangeGraphs(f);
            //feature = f;
            //feature1 = f.Name;
            //if (f.cf == null)
            //{
            //    feature2 = null;
            //}
            //else
            //{
            //    feature2 = f.cf.Name;
            //}
            //isSelected = true;
            //makeGraphs(); //frames will change
        }


        private void TheModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "VectorsOfFeatures":
                    VectorsOfFeatures = Model.VectorsOfFeatures;
                    break;
                case "ListOfFeatures":
                    ListOfFeatures = Model.ListOfFeatures;
                    break;
                case "Frames":
                    Frames = Model.Frames;
                    break;
                case "GraphFeature1":
                    GraphFeature1 = Model.GraphFeature1;
                    break;
                case "GraphFeature2":
                    GraphFeature2 = Model.GraphFeature2;
                    break;
                case "GraphCorrelatedFeatures":
                    GraphCorrelatedFeatures = Model.GraphCorrelatedFeatures;
                    break;
                default:
                    break;
            }

                    //
            //if (e.PropertyName == "VectorsOfFeatures")
            //{
            //    VectorsOfFeatures = Model.VectorsOfFeatures;
            //}
            //else if (e.PropertyName == "ListOfFeatures") 
            //{
            //    ListOfFeatures = Model.ListOfFeatures;
            //}
            //else if (e.PropertyName == "Frames")
            //{
            //    Frames = Model.Frames;
            //}
        }
    }
}
