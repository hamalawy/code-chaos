#region Using definitions
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Xna.Framework;
//
#endregion

namespace Stratagem
{
    /// <summary>
    /// ClassTemplate
    /// 
    /// </summary>
    public class Player
    {
        #region Fields
        private readonly string _internalName;

        //private readonly Cell[] cells;
        private readonly List<Cell> cells;
        private readonly Rectangle playerArea;

        private string name;
        private float credits;
        private float worth;
        #endregion

        #region Init
        public Player ( string InternalName, int[] CellIndexes, String Name, float Credits, float Worth)
        {
            _internalName = InternalName;

            cells = getCellsFromIndexes(CellIndexes);
            playerArea = new Rectangle ( cells[ 0 ].Bounds.X, cells[ 0 ].Bounds.Y,
                                       cells[ 7 ].Bounds.Right, cells[ 7 ].Bounds.Bottom );

            name = Name;
            credits = Credits;
            worth = Worth;
        }
        #endregion
 
        #region Functions
        public Cell GetCell ( int CellNumber )
        {
            return cells[ CellNumber ];
        }

        //  =======================================================
        //  private functions
        private List<Cell> getCellsFromIndexes ( int[] cellIndexes )
        {
            List<Cell> cellList = new List<Cell> ( );

            foreach (int index in cellIndexes)
            {
                cellList.Add ( Canvas.CellIndex[ index ] );
            }

            return cellList;
        }
        #endregion

        #region Properties
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public float Credits
        {
            get
            {
                return credits;
            }
            set
            {
                credits = value;
            }
        }

        public float Worth
        {
            get
            {
                return worth;
            }
            set
            {
                worth = value;
            }
        }

        #endregion
   }
}
