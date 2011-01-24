/*  -----------------------------------------------------------------------------
 *  GraphicsDeviceService.cs
 *  internal class GraphicsDevice
 *  
 *  Microsoft XNA and .Net libraries (C) Microsoft Corporation.
 *  
 *  This library is authored by David Boarman, 2011. All rights reserved
 *  by their respective owners.
 *  -----------------------------------------------------------------------------
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Drawing;
using System.Threading;
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//
using Engine_01.Interfaces;

namespace GameLib_01
{
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        #region Fields
        private static GraphicsDeviceService deviceService;
        private static int references;
        private static PresentationParameters parameters;

        GraphicsDevice graphicsDevice;
        #endregion

        #region Init
        private GraphicsDeviceService ( IntPtr windowHandle, Rectangle windowArea )
        {
            parameters = new PresentationParameters ( );

            parameters.BackBufferWidth = Math.Max ( windowArea.Width, 1 );
            parameters.BackBufferHeight = Math.Max ( windowArea.Height, 1 );
            parameters.BackBufferFormat = SurfaceFormat.Color;
            parameters.DepthStencilFormat = DepthFormat.Depth24;
            parameters.DeviceWindowHandle = windowHandle;
            parameters.PresentationInterval = PresentInterval.Immediate;
            parameters.IsFullScreen = false;

            graphicsDevice = new GraphicsDevice ( GraphicsAdapter.DefaultAdapter,
                                                  GraphicsProfile.HiDef,
                                                  parameters );
        }
        #endregion

        #region Function
        public static GraphicsDeviceService AddReference ( IntPtr WindowHandle, Rectangle WindowArea )
        {
            if (Interlocked.Increment ( ref references ) == 1)
            {
                deviceService = new GraphicsDeviceService ( WindowHandle, WindowArea );
            }

            return deviceService;
        }

        public void Release ( bool disposing )
        {
            // Decrement the "how many controls sharing the device" reference count.
            if (Interlocked.Decrement ( ref references ) == 0)
            {
                // If this is the last control to finish using the
                // device, we should dispose the singleton instance.
                if (disposing)
                {
                    if (DeviceDisposing != null)
                        DeviceDisposing ( this, EventArgs.Empty );

                    graphicsDevice.Dispose ( );
                }

                graphicsDevice = null;
            }
        }

        public void ResetDevice ( int width, int height )
        {
            if (DeviceResetting != null)
                DeviceResetting ( this, EventArgs.Empty );

            parameters.BackBufferWidth = Math.Max ( parameters.BackBufferWidth, width );
            parameters.BackBufferHeight = Math.Max ( parameters.BackBufferHeight, height );

            graphicsDevice.Reset ( parameters );

            if (DeviceReset != null)
                DeviceReset ( this, EventArgs.Empty );
        }

        #region IDisposable Members

        public void Dispose ( )
        {
            Release ( true );
        }

        #endregion
        #endregion

        #region Properties
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice;
            }
        }

        #endregion

        #region Events
        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        #endregion

    }
}
