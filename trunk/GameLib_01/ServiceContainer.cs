/*  -----------------------------------------------------------------------------
 *  ServiceContainer.cs
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

namespace GameLib_01
{
    public class ServiceContainer : IServiceProvider
    {
        #region Fields
        private static Dictionary<Type, object> services;
        #endregion

        #region Init
        static ServiceContainer ( )
        {
            services = new Dictionary<Type, object> ( );
        }
        #endregion

        #region Function
        public void AddService<TService> ( TService Service )
        {
            services.Add ( typeof ( TService ), Service );
        }

        #region IServiceProvider Members
        public object GetService ( Type ServiceType )
        {
            object service;

            services.TryGetValue ( ServiceType, out service );

            return service;
        }
        #endregion
        #endregion

        #region Properties

        #endregion
    }
}
