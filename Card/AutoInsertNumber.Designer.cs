namespace Card
{
    partial class AutoInsertNumber
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoInsertNumber));
            this.label1 = new System.Windows.Forms.Label();
            this.maskedTextBox_autoinsert = new System.Windows.Forms.MaskedTextBox();
            this.button_autoinsert_done = new System.Windows.Forms.Button();
            this.checkBox_autoinsert = new System.Windows.Forms.CheckBox();
            this.checkBox_sort_card = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Начать с карты:";
            // 
            // maskedTextBox_autoinsert
            // 
            this.maskedTextBox_autoinsert.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_autoinsert.Location = new System.Drawing.Point(201, 50);
            this.maskedTextBox_autoinsert.Mask = "0000000000";
            this.maskedTextBox_autoinsert.Name = "maskedTextBox_autoinsert";
            this.maskedTextBox_autoinsert.Size = new System.Drawing.Size(128, 35);
            this.maskedTextBox_autoinsert.TabIndex = 2;
            // 
            // button_autoinsert_done
            // 
            this.button_autoinsert_done.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_autoinsert_done.Location = new System.Drawing.Point(235, 253);
            this.button_autoinsert_done.Name = "button_autoinsert_done";
            this.button_autoinsert_done.Size = new System.Drawing.Size(123, 35);
            this.button_autoinsert_done.TabIndex = 3;
            this.button_autoinsert_done.Text = "Готово";
            this.button_autoinsert_done.UseVisualStyleBackColor = true;
            this.button_autoinsert_done.Click += new System.EventHandler(this.button_autoinsert_done_Click);
            // 
            // checkBox_autoinsert
            // 
            this.checkBox_autoinsert.AutoSize = true;
            this.checkBox_autoinsert.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_autoinsert.Location = new System.Drawing.Point(17, 117);
            this.checkBox_autoinsert.Name = "checkBox_autoinsert";
            this.checkBox_autoinsert.Size = new System.Drawing.Size(321, 32);
            this.checkBox_autoinsert.TabIndex = 4;
            this.checkBox_autoinsert.Text = "Включить автозаполнение";
            this.checkBox_autoinsert.UseVisualStyleBackColor = true;
            // 
            // checkBox_sort_card
            // 
            this.checkBox_sort_card.AutoSize = true;
            this.checkBox_sort_card.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox_sort_card.Location = new System.Drawing.Point(17, 195);
            this.checkBox_sort_card.Name = "checkBox_sort_card";
            this.checkBox_sort_card.Size = new System.Drawing.Size(438, 32);
            this.checkBox_sort_card.TabIndex = 5;
            this.checkBox_sort_card.Text = "Сортировать сформированные карты";
            this.checkBox_sort_card.UseVisualStyleBackColor = true;
            // 
            // AutoInsertNumber
            // 
            this.AcceptButton = this.button_autoinsert_done;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 300);
            this.Controls.Add(this.checkBox_sort_card);
            this.Controls.Add(this.checkBox_autoinsert);
            this.Controls.Add(this.button_autoinsert_done);
            this.Controls.Add(this.maskedTextBox_autoinsert);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AutoInsertNumber";
            this.Text = "Дополнительно";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_autoinsert_done;
        internal System.Windows.Forms.CheckBox checkBox_autoinsert;
        protected System.Windows.Forms.MaskedTextBox maskedTextBox_autoinsert;
        private System.Windows.Forms.CheckBox checkBox_sort_card;
    }
}