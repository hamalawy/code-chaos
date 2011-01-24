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
        private readonly string _internalName;
        private readonly int[] _cellIndexes;
        private readonly Cell[] cells;

        private string name;
        private float credits;
        private float worth;
        #endregion

        #region Init
        public Player ( string InternalName, int[] CellIndexes, String Name, float Credits, float Worth)
        {
            _internalName = InternalName;

            _cellIndexes = CellIndexes;
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
