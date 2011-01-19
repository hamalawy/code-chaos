using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//
using Engine_01.Runtime;
using Engine_01.Interfaces;

namespace Engine_01
{
    public static class Engine
    {
        #region Fields
        private static bool isRunning;
        private static CancellationTokenSource cancelSource;
        private static Task engineStartProcess;

        private static EngineClock engineClock;
        private static TimeSpan elapsedRuntime;
        private static long startTimeStamp;

        private static List<ITimeSyncedObject> activeTasks;
        #endregion

        #region Init
        //  Static constructor to instance single objects.
        static Engine ( )
        {
            activeTasks = new List<ITimeSyncedObject> ( );
            engineClock = EngineClock.Clock;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Starts the Engine and other components.
        /// </summary>
        /// <returns>Returns true if engine started.</returns>
        public static Boolean Start ( )
        {
            //  start engine on worker thread
            if (cancelSource == null)
            {
                Console.WriteLine ( "[{0}] Engine starting",
                    engineClock.ToString ( ) );

                cancelSource = new CancellationTokenSource ( );
                engineStartProcess = Task.Factory.StartNew ( ( ) => startEngine ( cancelSource.Token ),
                    cancelSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default );

                if (!cancelSource.Token.IsCancellationRequested)
                {
                    isRunning = true;
                    engineClock.ClockStarted += 
                        new EventHandler<ClockStartedEventArgs> ( engineClock_ClockStarted );
                    engineClock.ClockStopped += new 
                        EventHandler<ClockStoppedEventArgs> ( engineClock_ClockStopped );
                }
            }

            if (!isRunning)
            {
                Console.WriteLine ( "[{0}] Engine failed to start",
                    engineClock.ToString ( ) );
            }

            return isRunning;
        }
        /// <summary>
        /// Stops the Engine and all components.
        /// </summary>
        public static void Stop ( )
        {
            Console.WriteLine ( "[{0}] Engine stopping",
                engineClock.ToString() );

            stopEngine ( );

            Console.WriteLine ( "[{0}] Engine stopped: runtime {1}",
                engineClock.ToString(),
                engineClock.Elapsed );
        }
        /// <summary>
        /// Resets the Engine and all components.
        /// </summary>
        public static void ResetEngine ( )
        {
            Stop ( );
            Start ( );
        }

        //  =======================================================
        //  Usage undetermined.
        public static Action StartTimerTask ( Action action )
        {
            //if (activeTasks.ContainsKey ( action ))
            //{
            //    StopTimerTask ( action );
            //}

            //Timer timer = new Timer ( new TimerCallback ( runTask ), action, 0, 250 );
            //activeTasks.Add ( action, timer );

            return action;
        }
        //  Usage undetermined.
        public static void StopTimerTask ( Action action )
        {
            //if (activeTasks.ContainsKey ( action ))
            //{
            //    activeTasks[ action ].Change ( Timeout.Infinite, Timeout.Infinite );
            //    activeTasks.Remove ( action );
            //}
        }
        //  =======================================================

        //  =======================================================
        //  private functions

        /// <summary>
        /// Contains the main loop.
        /// </summary>
        /// <param name="token">The cancellation token used to monitor for
        /// cancellation request.</param>
        private static void startEngine ( CancellationToken token )
        {
            token.ThrowIfCancellationRequested ( );

            //  main engine loop
            while (!token.IsCancellationRequested)
            {
                if (engineClock.Status == EngineClockStatus.Stopped)
                {
                    engineClock.Start ( out isRunning );

                    Console.WriteLine ( "[{0}][{1}] Engine running",
                        engineClock.ToString ( ),
                        System.Threading.Thread.CurrentThread.ManagedThreadId );
                }
                // TODO: This line throws an exception that is unhandled when 
                // a new game is started too quickly after another
                // "Operation Canceled Exception"
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested ( );
                }

                Thread.Sleep ( 1 );
            }
        }

        /// <summary>
        /// Stops the engine by acting on a cancellation request. Also 
        /// cleans up any resources used by the engine.
        /// </summary>
        private static void stopEngine ( )
        {
            //  clean up resources
            if (cancelSource != null)
            {
                cancelSource.Cancel ( );
                engineStartProcess.Wait ( );

                if (engineStartProcess.Status == TaskStatus.RanToCompletion)
                {
                    engineClock.Stop ( out isRunning );

                    cancelSource = null;
                }
            }
        }

        static void engineClock_ClockStarted ( object sender, ClockStartedEventArgs e )
        {
            startTimeStamp = e.StartTimeStamp;

            Console.WriteLine ( "[{0}][{1}] Engine Clock {2}",
                   engineClock.ToString ( ),
                   System.Threading.Thread.CurrentThread.ManagedThreadId,
                   e.ClockStatus );
        }

        static void engineClock_ClockStopped ( object sender, ClockStoppedEventArgs e )
        {
            elapsedRuntime = e.ElapsedRunTime;

            Console.WriteLine ( "[{0}][{1}] Engine Clock {2}",
                   engineClock.ToString ( ),
                   System.Threading.Thread.CurrentThread.ManagedThreadId,
                   e.ClockStatus );
        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns a reference to the EngineClock.
        /// </summary>
        public static EngineClock Clock
        {
            get
            {
                return engineClock;
            }
        }
        /// <summary>
        /// Returns true is the engine is running.
        /// </summary>
        public static Boolean IsRunning
        {
            get
            {
                return isRunning;
            }
        }
        #endregion
    }
}
