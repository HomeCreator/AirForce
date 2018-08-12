using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Card
{
    public partial class Settings_DB : Form
    {
        public Settings_DB()
        {
            InitializeComponent();
            textBox_path_okidd5.Text = Properties.Settings.Default.textBox_OKDD5;
        }

        private void button_path_okidd5_Click(object sender, EventArgs e)
        {
            if(openFileDialog_okidd5.ShowDialog() == DialogResult.OK)
            {
                textBox_path_okidd5.Text = openFileDialog_okidd5.FileName;
            }
        }

        private void button_save_settings_DB_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.textBox_OKDD5 = textBox_path_okidd5.Text;

            if (!File.Exists(textBox_path_okidd5.Text))
            {
                MessageBox.Show("Отсутствует путь к одному или нескольким файлам. Пожалуйста, укажите путь ко всем файлам базы данных.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Properties.Settings.Default.Save();
            textBox_path_okidd5.Text = Properties.Settings.Default.textBox_OKDD5;

            this.Close();
        }
    }
}
