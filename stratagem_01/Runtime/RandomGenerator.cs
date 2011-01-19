using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;
//
using Engine_01;
using Engine_01.Interfaces;
using Engine_01.Runtime;

namespace Strategem.Runtime
{
    /// <summary>
    /// RandomGenerator is a singleton class.
    /// It must inherit ITimerTask and therefore cannot be static.
    /// There is only one RandomGenerator per instance of the 
    /// Game.
    /// 
    /// When triggered, RandomGenerator reinitializes it's seed.
    /// </summary>
    public class RandomGenerator : ITimeSyncedObject
    {
        #region Fields
        public static RandomGenerator Instance;

        private static readonly Object key = new object ( );

        private static EngineClock syncClock;
        private static List<double> baseRandoms;

        private ElapsedEventHandler timerCallback;
        private Action Reset;
        private Timer syncTimer;
        private int delay;
        private int cycle;
        #endregion

        #region Init
        static RandomGenerator ( )
        {
            Instance = new RandomGenerator ( );
        }
        private RandomGenerator ( )
        {
            syncClock = Engine.Clock;
            syncClock.ClockStarted += new EventHandler<ClockStartedEventArgs> ( syncClock_ClockStarted );
            syncClock.ClockStopped += new EventHandler<ClockStoppedEventArgs> ( syncClock_ClockStopped );

            syncTimer = new Timer ( );

            delay = 0;
            cycle = 15000;
        }
        #endregion
 
        #region Functions
        public double Next ( )
        {
            Random r = new Random ( (int)syncClock.TimeStamp );

            int index = r.Next ( 0, 99 );
            double seed = baseRandoms[ index ];

            baseRandoms[index] = new Random ( (int)syncClock.TimeStamp ).NextDouble ( );
            
            return seed;
        }

        public void TimerCallback ( object sender, ElapsedEventArgs args )
        {
            reset ( );
        }

        //  =======================================================
        //  private functions
        void reset ( )
        {
            Console.WriteLine ( "[{0}][{1}] TimeSync : reset",
                syncClock.ToString ( ),
                System.Threading.Thread.CurrentThread.ManagedThreadId );

            List<double> newRandoms = new List<double>();

            while (newRandoms.Count < 100)
            {
                double rndBase = new Random ( (int)syncClock.TimeStamp ).NextDouble ( );

                if (!newRandoms.Contains ( rndBase ))
                {
                    newRandoms.Add ( rndBase );
                }
            }

            baseRandoms = newRandoms;
            Console.WriteLine ( "BaseRandoms count: {0}", baseRandoms.Count );
        }

        void syncClock_ClockStarted ( object sender, ClockStartedEventArgs e )
        {
            Console.WriteLine ( "[{0}][{1}] TimeSync : started",
                syncClock.ToString ( ),
                System.Threading.Thread.CurrentThread.ManagedThreadId );

            timerCallback = new ElapsedEventHandler ( TimerCallback );

            syncClock.AddTimeSyncObject ( Instance );
            syncClock.StartTask ( Instance );

            reset ( );
        }

        void syncClock_ClockStopped ( object sender, ClockStoppedEventArgs e )
        {
            syncClock.StopTask ( Instance );
        }

        #endregion
 
        #region Properties

        #region ITimeSyncedObject Members
        ElapsedEventHandler ITimeSyncedObject.TimerCallback
        {
            get
            {
                return timerCallback;
            }
        }

        Timer ITimeSyncedObject.SyncTimer
        {
            get
            {
                return syncTimer;
            }
        }

        int ITimeSyncedObject.Delay
        {
            get
            {
                return delay;
            }
        }

        int ITimeSyncedObject.Cycle
        {
            get
            {
                return cycle;
            }
        }       
        #endregion
        #endregion
    }
}
