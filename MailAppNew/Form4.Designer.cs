namespace MailAppNew
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            label1 = new Label();
            button1 = new Button();
            textBox1 = new TextBox();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            textBox2 = new TextBox();
            button3 = new Button();
            cmbSearchField = new ComboBox();
            button4 = new Button();
            button5 = new Button();
            label3 = new Label();
            button6 = new Button();
            textBox3 = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            label4 = new Label();
            label5 = new Label();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            button7 = new Button();
            button8 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25.8000011F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(312, -3);
            label1.Name = "label1";
            label1.Size = new Size(389, 60);
            label1.TabIndex = 0;
            label1.Text = "Promotional SMS";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(12, 9);
            button1.Name = "button1";
            button1.Size = new Size(75, 78);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(119, 92);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(343, 164);
            textBox1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.HotTrack;
            label2.Location = new Point(119, 66);
            label2.Name = "label2";
            label2.Size = new Size(214, 23);
            label2.TabIndex = 3;
            label2.Text = "Type Your Massage Here :";
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.Control;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(119, 363);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(772, 372);
            dataGridView1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(781, 748);
            button2.Name = "button2";
            button2.Size = new Size(110, 34);
            button2.TabIndex = 5;
            button2.Text = "Send";
            button2.UseVisualStyleBackColor = true;
            button2.Click += this.button2_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(119, 266);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(247, 27);
            textBox2.TabIndex = 6;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(529, 267);
            button3.Name = "button3";
            button3.Size = new Size(110, 35);
            button3.TabIndex = 7;
            button3.Text = "Search";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // cmbSearchField
            // 
            cmbSearchField.FormattingEnabled = true;
            cmbSearchField.Items.AddRange(new object[] { "Customer Code", "Customer Group", "Customer Name", "Ref Code", "Mobile Number" });
            cmbSearchField.Location = new Point(372, 266);
            cmbSearchField.Name = "cmbSearchField";
            cmbSearchField.Size = new Size(151, 28);
            cmbSearchField.TabIndex = 8;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(781, 266);
            button4.Name = "button4";
            button4.Size = new Size(110, 35);
            button4.TabIndex = 9;
            button4.Text = "Select All";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(781, 322);
            button5.Name = "button5";
            button5.Size = new Size(110, 35);
            button5.TabIndex = 10;
            button5.Text = "Deselect All";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(837, 792);
            label3.Name = "label3";
            label3.Size = new Size(175, 20);
            label3.TabIndex = 11;
            label3.Text = "@ 24x7 Retail Solutions";
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(475, 92);
            button6.Name = "button6";
            button6.Size = new Size(64, 42);
            button6.TabIndex = 13;
            button6.Text = "UN";
            button6.UseVisualStyleBackColor = false;
            button6.Click += button6_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(549, 92);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(343, 164);
            textBox3.TabIndex = 14;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            checkBox1.Location = new Point(258, 303);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(76, 24);
            checkBox1.TabIndex = 15;
            checkBox1.Text = "Active";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            checkBox2.Location = new Point(340, 303);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(88, 24);
            checkBox2.TabIndex = 16;
            checkBox2.Text = "Inactive";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label4.Location = new Point(119, 303);
            label4.Name = "label4";
            label4.Size = new Size(135, 20);
            label4.TabIndex = 17;
            label4.Text = "Customer Status :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            label5.Location = new Point(119, 329);
            label5.Name = "label5";
            label5.Size = new Size(135, 20);
            label5.TabIndex = 18;
            label5.Text = "Loyalty  Status    :";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            checkBox3.Location = new Point(257, 333);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(76, 24);
            checkBox3.TabIndex = 19;
            checkBox3.Text = "Active";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            checkBox4.Location = new Point(340, 333);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(88, 24);
            checkBox4.TabIndex = 20;
            checkBox4.Text = "Inactive";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ActiveCaption;
            button7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(475, 151);
            button7.Name = "button7";
            button7.Size = new Size(64, 42);
            button7.TabIndex = 21;
            button7.Text = "ABC";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.BackColor = SystemColors.ActiveCaption;
            button8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            button8.Location = new Point(475, 214);
            button8.Name = "button8";
            button8.Size = new Size(64, 42);
            button8.TabIndex = 22;
            button8.Text = "Clear";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1013, 815);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(checkBox4);
            Controls.Add(checkBox3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(checkBox1);
            Controls.Add(textBox3);
            Controls.Add(button6);
            Controls.Add(label3);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(cmbSearchField);
            Controls.Add(button3);
            Controls.Add(textBox2);
            Controls.Add(button2);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(checkBox2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Promotional SMS";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeButtonDesign()
        {
            // Set initial button4 design
            button4.BackColor = Color.White;
            button4.ForeColor = Color.Black;
            button4.Font = new Font("Arial", 9, FontStyle.Bold);
            button4.FlatStyle = FlatStyle.Flat;

            // Set initial button5 design
            button5.BackColor = Color.White;
            button5.ForeColor = Color.Black;
            button5.Font = new Font("Arial", 9, FontStyle.Bold);
            button5.FlatStyle = FlatStyle.Flat;

            // Set initial button2 design
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            button2.Font = new Font("Arial", 9, FontStyle.Bold);
            button2.FlatStyle = FlatStyle.Flat;

            // Set initial button3 design
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Black;
            button3.Font = new Font("Arial", 9, FontStyle.Bold);
            button3.FlatStyle = FlatStyle.Flat;


            button4.MouseEnter += button4_MouseEnter;
            button4.MouseLeave += button4_MouseLeave;
            button5.MouseEnter += button5_MouseEnter;
            button5.MouseLeave += button5_MouseLeave;
            button2.MouseEnter += button2_MouseEnter;
            button2.MouseLeave += button2_MouseLeave;
            button3.MouseEnter += button3_MouseEnter;
            button3.MouseLeave += button3_MouseLeave;
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            button4.BackColor = Color.Blue;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.White;
        }
        #endregion

        private void button5_MouseEnter(object sender, EventArgs e)
        {
            button5.BackColor = Color.Blue;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.White;
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.BackColor = Color.Blue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.BackColor = Color.Blue;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.White;
        }

        private Label label1;
        private Button button1;
        private TextBox textBox1;
        private Label label2;
        private DataGridView dataGridView1;
        private Button button2;
        private TextBox textBox2;
        private Button button3;
        private ComboBox cmbSearchField;
        private Button button4;
        private Button button5;
        private Label label3;
        private Button button6;
        private TextBox textBox3;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private Label label4;
        private Label label5;
        private CheckBox checkBox3;
        private CheckBox checkBox4;
        private Button button7;
        private Button button8;
    }
}