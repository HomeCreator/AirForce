namespace Card
{
    partial class WriteInArchive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WriteInArchive));
            this.label1 = new System.Windows.Forms.Label();
            this.maskedTextBox_end_archive = new System.Windows.Forms.MaskedTextBox();
            this.button_write_archive = new System.Windows.Forms.Button();
            this.button_cancel_archive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата окончания периода:";
            // 
            // maskedTextBox_end_archive
            // 
            this.maskedTextBox_end_archive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maskedTextBox_end_archive.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_end_archive.Location = new System.Drawing.Point(303, 53);
            this.maskedTextBox_end_archive.Mask = "00/00/0000";
            this.maskedTextBox_end_archive.Name = "maskedTextBox_end_archive";
            this.maskedTextBox_end_archive.Size = new System.Drawing.Size(114, 35);
            this.maskedTextBox_end_archive.TabIndex = 37;
            this.maskedTextBox_end_archive.ValidatingType = typeof(System.DateTime);
            // 
            // button_write_archive
            // 
            this.button_write_archive.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_write_archive.Location = new System.Drawing.Point(17, 131);
            this.button_write_archive.Name = "button_write_archive";
            this.button_write_archive.Size = new System.Drawing.Size(129, 38);
            this.button_write_archive.TabIndex = 38;
            this.button_write_archive.Text = "Записать";
            this.button_write_archive.UseVisualStyleBackColor = true;
            this.button_write_archive.Click += new System.EventHandler(this.button_write_archive_Click);
            // 
            // button_cancel_archive
            // 
            this.button_cancel_archive.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel_archive.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cancel_archive.Location = new System.Drawing.Point(288, 131);
            this.button_cancel_archive.Name = "button_cancel_archive";
            this.button_cancel_archive.Size = new System.Drawing.Size(129, 38);
            this.button_cancel_archive.TabIndex = 39;
            this.button_cancel_archive.Text = "Отмена";
            this.button_cancel_archive.UseVisualStyleBackColor = true;
            // 
            // WriteInArchive
            // 
            this.AcceptButton = this.button_write_archive;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel_archive;
            this.ClientSize = new System.Drawing.Size(448, 202);
            this.Controls.Add(this.button_cancel_archive);
            this.Controls.Add(this.button_write_archive);
            this.Controls.Add(this.maskedTextBox_end_archive);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WriteInArchive";
            this.Text = "Запись в архив выполненных КК";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_end_archive;
        private System.Windows.Forms.Button button_write_archive;
        private System.Windows.Forms.Button button_cancel_archive;
    }
}