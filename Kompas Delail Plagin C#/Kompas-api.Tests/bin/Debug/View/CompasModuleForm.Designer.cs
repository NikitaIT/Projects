namespace WindowsFormsApplication1.View
{
    partial class CompasModuleForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompasModuleForm));
            this.textBoxMainDiam2 = new System.Windows.Forms.TextBox();
            this.labelMainDiam2 = new System.Windows.Forms.Label();
            this.labelHoleDiam = new System.Windows.Forms.Label();
            this.labelDiam = new System.Windows.Forms.Label();
            this.labelMainDiam = new System.Windows.Forms.Label();
            this.labelDepth = new System.Windows.Forms.Label();
            this.textBoxDiam = new System.Windows.Forms.TextBox();
            this.textHoleDiam = new System.Windows.Forms.TextBox();
            this.textBoxMainDiam = new System.Windows.Forms.TextBox();
            this.textBoxDepth = new System.Windows.Forms.TextBox();
            this.buttonCreateDetail = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonStartCompas = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxMainDiam2
            // 
            this.textBoxMainDiam2.Location = new System.Drawing.Point(182, 78);
            this.textBoxMainDiam2.Name = "textBoxMainDiam2";
            this.textBoxMainDiam2.Size = new System.Drawing.Size(84, 20);
            this.textBoxMainDiam2.TabIndex = 3;
            this.textBoxMainDiam2.Text = "60";
            this.textBoxMainDiam2.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMainDiam2_Validating);
            // 
            // labelMainDiam2
            // 
            this.labelMainDiam2.AutoSize = true;
            this.labelMainDiam2.Location = new System.Drawing.Point(6, 81);
            this.labelMainDiam2.Name = "labelMainDiam2";
            this.labelMainDiam2.Size = new System.Drawing.Size(98, 13);
            this.labelMainDiam2.TabIndex = 10;
            this.labelMainDiam2.Text = "Диаметр выступа";
            // 
            // labelHoleDiam
            // 
            this.labelHoleDiam.AutoSize = true;
            this.labelHoleDiam.Location = new System.Drawing.Point(6, 133);
            this.labelHoleDiam.Name = "labelHoleDiam";
            this.labelHoleDiam.Size = new System.Drawing.Size(168, 13);
            this.labelHoleDiam.TabIndex = 9;
            this.labelHoleDiam.Text = "Диаметр крепежных отверстий";
            // 
            // labelDiam
            // 
            this.labelDiam.AutoSize = true;
            this.labelDiam.Location = new System.Drawing.Point(6, 107);
            this.labelDiam.Name = "labelDiam";
            this.labelDiam.Size = new System.Drawing.Size(175, 13);
            this.labelDiam.TabIndex = 8;
            this.labelDiam.Text = "Диаметр посадочного отверстия";
            // 
            // labelMainDiam
            // 
            this.labelMainDiam.AutoSize = true;
            this.labelMainDiam.Location = new System.Drawing.Point(6, 55);
            this.labelMainDiam.Name = "labelMainDiam";
            this.labelMainDiam.Size = new System.Drawing.Size(86, 13);
            this.labelMainDiam.TabIndex = 7;
            this.labelMainDiam.Text = "Диаметр диска";
            // 
            // labelDepth
            // 
            this.labelDepth.AutoSize = true;
            this.labelDepth.Location = new System.Drawing.Point(6, 29);
            this.labelDepth.Name = "labelDepth";
            this.labelDepth.Size = new System.Drawing.Size(128, 13);
            this.labelDepth.TabIndex = 6;
            this.labelDepth.Text = "Толщина рабочей части";
            // 
            // textBoxDiam
            // 
            this.textBoxDiam.Location = new System.Drawing.Point(182, 104);
            this.textBoxDiam.Name = "textBoxDiam";
            this.textBoxDiam.Size = new System.Drawing.Size(84, 20);
            this.textBoxDiam.TabIndex = 4;
            this.textBoxDiam.Text = "20";
            this.textBoxDiam.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDiam_Validating);
            // 
            // textHoleDiam
            // 
            this.textHoleDiam.Location = new System.Drawing.Point(182, 130);
            this.textHoleDiam.Name = "textHoleDiam";
            this.textHoleDiam.Size = new System.Drawing.Size(84, 20);
            this.textHoleDiam.TabIndex = 5;
            this.textHoleDiam.Text = "5";
            this.textHoleDiam.Validating += new System.ComponentModel.CancelEventHandler(this.textHoleDiam_Validating);
            // 
            // textBoxMainDiam
            // 
            this.textBoxMainDiam.Location = new System.Drawing.Point(182, 52);
            this.textBoxMainDiam.Name = "textBoxMainDiam";
            this.textBoxMainDiam.Size = new System.Drawing.Size(84, 20);
            this.textBoxMainDiam.TabIndex = 2;
            this.textBoxMainDiam.Text = "100";
            this.textBoxMainDiam.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxMainDiam_Validating);
            // 
            // textBoxDepth
            // 
            this.textBoxDepth.Location = new System.Drawing.Point(182, 26);
            this.textBoxDepth.Name = "textBoxDepth";
            this.textBoxDepth.Size = new System.Drawing.Size(84, 20);
            this.textBoxDepth.TabIndex = 1;
            this.textBoxDepth.Text = "2";
            this.textBoxDepth.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxDepth_Validating);
            // 
            // buttonCreateDetail
            // 
            this.buttonCreateDetail.Location = new System.Drawing.Point(6, 170);
            this.buttonCreateDetail.Name = "buttonCreateDetail";
            this.buttonCreateDetail.Size = new System.Drawing.Size(257, 23);
            this.buttonCreateDetail.TabIndex = 0;
            this.buttonCreateDetail.Text = "Создать деталь";
            this.buttonCreateDetail.UseVisualStyleBackColor = true;
            this.buttonCreateDetail.Click += new System.EventHandler(this.Button1Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonStartCompas);
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 55);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kompas 3D";
            // 
            // buttonStartCompas
            // 
            this.buttonStartCompas.Location = new System.Drawing.Point(6, 19);
            this.buttonStartCompas.Name = "buttonStartCompas";
            this.buttonStartCompas.Size = new System.Drawing.Size(257, 23);
            this.buttonStartCompas.TabIndex = 1;
            this.buttonStartCompas.Text = "Запустить Компас 3D";
            this.buttonStartCompas.UseVisualStyleBackColor = true;
            this.buttonStartCompas.Click += new System.EventHandler(this.buttonStartCompas_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxMainDiam2);
            this.groupBox2.Controls.Add(this.labelDepth);
            this.groupBox2.Controls.Add(this.labelMainDiam2);
            this.groupBox2.Controls.Add(this.buttonCreateDetail);
            this.groupBox2.Controls.Add(this.labelHoleDiam);
            this.groupBox2.Controls.Add(this.textBoxDepth);
            this.groupBox2.Controls.Add(this.labelDiam);
            this.groupBox2.Controls.Add(this.textBoxMainDiam);
            this.groupBox2.Controls.Add(this.labelMainDiam);
            this.groupBox2.Controls.Add(this.textHoleDiam);
            this.groupBox2.Controls.Add(this.textBoxDiam);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(5, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 200);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Построение детали";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 267);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(296, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CompasModuleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(296, 289);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompasModuleForm";
            this.Text = "Плагин для компас \"Тормозной диск\"";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreateDetail;
        private System.Windows.Forms.TextBox textBoxDiam;
        private System.Windows.Forms.TextBox textHoleDiam;
        private System.Windows.Forms.TextBox textBoxMainDiam;
        private System.Windows.Forms.TextBox textBoxDepth;
        private System.Windows.Forms.Label labelHoleDiam;
        private System.Windows.Forms.Label labelDiam;
        private System.Windows.Forms.Label labelMainDiam;
        private System.Windows.Forms.Label labelDepth;
        private System.Windows.Forms.TextBox textBoxMainDiam2;
        private System.Windows.Forms.Label labelMainDiam2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonStartCompas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.StatusStrip statusStrip1;
    }
}

