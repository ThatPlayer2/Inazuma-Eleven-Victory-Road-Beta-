namespace InazumaElevenVictoryRoad
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            tabControl2 = new TabControl();
            tabPage2 = new TabPage();
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            tabPage3 = new TabPage();
            dataGridView2 = new DataGridView();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            tabPage4 = new TabPage();
            button5 = new Button();
            button2 = new Button();
            button1 = new Button();
            button3 = new Button();
            Unlcock_Alius = new Button();
            comboBoxMove2 = new ComboBox();
            comboBoxMove1 = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            languageToolStripMenuItem = new ToolStripMenuItem();
            italianToolStripMenuItem = new ToolStripMenuItem();
            englishToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            spiritToolStripMenuItem = new ToolStripMenuItem();
            itemEditToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutSaveEditorToolStripMenuItem = new ToolStripMenuItem();
            tabPage1 = new TabPage();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label4 = new Label();
            numericUpDown2 = new NumericUpDown();
            numericUpDown1 = new NumericUpDown();
            label3 = new Label();
            label2 = new Label();
            tabControl1 = new TabControl();
            tabControl2.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage4.SuspendLayout();
            menuStrip1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl2
            // 
            tabControl2.Controls.Add(tabPage2);
            tabControl2.Controls.Add(tabPage3);
            tabControl2.Controls.Add(tabPage4);
            tabControl2.Location = new Point(274, 40);
            tabControl2.Name = "tabControl2";
            tabControl2.SelectedIndex = 0;
            tabControl2.Size = new Size(462, 356);
            tabControl2.TabIndex = 2;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(454, 328);
            tabPage2.TabIndex = 0;
            tabPage2.Text = "Players";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4 });
            dataGridView1.Location = new Point(0, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(451, 325);
            dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "ID";
            Column1.Name = "Column1";
            Column1.Width = 120;
            // 
            // Column2
            // 
            Column2.HeaderText = "Name";
            Column2.Name = "Column2";
            Column2.Width = 160;
            // 
            // Column3
            // 
            Column3.HeaderText = "Level";
            Column3.Name = "Column3";
            Column3.Width = 60;
            // 
            // Column4
            // 
            Column4.HeaderText = "Grade";
            Column4.Name = "Column4";
            Column4.Width = 60;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView2);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(454, 328);
            tabPage3.TabIndex = 1;
            tabPage3.Text = "Tokens";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.BackgroundColor = SystemColors.ControlLightLight;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Columns.AddRange(new DataGridViewColumn[] { Column5, Column6, Column7 });
            dataGridView2.Location = new Point(3, 3);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(455, 329);
            dataGridView2.TabIndex = 0;
            // 
            // Column5
            // 
            Column5.HeaderText = "ID";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Resizable = DataGridViewTriState.False;
            Column5.Width = 80;
            // 
            // Column6
            // 
            Column6.HeaderText = "Name";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Resizable = DataGridViewTriState.False;
            Column6.Width = 265;
            // 
            // Column7
            // 
            Column7.HeaderText = "Quantity";
            Column7.Name = "Column7";
            Column7.Resizable = DataGridViewTriState.False;
            Column7.Width = 60;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(button5);
            tabPage4.Controls.Add(button2);
            tabPage4.Controls.Add(button1);
            tabPage4.Controls.Add(button3);
            tabPage4.Controls.Add(Unlcock_Alius);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(454, 328);
            tabPage4.TabIndex = 2;
            tabPage4.Text = "Sav";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(93, 137);
            button5.Name = "button5";
            button5.Size = new Size(80, 38);
            button5.TabIndex = 9;
            button5.Text = "Unlock All Spirit";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // button2
            // 
            button2.Location = new Point(179, 137);
            button2.Name = "button2";
            button2.Size = new Size(77, 38);
            button2.TabIndex = 8;
            button2.Text = "Unlock Victory Star";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderSize = 0;
            button1.Location = new Point(6, 20);
            button1.Name = "button1";
            button1.Size = new Size(71, 40);
            button1.TabIndex = 3;
            button1.Text = "Max Rank";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button3
            // 
            button3.Location = new Point(83, 20);
            button3.Name = "button3";
            button3.Size = new Size(101, 40);
            button3.TabIndex = 6;
            button3.Text = "999 Victory Gallery";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // Unlcock_Alius
            // 
            Unlcock_Alius.Location = new Point(6, 137);
            Unlcock_Alius.Name = "Unlcock_Alius";
            Unlcock_Alius.Size = new Size(81, 38);
            Unlcock_Alius.TabIndex = 7;
            Unlcock_Alius.Text = "Unlock Alius Academy";
            Unlcock_Alius.UseVisualStyleBackColor = true;
            Unlcock_Alius.Click += Button4_Click;
            // 
            // comboBoxMove2
            // 
            comboBoxMove2.FormattingEnabled = true;
            comboBoxMove2.Location = new Point(76, 198);
            comboBoxMove2.Name = "comboBoxMove2";
            comboBoxMove2.Size = new Size(163, 23);
            comboBoxMove2.TabIndex = 5;
            // 
            // comboBoxMove1
            // 
            comboBoxMove1.FormattingEnabled = true;
            comboBoxMove1.Location = new Point(76, 152);
            comboBoxMove1.Name = "comboBoxMove1";
            comboBoxMove1.Size = new Size(163, 23);
            comboBoxMove1.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 206);
            label6.Name = "label6";
            label6.Size = new Size(46, 15);
            label6.TabIndex = 1;
            label6.Text = "Move 2";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 155);
            label5.Name = "label5";
            label5.Size = new Size(46, 15);
            label5.TabIndex = 0;
            label5.Text = "Move 1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, optionsToolStripMenuItem, editToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(748, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click_1;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(103, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { languageToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // languageToolStripMenuItem
            // 
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { italianToolStripMenuItem, englishToolStripMenuItem });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            languageToolStripMenuItem.Size = new Size(126, 22);
            languageToolStripMenuItem.Text = "Language";
            // 
            // italianToolStripMenuItem
            // 
            italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            italianToolStripMenuItem.Size = new Size(112, 22);
            italianToolStripMenuItem.Text = "Italian";
            italianToolStripMenuItem.Click += ItalianToolStripMenuItem_Click;
            // 
            // englishToolStripMenuItem
            // 
            englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            englishToolStripMenuItem.Size = new Size(112, 22);
            englishToolStripMenuItem.Text = "English";
            englishToolStripMenuItem.Click += EnglishToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { spiritToolStripMenuItem, itemEditToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(57, 20);
            editToolStripMenuItem.Text = "Modify";
            // 
            // spiritToolStripMenuItem
            // 
            spiritToolStripMenuItem.Name = "spiritToolStripMenuItem";
            spiritToolStripMenuItem.Size = new Size(124, 22);
            spiritToolStripMenuItem.Text = "Edit Spirit";
            spiritToolStripMenuItem.Click += SpiritToolStripMenuItem_Click;
            // 
            // itemEditToolStripMenuItem
            // 
            itemEditToolStripMenuItem.Name = "itemEditToolStripMenuItem";
            itemEditToolStripMenuItem.Size = new Size(124, 22);
            itemEditToolStripMenuItem.Text = "Item Edit";
            itemEditToolStripMenuItem.Click += itemEditToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutSaveEditorToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // aboutSaveEditorToolStripMenuItem
            // 
            aboutSaveEditorToolStripMenuItem.Name = "aboutSaveEditorToolStripMenuItem";
            aboutSaveEditorToolStripMenuItem.Size = new Size(168, 22);
            aboutSaveEditorToolStripMenuItem.Text = "About Save Editor";
            aboutSaveEditorToolStripMenuItem.Click += AboutSaveEditorToolStripMenuItem_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(comboBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(comboBoxMove2);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(comboBoxMove1);
            tabPage1.Controls.Add(numericUpDown2);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(numericUpDown1);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(248, 332);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Player";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(76, 9);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(163, 23);
            comboBox1.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 12);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 7;
            label1.Text = "Player";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(133, 85);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 6;
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(76, 85);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(51, 23);
            numericUpDown2.TabIndex = 5;
            numericUpDown2.ValueChanged += NumericUpDown2_ValueChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(76, 43);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(51, 23);
            numericUpDown1.TabIndex = 4;
            numericUpDown1.ValueChanged += NumericUpDown1_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 87);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 3;
            label3.Text = "Grade";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 45);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 2;
            label2.Text = "Level";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 40);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(256, 360);
            tabControl1.TabIndex = 1;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(748, 411);
            Controls.Add(tabControl2);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximumSize = new Size(921, 487);
            Name = "Main";
            Text = "Inazuma Eleven Victory Road SaveEditor";
            tabControl2.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage4.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TabControl tabControl2;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private Button button1;
        private TabPage tabPage3;
        private DataGridView dataGridView2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Button button3;
        private Button Unlcock_Alius;
        private ToolStripMenuItem aboutSaveEditorToolStripMenuItem;
        private Button button2;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem italianToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
        private ComboBox comboBoxMove2;
        private ComboBox comboBoxMove1;
        private Label label6;
        private Label label5;
        private Button button5;
        private TabPage tabPage1;
        private Label label4;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
        private Label label3;
        private Label label2;
        private TabControl tabControl1;
        private TabPage tabPage4;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem spiritToolStripMenuItem;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private ToolStripMenuItem itemEditToolStripMenuItem;
        private ComboBox comboBox1;
        private Label label1;
    }
}
