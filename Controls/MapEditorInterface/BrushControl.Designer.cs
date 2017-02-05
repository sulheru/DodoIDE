namespace DodoIDE.Controls
{
    partial class BrushControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrushControl));
            this.imgBrushes = new System.Windows.Forms.ImageList(this.components);
            this.listBrushes = new System.Windows.Forms.ListView();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // imgBrushes
            // 
            this.imgBrushes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgBrushes.ImageStream")));
            this.imgBrushes.TransparentColor = System.Drawing.Color.Transparent;
            this.imgBrushes.Images.SetKeyName(0, "Brush001.png");
            // 
            // listBrushes
            // 
            this.listBrushes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBrushes.LargeImageList = this.imgBrushes;
            this.listBrushes.Location = new System.Drawing.Point(0, 0);
            this.listBrushes.Name = "listBrushes";
            this.listBrushes.Size = new System.Drawing.Size(324, 287);
            this.listBrushes.TabIndex = 0;
            this.listBrushes.UseCompatibleStateImageBehavior = false;
            this.listBrushes.View = System.Windows.Forms.View.Tile;
            this.listBrushes.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar1.Location = new System.Drawing.Point(0, 287);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(324, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.Value = 20;
            // 
            // BrushControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBrushes);
            this.Controls.Add(this.trackBar1);
            this.Name = "BrushControl";
            this.Size = new System.Drawing.Size(324, 332);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imgBrushes;
        private System.Windows.Forms.ListView listBrushes;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}
