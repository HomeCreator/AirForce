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
    public partial class WriteInArchive : Form
    {
        public WriteInArchive()
        {
            InitializeComponent();
        }

        private void button_write_archive_Click(object sender, EventArgs e)
        {
            if(!maskedTextBox_end_archive.MaskCompleted)
            {
                MessageBox.Show("Заполните все поля.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
                OleDbConnection connectionDB = new OleDbConnection(connectionString);
                try
                {
                    connectionDB.Open();
                    String archiveQuery = "INSERT INTO AOKIDD5 SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_is2, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp, fakt_isp FROM OKIDD5 WHERE fakt_isp IS NOT NULL and data_rez <= #" + maskedTextBox_end_archive.Text.Replace(".", "/")  + "#";
                    String archiveQueryDelete = "DELETE FROM OKIDD5 WHERE fakt_isp IS NOT NULL and data_rez <= #" + maskedTextBox_end_archive.Text.Replace(".", "/") + "#";
                    OleDbCommand archiveCommand = new OleDbCommand(archiveQuery, connectionDB);
                    OleDbCommand archiveDeleteCommand = new OleDbCommand(archiveQueryDelete, connectionDB);
                    archiveCommand.ExecuteNonQuery();
                    archiveDeleteCommand.ExecuteNonQuery();
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source);
                    return;
                }
                finally
                {
                    connectionDB.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source);
                return;
            }

            MessageBox.Show("Карты успешно перенесены в архив.", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
        }
    }
}
