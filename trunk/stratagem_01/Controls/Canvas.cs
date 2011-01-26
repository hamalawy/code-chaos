using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SysTimer = System.Timers.Timer;
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//
using GameLib_01;
//
using Engine_01.Runtime;
//
using Stratagem.Runtime;
using GameLib_01.Data;
using System.Collections;

namespace Stratagem
{
    public sealed class Canvas : GraphicsControl
    {
        #region Fields
        private readonly List<Cell[]> matrix;

        private Rectangle clientArea;
        private Grid grid;
        private Cell currentTile;
        private Rectangle currentArea;

        SpriteBatch SB;
        Texture2D gridLines;
        Texture2D neutralZone;
        Texture2D bazaar;
        Texture2D[] playerAreas;

        int playerhover = 0;
        #endregion

        #region Init
        public Canvas ( frmGameWindow Owner, System.Drawing.Size Area )
            : base (Owner)
        {
            base.Size = Area;
            owner = Owner;

            clientArea = new Rectangle ( ClientRectangle.X, 
                ClientRectangle.Y,
                ClientRectangle.Width, 
                ClientRectangle.Height );

            grid = new Grid ( 9, 5, Area);
            matrix = new List<Cell[]> ( grid.Rows );
            Cells = new Dictionary<Rectangle, Cell> ( );
            CellIndex = new List<Cell> ( );
            playerAreas = new Texture2D[ 4 ];

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

            SB = new SpriteBatch(base.GraphicsDevice); 

            gridLines = base.LoadAssets ("Grid");
            neutralZone = base.LoadAssets("neutralArea");
            bazaar = base.LoadAssets("communityBazaar");

            //  we could make this accessible to the class if necessary
            var playerCollection = (Dictionary<string, Player>)DataManager.CollectionIndex[ "players" ];

            var playerImages = ( from images in playerCollection.Keys
                                 select images )
                               .ToArray<string> ( );

            playerAreas = base.LoadAssets ( playerImages );
        }

        protected override void BeginDraw()
        {
            GraphicsDevice.Clear ( Color.Brown );

            SB.Begin();
            SB.Draw(neutralZone, new Vector2(0, 0), Color.White);
            SB.Draw(bazaar, new Rectangle(this[4, 2].Bounds.X, this[4, 2].Bounds.Y, 
                this[4, 2].Bounds.Width, this[4, 2].Bounds.Height), Color.White);

            if (Grid.Visible)
            {
                SB.Draw(gridLines, new Vector2(0, 0), Color.White);
            }

            SB.End();
        }

        protected override void OnCreateControl ( )
        {
            System.Drawing.Point location = base.Parent.ClientRectangle.Location;

            base.OnCreateControl ( );
        }

        protected override void OnMouseMoved ( object sender, MouseMovedEventArgs e )
        {
            checkCurrentTile ( e.Location );
            checkCurrentArea ( e.Location );
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
                    CellIndex.Add ( cell );
                }

                //Console.WriteLine ( "Create cell row #{0}", iRow );
            }
        }

        private void checkCurrentTile ( Point location )
        {
            var cell = ( from tile in Cells.Keys
                         where tile.Contains ( location )
                         select tile )
                        .SingleOrDefault ( );

            if (( cell == null ) || ( !Cells.ContainsKey ( cell ) ))
            {
                return;
            }

            if (( currentTile == null ) || ( !currentTile.Equals ( Cells[ cell ] ) ))
            {
                ( (frmGameWindow)owner ).lblCell.Text = String.Format ( "[{0}][{1}] Cell {2}",
                    EngineClock.Clock.ToString ( ),
                    System.Threading.Thread.CurrentThread.ManagedThreadId,
                    Cells[ cell ].Name );

                currentTile = Cells[ cell ];
            }
        }

        private void checkCurrentArea ( Point location )
        {
            var playerCollection = (Dictionary<string, Player>)DataManager.CollectionIndex[ "players" ];

            var player = ( from players in playerCollection.Values
                           where players.Area.Contains ( location )
                           select players )
                           .SingleOrDefault ( );

            if (player == null)
            {
                ( (frmGameWindow)owner ).lblArea.Text = string.Empty;
                return;
            }

            if (( currentArea == null ) || ( !currentArea.Equals ( player.Area ) ))
            {
                ( (frmGameWindow)owner ).lblArea.Text = player.Name;
            }
        }

        void GraphicsDevice_DeviceReset ( object sender, EventArgs e )
        {
            //throw new NotImplementedException ( );
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

        public Dictionary<Rectangle, Cell> Cells
        {
            get;
            private set;
        }

        public static List<Cell> CellIndex
        {
            get;
            private set;
        }
        #endregion
    }
}
