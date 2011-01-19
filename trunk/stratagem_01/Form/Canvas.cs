using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysTimer = System.Timers.Timer;
//
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XColor = Microsoft.Xna.Framework.Color;
//
using GameLib_01;
using Strategem.Runtime;
using Engine_01.Runtime;
using Microsoft.Xna.Framework;

namespace Strategem
{
    public sealed class Canvas : GraphicsControl
    {
        #region Fields
        private readonly List<Cell[]> matrix;

        private Rectangle clientArea;
        private Grid grid;
        private Board board;
        private Cell currentTile;
        private frmGameWindow owner;
        #endregion

        #region Init
        public Canvas ( frmGameWindow Owner, System.Drawing.Size Area )
        {
            base.Size = Area;
            owner = Owner;

            clientArea = new Rectangle ( owner.ClientRectangle.X, 
                owner.ClientRectangle.Y,
                owner.ClientRectangle.Width, 
                owner.ClientRectangle.Height );

            grid = new Grid ( 9, 5, new System.Drawing.Size(clientArea.Width, clientArea.Height ));
            matrix = new List<Cell[]> ( grid.Rows );
            Cells = new Dictionary<Rectangle, Cell> ( );

            for (int row = 0 ; row < grid.Rows ; row++)
            {
                matrix.Add ( new Cell[ grid.Columns ] );
            }

            buildMatrix ( );

            MouseMoved += new EventHandler<MouseMovedEventArgs> ( OnMouseMoved );
        }
        #endregion

        #region Functions
        public void DrawText ( string Text )
        {
            PaintUsingSystemDrawing ( base.CreateGraphics ( ), Text );
        }

        #region GraphicsControl Members
        protected override void Initialize ( )
        {
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate
            {
                Invalidate ( );
            };
        }

        protected override void BeginDraw ( )
        {
            GraphicsDevice.Clear ( Color.CornflowerBlue );
        }

        protected override void OnCreateControl ( )
        {
            System.Drawing.Point location = base.Parent.ClientRectangle.Location;

            base.OnCreateControl ( );
        }

        protected override void OnMouseMoved ( object sender, MouseMovedEventArgs e )
        {
            Console.WriteLine ( e.Location );

            var cell = ( from tile in Cells.Keys
                         where tile.Contains(e.Location)
                         select tile )
                        .SingleOrDefault ( );

            if (( cell == null ) || ( !Cells.ContainsKey ( cell ) ))
            {
                return;
            }

            if (( currentTile == null ) || ( !currentTile.Equals ( Cells[ cell ] ) ))
            {
                owner.lblCell.Text = String.Format("[{0}][{1}] Cell {2}",
                    EngineClock.Clock.ToString ( ),
                    System.Threading.Thread.CurrentThread.ManagedThreadId,
                    Cells[ cell ].Name );

                currentTile = Cells[ cell ];
            }
        }

        protected override void PaintUsingSystemDrawing ( System.Drawing.Graphics graphics, string text )
        {
            base.PaintUsingSystemDrawing ( graphics, text );
        }
        #endregion

        //  =======================================================
        //  private functions
        private void buildMatrix ( )
        {
            foreach (Cell[] row in matrix)
            {
                int iRow = matrix.IndexOf ( row );

                for (int column = 0 ; column < grid.Columns ; column++)
                {
                    Cell cell = new Cell ( new Rectangle ( (int)( column * grid.ColumnWidth ),
                        (int)( matrix.IndexOf ( row ) * grid.RowHeight ),
                        (int)grid.ColumnWidth, (int)grid.RowHeight ) );

                    cell.Name = string.Format ( "[{0}, {1}]", column, iRow );
                    row[ column ] = cell;

                    Cells.Add ( cell.Bounds, cell );
                }
            }
        }
        #endregion

        #region Properties
        //  Rectangle
        public Rectangle UserArea
        {
            get
            {
                return clientArea;
            }
        }

        //  Grid
        public Grid Grid
        {
            get
            {
                return grid;
            }
            set
            {
                grid = value;
            }
        }

        public List<Cell[]> Matrix
        {
            get
            {
                return board.Matrix;
            }
        }

        public Cell this[ int Column, int Row ]
        {
            get
            {
                return matrix[ Row ][ Column ];
            }
        }

        public Dictionary<Rectangle, Cell> Cells
        {
            get;
            private set;
        }
        #endregion
    }
}
