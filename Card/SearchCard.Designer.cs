namespace Card
{
    partial class SearchCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCard));
            this.label_formation_number_card = new System.Windows.Forms.Label();
            this.button_set_number_card = new System.Windows.Forms.Button();
            this.button_one_card_cancel = new System.Windows.Forms.Button();
            this.maskedTextBox_seek_number_card = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label_formation_number_card
            // 
            this.label_formation_number_card.AutoSize = true;
            this.label_formation_number_card.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_formation_number_card.Location = new System.Drawing.Point(9, 29);
            this.label_formation_number_card.Name = "label_formation_number_card";
            this.label_formation_number_card.Size = new System.Drawing.Size(159, 28);
            this.label_formation_number_card.TabIndex = 0;
            this.label_formation_number_card.Text = "Номер карты:";
            // 
            // button_set_number_card
            // 
            this.button_set_number_card.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_set_number_card.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_set_number_card.Location = new System.Drawing.Point(14, 83);
            this.button_set_number_card.Name = "button_set_number_card";
            this.button_set_number_card.Size = new System.Drawing.Size(128, 39);
            this.button_set_number_card.TabIndex = 2;
            this.button_set_number_card.Text = "Указать";
            this.button_set_number_card.UseVisualStyleBackColor = true;
            this.button_set_number_card.Click += new System.EventHandler(this.button_set_number_card_Click);
            // 
            // button_one_card_cancel
            // 
            this.button_one_card_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_one_card_cancel.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_one_card_cancel.Location = new System.Drawing.Point(177, 83);
            this.button_one_card_cancel.Name = "button_one_card_cancel";
            this.button_one_card_cancel.Size = new System.Drawing.Size(128, 39);
            this.button_one_card_cancel.TabIndex = 3;
            this.button_one_card_cancel.Text = "Отмена";
            this.button_one_card_cancel.UseVisualStyleBackColor = true;
            // 
            // maskedTextBox_seek_number_card
            // 
            this.maskedTextBox_seek_number_card.Font = new System.Drawing.Font("Roboto Slab", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.maskedTextBox_seek_number_card.Location = new System.Drawing.Point(174, 26);
            this.maskedTextBox_seek_number_card.Mask = "0000000000";
            this.maskedTextBox_seek_number_card.Name = "maskedTextBox_seek_number_card";
            this.maskedTextBox_seek_number_card.Size = new System.Drawing.Size(131, 35);
            this.maskedTextBox_seek_number_card.TabIndex = 1;
            // 
            // SearchCard
            // 
            this.AcceptButton = this.button_set_number_card;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_one_card_cancel;
            this.ClientSize = new System.Drawing.Size(334, 134);
            this.Controls.Add(this.maskedTextBox_seek_number_card);
            this.Controls.Add(this.button_one_card_cancel);
            this.Controls.Add(this.button_set_number_card);
            this.Controls.Add(this.label_formation_number_card);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchCard";
            this.Text = "Поиск карты";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_formation_number_card;
        private System.Windows.Forms.Button button_set_number_card;
        private System.Windows.Forms.Button button_one_card_cancel;
        private System.Windows.Forms.MaskedTextBox maskedTextBox_seek_number_card;
    }
}