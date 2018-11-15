namespace shvMorse
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBoxWPM = new System.Windows.Forms.ComboBox();
            this.labelWPM = new System.Windows.Forms.Label();
            this.comboBoxFREQ = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewResponseTime = new System.Windows.Forms.DataGridView();
            this.button5 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxCharSet = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResponseTime)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(688, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "600->1200Hz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(688, 32);
            this.button2.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 21);
            this.button2.TabIndex = 1;
            this.button2.Text = "Generate wav file";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(688, 56);
            this.button3.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(98, 21);
            this.button3.TabIndex = 2;
            this.button3.Text = "High Speed";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(16, 82);
            this.button4.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(98, 21);
            this.button4.TabIndex = 3;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(468, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(215, 123);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // comboBoxWPM
            // 
            this.comboBoxWPM.FormattingEnabled = true;
            this.comboBoxWPM.Items.AddRange(new object[] {
            "9",
            "12",
            "15",
            "18",
            "21",
            "24",
            "27",
            "30",
            "33",
            "36",
            "39"});
            this.comboBoxWPM.Location = new System.Drawing.Point(16, 50);
            this.comboBoxWPM.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.comboBoxWPM.Name = "comboBoxWPM";
            this.comboBoxWPM.Size = new System.Drawing.Size(84, 20);
            this.comboBoxWPM.TabIndex = 5;
            this.comboBoxWPM.Text = "15";
            // 
            // labelWPM
            // 
            this.labelWPM.AutoSize = true;
            this.labelWPM.Location = new System.Drawing.Point(25, 34);
            this.labelWPM.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelWPM.Name = "labelWPM";
            this.labelWPM.Size = new System.Drawing.Size(30, 12);
            this.labelWPM.TabIndex = 6;
            this.labelWPM.Text = "WPM";
            // 
            // comboBoxFREQ
            // 
            this.comboBoxFREQ.FormattingEnabled = true;
            this.comboBoxFREQ.Items.AddRange(new object[] {
            "500",
            "550",
            "600",
            "650",
            "700",
            "750",
            "800"});
            this.comboBoxFREQ.Location = new System.Drawing.Point(128, 50);
            this.comboBoxFREQ.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.comboBoxFREQ.Name = "comboBoxFREQ";
            this.comboBoxFREQ.Size = new System.Drawing.Size(84, 20);
            this.comboBoxFREQ.TabIndex = 7;
            this.comboBoxFREQ.Text = "600";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "FREQ.";
            // 
            // dataGridViewResponseTime
            // 
            this.dataGridViewResponseTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResponseTime.Location = new System.Drawing.Point(16, 142);
            this.dataGridViewResponseTime.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.dataGridViewResponseTime.Name = "dataGridViewResponseTime";
            this.dataGridViewResponseTime.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewResponseTime.RowTemplate.Height = 39;
            this.dataGridViewResponseTime.Size = new System.Drawing.Size(771, 401);
            this.dataGridViewResponseTime.TabIndex = 10;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(236, 82);
            this.button5.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(126, 37);
            this.button5.TabIndex = 11;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(242, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Character Set";
            // 
            // comboBoxCharSet
            // 
            this.comboBoxCharSet.FormattingEnabled = true;
            this.comboBoxCharSet.Items.AddRange(new object[] {
            "Characters",
            "C+Numbers",
            "C+N+Special Characters",
            "C+N+S+Brackets"});
            this.comboBoxCharSet.Location = new System.Drawing.Point(236, 50);
            this.comboBoxCharSet.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.comboBoxCharSet.Name = "comboBoxCharSet";
            this.comboBoxCharSet.Size = new System.Drawing.Size(129, 20);
            this.comboBoxCharSet.TabIndex = 12;
            this.comboBoxCharSet.Text = "Characters";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(812, 569);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxCharSet);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dataGridViewResponseTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxFREQ);
            this.Controls.Add(this.labelWPM);
            this.Controls.Add(this.comboBoxWPM);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResponseTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBoxWPM;
        private System.Windows.Forms.Label labelWPM;
        private System.Windows.Forms.ComboBox comboBoxFREQ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewResponseTime;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxCharSet;
    }
}

