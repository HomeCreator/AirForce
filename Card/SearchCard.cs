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
    public partial class SearchCard : Form
    {
        public SearchCard()
        {
            InitializeComponent();
        }

        private void button_set_number_card_Click(object sender, EventArgs e)
        {
            if (maskedTextBox_seek_number_card.Text == String.Empty)
            {
                MessageBox.Show("Заполните строку с номером карты.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }

            OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
            try
            {
                connectionDB.Open();
                String seekQuery = "SELECT nomer FROM OKIDD5 WHERE nomer = " + Convert.ToInt32(maskedTextBox_seek_number_card.Text.Replace(" ", ""));
                OleDbCommand seekCommand = new OleDbCommand(seekQuery, connectionDB);
                if (seekCommand.ExecuteScalar() != null)
                {
                    String checkQuery = "SELECT fakt_isp FROM OKIDD5 WHERE fakt_isp IS NOT NULL and nomer = " + Convert.ToInt32(maskedTextBox_seek_number_card.Text.Replace(" ", ""));
                    OleDbCommand checkCommand = new OleDbCommand(checkQuery, connectionDB);
                    if (checkCommand.ExecuteScalar() != null)
                    {
                        MessageBox.Show("Карточка снята с контроля.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        Service.CardNumber = maskedTextBox_seek_number_card.Text.Replace(" ", "");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Контрольной карты с таким номером не существует.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                }
            }
            catch (Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
                this.Close();
            }
            finally
            {
                connectionDB.Close();
            }
        }
    }
}
