using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
//
using Engine_01;
using GameLib_01.Data;
//
using Strategem.Runtime;
using System.Configuration;
//

namespace Strategem
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

        static void gameBoard_Load ( object sender, EventArgs e )
        {
            if (!Engine.Start ( ))
            {
                gameBoard.closeToolStripMenuItem.PerformClick ( );
            }

            if (!gameBoard.IsDisposed)
            {
                //  check for directory structure
                DirectoryInfo dir = new DirectoryInfo ( Application.ExecutablePath );
                dir = dir.Parent;

                //  get fileinfo object for calc spreadsheet
                FileInfo file = new FileInfo ( dir.FullName + ConfigurationManager.AppSettings[ "_boardStats" ] );
                // get data connector
                DataManager.LoadDBContent ( file );
            }
        }
        #endregion
    }
}
