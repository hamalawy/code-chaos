/*  -----------------------------------------------------------------------------
 *  GraphicsControl.cs
 *  
 *  Microsoft XNA and .Net libraries (C) Microsoft Corporation.
 *  
 *  This library is authored by David Boarman, 2011. All rights reserved
 *  by their respective owners.
 *  -----------------------------------------------------------------------------
*/

using System;
//using System.Drawing;
using System.Windows.Forms;
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//
using Engine_01.Runtime;
//
using GameLib_01.Extensions;
using GameLib_01.Interfaces;

namespace GameLib_01
{
    // System.Drawing and the XNA Framework both define Color and Rectangle
    // types. To avoid conflicts, we specify exactly which ones to use.
    using Color = System.Drawing.Color;

    public abstract class GraphicsControl : Control
    {
        #region Events
        protected event EventHandler<MouseMovedEventArgs> MouseMoved;
        #endregion

        #region Fields
        GraphicsDeviceService graphicsDeviceService;
        ServiceContainer services = ServiceContainer.Container;
        #endregion

        #region Init

        #endregion

        #region Functions
        protected abstract void Initialize ( );
        protected abstract void BeginDraw ( );
        protected abstract void OnMouseMoved ( object sender, MouseMovedEventArgs e );

        protected override void OnCreateControl ( )
        {
            // Don't initialize the graphics device if we are running in the designer.
            if (!DesignMode)
            {
                Rectangle vpRectangle = base.RectangleToScreen ( ClientRectangle ).ToXNARectangle();

                graphicsDeviceService =
                    GraphicsDeviceService.AddReference
                    (
                        Handle,
                        vpRectangle
                    );

                // Register the service, so components like ContentManager can find it.
                services.AddService ( graphicsDeviceService );

                // Give derived classes a chance to initialize themselves.
                Initialize ( );
            }

            base.OnCreateControl ( );
        }

        protected override void Dispose ( bool disposing )
        {
            if (graphicsDeviceService != null)
            {
                graphicsDeviceService.Release ( disposing );
                graphicsDeviceService = null;
            }

            base.Dispose ( disposing );
        }

        protected override void OnPaint ( PaintEventArgs e )
        {
            string beginDrawError = DeviceBeginDraw ( );

            if (string.IsNullOrEmpty ( beginDrawError ))
            {
                // Draw the control using the GraphicsDevice.
                BeginDraw ( );
                EndDraw ( );
            }
            else
            {
                // If BeginDraw failed, show an error message using System.Drawing.
                PaintUsingSystemDrawing ( e.Graphics, beginDrawError );
            }
        }

        string DeviceBeginDraw ( )
        {
            // If we have no graphics device, we must be running in the designer.
            if (graphicsDeviceService == null)
            {
                return Text + "\n\n" + GetType ( );
            }

            // Make sure the graphics device is big enough, and is not lost.
            string deviceResetError = HandleDeviceReset ( );

            if (!string.IsNullOrEmpty ( deviceResetError ))
            {
                return deviceResetError;
            }

            // Many GraphicsDeviceControl instances can be sharing the same
            // GraphicsDevice. The device backbuffer will be resized to fit the
            // largest of these controls. But what if we are currently drawing
            // a smaller control? To avoid unwanted stretching, we set the
            // viewport to only use the top left portion of the full backbuffer.
            Viewport viewport = new Viewport ( );

            viewport.X = 0;
            viewport.Y = 0;

            viewport.Width = ClientSize.Width;
            viewport.Height = ClientSize.Height;

            viewport.MinDepth = 0;
            viewport.MaxDepth = 1;

            GraphicsDevice.Viewport = viewport;

            MouseState mouse = Mouse.GetState ( );

            if (GraphicsDevice.Viewport.Bounds.Contains ( mouse.X, mouse.Y ))
            {
                //  fire event
                OnMouseMoved(this, 
                    new MouseMovedEventArgs(new Microsoft.Xna.Framework.Point(mouse.X, mouse.Y)));
            }
            
            return null;
        }

        void EndDraw ( )
        {
            try
            {
                Rectangle vpRectangle = base.RectangleToScreen(ClientRectangle).ToXNARectangle ( );

                //Rectangle sourceRectangle = new Rectangle ( 0, 0, ClientSize.Width,
                //                                                ClientSize.Height );

                GraphicsDevice.Present ( vpRectangle, null, this.Handle );
            }
            catch
            {
                // Present might throw if the device became lost while we were
                // drawing. The lost device will be handled by the next BeginDraw,
                // so we just swallow the exception.
            }
        }

        string HandleDeviceReset ( )
        {
            bool deviceNeedsReset = false;

            switch (GraphicsDevice.GraphicsDeviceStatus)
            {
                case GraphicsDeviceStatus.Lost:
                    // If the graphics device is lost, we cannot use it at all.
                    return "Graphics device lost";

                case GraphicsDeviceStatus.NotReset:
                    // If device is in the not-reset state, we should try to reset it.
                    deviceNeedsReset = true;
                    break;

                default:
                    // If the device state is ok, check whether it is big enough.
                    PresentationParameters pp = GraphicsDevice.PresentationParameters;

                    deviceNeedsReset = ( ClientSize.Width > pp.BackBufferWidth ) ||
                                       ( ClientSize.Height > pp.BackBufferHeight );
                    break;
            }

            // Do we need to reset the device?
            if (deviceNeedsReset)
            {
                try
                {
                    graphicsDeviceService.ResetDevice ( ClientSize.Width,
                                                      ClientSize.Height );
                }
                catch (Exception e)
                {
                    return "Graphics device reset failed\n\n" + e;
                }
            }

            return null;
        }

        protected virtual void PaintUsingSystemDrawing ( System.Drawing.Graphics graphics, string text )
        {
            graphics.Clear ( Color.CornflowerBlue );

            using (System.Drawing.Brush brush = new System.Drawing.SolidBrush ( Color.Black ))
            {
                using (System.Drawing.StringFormat format = new System.Drawing.StringFormat ( ))
                {
                    format.Alignment = System.Drawing.StringAlignment.Center;
                    format.LineAlignment = System.Drawing.StringAlignment.Center;

                    graphics.DrawString ( text, Font, brush, ClientRectangle, format );
                }
            }
        }

        protected override void OnPaintBackground ( PaintEventArgs pevent )
        {
            //  prevent control from painting background
        }
        #endregion

        #region Properties
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDeviceService.GraphicsDevice;
            }
        }

        public ServiceContainer Services
        {
            get
            {
                return services;
            }
        }
        #endregion
    }
}
