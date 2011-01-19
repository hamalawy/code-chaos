using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Engine_01.Interfaces
{
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
