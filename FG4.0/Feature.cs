using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FG4._0
{
    public class Feature
    {
        public string Name { get; set; }
        public Feature cf { get; set; } 
        public List<List<Point>> points { get; set; }  
        public Dictionary<int, bool> anomalies { get; set; }  
        public Feature(string name)
        {
            Name = name;
            cf = null;
            points = new List<List<Point>>();
            anomalies = null;
        }
    }
}
