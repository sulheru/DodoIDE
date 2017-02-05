namespace DodoIDE.Controls
{
    partial class MapViewerControl
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapViewerControl));
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.graphProps = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lstPivots = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.XPoint = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.TxtDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtHi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtFormat = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.graphProps.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.Black;
            this.Canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(316, 343);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            // 
            // graphProps
            // 
            this.graphProps.Controls.Add(this.panel4);
            this.graphProps.Controls.Add(this.TxtDesc);
            this.graphProps.Controls.Add(this.label4);
            this.graphProps.Controls.Add(this.panel2);
            this.graphProps.Controls.Add(this.label3);
            this.graphProps.Controls.Add(this.panel3);
            this.graphProps.Dock = System.Windows.Forms.DockStyle.Right;
            this.graphProps.Location = new System.Drawing.Point(316, 0);
            this.graphProps.Name = "graphProps";
            this.graphProps.Size = new System.Drawing.Size(195, 343);
            this.graphProps.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lstPivots);
            this.panel4.Controls.Add(this.toolStrip1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 175);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(195, 168);
            this.panel4.TabIndex = 9;
            // 
            // lstPivots
            // 
            this.lstPivots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPivots.FormattingEnabled = true;
            this.lstPivots.Location = new System.Drawing.Point(0, 25);
            this.lstPivots.Name = "lstPivots";
            this.lstPivots.Size = new System.Drawing.Size(195, 143);
            this.lstPivots.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.XPoint,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(195, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(14, 22);
            this.toolStripLabel1.Text = "X";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(30, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(14, 22);
            this.toolStripLabel2.Text = "Y";
            // 
            // XPoint
            // 
            this.XPoint.Name = "XPoint";
            this.XPoint.Size = new System.Drawing.Size(30, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            // 
            // TxtDesc
            // 
            this.TxtDesc.BackColor = System.Drawing.Color.White;
            this.TxtDesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.TxtDesc.Location = new System.Drawing.Point(0, 83);
            this.TxtDesc.Multiline = true;
            this.TxtDesc.Name = "TxtDesc";
            this.TxtDesc.Size = new System.Drawing.Size(195, 92);
            this.TxtDesc.TabIndex = 5;
            this.TxtDesc.TextChanged += new System.EventHandler(this.TxtDesc_TextChanged);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 64);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(3);
            this.label4.Size = new System.Drawing.Size(195, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Description";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtHi);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtWi);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(195, 22);
            this.panel2.TabIndex = 1;
            // 
            // txtHi
            // 
            this.txtHi.BackColor = System.Drawing.SystemColors.Control;
            this.txtHi.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtHi.Location = new System.Drawing.Point(122, 0);
            this.txtHi.Name = "txtHi";
            this.txtHi.ReadOnly = true;
            this.txtHi.Size = new System.Drawing.Size(37, 20);
            this.txtHi.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(78, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(3);
            this.label2.Size = new System.Drawing.Size(44, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Height";
            // 
            // txtWi
            // 
            this.txtWi.BackColor = System.Drawing.SystemColors.Control;
            this.txtWi.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtWi.Location = new System.Drawing.Point(41, 0);
            this.txtWi.Name = "txtWi";
            this.txtWi.ReadOnly = true;
            this.txtWi.Size = new System.Drawing.Size(37, 20);
            this.txtWi.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3);
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 23);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(3);
            this.label3.Size = new System.Drawing.Size(33, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Size";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFormat);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(195, 23);
            this.panel3.TabIndex = 8;
            // 
            // txtFormat
            // 
            this.txtFormat.BackColor = System.Drawing.SystemColors.Control;
            this.txtFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFormat.Location = new System.Drawing.Point(85, 0);
            this.txtFormat.Name = "txtFormat";
            this.txtFormat.ReadOnly = true;
            this.txtFormat.Size = new System.Drawing.Size(110, 20);
            this.txtFormat.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Graphic Format";
            // 
            // MapViewerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.graphProps);
            this.Name = "MapViewerControl";
            this.Size = new System.Drawing.Size(511, 343);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.graphProps.ResumeLayout(false);
            this.graphProps.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel graphProps;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtWi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtHi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtDesc;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox lstPivots;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox XPoint;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtFormat;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.PictureBox Canvas;
    }
}
