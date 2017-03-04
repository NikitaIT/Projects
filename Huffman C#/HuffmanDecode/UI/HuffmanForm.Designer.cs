namespace HuffmanDecode.UI
{
    partial class HuffmanForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbIn = new System.Windows.Forms.RichTextBox();
            this.rtbOut = new System.Windows.Forms.RichTextBox();
            this.btnDecode = new System.Windows.Forms.Button();
            this.openFileDialogIn = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFileIn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.saveFileDialogOut = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbIn
            // 
            this.rtbIn.Location = new System.Drawing.Point(6, 19);
            this.rtbIn.Name = "rtbIn";
            this.rtbIn.Size = new System.Drawing.Size(254, 26);
            this.rtbIn.TabIndex = 1;
            this.rtbIn.Text = "";
            // 
            // rtbOut
            // 
            this.rtbOut.Location = new System.Drawing.Point(6, 51);
            this.rtbOut.Name = "rtbOut";
            this.rtbOut.Size = new System.Drawing.Size(254, 30);
            this.rtbOut.TabIndex = 3;
            this.rtbOut.Text = "";
            // 
            // btnDecode
            // 
            this.btnDecode.Enabled = false;
            this.btnDecode.Location = new System.Drawing.Point(140, 19);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(119, 33);
            this.btnDecode.TabIndex = 5;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            // 
            // openFileDialogIn
            // 
            this.openFileDialogIn.FileName = "in.txt";
            // 
            // btnOpenFileIn
            // 
            this.btnOpenFileIn.Location = new System.Drawing.Point(10, 19);
            this.btnOpenFileIn.Name = "btnOpenFileIn";
            this.btnOpenFileIn.Size = new System.Drawing.Size(111, 33);
            this.btnOpenFileIn.TabIndex = 6;
            this.btnOpenFileIn.Text = "OpenFileIn";
            this.btnOpenFileIn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtbOut);
            this.groupBox1.Controls.Add(this.rtbIn);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 87);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "In/Out";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOpenFileIn);
            this.groupBox2.Controls.Add(this.btnDecode);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 59);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Control";
            // 
            // saveFileDialogOut
            // 
            this.saveFileDialogOut.FileName = "out.txt";
            // 
            // HuffmanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 170);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "HuffmanForm";
            this.Text = "Huffman";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtbIn;
        private System.Windows.Forms.RichTextBox rtbOut;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.OpenFileDialog openFileDialogIn;
        private System.Windows.Forms.Button btnOpenFileIn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.SaveFileDialog saveFileDialogOut;
    }
}

