using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Reflection;
using System.Text;
//using System.Windows.Forms;
using SysTimer = System.Timers.Timer;
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
//
using Engine_01.Runtime;

namespace Strategem.Runtime
{
    public class Board
    {
        #region Fields
        private readonly List<Cell[]> matrix;
        private readonly Dictionary<Rectangle, Cell> cells;
        #endregion

        #region Init
        static Board ( )
        {
        }
        public Board ( Canvas BaseCanvas )
        {
            matrix = new List<Cell[]> ( BaseCanvas.Grid.Rows );
            cells = new Dictionary<Rectangle, Cell> ( );

            for (int row = 0 ; row < matrix.Capacity ; row++)
            {
                matrix.Add ( new Cell[ BaseCanvas.Grid.Columns ] );
            }

            buildMatrix ( BaseCanvas, matrix );
            //BaseCanvas.Cells = cells;
        }

        #endregion

        #region Functions

        //  =======================================================
        //  private functions
        private void buildMatrix ( Canvas canvas, List<Cell[]> matrix )
        {
            Grid grid = canvas.Grid;

            foreach (Cell[] row in matrix)
            {
                int iRow = matrix.IndexOf(row);

                for (int column = 0 ; column < grid.Columns ; column++)
                {
                    Cell cell = new Cell(new Rectangle ( (int)( column * grid.ColumnWidth ),
                        (int)( matrix.IndexOf ( row ) * grid.RowHeight ),
                        (int)grid.ColumnWidth, (int)grid.RowHeight ));

                    cell.Name = string.Format ( "[{0}, {1}]", column, iRow );
                    row[ column ] = cell;

                    cells.Add ( cell.Bounds, cell );
                }
            }
        }

        #endregion

        #region Properties
        public List<Cell[]> Matrix
        {
            get
            {
                return matrix;
            }
        }

        public Cell this[ int Column, int Row ]
        {
            get
            {
                return matrix[ Row ][ Column ];
            }
        }
        #endregion
    }
}
