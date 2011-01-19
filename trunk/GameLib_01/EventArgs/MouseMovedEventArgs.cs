using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Xna.Framework;
//
using Engine_01.Runtime;

namespace GameLib_01
{
    public class MouseMovedEventArgs : EventArgs
    {
        #region Fields
        
        #endregion

        #region Init
        public MouseMovedEventArgs ( Point Location )
        {
            this.Location = Location;
        }
        #endregion
 
        #region Functions
        
        #endregion

        #region Properties
        public Point Location
        {
            get;
            private set;
        }
        #endregion
   }
}
