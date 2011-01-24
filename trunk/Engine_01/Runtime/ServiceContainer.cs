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

        private static Dictionary<Type, Object> services;
        #endregion

        #region Init
        //  Static constructor to instance single objects.
        static ServiceContainer ( )
        {
            Container = new ServiceContainer ( );
            services = new Dictionary<Type, Object> ( );
        }
        //  Private constructor for singleton object.
        private ServiceContainer ( )
        {
            //  empty
        }
        #endregion

        #region Function
        /// <summary>
        /// Adds an IServiceObject to the service container.
        /// </summary>
        /// <typeparam name="TService">An IServiceObject type.</typeparam>
        /// <param name="Service">The object implementing IServiceObject.</param>
        public void AddService<TService> ( TService Service )
            //where TService : IServiceObject
        {
            services.Add ( typeof ( TService ), Service );
        }

        #region IServiceProvider Members
        /// <summary>
        /// Gets the service from the service container.
        /// </summary>
        /// <param name="ServiceType">The service type to retrieve.</param>
        /// <returns>Returns the service object.</returns>
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
