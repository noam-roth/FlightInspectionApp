using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    class correlatedFeatures
    {
        private string feature1;
        private string feature2;  // names of the correlated feature
        private float corrlation;
        private Line lin_reg;
        private float threshold;
        private float cx;
        private float cy;

        public string Feature1 { get { return feature1; } }

        public string Feature2 { get { return feature2; } }

        public float Threshold { get; set; }

        public Line Lin_reg { get; set; }

        public float Cx { get; set; }

        public float Cy { get; set; }

        public correlatedFeatures(string f1, string f2, float c, Line l, float t, float cx, float cy) {
            feature1 = f1;
            feature2 = f2;
            corrlation = c;
            lin_reg = l;
            threshold = t;
            this.cx = cx;
            this.cy = cy;
        }

        public override string ToString()
        {
            return "f1= " + feature1 + ", f2= " + feature2 + ", cor= " + corrlation + 
                ", Line= (" + lin_reg.getA() + "," + lin_reg.getB() + "), thres= " + threshold +
                ", cx= " + cx + ", cy= " + cy;
        }

    }
}
