using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//
using Engine_01;
using GameLib_01.Data;
//
using Stratagem.Runtime;

namespace Stratagem
{
    /// <summary>
    /// Entry to WinForm game
    /// </summary>
    static class Program
    {
        #region Fields
        static Dictionary<string, Canvas> canvases;
        static frmGameWindow gameBoard;
        static RandomGenerator randGenerator;
        static DataTable boardStats;
        #endregion

        #region Entry
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            randGenerator = RandomGenerator.Instance;

            gameBoard = new frmGameWindow ( canvases );
            gameBoard.Shown += new EventHandler ( gameBoard_Load );

            canvases = gameBoard.canvases;

            Application.Run(gameBoard);
        }
        #endregion

        #region Functions

        // - private -
        static void gameBoard_Load ( object sender, EventArgs e )
        {
            if (!Engine.Start ( ))
            {
                gameBoard.closeToolStripMenuItem.PerformClick ( );
            }

            if (!gameBoard.IsDisposed)
            {
                //  get fileinfo object for calc spreadsheet
                FileInfo file = getDataFile ( ConfigurationManager.AppSettings[ "_boardStats" ] );

                // get data
                string table = DataManager.LoadDBContent ( DataFileType.CSV, file );
                boardStats = DataManager.GetTable(table);
            }

            if (boardStats != null)
            {
                Console.WriteLine ( "[{0}][{1}] Table loaded: {2}",
                   Engine.Clock.Elapsed,
                   System.Threading.Thread.CurrentThread.ManagedThreadId,
                   boardStats.TableName );
            }
        }

        static FileInfo getDataFile ( string FileName )
        {
            DirectoryInfo dir = new DirectoryInfo ( Application.ExecutablePath );
            dir = dir.Parent;

            //  get fileinfo object for calc spreadsheet
            return new FileInfo ( dir.FullName + FileName );
        }
        #endregion
    }
}
