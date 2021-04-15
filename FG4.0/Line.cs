﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FG4._0
{
    class Line
    {
        private float a, b;

        public Line()
        {
            a = 0;
            b = 0;
        }
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public float f(float x)
        {
            return a * x + b;
        }

        public float getA()
        {
            return a;
        }

        public float getB()
        {
            return b;
        }

    }
}