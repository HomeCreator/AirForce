using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Card
{
    public partial class UntimelyExecute : Form
    {
        public UntimelyExecute()
        {
            InitializeComponent();
        }

        private void button_form_Click(object sender, EventArgs e)
        {
            if(!maskedTextBox_begin.MaskCompleted || !maskedTextBox_end.MaskCompleted)
            {
                MessageBox.Show("Заполните корректно все поля.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
