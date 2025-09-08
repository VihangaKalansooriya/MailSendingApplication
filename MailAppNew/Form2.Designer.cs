namespace MailAppNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            btn_SelectAll = new Button();
            btn_DeselectAll = new Button();
            btn_Refresh = new Button();
            btn_Send = new Button();
            dateTimePicker1 = new DateTimePicker();
            label1 = new Label();
            RB_01 = new RadioButton();
            RB_02 = new RadioButton();
            txt_Search = new TextBox();
            LBL_copyright = new Label();
            btn_search = new Button();
            pictureBox2 = new PictureBox();
            dataGridView1 = new DataGridView();
            label2 = new Label();
            button1 = new Button();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btn_SelectAll
            // 
            btn_SelectAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btn_SelectAll.Location = new Point(78, 561);
            btn_SelectAll.Margin = new Padding(3, 4, 3, 4);
            btn_SelectAll.Name = "btn_SelectAll";
            btn_SelectAll.Size = new Size(146, 40);
            btn_SelectAll.TabIndex = 1;
            btn_SelectAll.Text = "Select All";
            btn_SelectAll.UseVisualStyleBackColor = true;
            btn_SelectAll.Click += btn_SelectAll_Click;
            // 
            // btn_DeselectAll
            // 
            btn_DeselectAll.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btn_DeselectAll.Location = new Point(311, 561);
            btn_DeselectAll.Margin = new Padding(3, 4, 3, 4);
            btn_DeselectAll.Name = "btn_DeselectAll";
            btn_DeselectAll.Size = new Size(146, 40);
            btn_DeselectAll.TabIndex = 2;
            btn_DeselectAll.Text = "Deselect All";
            btn_DeselectAll.UseVisualStyleBackColor = true;
            btn_DeselectAll.Click += btn_DeselectAll_Click;
            // 
            // btn_Refresh
            // 
            btn_Refresh.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Refresh.Location = new Point(553, 561);
            btn_Refresh.Margin = new Padding(3, 4, 3, 4);
            btn_Refresh.Name = "btn_Refresh";
            btn_Refresh.Size = new Size(146, 40);
            btn_Refresh.TabIndex = 3;
            btn_Refresh.Text = "Refresh";
            btn_Refresh.UseVisualStyleBackColor = true;
            btn_Refresh.Click += btn_Refresh_Click;
            // 
            // btn_Send
            // 
            btn_Send.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            btn_Send.Location = new Point(794, 561);
            btn_Send.Margin = new Padding(3, 4, 3, 4);
            btn_Send.Name = "btn_Send";
            btn_Send.Size = new Size(146, 40);
            btn_Send.TabIndex = 4;
            btn_Send.Text = "Send";
            btn_Send.UseVisualStyleBackColor = true;
            btn_Send.Click += btn_Send_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarFont = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePicker1.Cursor = Cursors.Hand;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(98, 159);
            dateTimePicker1.Margin = new Padding(6, 7, 6, 7);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(126, 29);
            dateTimePicker1.TabIndex = 13;
            dateTimePicker1.Value = new DateTime(2024, 3, 20, 0, 0, 0, 0);
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(46, 161);
            label1.Name = "label1";
            label1.Size = new Size(58, 23);
            label1.TabIndex = 6;
            label1.Text = "Date :";
            // 
            // RB_01
            // 
            RB_01.AutoSize = true;
            RB_01.Cursor = Cursors.Hand;
            RB_01.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            RB_01.Location = new Point(273, 161);
            RB_01.Margin = new Padding(3, 4, 3, 4);
            RB_01.Name = "RB_01";
            RB_01.Size = new Size(71, 27);
            RB_01.TabIndex = 7;
            RB_01.TabStop = true;
            RB_01.Text = "Send";
            RB_01.UseVisualStyleBackColor = true;
            RB_01.CheckedChanged += RB_01_CheckedChanged;
            // 
            // RB_02
            // 
            RB_02.AutoSize = true;
            RB_02.Cursor = Cursors.Hand;
            RB_02.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            RB_02.Location = new Point(353, 163);
            RB_02.Margin = new Padding(3, 4, 3, 4);
            RB_02.Name = "RB_02";
            RB_02.Size = new Size(90, 27);
            RB_02.TabIndex = 8;
            RB_02.TabStop = true;
            RB_02.Text = "Unsend";
            RB_02.UseVisualStyleBackColor = true;
            RB_02.CheckedChanged += RB_02_CheckedChanged;
            // 
            // txt_Search
            // 
            txt_Search.BorderStyle = BorderStyle.None;
            txt_Search.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            txt_Search.Location = new Point(611, 164);
            txt_Search.Margin = new Padding(3, 4, 3, 4);
            txt_Search.Name = "txt_Search";
            txt_Search.Size = new Size(320, 21);
            txt_Search.TabIndex = 9;
            // 
            // LBL_copyright
            // 
            LBL_copyright.AutoSize = true;
            LBL_copyright.FlatStyle = FlatStyle.Flat;
            LBL_copyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            LBL_copyright.Location = new Point(827, 634);
            LBL_copyright.Name = "LBL_copyright";
            LBL_copyright.Size = new Size(199, 23);
            LBL_copyright.TabIndex = 14;
            LBL_copyright.Text = "© 24x7 Retail Solutions";
            // 
            // btn_search
            // 
            btn_search.BackColor = Color.Transparent;
            btn_search.BackgroundImage = (Image)resources.GetObject("btn_search.BackgroundImage");
            btn_search.BackgroundImageLayout = ImageLayout.Zoom;
            btn_search.Cursor = Cursors.Hand;
            btn_search.FlatStyle = FlatStyle.Flat;
            btn_search.ForeColor = Color.Transparent;
            btn_search.Location = new Point(934, 163);
            btn_search.Margin = new Padding(3, 4, 3, 4);
            btn_search.Name = "btn_search";
            btn_search.Size = new Size(29, 24);
            btn_search.TabIndex = 18;
            btn_search.UseVisualStyleBackColor = false;
            btn_search.Click += btn_search_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(593, 141);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(378, 65);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 20;
            pictureBox2.TabStop = false;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(39, 203);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(947, 344);
            dataGridView1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(469, 163);
            label2.Name = "label2";
            label2.Size = new Size(131, 23);
            label2.TabIndex = 21;
            label2.Text = "Receiver Email:";
            // 
            // button1
            // 
            button1.Image = (Image)resources.GetObject("button1.Image");
            button1.Location = new Point(0, 1);
            button1.Name = "button1";
            button1.Size = new Size(75, 78);
            button1.TabIndex = 26;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(332, 22);
            label4.Name = "label4";
            label4.Size = new Size(406, 57);
            label4.TabIndex = 27;
            label4.Text = "Transactional Email";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1026, 657);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(label2);
            Controls.Add(btn_search);
            Controls.Add(LBL_copyright);
            Controls.Add(txt_Search);
            Controls.Add(RB_02);
            Controls.Add(RB_01);
            Controls.Add(label1);
            Controls.Add(dateTimePicker1);
            Controls.Add(btn_Send);
            Controls.Add(btn_Refresh);
            Controls.Add(btn_DeselectAll);
            Controls.Add(btn_SelectAll);
            Controls.Add(pictureBox2);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Transaction Email";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void InitializeButtonDesign()
        {
            // Set initial btn_selectall design
            btn_SelectAll.BackColor = Color.White;
            btn_SelectAll.ForeColor = Color.Black;
            btn_SelectAll.Font = new Font("Arial", 9, FontStyle.Bold);
            btn_SelectAll.FlatStyle = FlatStyle.Flat;

            // Set initial btn_deselectall design
            btn_DeselectAll.BackColor = Color.White;
            btn_DeselectAll.ForeColor = Color.Black;
            btn_DeselectAll.Font = new Font("Arial", 9, FontStyle.Bold);
            btn_DeselectAll.FlatStyle = FlatStyle.Flat;

            // Set initial btn_refresh design
            btn_Refresh.BackColor = Color.White;
            btn_Refresh.ForeColor = Color.Black;
            btn_Refresh.Font = new Font("Arial", 9, FontStyle.Bold);
            btn_Refresh.FlatStyle = FlatStyle.Flat;

            // Set initial btn_send design
            btn_Send.BackColor = Color.White;
            btn_Send.ForeColor = Color.Black;
            btn_Send.Font = new Font("Arial", 9, FontStyle.Bold);
            btn_Send.FlatStyle = FlatStyle.Flat;

            btn_SelectAll.MouseEnter += btn_SelectAll_MouseEnter;
            btn_SelectAll.MouseLeave += btn_SelectAll_MouseLeave;
            btn_DeselectAll.MouseEnter += btn_DeselectAll_MouseEnter;
            btn_DeselectAll.MouseLeave += btn_DeselectAll_MouseLeave;
            btn_Refresh.MouseEnter += btn_Refresh_MouseEnter;
            btn_Refresh.MouseLeave += btn_Refresh_MouseLeave;
            btn_Send.MouseEnter += btn_Send_MouseEnter;
            btn_Send.MouseLeave += btn_Send_MouseLeave;
        }
        private void btn_SelectAll_MouseEnter(object sender, EventArgs e)
        {
            btn_SelectAll.BackColor = Color.Blue;
        }

        private void btn_SelectAll_MouseLeave(object sender, EventArgs e)
        {
            btn_SelectAll.BackColor = Color.White;
        }
        private void btn_DeselectAll_MouseEnter(object sender, EventArgs e)
        {
            btn_DeselectAll.BackColor = Color.Blue;
        }

        private void btn_DeselectAll_MouseLeave(object sender, EventArgs e)
        {
            btn_DeselectAll.BackColor = Color.White;
        }
        private void btn_Refresh_MouseEnter(object sender, EventArgs e)
        {
            btn_Refresh.BackColor = Color.Blue;
        }

        private void btn_Refresh_MouseLeave(object sender, EventArgs e)
        {
            btn_Refresh.BackColor = Color.White;
        }
        private void btn_Send_MouseEnter(object sender, EventArgs e)
        {
            btn_Send.BackColor = Color.Blue;
        }

        private void btn_Send_MouseLeave(object sender, EventArgs e)
        {
            btn_Send.BackColor = Color.White;
        }

#endregion
        private Button btn_SelectAll;
        private Button btn_DeselectAll;
        private Button btn_Refresh;
        private Button btn_Send;
        private DateTimePicker dateTimePicker1;
        private Label label1;
        private RadioButton RB_01;
        private RadioButton RB_02;
        private TextBox txt_Search;
        private Label LBL_copyright;
        private Button btn_search;
        private PictureBox pictureBox2;
        private DataGridView dataGridView1;
        private Label label2;
        private Button button1;
        private Label label4;
    }
}