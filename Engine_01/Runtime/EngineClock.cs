/*  -----------------------------------------------------------------------------
 *  EngineClock.cs
 *  
 *  Microsoft XNA and .Net libraries (C) Microsoft Corporation.
 *  EngineClock (C) David Boarman, 2011.
 *  
 *  This library is authored by David Boarman, 2011. All rights reserved
 *  by their respective owners/authors. The EngineClock object is based on the 
 *  core methods of the Microsoft class, System.Diagnostics.Stopwatch. Microsoft 
 *  Corporation is recognized as the original author of the Stopwatch class. 
 *  
 *  Any implementation of the EngineClock class outside of this application must 
 *  include the above recognition verbatim. This may need additional documentation 
 *  to qualify as proper recognition of the original authors.
 *  
 *  Hi-Resolution capabilities have been removed due to inherent issues with 
 *  multi-core processors and frequency conditions. This implementation of a
 *  standard clock mechanism is architecture independent.
 *  -----------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
//
using Engine_01.Interfaces;
using System.Diagnostics;

namespace Engine_01.Runtime
{
    /// <summary>
    /// EngineClock
    ///     EngineClock is a customized timing service and clock with start and 
    ///     stop events. As a service, accessible via ServiceContainer, EngineClock 
    ///     accepts a collection of CallBacks for Timer operations (using 
    ///     ITimeSyncedObject).
    ///     
    ///     ITimeSyncedObject may be used to build a class that keeps timed objects
    ///     synchronized with other objects. This is yet to be determined.
    /// </summary>
    public class EngineClock
    {
        #region Events
        public event EventHandler<ClockStartedEventArgs> ClockStarted;
        public event EventHandler<ClockStoppedEventArgs> ClockStopped;
        #endregion

        #region Fields
        public static readonly EngineClock Clock;

        private readonly ServiceContainer services;

        //  not currently used
        private readonly List<ITimeSyncedObject> syncedObjects;

        private long elapsed;
        private long startTimeStamp;

        private EngineClockStatus clockStatus;
        #endregion

        #region Init
        //  Static constructor to instance single objects.
        static EngineClock ( )
        {
            Clock = new EngineClock ( );
        }
        //  Private constructor for singleton object.
        private EngineClock ( )
        {
            services = ServiceContainer.Container;
            services.AddService ( Clock );

            syncedObjects = new List<ITimeSyncedObject> ( );

            Reset ( );
        }
        #endregion

        #region Functions
        /// <summary>
        /// Resets the EngineClock.
        /// </summary>
        /// <returns>Returns elapsed ticks prior to engine clock reset.</returns>
        public long Reset ( )
        {
            long elapsed = this.elapsed;

            this.elapsed = 0L;
            startTimeStamp = 0L;

            clockStatus = EngineClockStatus.Stopped;
            OnClockStopped ( );

            return elapsed;
        }
        /// <summary>
        /// Restarts the EngineClock.
        /// </summary>
        public void Restart ( )
        {
            elapsed = 0L;
            startTimeStamp = TimeStamp;
        }
        /// <summary>
        /// Starts the EngineClock.
        /// </summary>
        /// <param name="IsRunning">Out is true if running.</param>
        public void Start ( out bool IsRunning )
        {
            if (clockStatus == EngineClockStatus.Running)
            {
                IsRunning = true;
                return;
            }

            startTimeStamp = TimeStamp;
            
            clockStatus = EngineClockStatus.Running;
            OnClockStarted ( startTimeStamp );

            IsRunning = true;
        }
        /// <summary>
        /// Stops the EngineClock.
        /// </summary>
        /// <param name="IsRunning">Out is false if stopped.</param>
        public void Stop ( out bool IsRunning )
        {
            if (clockStatus == EngineClockStatus.Stopped)
            {
                IsRunning = false;
                return;
            }

            long tempNum = TimeStamp - startTimeStamp;

            elapsed += tempNum;
            clockStatus = EngineClockStatus.Stopped;
            OnClockStopped ( );

            if (elapsed < 0L)
            {
                elapsed = 0L;
            }

            IsRunning = false;
        }
        /// <summary>
        /// Add an ITimeSyncedObject to the event tree.
        /// </summary>
        /// <param name="SyncedObject"></param>
        public void AddTimeSyncObject ( ITimeSyncedObject SyncedObject )
        {
            //  add synced object to collection
            if (!syncedObjects.Contains ( SyncedObject ))
            {
                try
                {
                    SyncedObject.SyncTimer.Interval = SyncedObject.Cycle;
                    SyncedObject.SyncTimer.Elapsed += SyncedObject.TimerCallback;

                    syncedObjects.Add ( SyncedObject );
                }
                catch (Exception ex)
                {
                    Console.WriteLine ( "[{0}] Exception adding TimeSyncedObject: {1}\r\n{2}",
                        ToString ( ),
                        ex.Message,
                        new StackTrace ( ex ).ToString ( ) );

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine ( "[{0}] Inner exception: {1}\r\n{2}",
                            ToString ( ),
                            ex.InnerException.Message,
                            new StackTrace ( ex.InnerException ).ToString ( ) );
                    }
                }
            }
        }
        /// <summary>
        /// Starts an ITimeSyncedObject.
        /// </summary>
        /// <typeparam name="TSyncedObject">An ITimeSyncedObject type.</typeparam>
        /// <param name="SyncedObject">The object implementing ITimeSynced.</param>
        public void StartTask<TSyncedObject> ( TSyncedObject SyncedObject )
            where TSyncedObject : ITimeSyncedObject
        {
            TSyncedObject syncObject = getSyncedObject ( SyncedObject );

            SyncedObject.SyncTimer.Start ( );
        }
        /// <summary>
        /// Stops an ITimeSyncedObject.
        /// </summary>
        /// <typeparam name="TSyncedObject">An ITimeSyncedObject type.</typeparam>
        /// <param name="SyncedObject">The object implementing ITimeSynced.</param>
        public void StopTask<TSyncedObject> ( TSyncedObject SyncedObject )
            where TSyncedObject : ITimeSyncedObject
        {
            TSyncedObject syncObject = getSyncedObject ( SyncedObject );

            syncObject.SyncTimer.Stop ( );
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The EngineClock time stamp in the form hh:mm:ss.fffffff</returns>
        public override string ToString ( )
        {
            if (String.IsNullOrEmpty ( StartTimeStamp ))
            {
                StartTimeStamp = new DateTime ( TimeStamp ).ToString ( "hh:mm:ss.fffffff" );
                return StartTimeStamp;
            }

            return new DateTime ( TimeStamp ).ToString ( "hh:mm:ss.fffffff" );
        }
        /// <summary>
        /// OnClockStarted event method.
        /// </summary>
        /// <param name="TimeStamp">EngineClock time stamp at startup.</param>
        public void OnClockStarted ( long TimeStamp )
        {
            EventHandler<ClockStartedEventArgs> _clockStarted = ClockStarted;
            if (_clockStarted != null)
                _clockStarted ( this, new ClockStartedEventArgs ( TimeStamp, clockStatus ) );
        }
        /// <summary>
        /// OnClockedStopped event method.
        /// </summary>
        public void OnClockStopped ( )
        {
            EventHandler<ClockStoppedEventArgs> _clockStopped = ClockStopped;
            if (_clockStopped != null)
                _clockStopped ( this, new ClockStoppedEventArgs ( Elapsed, clockStatus ) );
        }

        #region IDisposable Members
        public void Dispose ( )
        {
            bool isRunning;

            Clock.Stop ( out isRunning );

            if (!isRunning)
            {
                // why haven't we shut down EngineClock?
            }
            else
            {
                //  dispose of all resources and ITimeSyncedObjects
            }
        }

        #endregion

        //  =======================================================
        //  private functions
        private long getRawElaspedTicks ( )
        {
            long elapsed = this.elapsed;

            if (clockStatus == EngineClockStatus.Running)
            {
                long tempNum = TimeStamp - startTimeStamp;
                elapsed += tempNum;
            }

            return elapsed;
        }

        private TSyncedObject getSyncedObject<TSyncedObject> ( TSyncedObject syncedObject )
        {
            object syncObject = ( from timers in syncedObjects
                                  where timers.GetType ( ).Equals ( typeof ( TSyncedObject ) )
                                  select timers )
                                .First ( );

            return (TSyncedObject)syncObject;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Elapsed engine run-time as TimeSpan.
        /// </summary>
        public TimeSpan Elapsed
        {
            get
            {
                return new TimeSpan ( getRawElaspedTicks ( ) );
            }
        }
        /// <summary>
        /// Elapsed engine run-time in milliseconds.
        /// </summary>
        public long ElapsedMilliseconds
        {
            get
            {
                return ( getRawElaspedTicks ( ) / 0x2710L );
            }
        }
        /// <summary>
        /// Elapsed engine run-time in ticks.
        /// </summary>
        public long ElapsedTicks
        {
            get
            {
                return getRawElaspedTicks ( );
            }
        }
        /// <summary>
        /// Current EngineClock status.
        /// </summary>
        public EngineClockStatus Status
        {
            get
            {
                return clockStatus;
            }
        }
        /// <summary>
        /// Current EngineClock time stamp. Note that the engine
        /// clock need not be running to retrieve a time stamp.
        /// </summary>
        public long TimeStamp
        {
            get
            {
                return DateTime.UtcNow.Ticks;
            }
        }
        /// <summary>
        /// Gets the EngineClock startup time stamp as a
        /// string value.
        /// </summary>
        public string StartTimeStamp
        {
            get;
            private set;
        }
        #endregion
    }
    /// <summary>
    /// EngineClockStatus
    ///     
    ///     Enumerator for EngineClock status.
    /// </summary>
    public enum EngineClockStatus
    {
        Stopped = 0,
        Running = 1,
        Restarted = 2,
    }

}
