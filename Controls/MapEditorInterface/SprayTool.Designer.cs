namespace DodoIDE.Controls.MapEditorInterface
{
    partial class SprayTool
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
            this.trSize = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trSize)).BeginInit();
            this.SuspendLayout();
            // 
            // trSize
            // 
            this.trSize.Dock = System.Windows.Forms.DockStyle.Top;
            this.trSize.Location = new System.Drawing.Point(0, 0);
            this.trSize.Name = "trSize";
            this.trSize.Size = new System.Drawing.Size(150, 45);
            this.trSize.TabIndex = 0;
            // 
            // SprayTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trSize);
            this.Name = "SprayTool";
            ((System.ComponentModel.ISupportInitialize)(this.trSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trSize;
    }
}
