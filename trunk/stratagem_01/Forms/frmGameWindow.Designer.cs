namespace Strategem
{
    partial class frmGameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.canvas = new System.Windows.Forms.Panel ( );
            this.panel2 = new System.Windows.Forms.Panel ( );
            this.menuStrip1 = new System.Windows.Forms.MenuStrip ( );
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator ( );
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem ( );
            this.panel1 = new System.Windows.Forms.Panel ( );
            this.lblCell = new System.Windows.Forms.Label ( );
            this.panel2.SuspendLayout ( );
            this.menuStrip1.SuspendLayout ( );
            this.panel1.SuspendLayout ( );
            this.SuspendLayout ( );
            // 
            // canvas
            // 
            this.canvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point ( 2, 1 );
            this.canvas.Name = "canvas";
            this.canvas.Padding = new System.Windows.Forms.Padding ( 3, 1, 3, 1 );
            this.canvas.Size = new System.Drawing.Size ( 1012, 534 );
            this.canvas.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb ( ( (int)( ( (byte)( 64 ) ) ) ), ( (int)( ( (byte)( 64 ) ) ) ), ( (int)( ( (byte)( 64 ) ) ) ) );
            this.panel2.Controls.Add ( this.lblCell );
            this.panel2.Controls.Add ( this.menuStrip1 );
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.ForeColor = System.Drawing.Color.FromArgb ( ( (int)( ( (byte)( 255 ) ) ) ), ( (int)( ( (byte)( 192 ) ) ) ), ( (int)( ( (byte)( 128 ) ) ) ) );
            this.panel2.Location = new System.Drawing.Point ( 0, 536 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size ( 1016, 208 );
            this.panel2.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.viewToolStripMenuItem} );
            this.menuStrip1.Location = new System.Drawing.Point ( 0, 0 );
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size ( 1016, 24 );
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripMenuItem1,
            this.closeToolStripMenuItem} );
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size ( 46, 20 );
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size ( 111, 22 );
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler ( this.newToolStripMenuItem_Click );
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size ( 108, 6 );
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size ( 111, 22 );
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler ( this.closeToolStripMenuItem_Click );
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange ( new System.Windows.Forms.ToolStripItem[] {
            this.gridToolStripMenuItem} );
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size ( 41, 20 );
            this.viewToolStripMenuItem.Text = "View";
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.Size = new System.Drawing.Size ( 104, 22 );
            this.gridToolStripMenuItem.Text = "Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler ( this.gridToolStripMenuItem_Click );
            // 
            // panel1
            // 
            this.panel1.Controls.Add ( this.canvas );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point ( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding ( 2, 1, 2, 1 );
            this.panel1.Size = new System.Drawing.Size ( 1016, 536 );
            this.panel1.TabIndex = 2;
            // 
            // lblCell
            // 
            this.lblCell.AutoSize = true;
            this.lblCell.Location = new System.Drawing.Point ( 12, 34 );
            this.lblCell.Name = "lblCell";
            this.lblCell.Size = new System.Drawing.Size ( 35, 13 );
            this.lblCell.TabIndex = 2;
            this.lblCell.Text = "label1";
            // 
            // frmGameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF ( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size ( 1016, 744 );
            this.ControlBox = false;
            this.Controls.Add ( this.panel1 );
            this.Controls.Add ( this.panel2 );
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmGameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Strategem 0.0.0001";
            this.panel2.ResumeLayout ( false );
            this.panel2.PerformLayout ( );
            this.menuStrip1.ResumeLayout ( false );
            this.menuStrip1.PerformLayout ( );
            this.panel1.ResumeLayout ( false );
            this.ResumeLayout ( false );

        }

        #endregion

        private System.Windows.Forms.Panel canvas;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        internal System.Windows.Forms.Label lblCell;
    }
}

