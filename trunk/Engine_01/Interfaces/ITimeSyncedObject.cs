using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Engine_01.Interfaces
{
    /// <summary>
    /// ITimeSyncedObject
    ///     
    ///     Creates an interface for objects that should be 
    ///     time-synced with the engine. This is currently not
    ///     being used.
    ///     
    ///     I hesitate to delete becaue there may be a use
    ///     at some point.
    /// </summary>
    public interface ITimeSyncedObject
    {
        #region Functions
        
        #endregion

        #region Properties
        ElapsedEventHandler TimerCallback
        {
            get;
        }

        int Delay
        {
            get;
        }

        int Cycle
        {
            get;
        }

        Timer SyncTimer
        {
            get;
        }
        #endregion
   }
}
