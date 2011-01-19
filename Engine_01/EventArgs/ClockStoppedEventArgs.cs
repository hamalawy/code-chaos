using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Engine_01.Runtime;

namespace Engine_01
{
    public class ClockStoppedEventArgs : EventArgs
    {
        #region Fields
        
        #endregion

        #region Init
        public ClockStoppedEventArgs ( TimeSpan ElapsedTime, EngineClockStatus ClockStatus )
        {
            ElapsedRunTime = ElapsedTime;
            this.ClockStatus = ClockStatus;
        }
        #endregion
 
        #region Functions
        
        #endregion

        #region Properties
        public TimeSpan ElapsedRunTime
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
