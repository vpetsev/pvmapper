namespace PVDESKTOP
{
    partial class Help
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.picHelpBox = new System.Windows.Forms.PictureBox();
            this.btnCloseHelper = new System.Windows.Forms.Button();
            this.btnNextHelp = new System.Windows.Forms.Button();
            this.btnPreviousHelp = new System.Windows.Forms.Button();
            this.imageListHelp = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHelpBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.picHelpBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnCloseHelper);
            this.splitContainer1.Panel2.Controls.Add(this.btnNextHelp);
            this.splitContainer1.Panel2.Controls.Add(this.btnPreviousHelp);
            this.splitContainer1.Size = new System.Drawing.Size(189, 300);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.TabIndex = 0;
            // 
            // picHelpBox
            // 
            this.picHelpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picHelpBox.Image = ((System.Drawing.Image)(resources.GetObject("picHelpBox.Image")));
            this.picHelpBox.Location = new System.Drawing.Point(0, 0);
            this.picHelpBox.Name = "picHelpBox";
            this.picHelpBox.Size = new System.Drawing.Size(189, 264);
            this.picHelpBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picHelpBox.TabIndex = 0;
            this.picHelpBox.TabStop = false;
            // 
            // btnCloseHelper
            // 
            this.btnCloseHelper.Location = new System.Drawing.Point(129, 3);
            this.btnCloseHelper.Name = "btnCloseHelper";
            this.btnCloseHelper.Size = new System.Drawing.Size(53, 23);
            this.btnCloseHelper.TabIndex = 3;
            this.btnCloseHelper.Text = "Close";
            this.btnCloseHelper.UseVisualStyleBackColor = true;
            this.btnCloseHelper.Click += new System.EventHandler(this.btnCloseHelper_Click);
            // 
            // btnNextHelp
            // 
            this.btnNextHelp.Location = new System.Drawing.Point(63, 3);
            this.btnNextHelp.Name = "btnNextHelp";
            this.btnNextHelp.Size = new System.Drawing.Size(42, 23);
            this.btnNextHelp.TabIndex = 2;
            this.btnNextHelp.Text = "Next";
            this.btnNextHelp.UseVisualStyleBackColor = true;
            // 
            // btnPreviousHelp
            // 
            this.btnPreviousHelp.Location = new System.Drawing.Point(2, 3);
            this.btnPreviousHelp.Name = "btnPreviousHelp";
            this.btnPreviousHelp.Size = new System.Drawing.Size(59, 23);
            this.btnPreviousHelp.TabIndex = 1;
            this.btnPreviousHelp.Text = "Previous";
            this.btnPreviousHelp.UseVisualStyleBackColor = true;
            // 
            // imageListHelp
            // 
            this.imageListHelp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListHelp.ImageStream")));
            this.imageListHelp.TransparentColor = System.Drawing.Color.Silver;
            this.imageListHelp.Images.SetKeyName(0, "HelpStep1.PNG");
            this.imageListHelp.Images.SetKeyName(1, "Help Step 2.PNG");
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(180, 300);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Help";
            this.Size = new System.Drawing.Size(189, 300);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHelpBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.Button btnNextHelp;
        public System.Windows.Forms.Button btnPreviousHelp;
        public System.Windows.Forms.ImageList imageListHelp;
        public System.Windows.Forms.PictureBox picHelpBox;
        private System.Windows.Forms.Button btnCloseHelper;
    }
}
