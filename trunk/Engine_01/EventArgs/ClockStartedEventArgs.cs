using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Engine_01.Runtime;

namespace Engine_01
{
    public class ClockStartedEventArgs : EventArgs
    {
        #region Fields
        
        #endregion

        #region Init
        public ClockStartedEventArgs ( long TimeStamp, EngineClockStatus ClockStatus )
        {
            StartTimeStamp = TimeStamp;
            this.ClockStatus = ClockStatus;
        }
        #endregion
 
        #region Functions
        
        #endregion

        #region Properties
        public long StartTimeStamp
        {
            get;
            private set;
        }
        public EngineClockStatus ClockStatus
        {
            get;
            private set;
        }
        #endregion
   }
}
