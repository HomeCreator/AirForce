using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Card
{
    public partial class AutoInsertNumber : Form
    {
        public AutoInsertNumber()
        {
            InitializeComponent();
            maskedTextBox_autoinsert.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();
            checkBox_autoinsert.Checked = Properties.Settings.Default.checkBox_autoinsert;
            checkBox_sort_card.Checked = Properties.Settings.Default.checkBox_sort_card;
        }

        private void button_autoinsert_done_Click(object sender, EventArgs e)
        {
            if (maskedTextBox_autoinsert.Text == String.Empty)
            {
                MessageBox.Show("Заполните поле номера карты.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Properties.Settings.Default.maskedTextBox_autoinsert = Convert.ToInt32(maskedTextBox_autoinsert.Text);
            Properties.Settings.Default.checkBox_autoinsert = checkBox_autoinsert.Checked;
            Properties.Settings.Default.checkBox_sort_card = checkBox_sort_card.Checked;

            Properties.Settings.Default.Save();

            maskedTextBox_autoinsert.Text = Properties.Settings.Default.ToString();
            checkBox_autoinsert.Checked = Properties.Settings.Default.checkBox_autoinsert;
            checkBox_sort_card.Checked = Properties.Settings.Default.checkBox_sort_card;

            this.Close();
        }
    }
}
