namespace PVDESKTOP
{
    partial class frmMovePanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovePanel));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveMove = new System.Windows.Forms.Button();
            this.btnCancelMove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 195);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // btnSaveMove
            // 
            this.btnSaveMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveMove.Location = new System.Drawing.Point(193, 201);
            this.btnSaveMove.Name = "btnSaveMove";
            this.btnSaveMove.Size = new System.Drawing.Size(75, 23);
            this.btnSaveMove.TabIndex = 1;
            this.btnSaveMove.Text = "Save Changes";
            this.btnSaveMove.UseVisualStyleBackColor = true;
            this.btnSaveMove.Click += new System.EventHandler(this.btnSaveMove_Click);
            // 
            // btnCancelMove
            // 
            this.btnCancelMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelMove.Location = new System.Drawing.Point(112, 201);
            this.btnCancelMove.Name = "btnCancelMove";
            this.btnCancelMove.Size = new System.Drawing.Size(75, 23);
            this.btnCancelMove.TabIndex = 2;
            this.btnCancelMove.Text = "Cancel";
            this.btnCancelMove.UseVisualStyleBackColor = true;
            this.btnCancelMove.Click += new System.EventHandler(this.btnCancelMove_Click);
            // 
            // frmMovePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 236);
            this.Controls.Add(this.btnCancelMove);
            this.Controls.Add(this.btnSaveMove);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMovePanel";
            this.Text = "Advandced Panel Editing";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveMove;
        private System.Windows.Forms.Button btnCancelMove;
    }
}