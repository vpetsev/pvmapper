namespace PVDESKTOP
{
    partial class frmAlignmentBoundry
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
            this.btnCanceAlignment = new System.Windows.Forms.Button();
            this.btnRedrawAlignment = new System.Windows.Forms.Button();
            this.btnSaveAlignment = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCanceAlignment
            // 
            this.btnCanceAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCanceAlignment.Location = new System.Drawing.Point(30, 80);
            this.btnCanceAlignment.Name = "btnCanceAlignment";
            this.btnCanceAlignment.Size = new System.Drawing.Size(75, 23);
            this.btnCanceAlignment.TabIndex = 7;
            this.btnCanceAlignment.Text = "Cancel";
            this.btnCanceAlignment.UseVisualStyleBackColor = true;
            // 
            // btnRedrawAlignment
            // 
            this.btnRedrawAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRedrawAlignment.Location = new System.Drawing.Point(111, 80);
            this.btnRedrawAlignment.Name = "btnRedrawAlignment";
            this.btnRedrawAlignment.Size = new System.Drawing.Size(75, 23);
            this.btnRedrawAlignment.TabIndex = 6;
            this.btnRedrawAlignment.Text = "Redraw";
            this.btnRedrawAlignment.UseVisualStyleBackColor = true;
            this.btnRedrawAlignment.Click += new System.EventHandler(this.btnRedrawAlignment_Click);
            // 
            // btnSaveAlignment
            // 
            this.btnSaveAlignment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAlignment.Location = new System.Drawing.Point(192, 80);
            this.btnSaveAlignment.Name = "btnSaveAlignment";
            this.btnSaveAlignment.Size = new System.Drawing.Size(90, 23);
            this.btnSaveAlignment.TabIndex = 5;
            this.btnSaveAlignment.Text = "Save and Close";
            this.btnSaveAlignment.UseVisualStyleBackColor = true;
            this.btnSaveAlignment.Click += new System.EventHandler(this.btnSaveAlignment_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(281, 52);
            this.label1.TabIndex = 4;
            this.label1.Text = "While this window is open, draw as many lines on the \r\nmap as needed. Lines are l" +
    "imited to two points. There \r\nis no need to right click, lines automatically wil" +
    "l be created \r\nafter second click. \r\n";
            // 
            // frmAlignmentBoundry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 115);
            this.Controls.Add(this.btnCanceAlignment);
            this.Controls.Add(this.btnRedrawAlignment);
            this.Controls.Add(this.btnSaveAlignment);
            this.Controls.Add(this.label1);
            this.Name = "frmAlignmentBoundry";
            this.Text = "Draw Alignments (lines)";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCanceAlignment;
        private System.Windows.Forms.Button btnRedrawAlignment;
        private System.Windows.Forms.Button btnSaveAlignment;
        private System.Windows.Forms.Label label1;
    }
}