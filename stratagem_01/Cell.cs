using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Xna.Framework;

namespace Stratagem
{
    public class Cell
    {
        #region Fields
        private Rectangle tile;
        #endregion

        #region Init
        public Cell ( Rectangle Tile )
        {
            tile = Tile;
        }
        #endregion
 
        #region Functions
        public bool Contains ( Point Location )
        {
            return tile.Contains ( Location );
        }
        #endregion

        #region Properties
        public string Name
        {
            get;
            set;
        }
        public Rectangle Bounds
        {
            get
            {
                return tile;
            }
        }
        #endregion
   }
}
