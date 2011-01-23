#region Using definitions
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
// 
using LumenWorks.Framework.IO;
using LumenWorks.Framework.IO.Csv;   //CSV reader
//
using Engine_01.Runtime;
#endregion

namespace GameLib_01.Data
{
    /// <summary>
    /// DataManager
    /// 
    ///     TODO:   Add class comments
    ///             Add ability to handle multiple data files
    ///             
    /// currently working as an instance class although may not be instanced
    /// at all since I am going to provide public static methods for now - may
    /// be internally instanced - not sure at this point
    /// </summary>
    public static class DataManager
    {
        #region Fields
        private static EngineClock _clock = EngineClock.Clock;
        private static Dictionary<string, DataTable> dataTables;
        #endregion

        #region Init
        static DataManager ( )
        {
            dataTables = new Dictionary<string, DataTable> ( );
        }
        #endregion

        #region Functions
        public static string LoadDBContent ( DataFileType FileType, FileInfo DataFile )
        {
            //  TODO: implement DataFileType to handle csv, xml, and bin(ary) files

            //  load data and return string[] array
            if (File.Exists ( DataFile.FullName ))
            {
                return readFile ( DataFile );
            }
            
            return string.Empty;
        }

        public static DataTable GetTable ( string TableName )
        {
            return dataTables[ TableName ];
        }

        //  =======================================================
        //  private functions
        private static string readFile ( FileInfo dataFile )
        {
            //  TODO: should be able to handle multiple data files
            bool _hasTable = false;

            string[] members = dataFile.Name.Split('.');
            string tblName = members[ 0 ];

            DataTable table = new DataTable ( tblName );

            using (StreamReader stream = new StreamReader ( dataFile.FullName, Encoding.UTF8 ))
            using (CsvReader csv = new CsvReader ( stream, true ))
            {
                table.Load ( csv );

                if (table.Rows.Count > 0)
                {
                    _hasTable = true;
                    dataTables.Add ( tblName, table );
                }
            }

            return tblName;
        }
        #endregion

        #region Properties
        #endregion
    }

    /// <summary>
    /// This enum will tell DataManager what type of data file is being loaded. Current
    /// supported file types: .csv, .xml, .bin (binary serialized).
    /// 
    /// Note that this is in a temporary location. 
    /// </summary>
    public enum DataFileType
    {
        CSV     = 0,
        XML,
        BIN,
    }
}
