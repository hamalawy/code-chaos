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
using System.IO;
using System.Windows.Forms;
//
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
//
using Engine_01.Interfaces;
using Engine_01.Runtime;
//
using GameLib_01.Extensions;

namespace GameLib_01
{
    // System.Drawing and the XNA Framework both define Color and Rectangle
    // types. To avoid conflicts, we specify exactly which ones to use.
    //using Color = System.Drawing.Color;

    public abstract class GraphicsControl : Control
    {
        #region Events
        protected event EventHandler<MouseMovedEventArgs> MouseMoved;
        #endregion

        #region Fields
        GraphicsDeviceService graphicsDeviceService;
        ServiceContainer services = ServiceContainer.Container;

        protected System.Windows.Forms.Form owner;
        ContentManager Content;
        SpriteBatch SB;
        #endregion

        #region Init
        protected GraphicsControl ( System.Windows.Forms.Form Owner )
        {
            owner = Owner;
        }
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
                Rectangle vpRectangle = ClientRectangle.ToXNARectangle ( );

                graphicsDeviceService =
                    GraphicsDeviceService.AddReference
                    (
                       // Mouse.WindowHandle,//Changed to be the form handle and it moves the xna game window to the right spot
                        owner.Handle,
                        // it can also be changed to Form.ActiveForm.Handle
                        vpRectangle
                    );
                
                // Register the service, so components like ContentManager can find it.
                services.AddService ( (IGraphicsDeviceService)graphicsDeviceService );

                // Give derived classes a chance to initialize themselves.
                Initialize ( );
            }
            base.OnCreateControl ( );
        }
        public Texture2D LoadAssets(string name)
        {
            DirectoryInfo _dirContent = new DirectoryInfo ( @"Content\Images" );
            Content = new ContentManager(services, _dirContent.FullName);

            Texture2D texture = Content.Load<Texture2D>(name);
            return texture;
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
                //  Draw the control using the GraphicsDevice.
                BeginDraw ( );
                EndDraw ( );
            }
            else
            {
                //  If BeginDraw failed, show an error message using System.Drawing.
                PaintUsingSystemDrawing ( e.Graphics, beginDrawError );
            }
        }

        string DeviceBeginDraw ( )
        {
            //  If we have no graphics device, we must be running in the designer.
            if (graphicsDeviceService == null)
            {
                return Text + "\n\n" + GetType ( );
            }

            //  Make sure the graphics device is big enough, and is not lost.
            string deviceResetError = HandleDeviceReset ( );

            if (!string.IsNullOrEmpty ( deviceResetError ))
            {
                return deviceResetError;
            }

            //  change mouse coords to be relative to the parent control
            Mouse.WindowHandle = Handle;

            //  get mouse state from game window
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
                Rectangle vpRectangle = ClientRectangle.ToXNARectangle ( );

                GraphicsDevice.Present ( vpRectangle, null, Mouse.WindowHandle );
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
            graphics.Clear ( System.Drawing.Color.LightSlateGray );

            using (System.Drawing.Brush brush = new System.Drawing.SolidBrush ( System.Drawing.Color.Black ))
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
