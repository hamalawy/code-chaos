using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Stratagem.Runtime
{
    public static class DataCollections
    {
        #region Fields
        private static Dictionary<string, DataTable> data;
        #endregion

        #region Init
        static DataCollections ( )
        {
            data = new Dictionary<string, DataTable> ( );
        }
        #endregion

        #region Functions
        //  Include a functions section only in objects where functions
        //  are defined.
        #endregion

        #region Properties
        public static Dictionary<string, DataTable> Data
        {
            get
            {
                return data;
            }
        }
        #endregion
        }
}
