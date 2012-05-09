namespace Oleg_ivo.Tools.UI
{
    partial class DoubleListBoxControl
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLeft = new System.Windows.Forms.ListBox();
            this.lbRight = new System.Windows.Forms.ListBox();
            this.btnMoveToLeft = new System.Windows.Forms.Button();
            this.btnMoveToLeftAll = new System.Windows.Forms.Button();
            this.btnMoveToRight = new System.Windows.Forms.Button();
            this.btnMoveToRightAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLeft
            // 
            this.lbLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbLeft.FormattingEnabled = true;
            this.lbLeft.HorizontalScrollbar = true;
            this.lbLeft.Location = new System.Drawing.Point(6, 19);
            this.lbLeft.Name = "lbLeft";
            this.lbLeft.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbLeft.Size = new System.Drawing.Size(124, 147);
            this.lbLeft.TabIndex = 0;
            this.lbLeft.DoubleClick += new System.EventHandler(this.lbLeft_DoubleClick);
            this.lbLeft.SelectedValueChanged += new System.EventHandler(this.lbLeft_SelectedValueChanged);
            // 
            // lbRight
            // 
            this.lbRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbRight.FormattingEnabled = true;
            this.lbRight.HorizontalScrollbar = true;
            this.lbRight.Location = new System.Drawing.Point(232, 19);
            this.lbRight.Name = "lbRight";
            this.lbRight.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbRight.Size = new System.Drawing.Size(124, 147);
            this.lbRight.TabIndex = 1;
            this.lbRight.DoubleClick += new System.EventHandler(this.lbRight_DoubleClick);
            this.lbRight.SelectedValueChanged += new System.EventHandler(this.lbRight_SelectedValueChanged);
            // 
            // btnMoveToLeft
            // 
            this.btnMoveToLeft.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveToLeft.Enabled = false;
            this.btnMoveToLeft.Location = new System.Drawing.Point(136, 88);
            this.btnMoveToLeft.Name = "btnMoveToLeft";
            this.btnMoveToLeft.Size = new System.Drawing.Size(90, 23);
            this.btnMoveToLeft.TabIndex = 2;
            this.btnMoveToLeft.Text = "<";
            this.btnMoveToLeft.UseVisualStyleBackColor = true;
            this.btnMoveToLeft.Click += new System.EventHandler(this.btnMoveToLeft_Click);
            // 
            // btnMoveToLeftAll
            // 
            this.btnMoveToLeftAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveToLeftAll.Enabled = false;
            this.btnMoveToLeftAll.Location = new System.Drawing.Point(136, 117);
            this.btnMoveToLeftAll.Name = "btnMoveToLeftAll";
            this.btnMoveToLeftAll.Size = new System.Drawing.Size(90, 23);
            this.btnMoveToLeftAll.TabIndex = 3;
            this.btnMoveToLeftAll.Text = "<<";
            this.btnMoveToLeftAll.UseVisualStyleBackColor = true;
            this.btnMoveToLeftAll.Click += new System.EventHandler(this.btnMoveToLeftAll_Click);
            // 
            // btnMoveToRight
            // 
            this.btnMoveToRight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveToRight.Enabled = false;
            this.btnMoveToRight.Location = new System.Drawing.Point(136, 19);
            this.btnMoveToRight.Name = "btnMoveToRight";
            this.btnMoveToRight.Size = new System.Drawing.Size(90, 23);
            this.btnMoveToRight.TabIndex = 4;
            this.btnMoveToRight.Text = ">";
            this.btnMoveToRight.UseVisualStyleBackColor = true;
            this.btnMoveToRight.Click += new System.EventHandler(this.btnMoveToRight_Click);
            // 
            // btnMoveToRightAll
            // 
            this.btnMoveToRightAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveToRightAll.Enabled = false;
            this.btnMoveToRightAll.Location = new System.Drawing.Point(136, 48);
            this.btnMoveToRightAll.Name = "btnMoveToRightAll";
            this.btnMoveToRightAll.Size = new System.Drawing.Size(90, 23);
            this.btnMoveToRightAll.TabIndex = 5;
            this.btnMoveToRightAll.Text = ">>";
            this.btnMoveToRightAll.UseVisualStyleBackColor = true;
            this.btnMoveToRightAll.Click += new System.EventHandler(this.btnMoveToRightAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLeft);
            this.groupBox1.Controls.Add(this.btnMoveToLeftAll);
            this.groupBox1.Controls.Add(this.btnMoveToRightAll);
            this.groupBox1.Controls.Add(this.btnMoveToLeft);
            this.groupBox1.Controls.Add(this.lbRight);
            this.groupBox1.Controls.Add(this.btnMoveToRight);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 177);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // DoubleListBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DoubleListBoxControl";
            this.Size = new System.Drawing.Size(362, 177);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLeft;
        private System.Windows.Forms.ListBox lbRight;
        private System.Windows.Forms.Button btnMoveToLeft;
        private System.Windows.Forms.Button btnMoveToLeftAll;
        private System.Windows.Forms.Button btnMoveToRight;
        private System.Windows.Forms.Button btnMoveToRightAll;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
