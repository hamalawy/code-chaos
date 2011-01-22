using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Xna.Framework;

namespace Stratagem.Runtime
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
        #endregion

        #region Init
        public Grid ( int Columns, int Rows, System.Drawing.Size Size )
        {
            if (Columns == 0)
                columns = 1;
            else
                columns = Columns;

            if (Rows == 0)
                rows = 1;
            else
                rows = Rows;

            bounds = new Rectangle ( 0, 0, Size.Width, Size.Height );

            ColumnWidth = bounds.Width / columns;
            RowHeight = bounds.Height / rows;
        }
        #endregion

        #region Functions
        public void Move ( Point Location )
        {
            bounds.Location = Location;
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
