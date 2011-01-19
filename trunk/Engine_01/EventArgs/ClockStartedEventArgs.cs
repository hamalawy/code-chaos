using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Engine_01.Runtime;

namespace Engine_01
{
    /// <summary>
    /// ClockStartedEventArgs
    /// 
    ///     Derived EventArgs class to pass event arguments to
    ///     event subscribers.
    /// </summary>
    public class ClockStartedEventArgs : EventArgs
    {
        #region Fields
        
        #endregion

        #region Init
        /// <summary>
        /// ClockStartedEventArgs
        /// </summary>
        /// <param name="TimeStamp">TimeStamp in ticks when the clock started.</param>
        /// <param name="ClockStatus">EngineClockStatus: clock status.</param>
        public ClockStartedEventArgs ( long TimeStamp, EngineClockStatus ClockStatus )
        {
            StartTimeStamp = TimeStamp;
            this.ClockStatus = ClockStatus;
        }
        #endregion
 
        #region Functions
        
        #endregion

        #region Properties
        /// <summary>
        /// TimeStamp when the clock started.
        /// </summary>
        public long StartTimeStamp
        {
            get;
            private set;
        }
        /// <summary>
        /// Clock status.
        /// </summary>
        public EngineClockStatus ClockStatus
        {
            get;
            private set;
        }
        #endregion
   }
}
