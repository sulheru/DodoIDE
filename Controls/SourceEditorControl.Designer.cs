namespace DodoIDE.Controls
{
    partial class SourceEditorControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceEditorControl));
            this.SourceEditorBox = new ScintillaNET.Scintilla();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.SuspendLayout();
            // 
            // SourceEditorBox
            // 
            this.SourceEditorBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceEditorBox.Location = new System.Drawing.Point(0, 0);
            this.SourceEditorBox.Name = "SourceEditorBox";
            this.SourceEditorBox.Size = new System.Drawing.Size(627, 383);
            this.SourceEditorBox.TabIndex = 0;
            this.SourceEditorBox.Click += new System.EventHandler(this.SourceEditorBox_Click);
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autocompleteMenu1.Colors")));
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // SourceEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SourceEditorBox);
            this.Name = "SourceEditorControl";
            this.Size = new System.Drawing.Size(627, 383);
            this.ResumeLayout(false);

        }

        #endregion

        public ScintillaNET.Scintilla SourceEditorBox;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
    }
}
