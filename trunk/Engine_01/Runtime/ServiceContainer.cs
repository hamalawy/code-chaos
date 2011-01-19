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
//
using Engine_01.Interfaces;

namespace Engine_01.Runtime
{
    public class ServiceContainer : IServiceProvider
    {
        #region Fields
        public static ServiceContainer Container;

        private static Dictionary<Type, IServiceObject> services;
        #endregion

        #region Init
        static ServiceContainer ( )
        {
            Container = new ServiceContainer ( );
            services = new Dictionary<Type, IServiceObject> ( );
        }
        private ServiceContainer ( )
        {
        }
        #endregion

        #region Function
        public void AddService<TService> ( TService Service )
            where TService : IServiceObject
        {
            services.Add ( typeof ( TService ), Service );
        }

        #region IServiceProvider Members
        public object GetService ( Type ServiceType )
        {
            object service;

            if (services.ContainsKey ( ServiceType ))
            {
                service = services[ ServiceType ];
            }
            else
            {
                service = null;
            }

            return service;
        }
        #endregion
        #endregion

        #region Properties

        #endregion
    }
}
