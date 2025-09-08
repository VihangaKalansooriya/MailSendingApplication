namespace MailAppNew
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            LBL_Copyright = new Label();
            LBL_Version = new Label();
            pictureBox1 = new PictureBox();
            status = new Label();
            label1 = new Label();
            LBL_database = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(12, 205);
            button1.Name = "button1";
            button1.Size = new Size(375, 57);
            button1.TabIndex = 0;
            button1.Text = "Promotional SMS";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(427, 205);
            button2.Name = "button2";
            button2.Size = new Size(375, 57);
            button2.TabIndex = 1;
            button2.Text = "Annivasary SMS";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(12, 299);
            button3.Name = "button3";
            button3.Size = new Size(375, 57);
            button3.TabIndex = 2;
            button3.Text = "Birthday SMS";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(427, 299);
            button4.Name = "button4";
            button4.Size = new Size(375, 57);
            button4.TabIndex = 3;
            button4.Text = "Transaction Email";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(225, 396);
            button5.Name = "button5";
            button5.Size = new Size(375, 57);
            button5.TabIndex = 4;
            button5.Text = "Promotional Email";
            button5.UseVisualStyleBackColor = true;
            // 
            // LBL_Copyright
            // 
            LBL_Copyright.AutoSize = true;
            LBL_Copyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LBL_Copyright.Location = new Point(-1, 486);
            LBL_Copyright.Name = "LBL_Copyright";
            LBL_Copyright.Size = new Size(199, 23);
            LBL_Copyright.TabIndex = 5;
            LBL_Copyright.Text = "© 24x7 Retail Solutions";
            // 
            // LBL_Version
            // 
            LBL_Version.AutoSize = true;
            LBL_Version.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LBL_Version.Location = new Point(750, 486);
            LBL_Version.Name = "LBL_Version";
            LBL_Version.Size = new Size(66, 23);
            LBL_Version.TabIndex = 6;
            LBL_Version.Text = "V 1.0.0";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(283, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(265, 122);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // status
            // 
            status.AutoSize = true;
            status.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            status.ForeColor = Color.Green;
            status.Location = new Point(619, 485);
            status.Name = "status";
            status.Size = new Size(93, 23);
            status.TabIndex = 8;
            status.Text = "Connected";
            status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label1.Location = new Point(450, 485);
            label1.Name = "label1";
            label1.Size = new Size(184, 23);
            label1.TabIndex = 9;
            label1.Text = "Network Connection :";
            // 
            // LBL_database
            // 
            LBL_database.AutoSize = true;
            LBL_database.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            LBL_database.Location = new Point(292, 489);
            LBL_database.Name = "LBL_database";
            LBL_database.RightToLeft = RightToLeft.No;
            LBL_database.Size = new Size(53, 20);
            LBL_database.TabIndex = 10;
            LBL_database.Text = "Server";
            LBL_database.Click += LBL_database_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(204, 485);
            label2.Name = "label2";
            label2.Size = new Size(93, 23);
            label2.TabIndex = 11;
            label2.Text = "Server ID :";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(816, 515);
            Controls.Add(label2);
            Controls.Add(LBL_database);
            Controls.Add(label1);
            Controls.Add(status);
            Controls.Add(pictureBox1);
            Controls.Add(LBL_Version);
            Controls.Add(LBL_Copyright);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label LBL_Copyright;
        private Label LBL_Version;
        private PictureBox pictureBox1;
        private Label status;
        private Label label1;
        private Label LBL_database;
        private Label label2;
    }
}