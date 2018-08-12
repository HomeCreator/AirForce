namespace Card
{
    partial class Settings_DB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings_DB));
            this.label_path_okidd5 = new System.Windows.Forms.Label();
            this.textBox_path_okidd5 = new System.Windows.Forms.TextBox();
            this.button_path_okidd5 = new System.Windows.Forms.Button();
            this.openFileDialog_okidd5 = new System.Windows.Forms.OpenFileDialog();
            this.button_save_settings_DB = new System.Windows.Forms.Button();
            this.button_cancel_settings_DB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_path_okidd5
            // 
            this.label_path_okidd5.AutoSize = true;
            this.label_path_okidd5.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_path_okidd5.Location = new System.Drawing.Point(12, 29);
            this.label_path_okidd5.Name = "label_path_okidd5";
            this.label_path_okidd5.Size = new System.Drawing.Size(225, 28);
            this.label_path_okidd5.TabIndex = 3;
            this.label_path_okidd5.Text = "Путь к базе данных:";
            // 
            // textBox_path_okidd5
            // 
            this.textBox_path_okidd5.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_path_okidd5.Location = new System.Drawing.Point(243, 29);
            this.textBox_path_okidd5.Name = "textBox_path_okidd5";
            this.textBox_path_okidd5.ReadOnly = true;
            this.textBox_path_okidd5.Size = new System.Drawing.Size(434, 35);
            this.textBox_path_okidd5.TabIndex = 4;
            // 
            // button_path_okidd5
            // 
            this.button_path_okidd5.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_path_okidd5.Location = new System.Drawing.Point(683, 29);
            this.button_path_okidd5.Name = "button_path_okidd5";
            this.button_path_okidd5.Size = new System.Drawing.Size(62, 35);
            this.button_path_okidd5.TabIndex = 5;
            this.button_path_okidd5.Text = "...";
            this.button_path_okidd5.UseVisualStyleBackColor = true;
            this.button_path_okidd5.Click += new System.EventHandler(this.button_path_okidd5_Click);
            // 
            // openFileDialog_okidd5
            // 
            this.openFileDialog_okidd5.FileName = "OKIDD5.accdb";
            this.openFileDialog_okidd5.Filter = "(*.accdb)|*.accdb";
            // 
            // button_save_settings_DB
            // 
            this.button_save_settings_DB.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_save_settings_DB.Location = new System.Drawing.Point(152, 113);
            this.button_save_settings_DB.Name = "button_save_settings_DB";
            this.button_save_settings_DB.Size = new System.Drawing.Size(148, 39);
            this.button_save_settings_DB.TabIndex = 13;
            this.button_save_settings_DB.Text = "Сохранить";
            this.button_save_settings_DB.UseVisualStyleBackColor = true;
            this.button_save_settings_DB.Click += new System.EventHandler(this.button_save_settings_DB_Click);
            // 
            // button_cancel_settings_DB
            // 
            this.button_cancel_settings_DB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel_settings_DB.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_cancel_settings_DB.Location = new System.Drawing.Point(458, 113);
            this.button_cancel_settings_DB.Name = "button_cancel_settings_DB";
            this.button_cancel_settings_DB.Size = new System.Drawing.Size(142, 39);
            this.button_cancel_settings_DB.TabIndex = 14;
            this.button_cancel_settings_DB.Text = "Отмена";
            this.button_cancel_settings_DB.UseVisualStyleBackColor = true;
            // 
            // Settings_DB
            // 
            this.AcceptButton = this.button_save_settings_DB;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel_settings_DB;
            this.ClientSize = new System.Drawing.Size(760, 164);
            this.Controls.Add(this.button_cancel_settings_DB);
            this.Controls.Add(this.button_save_settings_DB);
            this.Controls.Add(this.button_path_okidd5);
            this.Controls.Add(this.textBox_path_okidd5);
            this.Controls.Add(this.label_path_okidd5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Settings_DB";
            this.Text = "Настройка таблиц";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_path_okidd5;
        private System.Windows.Forms.Button button_path_okidd5;
        private System.Windows.Forms.Button button_save_settings_DB;
        private System.Windows.Forms.Button button_cancel_settings_DB;
        private System.Windows.Forms.OpenFileDialog openFileDialog_okidd5;
        internal System.Windows.Forms.TextBox textBox_path_okidd5;
    }
}