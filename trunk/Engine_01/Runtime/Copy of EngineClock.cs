/*  -----------------------------------------------------------------------------
 *  EngineClock.cs
 *  
 *  Microsoft XNA and .Net libraries (C) Microsoft Corporation.
 *  
 *  This library is authored by David Boarman, 2011. All rights reserved
 *  by their respective owners. The EngineClock is written based on the Stopwatch 
 *  class originally authored by Microsoft.
 *  
 *  This may need additional documentation to qualify as proper recognition.
 *  -----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
//
using Engine_01;
using Engine_01.Runtime;

namespace DO_NOT_USE
{
    public class EngineClock_temp
    {
        [DllImport ( "CoreDll.dll" )]
        public static extern bool QueryPerformanceFrequency ( out long value );
        [DllImport ( "CoreDll.dll" )]
        public static extern bool QueryPerformanceCounter ( out long value );

        //bool QueryPerformanceFrequency ( out long value )
        //{
        //    value = 0L;

        //    return false;
        //}

        //void QueryPerformanceCounter ( out long value )
        //{
        //    value = 0L;
        //}
        #region Events
        public EventHandler<ClockStartedEventArgs> ClockStarted;
        public EventHandler<ClockStoppedEventArgs> ClockStopped;
        #endregion

        #region Fields
        public static EngineClock Clock;

        private long elapsed;
        private long startTimeStamp;

        private EngineClockStatus clockStatus;

        private readonly bool isHiRes;
        private readonly long frequency;
        private readonly double tickFrequency;
        #endregion

        #region Init
        static EngineClock_temp ( )
        {
            Clock = EngineClock.Clock;
        }
        public EngineClock_temp ( )
        {
            if (!QueryPerformanceFrequency ( out frequency ))
            {
                isHiRes = false;
                frequency = 0x989680L;
                tickFrequency = 1.0;
            }
            else
            {
                isHiRes = true;
                tickFrequency = 10000000.0;
                tickFrequency /= (double)frequency;
            }

            Reset ( );
        }
        #endregion
 
        #region Functions
        private long getElapsedDateTimeTicks ( )
        {
            long rawElapsedTicks = getRawElaspedTicks ( );

            if (isHiRes)
            {
                double tempNum = rawElapsedTicks;

                tempNum *= tickFrequency;

                return (long)tempNum;
            }

            return rawElapsedTicks;
        }

        private long getRawElaspedTicks ( )
        {
            long elapsed = this.elapsed;

            if (clockStatus == EngineClockStatus.Running)
            {
                long tempNum = getTimeStamp ( ) - startTimeStamp;
                elapsed += tempNum;
            }

            return elapsed;
        }

        private long getTimeStamp ( )
        {
            if (isHiRes)
            {
                long tempNum = 0L;
                QueryPerformanceCounter ( out tempNum );

                return tempNum;
            }

            return DateTime.UtcNow.Ticks;
        }

        public long Reset ( )
        {
            long elapsed = this.elapsed;

            this.elapsed = 0L;
            startTimeStamp = 0L;
            clockStatus = EngineClockStatus.Stopped;

            return elapsed;
        }

        public void Restart ( )
        {
            elapsed = 0L;
            startTimeStamp = getTimeStamp ( );
        }

        public void Start ( )
        {
            if (clockStatus == EngineClockStatus.Stopped)
            {
                startTimeStamp = getTimeStamp ( );
                clockStatus = EngineClockStatus.Running;
            }
        }

        public void Stop ( )
        {
            if (clockStatus == EngineClockStatus.Running)
            {
                long tempNum = getTimeStamp ( ) - startTimeStamp;

                elapsed += tempNum;
                clockStatus = EngineClockStatus.Stopped;

                if (elapsed < 0L)
                {
                    elapsed = 0L;
                }
            }
        }

        public void OnClockStarted ( long TimeStamp, EngineClockStatus ClockStatus )
        {
            EventHandler<ClockStartedEventArgs> _clockStarted = ClockStarted;
            if (_clockStarted != null)
                _clockStarted ( this,  new ClockStartedEventArgs  ( TimeStamp, ClockStatus  ) );
        }

        public void OnClockStopped ( )
        {
            EventHandler<ClockStoppedEventArgs> _clockStopped = ClockStopped;
            if (_clockStopped != null)
                _clockStopped ( this, new ClockStoppedEventArgs ( Elapsed ) );
        }
        #endregion

        #region Properties
        public TimeSpan Elapsed
        {
            get
            {
                return new TimeSpan ( getElapsedDateTimeTicks ( ) );
            }
        }

        public long ElapsedMilliseconds
        {
            get
            {
                return ( getElapsedDateTimeTicks ( ) / 0x2710L );
            }
        }

        public long ElapsedTicks
        {
            get
            {
                return getRawElaspedTicks ( );
            }
        }

        public EngineClockStatus IsRunning
        {
            get
            {
                return clockStatus;
            }
        }
        #endregion
   }
}
