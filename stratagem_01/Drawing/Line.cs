using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Strategem
{
    public struct Line
    {
        #region Fields
        private readonly Point head;
        private readonly Point tail;
        private readonly double angle;
        private readonly float length;

        private Color? color;
        private Pen pen;
        #endregion

        #region Init
        private Line (bool empty )
        {
            head = new Point ( );
            tail = new Point ( );
            angle = default ( double );
            length = default ( float );
            color = null;
            pen = null;
        }
        private Line ( Line line )
        {
            head = line.head;
            tail = line.tail;
            angle = line.angle;
            length = line.length;
            color = line.color;
            pen = line.pen;
        }
        public Line ( Point Head, Point Tail, Color Color)
        {
            head = Head;
            tail = Tail;
            angle = Math.Atan ((( head.X - tail.X ) / ( head.Y - tail.Y )) );
            length = default ( float );
            color = Color;
            pen = new Pen ( Color, 1f );
        }
        public Line ( Point Head, double Angle, float Length, Color Color)
        {
            head = Head;
            tail = new Point();
            angle = Angle;
            length = Length;
            color = Color;
            pen = new Pen ( Color, 1f );
        }
        public Line ( Point Head, Point Tail, Pen Pen)
        {
            head = Head;
            tail = Tail;

            int deltaX = Head.X - Tail.X;
            int deltaY = Head.Y - Tail.Y;

            angle = Math.Atan2 ( deltaY, deltaX );
            length = default ( float );
            color = Pen.Color;
            pen = Pen;
        }
        public Line ( Point Head, double Angle, float Length, Pen Pen )
        {
            head = Head;
            tail = new Point();
            angle = Angle;
            length = Length;
            color = Pen.Color;
            pen = Pen;
        }
        #endregion

        #region Functions
        public static Line Copy ( Line Line )
        {
            return new Line ( Line );
        }

        public static Line Empty ( )
        {
            return new Line ( true );
        }

        public void Resize ( Point Head, Point Tail )
        {
            this = new Line ( Head, Tail, (Color)color );
        }

        public void Resize ( Point Head, double Angle, float Length )
        {
            this = new Line ( Head, Angle, Length, (Color)color );
        }
        #endregion

        #region Properties
        public double Angle
        {
            get
            {
                return angle;
            }
        }

        public float Length
        {
            get
            {
                return length;
            }
        }

        public Point Head
        {
            get
            {
                return head;
            }
        }

        public Point Tail
        {
            get
            {
                return tail;
            }
        }

        public Pen Pen
        {
            get
            {
                return pen;
            }
            set
            {
                pen = value;
            }
        }
        #endregion
    }
}
