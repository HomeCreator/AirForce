namespace Card
{
    partial class UntimelyExecute
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UntimelyExecute));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextBox_end = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox_begin = new System.Windows.Forms.MaskedTextBox();
            this.button_form = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(359, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата начала отчётного периода:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 28);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата окончания отчётного периода:";
            // 
            // maskedTextBox_end
            // 
            this.maskedTextBox_end.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_end.Location = new System.Drawing.Point(419, 74);
            this.maskedTextBox_end.Mask = "00/00/0000";
            this.maskedTextBox_end.Name = "maskedTextBox_end";
            this.maskedTextBox_end.Size = new System.Drawing.Size(114, 35);
            this.maskedTextBox_end.TabIndex = 12;
            this.maskedTextBox_end.ValidatingType = typeof(System.DateTime);
            // 
            // maskedTextBox_begin
            // 
            this.maskedTextBox_begin.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_begin.Location = new System.Drawing.Point(378, 28);
            this.maskedTextBox_begin.Mask = "00/00/0000";
            this.maskedTextBox_begin.Name = "maskedTextBox_begin";
            this.maskedTextBox_begin.Size = new System.Drawing.Size(114, 35);
            this.maskedTextBox_begin.TabIndex = 11;
            this.maskedTextBox_begin.ValidatingType = typeof(System.DateTime);
            // 
            // button_form
            // 
            this.button_form.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_form.Location = new System.Drawing.Point(180, 125);
            this.button_form.Name = "button_form";
            this.button_form.Size = new System.Drawing.Size(192, 38);
            this.button_form.TabIndex = 13;
            this.button_form.Text = "Формировать";
            this.button_form.UseVisualStyleBackColor = true;
            this.button_form.Click += new System.EventHandler(this.button_form_Click);
            // 
            // UntimelyExecute
            // 
            this.AcceptButton = this.button_form;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 175);
            this.Controls.Add(this.button_form);
            this.Controls.Add(this.maskedTextBox_end);
            this.Controls.Add(this.maskedTextBox_begin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UntimelyExecute";
            this.Text = "Формирование несвоевременных карт";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_form;
        internal System.Windows.Forms.MaskedTextBox maskedTextBox_end;
        internal System.Windows.Forms.MaskedTextBox maskedTextBox_begin;
    }
}