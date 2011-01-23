#region Using definitions
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Cell[] cells;
        private string name;
        private float credits;
        private float worth;
        #endregion

        #region Init
        public Player ( Cell[] Cells, String Name, float Credits, float Worth)
        {
            cells = Cells;
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
