using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Engine_01.Runtime;

namespace Engine_01
{
    /// <summary>
    /// ClockStoppedEventArgs
    /// 
    ///     Derived EventArgs class to pass event arguments to
    ///     event subscribers.
    /// </summary>
    public class ClockStoppedEventArgs : EventArgs
    {
        #region Fields
        #endregion

        #region Init
        /// <summary>
        /// ClockStoppedEventArgs
        /// </summary>
        /// <param name="ElapsedTime">ElsapsedTime as a TimeSpan object the engine clock has been running.</param>
        /// <param name="ClockStatus">EngineClockStatus: clock status.</param>
        public ClockStoppedEventArgs ( TimeSpan ElapsedTime, EngineClockStatus ClockStatus )
        {
            ElapsedRunTime = ElapsedTime;
            this.ClockStatus = ClockStatus;
        }
        #endregion
 
        #region Functions
        
        #endregion

        #region Properties
        /// <summary>
        /// Elapsed time engine has been running as a TimeSpan object.
        /// </summary>
        public TimeSpan ElapsedRunTime
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
