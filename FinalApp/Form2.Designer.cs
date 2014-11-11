namespace FinalApp
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.location = new System.Windows.Forms.TextBox();
            this.maps = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Location Spoke";
            // 
            // location
            // 
            this.location.Location = new System.Drawing.Point(102, 13);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(926, 20);
            this.location.TabIndex = 1;
            this.location.TextChanged += new System.EventHandler(this.location_TextChanged);
            // 
            // maps
            // 
            this.maps.Location = new System.Drawing.Point(16, 39);
            this.maps.MinimumSize = new System.Drawing.Size(20, 20);
            this.maps.Name = "maps";
            this.maps.Size = new System.Drawing.Size(1012, 557);
            this.maps.TabIndex = 4;
            this.maps.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1040, 608);
            this.Controls.Add(this.maps);
            this.Controls.Add(this.location);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form2_FormClosed);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox location;
        private System.Windows.Forms.WebBrowser maps;
    }
}