namespace MailAppNew
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            LBL_Copyright = new Label();
            LBL_Version = new Label();
            mainIMG = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)mainIMG).BeginInit();
            SuspendLayout();
            // 
            // LBL_Copyright
            // 
            LBL_Copyright.AutoSize = true;
            LBL_Copyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LBL_Copyright.Location = new Point(429, 403);
            LBL_Copyright.Name = "LBL_Copyright";
            LBL_Copyright.Size = new Size(152, 17);
            LBL_Copyright.TabIndex = 0;
            LBL_Copyright.Text = "© 24x7 Retail Solutions";
            // 
            // LBL_Version
            // 
            LBL_Version.AutoSize = true;
            LBL_Version.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LBL_Version.Location = new Point(531, 420);
            LBL_Version.Name = "LBL_Version";
            LBL_Version.Size = new Size(50, 17);
            LBL_Version.TabIndex = 1;
            LBL_Version.Text = "V 1.0.0";
            // 
            // mainIMG
            // 
            mainIMG.Image = (Image)resources.GetObject("mainIMG.Image");
            mainIMG.Location = new Point(-1, -3);
            mainIMG.Name = "mainIMG";
            mainIMG.Size = new Size(587, 447);
            mainIMG.SizeMode = PictureBoxSizeMode.Zoom;
            mainIMG.TabIndex = 2;
            mainIMG.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 440);
            ControlBox = false;
            Controls.Add(LBL_Version);
            Controls.Add(LBL_Copyright);
            Controls.Add(mainIMG);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
            MdiChildrenMinimizedAnchorBottom = false;
            MinimizeBox = false;
            Name = "Form1";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)mainIMG).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LBL_Copyright;
        private Label LBL_Version;
        private PictureBox mainIMG;
    }
}
