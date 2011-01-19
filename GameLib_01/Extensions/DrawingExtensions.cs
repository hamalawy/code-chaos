using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Microsoft.Xna.Framework;

namespace GameLib_01.Extensions
{
    /// <summary>
    /// Extension methods related to GDI to XNA.Framework conversions
    /// </summary>
    public static class DrawingExtensions
    {
        /// <summary>
        /// Converts a System.Drawing.Rectangle to an Xna.Framework.Rectangle.
        /// </summary>
        /// <param name="GDIRectangle">The System.Drawing.Rectangle to convert.</param>
        /// <returns>A Microsoft.Xna.Framework.Rectangle.</returns>
        public static Rectangle ToXNARectangle ( this System.Drawing.Rectangle GDIRectangle )
        {
            Rectangle newRect = new Rectangle
            (
                GDIRectangle.X,
                GDIRectangle.Y,
                GDIRectangle.Width,
                GDIRectangle.Height
            );

            return newRect;
        }

        /// <summary>
        /// Converts an Xna.Framework.Rectangle to a System.Drawing.Size.
        /// </summary>
        /// <param name="XNARectangle">The Xna.Framework.Rectangle to convert.</param>
        /// <returns>A System.Drawing.Size</returns>
        public static System.Drawing.Size ToGDISize ( this Rectangle XNARectangle )
        {
            System.Drawing.Size newSize = new System.Drawing.Size
            (
                XNARectangle.Width,
                XNARectangle.Height
            );

            return newSize;
        }
    }
}
