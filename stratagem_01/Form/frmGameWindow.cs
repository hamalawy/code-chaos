using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
//
using Engine_01;

namespace Strategem
{
    public partial class frmGameWindow : Form
    {
        #region Fields
        internal readonly Dictionary<string, Canvas> canvases;
        private readonly DirectoryInfo resPath;
        #endregion

        #region Init
        public frmGameWindow(Dictionary<string, Canvas> Canvases)
        {
            canvases = Canvases;
            InitializeComponent();

            resPath = new DirectoryInfo(@"C:\Projects\mule_01\maps\");

            if (canvases == null)
                canvases = new Dictionary<string, Canvas> ( );

            Canvas newCanvas = AddCanvas ( "base", canvas.ClientSize );
            canvas.Controls.Add(newCanvas);

            newCanvas.Location = canvas.ClientRectangle.Location;
            newCanvas.Dock = DockStyle.Fill;

            canvas.Update ( );
        }
        #endregion

        #region Functions
        private Canvas AddCanvas ( string CanvasName, Size CanvasSize )
        {
            Canvas canvas = new Canvas ( this, CanvasSize );
            canvas.Name = CanvasName;

            canvases.Add ( CanvasName, canvas );

            return canvas;
        }

        internal void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Engine.Stop ( );

            if (!Engine.IsRunning)
                Close ( );
        }

        private void gridToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            gridToolStripMenuItem.Checked = !gridToolStripMenuItem.Checked;
            canvases[ "base" ].Grid.Visible = gridToolStripMenuItem.Checked;
            canvases[ "base" ].Invalidate ( );
        }

        private void newToolStripMenuItem_Click ( object sender, EventArgs e )
        {
            Engine.ResetEngine ( );
        }
        #endregion

        #region Properties
        
        #endregion
    }
}
