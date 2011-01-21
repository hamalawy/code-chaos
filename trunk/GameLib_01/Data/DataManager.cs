#region Using definitions
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
// 

//
using Engine_01.Runtime; 
#endregion

namespace GameLib_01.Data
{
    /// <summary>
    /// DataManager
    /// 
    ///     TODO: Add class comments
    ///     
    /// currently working as an instance class although may not be instanced
    /// at all since I am going to provide public static methods for now - may
    /// be internally instanced - not sure at this point
    /// </summary>
    public class DataManager
    {
        #region Fields
        private static EngineClock _clock = EngineClock.Clock;
        #endregion

        #region Init
        //  Include an init section only in objects where a constructor
        //  is defined.
        #endregion
 
        #region Functions
        public static bool LoadDBContent ( FileInfo DataFile )
        {
            object RowSetObj;
            object ConnectToDatabase;

            if (File.Exists ( DataFile.FullName ))
            {
                readFile ( DataFile );
            }

            return false;
        }

        //  =======================================================
        //  private functions
        private static bool readFile ( FileInfo dataFile )
        {
            bool _readSuccess = false;

            using (Stream stream = new FileStream ( dataFile.OpenRead ( ).SafeFileHandle, FileAccess.Read ))
            {
                _readSuccess = true;
            }

            return _readSuccess;
        }
        #endregion

        #region Properties
        //  Include a properies section only in objects where properties
        //  are defined.
        #endregion
   }

}
