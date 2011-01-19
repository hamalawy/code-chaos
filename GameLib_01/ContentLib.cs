/*  -----------------------------------------------------------------------------
 *  ContentLib.cs
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameLib_01
{
    public class ContentLib : IDisposable
    {
        #region Fields
        ContentManager contentMgr;
        
        #endregion

        #region Init
        public ContentLib ( IServiceProvider ServiceProvider, String ContentDirectory )
        {
            contentMgr = new ContentManager ( ServiceProvider, ContentDirectory );
        }
        #endregion

        #region Function
        public void Load<TContent> ( String assetName )
        {
            contentMgr.Load<TContent> ( assetName );
        }

        private void unload_assets ( )
        {
            contentMgr.Unload ( );
        }
        #endregion

        #region Properties
        #region IDisposable Members
        public void Dispose ( )
        {
            unload_assets ( );
        }

        #endregion
        #endregion
    }
}
