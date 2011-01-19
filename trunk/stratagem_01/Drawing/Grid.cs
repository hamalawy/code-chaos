using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;

namespace Strategem
{
    //  this needs to be converted to a GraphicsDeviceControl
    //  but works sufficiently to provide a grid/coordinate 
    //  system.
    public class Grid
    {
        #region Fields
        private int columns;
        private int rows;

        private Rectangle bounds;
        private List<Line> lines;
        #endregion

        #region Init
        public Grid ( int Columns, int Rows, Size Size )
        {
            if (Columns == 0)
                columns = 1;
            else
                columns = Columns;

            if (Rows == 0)
                rows = 1;
            else
                rows = Rows;

            bounds = new Rectangle ( new Point ( 0, 0 ), Size );
            lines = new List<Line> ( );

            CreateGrid ( );
        }
        #endregion

        #region Functions
        public void Move ( Point Location )
        {
            bounds.Location = Location;
        }

        private void CreateGrid ( )
        {
            lines.Clear ( );

            float x_interval = bounds.Width / columns;
            float y_interval = bounds.Height / rows;

            for (float x = 0 ; x <= columns ; x++)
            {
                lines.Add ( new Line ( new Point ( (int)( x * x_interval ), bounds.Top ),
                                       new Point ( (int)( x * x_interval ), bounds.Height ),
                                       new Pen ( Color.Gray, 2f ) ) );
            }

            for (float y = 0 ; y <= rows ; y++)
            {
                lines.Add ( new Line ( new Point ( bounds.Left, (int)( y * y_interval ) ),
                                       new Point ( bounds.Right, (int)( y * y_interval ) ),
                                       new Pen ( Color.Gray, 2f ) ) );
            }

            ColumnWidth = x_interval;
            RowHeight = y_interval;
        }
        #endregion

        #region Properties
        public Rectangle Bounds
        {
            get
            {
                return bounds;
            }
        }

        public Line[] Pattern
        {
            get
            {
                return lines.ToArray ( );
            }
        }

        public Boolean Visible
        {
            get;
            set;
        }

        public int Columns
        {
            get
            {
                return columns;
            }
        }

        public int Rows
        {
            get
            {
                return rows;
            }
        }

        public float ColumnWidth
        {
            get;
            private set;
        }

        public float RowHeight
        {
            get;
            private set;
        }
        #endregion
    }
}
