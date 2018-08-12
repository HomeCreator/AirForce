using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace Card
{
    public partial class Card : Form
    {
        public Card()
        {
            InitializeComponent();
        }

        private void новойКонтрольнойКартыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_new_control_card.Visible = true;
            panel_correct.Visible = false;
            panel_card_filter.Visible = false;
            panel_reference.Visible = false;
            label_term_execute_end.Visible = true;
            maskedTextBox_term_execute_end.Visible = true;

            groupBox_new_card_main.Text = "Основное - Ввод новой контрольной карты";
            groupBox_new_card_content.Text = "Содержание - Ввод новой контрольной карты";

            String[] typesDocument = new String[] { "Приказ", "Протокол", "Летучий контроль", "Служебная записка", "Распоряжение", "Карта разрешения", "График", "Внедрение извещений", "Мероприятие", "Акт на брак", "Акт о приостановке", "Контрольная сборка", "Комплексная проверка", "Техническая дисциплина", "Авторский надзор", "Программа качетсва", "Решение", "Акт", "График оборудования" };

            comboBox_type_document.Items.Clear();
            comboBox_type_document.Text = String.Empty;
            maskedTextBox_term_execute_end.Clear();

            foreach (String value in typesDocument)
                comboBox_type_document.Items.Add(value);

            if (Properties.Settings.Default.checkBox_autoinsert)
                maskedTextBox_control_card.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();
        }

        private void базыДанныхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings_DB settings_DB = new Settings_DB();
            settings_DB.ShowDialog();
        }

        private void button_create_new_card_Click(object sender, EventArgs e)
        {
            comboBox_type_document.TabStop = true;
            textBox_number.TabStop = true;
            maskedTextBox_from.TabStop = true;
            richTextBox_theme.TabStop = true;
            maskedTextBox_term_execute_begin.TabStop = true;
            maskedTextBox_term_execute_end.TabStop = true;
            textBox_punkt.TabStop = true;

            if (maskedTextBox_control_card.Text.Replace(" ", "") == String.Empty || comboBox_type_document.SelectedIndex == -1 || comboBox_type_document.Text.Trim() == String.Empty || !maskedTextBox_from.MaskCompleted || maskedTextBox_responsible_executer.Text.Replace(" ", "") == String.Empty || richTextBox_theme.Text.Trim() == String.Empty || !maskedTextBox_term_execute_begin.MaskCompleted || !maskedTextBox_term_execute_end.MaskCompleted && maskedTextBox_term_execute_end.Text.Replace(".", "").Trim() != String.Empty)
            {
                MessageBox.Show("Не все поля заполнены корректно для ввода новой контрольной карты. Пожалуйста, корректно заполните все поля, в том числе и обязательные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Подтвердите правильность ввода данных", "Ввод контрольной карты", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //String connectionString = "Driver = {Microsoft dBase Driver (*.dbf)}; SourceType = DBF; SourceDB = " + settings_DB.textBox_path_okidd5.Text + "; Exclusive = No; Collate = Machine; NULL = NO; DETECTED=NO; BACKGROUNDFETCH=NO"; 0x80004005
                try
                {
                    OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                    try
                    {
                        connectionDB.Open();

                        String seekQuery = "SELECT nomer FROM OKIDD5 WHERE nomer = " + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", ""));
                        OleDbCommand seekCommand = new OleDbCommand(seekQuery, connectionDB);

                        if (seekCommand.ExecuteScalar() != null)
                        {
                            MessageBox.Show("Контрольная карта с таким номером уже существует.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (maskedTextBox_term_execute_end.Text.Replace(".", "").Trim() == String.Empty)
                        {
                            String insertQuery = "INSERT INTO OKIDD5 (nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per) VALUES (" + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", "")) + ", '" + comboBox_type_document.Text + "', '" + textBox_number.Text + "', '" + maskedTextBox_from.Text + "', '" + textBox_punkt.Text + "', " + Convert.ToInt32(maskedTextBox_responsible_executer.Text.Replace(" ", "")) + ", '" + richTextBox_theme.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', 0)";
                            OleDbCommand insertCommand = new OleDbCommand(insertQuery, connectionDB);
                            insertCommand.ExecuteNonQuery();
                        }
                        else
                        {
                            String insertQuery = "INSERT INTO OKIDD5 (nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, data_is2, kol_per) VALUES (" + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", "")) + ", '" + comboBox_type_document.Text + "', '" + textBox_number.Text + "', '" + maskedTextBox_from.Text + "', '" + textBox_punkt.Text + "', " + Convert.ToInt32(maskedTextBox_responsible_executer.Text.Replace(" ", "")) + ", '" + richTextBox_theme.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', '" + maskedTextBox_term_execute_end.Text + "', '" + maskedTextBox_term_execute_end.Text + "', 0)";
                            OleDbCommand insertCommand = new OleDbCommand(insertQuery, connectionDB);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        connectionDB.Close();
                    }
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Карта успешно записана в базу данных!", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                if (Properties.Settings.Default.checkBox_autoinsert)
                {
                    Properties.Settings.Default.maskedTextBox_autoinsert++;
                    Properties.Settings.Default.Save();
                    maskedTextBox_control_card.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();
                }
                else
                    maskedTextBox_control_card.Clear();

                comboBox_type_document.Text = String.Empty;
                textBox_punkt.Clear();
                textBox_number.Clear();
                maskedTextBox_from.Text = String.Empty;
                maskedTextBox_responsible_executer.Text = String.Empty;
                richTextBox_theme.Clear();
                maskedTextBox_term_execute_begin.Text = String.Empty;
                maskedTextBox_term_execute_end.Text = String.Empty;
            }
        }

        private void Card_Load(object sender, EventArgs e)
        {
            Settings_DB settings_DB = new Settings_DB();
            if (!File.Exists(Properties.Settings.Default.textBox_OKDD5))
                if (MessageBox.Show("Отсутствует путь к базе данных. Пожалуйста, укажите путь к файлу базы данных в формате .accdb", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    settings_DB.ShowDialog();

            panel_new_control_card.Dock = DockStyle.Fill;
            panel_correct.Dock = DockStyle.Fill;
            panel_card_filter.Dock = DockStyle.Fill;

            maskedTextBox_control_card.TabStop = (Properties.Settings.Default.checkBox_autoinsert) ? false : true;
        }

        private void однойКонтрольнойКартыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Service.GetNumberCard())
                return;

            String formQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE nomer = " + Service.CardNumber;
            Service.Printing(formQuery, printDocument2, printDialog1);
        }

        private void button_create_new_card_some_Click(object sender, EventArgs e)
        {

            if (maskedTextBox_control_card.Text.Replace(" ", "") == String.Empty || comboBox_type_document.SelectedIndex == -1 || comboBox_type_document.Text.Trim() == String.Empty || !maskedTextBox_from.MaskCompleted || maskedTextBox_responsible_executer.Text.Replace(" ", "") == String.Empty || richTextBox_theme.Text.Trim() == String.Empty || !maskedTextBox_term_execute_begin.MaskCompleted)
            {
                MessageBox.Show("Не все поля заполнены корректно для ввода новой контрольной карты. Пожалуйста, корректно заполните все поля, в том числе и обязательные.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Подтвердите правильность ввода данных", "Ввод контрольной карты", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //String connectionString = "Driver = {Microsoft dBase Driver (*.dbf)}; SourceType = DBF; SourceDB = " + settings_DB.textBox_path_okidd5.Text + "; Exclusive = No; Collate = Machine; NULL = NO; DETECTED=NO; BACKGROUNDFETCH=NO";
                try
                {
                    //String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
                    OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                    try
                    {
                        connectionDB.Open();
                        String checkQuery = "SELECT nomer FROM OKIDD5 WHERE nomer = " + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", ""));
                        OleDbCommand checkCommand = new OleDbCommand(checkQuery, connectionDB);

                        if (checkCommand.ExecuteScalar() != null)
                        {
                            MessageBox.Show("Контрольная карта с таким номером уже существует.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (maskedTextBox_term_execute_end.Text.Replace(".", "").Trim() == String.Empty)
                        {
                            String query = "INSERT INTO OKIDD5 (nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per) VALUES (" + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", "")) + ", '" + comboBox_type_document.Text + "', '" + textBox_number.Text + "', '" + maskedTextBox_from.Text + "', '" + textBox_punkt.Text + "', " + Convert.ToInt32(maskedTextBox_responsible_executer.Text.Replace(" ", "")) + ", '" + richTextBox_theme.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', 0)";
                            OleDbCommand command = new OleDbCommand(query, connectionDB);
                            command.ExecuteNonQuery();
                        }
                        else
                        {
                            String query = "INSERT INTO OKIDD5 (nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, data_is2, kol_per) VALUES (" + Convert.ToInt32(maskedTextBox_control_card.Text.Replace(" ", "")) + ", '" + comboBox_type_document.Text + "', '" + textBox_number.Text + "', '" + maskedTextBox_from.Text + "', '" + textBox_punkt.Text + "', " + Convert.ToInt32(maskedTextBox_responsible_executer.Text.Replace(" ", "")) + ", '" + richTextBox_theme.Text + "', '" + maskedTextBox_term_execute_begin.Text + "', '" + maskedTextBox_term_execute_end.Text + "', '" + maskedTextBox_term_execute_end.Text + "', 0)";
                            OleDbCommand command = new OleDbCommand(query, connectionDB);
                            command.ExecuteNonQuery();
                        }
                    }
                    catch(Exception Exp)
                    {
                        MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        connectionDB.Close();
                    }
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                MessageBox.Show("Карта успешно записана в базу данных!", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                if (Properties.Settings.Default.checkBox_autoinsert)
                {
                    Properties.Settings.Default.maskedTextBox_autoinsert++;
                    Properties.Settings.Default.Save();
                    maskedTextBox_control_card.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();
                }
                else
                    maskedTextBox_control_card.Clear();

                maskedTextBox_responsible_executer.Text = String.Empty;
            }

            comboBox_type_document.TabStop = false;
            textBox_number.TabStop = false;
            maskedTextBox_from.TabStop = false;
            richTextBox_theme.TabStop = false;
            maskedTextBox_term_execute_begin.TabStop = false;
            maskedTextBox_term_execute_end.TabStop = false;
            textBox_punkt.TabStop = false;
        }

        private void новойККToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_new_control_card.Visible = true;
            panel_correct.Visible = false;
            panel_card_filter.Visible = false;
            panel_reference.Visible = false;
            label_term_execute_end.Visible = false;
            maskedTextBox_term_execute_end.Visible = false;

            groupBox_new_card_main.Text = "Основное - Ввод новой КК \"Протокол ПДКК\", \"Прогр. качества\"";
            groupBox_new_card_content.Text = "Содержание - Ввод новой КК \"Протокол ПДКК\", \"Прогр. качества\"";

            String[] typesDocument = new String[] { "Протокол ПДКК", "Программа качества"};

            comboBox_type_document.Text = String.Empty;
            comboBox_type_document.Items.Clear();
            maskedTextBox_term_execute_end.Clear();

            foreach (String value in typesDocument)
                comboBox_type_document.Items.Add(value);

            if (Properties.Settings.Default.checkBox_autoinsert)
                maskedTextBox_control_card.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();
        }

        private void ответственногоИсполнителяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Service.GetNumberCard();
        }

        private void корректировкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(SelectCard())
            {
                panel_correct.Visible = true;
                panel_correct.BringToFront();
            }
        }

        private void button_select_card_Click(object sender, EventArgs e)
        {
            SelectCard();
        }

        internal bool SelectCard()
        {
            if (!Service.GetNumberCard())
                return false;

            try
            {
                //String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
                OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                try
                {
                    String popQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE nomer = " + Service.CardNumber;
                    connectionDB.Open();
                    OleDbCommand popCommand = new OleDbCommand(popQuery, connectionDB);
                    OleDbDataReader dataReader = popCommand.ExecuteReader();
                    dataReader.Read();
                    maskedTextBox_control_card_correct.Text = dataReader[0].ToString();
                    textBox_type_document_correct.Text = dataReader[1].ToString();
                    textBox_number_correct.Text = dataReader[2].ToString();
                    maskedTextBox_from_correct.Text = dataReader[3].ToString();
                    textBox_punkt_correct.Text = dataReader[4].ToString();
                    maskedTextBox_responsible_executer_correct.Text = dataReader[5].ToString();
                    richTextBox_theme_correct.Text = dataReader[6].ToString();
                    maskedTextBox_term_execute_begin_correct.Text = dataReader[7].ToString();
                    maskedTextBox_term_execute_end_correct.Text = dataReader[8].ToString();
                    label_total_moved_correct.Text = dataReader[9].ToString();
                    maskedTextBox_term_moved1.Text = dataReader[10].ToString();
                    maskedTextBox_term_moved2.Text = dataReader[11].ToString();
                    maskedTextBox_term_moved3.Text = dataReader[12].ToString();
                    maskedTextBox_term_moved4.Text = dataReader[13].ToString();
                    maskedTextBox_last_moved.Text = dataReader[14].ToString();
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                finally
                {
                    connectionDB.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            Service.Moved[0] = maskedTextBox_term_moved1.Text;
            Service.Moved[1] = maskedTextBox_term_moved2.Text;
            Service.Moved[2] = maskedTextBox_term_moved3.Text;
            Service.Moved[3] = maskedTextBox_term_moved4.Text;

            IncludeRespExecuter();
            return true;
        }

        private void button_correct_Click(object sender, EventArgs e)
        {
            if (!maskedTextBox_term_execute_begin_correct.MaskCompleted || maskedTextBox_responsible_executer_correct.Text.Trim() == String.Empty || !maskedTextBox_term_execute_end_correct.MaskCompleted && maskedTextBox_term_execute_end_correct.Text.Replace(".", "").Trim() != String.Empty || !maskedTextBox_term_moved1.MaskCompleted && maskedTextBox_term_moved1.Text.Replace(".", "").Trim() != String.Empty || !maskedTextBox_term_moved2.MaskCompleted && maskedTextBox_term_moved2.Text.Replace(".", "").Trim() != String.Empty || !maskedTextBox_term_moved3.MaskCompleted && maskedTextBox_term_moved3.Text.Replace(".", "").Trim() != String.Empty || !maskedTextBox_term_moved4.MaskCompleted && maskedTextBox_term_moved4.Text.Replace(".", "").Trim() != String.Empty || !maskedTextBox_last_moved.MaskCompleted && maskedTextBox_last_moved.Text.Replace(".", "").Trim() != String.Empty)
            {
                MessageBox.Show("Все поля должны быть корректно заполнены.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Подтвердите правильность корректировки.", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (maskedTextBox_term_moved1.Text != Service.Moved[0] && maskedTextBox_term_moved1.Text.Replace(".", "").Trim() != String.Empty)
                label_total_moved_correct.Text = (Convert.ToInt32(label_total_moved_correct.Text) + 1).ToString();

            if (maskedTextBox_term_moved2.Text != Service.Moved[1] && maskedTextBox_term_moved2.Text.Replace(".", "").Trim() != String.Empty)
                label_total_moved_correct.Text = (Convert.ToInt32(label_total_moved_correct.Text) + 1).ToString();

            if (maskedTextBox_term_moved3.Text != Service.Moved[2] && maskedTextBox_term_moved3.Text.Replace(".", "").Trim() != String.Empty)
                label_total_moved_correct.Text = (Convert.ToInt32(label_total_moved_correct.Text) + 1).ToString();

            if (maskedTextBox_term_moved4.Text != Service.Moved[3] && maskedTextBox_term_moved4.Text.Replace(".", "").Trim() != String.Empty)
                label_total_moved_correct.Text = (Convert.ToInt32(label_total_moved_correct.Text) + 1).ToString();

            string data_rez = (maskedTextBox_term_execute_end_correct.MaskCompleted) ? "data_rez = " + "'" + maskedTextBox_term_execute_end_correct.Text + "', " : "data_rez = NULL, ";
            string data_per = (maskedTextBox_term_moved1.MaskCompleted) ? "data_per = " + "'" + maskedTextBox_term_moved1.Text + "', " : "data_per = NULL, ";
            string d_per2 = (maskedTextBox_term_moved2.MaskCompleted) ? "d_per2 = " + "'" + maskedTextBox_term_moved2.Text  + "', " : "d_per2 = NULL, ";
            string d_per3 = (maskedTextBox_term_moved3.MaskCompleted) ? "d_per3 = " + "'" + maskedTextBox_term_moved3.Text + "', " : "d_per3 = NULL, ";
            string d_per4 = (maskedTextBox_term_moved4.MaskCompleted) ? "d_per4 = " + "'" + maskedTextBox_term_moved4.Text + "', " : "d_per4 = NULL, ";
            string d_sog_pp = (maskedTextBox_last_moved.MaskCompleted) ? "d_sog_pp = " + "'" + maskedTextBox_last_moved.Text + "', " : "d_sog_pp = NULL, ";

            try
            {
                OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                try
                {
                    String updateQuery = "UPDATE OKIDD5 SET " + data_rez + data_per + d_per2 + d_per3 + d_per4 + d_sog_pp + "ispoln = " + Convert.ToInt32(maskedTextBox_responsible_executer_correct.Text.Replace(" ", "")) + ", data_isp = '" + maskedTextBox_term_execute_begin_correct.Text + "', kol_per = " + Convert.ToInt32(label_total_moved_correct.Text) + " WHERE fakt_isp IS NULL and nomer = " + Convert.ToInt32(maskedTextBox_control_card_correct.Text);
                    connectionDB.Open();
                    OleDbCommand replaceCommand = new OleDbCommand(updateQuery, connectionDB);
                    if(replaceCommand.ExecuteNonQuery() == 0)
                    {
                        MessageBox.Show("Карточка снята с контроля или отсутствует в таблице.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    connectionDB.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Service.Moved[0] = maskedTextBox_term_moved1.Text;
            Service.Moved[1] = maskedTextBox_term_moved2.Text;
            Service.Moved[2] = maskedTextBox_term_moved3.Text;
            Service.Moved[3] = maskedTextBox_term_moved4.Text;

            IncludeRespExecuter();
            
            MessageBox.Show("Карта с номером " + Service.CardNumber + " успешно откорректирована!", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void IncludeRespExecuter()
        {
            try
            {
                OleDbConnection connectionDB_KSPR = new OleDbConnection(Service.ConnectionString);
                try
                {
                    String ResponsibleExecutorQuery = "SELECT naim FROM KSPR WHERE kod_spr = 10 and kod_naim = " + maskedTextBox_responsible_executer_correct.Text;
                    connectionDB_KSPR.Open();
                    OleDbCommand popCommand_KSPR = new OleDbCommand(ResponsibleExecutorQuery, connectionDB_KSPR);
                    OleDbDataReader dataReaderKSPR = popCommand_KSPR.ExecuteReader();
                    dataReaderKSPR.Read();
                    textBox_responsible_executer.Text = dataReaderKSPR[0].ToString();
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox_responsible_executer.Clear();
                }
                finally
                {
                    connectionDB_KSPR.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void снятиеСКонтроляКартыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Service.GetNumberCard())
                return;

            try
            {
                //String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
                OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                try
                {
                    connectionDB.Open();
                    String insertQuery = "UPDATE OKIDD5 SET fakt_isp = #" + DateTime.Now.Date.ToShortDateString().Replace(".", "/") + "# WHERE nomer = " + Service.CardNumber;
                    OleDbCommand insertCommand = new OleDbCommand(insertQuery, connectionDB);
                    insertCommand.ExecuteNonQuery();
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    connectionDB.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Service.CardNumber == maskedTextBox_control_card_correct.Text)
                panel_correct.Visible = false;

            MessageBox.Show("Карточка успешно снята с контроля!", "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void maskedTextBox_term_moved1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox_term_moved1.MaskCompleted && maskedTextBox_term_moved1.Text.Replace(".", "") != String.Empty)
            {
                maskedTextBox_term_moved2.ReadOnly = false;
                maskedTextBox_term_moved2.TabStop = true;

                if(maskedTextBox_term_moved2.MaskCompleted)
                {
                    maskedTextBox_term_moved3.ReadOnly = false;
                    maskedTextBox_term_moved3.TabStop = true;
                }

                if(maskedTextBox_term_moved3.MaskCompleted)
                {
                    maskedTextBox_term_moved4.ReadOnly = false;
                    maskedTextBox_term_moved4.TabStop = true;
                }
            }
            else
            {
                maskedTextBox_term_moved2.ReadOnly = true;
                maskedTextBox_term_moved3.ReadOnly = true;
                maskedTextBox_term_moved4.ReadOnly = true;
                maskedTextBox_term_moved2.TabStop = false;
                maskedTextBox_term_moved3.TabStop = false;
                maskedTextBox_term_moved4.TabStop = false;
                maskedTextBox_term_moved2.Clear();
                maskedTextBox_term_moved3.Clear();
                maskedTextBox_term_moved4.Clear();
            }
        }

        private void maskedTextBox_term_moved2_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox_term_moved2.MaskCompleted && maskedTextBox_term_moved2.Text.Replace(".", "") != String.Empty)
            {
                maskedTextBox_term_moved3.ReadOnly = false;
                maskedTextBox_term_moved3.TabStop = true;

                if (maskedTextBox_term_moved3.MaskCompleted)
                {
                    maskedTextBox_term_moved4.ReadOnly = false;
                    maskedTextBox_term_moved4.TabStop = true;
                }
            }
            else
            {
                maskedTextBox_term_moved3.ReadOnly = true;
                maskedTextBox_term_moved4.ReadOnly = true;
                maskedTextBox_term_moved3.TabStop = false;
                maskedTextBox_term_moved4.TabStop = false;
                maskedTextBox_term_moved3.Clear();
                maskedTextBox_term_moved4.Clear();
            }
        }

        private void maskedTextBox_term_moved3_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox_term_moved3.MaskCompleted && maskedTextBox_term_moved3.Text.Replace(".", "") != String.Empty)
            {
                maskedTextBox_term_moved4.ReadOnly = false;
                maskedTextBox_term_moved4.TabStop = true;
            }
            else
            {
                maskedTextBox_term_moved4.ReadOnly = true;
                maskedTextBox_term_moved4.TabStop = false;
                maskedTextBox_term_moved4.Clear();
            }
        }

        private void невыполненныхКонтрольныхКартToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String formQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE fakt_isp IS NULL";
            Service.Printing(formQuery, printDocument2, printDialog1);
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Service.PrintPageEvent(e);
        }

        private void withFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_card_filter.Visible = true;
            panel_card_filter.BringToFront();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            SearchCards();
        }

        public string SearchCards()
        {
            if (!checkBox_type_document_filter.Checked && !checkBox_punkt_filter.Checked && !checkBox_number_doc_filter.Checked && !checkBox_last_move_filter.Checked && !checkBox_executor_filter.Checked && !checkBox_end_filter.Checked && !checkBox_date_move4_filter.Checked && !checkBox_date_move3_filter.Checked && !checkBox_date_move2_filter.Checked && !checkBox_date_move1_fitler.Checked && !checkBox_date_dok_filter.Checked && !checkBox_begin_filter.Checked)
            {
                MessageBox.Show("Выберите хотя бы один фильтр.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return String.Empty;
            }

            if (checkBox_type_document_filter.Checked && (comboBox_type_document_filter.SelectedIndex == -1 || comboBox_type_document_filter.Text.Trim() == String.Empty) || checkBox_punkt_filter.Checked && textBox_punkt_filter.Text.Trim() == String.Empty || checkBox_number_doc_filter.Checked && textBox_number_doc_filter.Text.Trim() == String.Empty || checkBox_last_move_filter.Checked && (maskedTextBox_last_move_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_last_move_filter.MaskCompleted) || checkBox_executor_filter.Checked && maskedTextBox_executor_filter.Text.Trim() == String.Empty || checkBox_end_filter.Checked && (maskedTextBox_end_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_end_filter.MaskCompleted) || checkBox_date_move4_filter.Checked && (maskedTextBox_date_move4_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_date_move4_filter.MaskCompleted) || checkBox_date_move3_filter.Checked && (maskedTextBox_date_move3_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_date_move3_filter.MaskCompleted) || checkBox_date_move2_filter.Checked && (maskedTextBox_date_move2_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_date_move2_filter.MaskCompleted) || checkBox_date_move1_fitler.Checked && (maskedTextBox_date_move1_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_date_move1_filter.MaskCompleted) || checkBox_date_dok_filter.Checked && (maskedTextBox_date_dok_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_date_dok_filter.MaskCompleted) || checkBox_begin_filter.Checked && (maskedTextBox_begin_filter.Text.Replace(".", "") == String.Empty || !maskedTextBox_begin_filter.MaskCompleted))
            {
                MessageBox.Show("Заполните все выбранные фильтры.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return String.Empty;
            }

            string typeDocument = String.Empty;
            string punkt = String.Empty;
            string numbersDocument = String.Empty;
            string dateDocument = String.Empty;
            string Executor = String.Empty;
            string lastMove = String.Empty;
            string dateBegin = String.Empty;
            string dateEnd = String.Empty;
            string dateMove1 = String.Empty;
            string dateMove2 = String.Empty;
            string dateMove3 = String.Empty;
            string dateMove4 = String.Empty;

            string Operation = "and";
            char operationBegin = '=';
            char operationEnd = '=';
            char operationLast = '=';

            if (radioButton_larger_begin.Checked)
                operationBegin = '>';
            if (radioButton2.Checked)
                operationBegin = '<';
            if (radioButton1.Checked)
                operationBegin = '=';

            if (radioButton_larger_end.Checked)
                operationEnd = '>';
            if (radioButton_less_end.Checked)
                operationEnd = '<';
            if (radioButton_equal_end.Checked)
                operationEnd = '=';

            if (radioButton_larger_last.Checked)
                operationLast = '>';
            if (radioButton_less_last.Checked)
                operationLast = '<';
            if (radioButton_equal_last.Checked)
                operationLast = '=';

            if (radioButton_and.Checked)
                Operation = "and";
            if (radioButton_or.Checked)
                Operation = "or";
            if (radioButton_not.Checked)
                Operation = "not";



            if (checkBox_type_document_filter.Checked)
                typeDocument = "vid_dok = '" + comboBox_type_document_filter.Text.Trim() + "' ";

            if (checkBox_punkt_filter.Checked && checkBox_type_document_filter.Checked)
                punkt = Operation + " punkt = '" + textBox_punkt_filter.Text.Trim() + "' ";
            else
                if (checkBox_punkt_filter.Checked)
                punkt = "punkt = '" + textBox_punkt_filter.Text.Trim() + "' ";

            if (checkBox_number_doc_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked))
                numbersDocument = Operation + " nomer_dok = '" + textBox_number_doc_filter.Text.Trim() + "' ";
            else
                if (checkBox_number_doc_filter.Checked)
                numbersDocument = "nomer_dok = '" + textBox_number_doc_filter.Text.Trim() + "' ";

            if (checkBox_last_move_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked))
                lastMove = Operation + " d_sog_pp " + operationLast + " #" + maskedTextBox_last_move_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_last_move_filter.Checked)
                lastMove = "d_sog_pp " + operationLast + " #" + maskedTextBox_last_move_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_executor_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked))
                Executor = Operation + " ispoln = " + maskedTextBox_executor_filter.Text + " ";
            else
                if (checkBox_executor_filter.Checked)
                Executor = "ispoln = " + maskedTextBox_executor_filter.Text + " ";

            if (checkBox_end_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked))
                dateEnd = Operation + " data_rez " + operationEnd + " #" + maskedTextBox_end_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_end_filter.Checked)
                dateEnd = "data_rez " + operationEnd + " #" + maskedTextBox_end_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_date_move4_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked))
                dateMove4 = Operation + " d_per4 = #" + maskedTextBox_date_move4_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_date_move4_filter.Checked)
                dateMove4 = "d_per4 = #" + maskedTextBox_date_move4_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_date_move3_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked || checkBox_date_move4_filter.Checked))
                dateMove3 = Operation + " d_per3 = #" + maskedTextBox_date_move3_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_date_move3_filter.Checked)
                dateMove3 = "d_per3 = #" + maskedTextBox_date_move3_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_date_move2_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked || checkBox_date_move4_filter.Checked || checkBox_date_move3_filter.Checked))
                dateMove2 = Operation + " d_per2 = #" + maskedTextBox_date_move2_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_date_move2_filter.Checked)
                dateMove2 = "d_per2 = #" + maskedTextBox_date_move2_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_date_move1_fitler.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked || checkBox_date_move4_filter.Checked || checkBox_date_move3_filter.Checked || checkBox_date_move2_filter.Checked))
                dateMove1 = Operation + " data_per = #" + maskedTextBox_date_move1_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_date_move1_fitler.Checked)
                dateMove1 = "data_per = #" + maskedTextBox_date_move1_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_date_dok_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked || checkBox_date_move4_filter.Checked || checkBox_date_move3_filter.Checked || checkBox_date_move2_filter.Checked || checkBox_date_move1_fitler.Checked))
                dateDocument = Operation + " data_dok = #" + maskedTextBox_date_dok_filter.Text.Replace(".", "/") + "# ";
            else
                if (checkBox_date_dok_filter.Checked)
                dateDocument = "data_dok = #" + maskedTextBox_date_dok_filter.Text.Replace(".", "/") + "# ";

            if (checkBox_begin_filter.Checked && (checkBox_type_document_filter.Checked || checkBox_punkt_filter.Checked || checkBox_number_doc_filter.Checked || checkBox_last_move_filter.Checked || checkBox_executor_filter.Checked || checkBox_end_filter.Checked || checkBox_date_move4_filter.Checked || checkBox_date_move3_filter.Checked || checkBox_date_move2_filter.Checked || checkBox_date_move1_fitler.Checked || checkBox_date_dok_filter.Checked))
                dateBegin = Operation + " data_isp " + operationBegin + " #" + maskedTextBox_begin_filter.Text.Replace(".", "/") + "#";
            else
                if (checkBox_begin_filter.Checked)
                dateBegin = "data_isp " + operationBegin + " #" + maskedTextBox_begin_filter.Text.Replace(".", "/") + "#";

            int Count = 0;

            String searchQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE (" + typeDocument + punkt + numbersDocument + lastMove + Executor + dateEnd + dateMove4 + dateMove3 + dateMove2 + dateMove1 + dateDocument + dateBegin + ") and fakt_isp IS NULL";
            try
            {
                //String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
                OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                try
                {
                    connectionDB.Open();
                    OleDbCommand searchCommand = new OleDbCommand(searchQuery, connectionDB);
                    OleDbDataReader dataReader = searchCommand.ExecuteReader();
                    while (dataReader.Read())
                        Count++;
                }
                catch (Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return String.Empty;
                }
                finally
                {
                    connectionDB.Close();
                }
            }
            catch(Exception Exp)
            {
                MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return String.Empty;
            }

            label_report.Text = "Всего найдено карточек: " + Count;

            return searchQuery;
        }

        private void button_form_Click(object sender, EventArgs e)
        {
            Service.Printing(SearchCards(), printDocument2, printDialog1);
        }

        private void перечняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UntimelyExecute untimelyExecute = new UntimelyExecute();
            
            if(untimelyExecute.ShowDialog() == DialogResult.OK)
            {
                String formQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE fakt_isp > data_rez and fakt_isp IS NOT NULL and fakt_isp >= #" + untimelyExecute.maskedTextBox_begin.Text.Replace(".", "/") + "# and fakt_isp <= #" + untimelyExecute.maskedTextBox_end.Text.Replace(".", "/") + "#";
                Service.Printing(formQuery, printDocument2, printDialog1);
            }
        }

        private void перечняПоНесвоевременномуПереносуККToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UntimelyExecute untimelyExecute = new UntimelyExecute();

            if(untimelyExecute.ShowDialog() == DialogResult.OK)
            {
                String formQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE data_rez < d_sog_pp and d_sog_pp IS NOT NULL and d_sog_pp >= #" + untimelyExecute.maskedTextBox_begin.Text.Replace(".", "/") + "# and d_sog_pp <= #" + untimelyExecute.maskedTextBox_end.Text.Replace(".", "/") + "#";
                Service.Printing(formQuery, printDocument2, printDialog1);
            }
        }

        private void программаКачестваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_reference.Visible = true;
            panel_reference.BringToFront();
            maskedTextBox_end_reference.Visible = false;
            comboBox_type_document_reference.Visible = true;
            label_desc.Text = "Признак выбора мероприятий:";
            Service.Reference = true;
            label_head.Text = "Справка по мероприятиям \"Программы качества\" на " + DateTime.Now.Date.ToShortDateString();
            label_all.Text = "Запланировано: ";
            label_complete.Text = "Выполнено: ";
            label_moved.Text = "Перенесено: ";
            label_notcomplete.Text = "Не выполнено: ";
            label_treaded.Text = "Не обработанных: ";
        }

        private void протоклПДККToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel_reference.Visible = true;
            panel_reference.BringToFront();
            label_desc.Text = "Дата окончания отчётного периода: ";
            maskedTextBox_end_reference.Visible = true;
            comboBox_type_document_reference.Visible = false;
            label_head.Text = "Справка по мероприятиям \"Протокол ПДКК\"";
            label_all.Text = "Запланировано: ";
            label_complete.Text = "Выполнено: ";
            label_moved.Text = "Перенесено: ";
            label_notcomplete.Text = "Не выполнено: ";
            label_treaded.Text = "Не обработанных: ";
            Service.Reference = false;
        }

        private void button_show_Click(object sender, EventArgs e)
        {
            int all = 0;
            int complete = 0;
            int moved = 0;
            int notcomplete = 0;

            int incorrect = 0;

            if (Service.Reference)
            {
                if (comboBox_type_document_reference.Text.Trim() == String.Empty || comboBox_type_document_reference.SelectedIndex == -1)
                {
                    MessageBox.Show("Выберете вид документа.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                    try
                    {
                        connectionDB.Open();
                        String searchQuery = "SELECT data_rez, kol_per, fakt_isp FROM OKIDD5 WHERE vid_dok = '" + comboBox_type_document_reference.Text + "'";
                        OleDbCommand searchCommand = new OleDbCommand(searchQuery, connectionDB);
                        OleDbDataReader dataReader = searchCommand.ExecuteReader();

                        while (dataReader.Read())
                        {
                            all++;
                            if (dataReader[0].ToString() == String.Empty || dataReader[1].ToString() == String.Empty)
                                incorrect++;
                            else
                            {
                                if (dataReader[2].ToString() == String.Empty)
                                    if (Convert.ToDateTime(dataReader[0]) > DateTime.Now.Date && Convert.ToInt32(dataReader[1]) > 0)
                                        moved++;
                                    else
                                        notcomplete++;
                                else
                                    complete++;
                            }
                        }
                    }
                    catch(Exception Exp)
                    {
                        MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        connectionDB.Close();
                    }
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                label_head.Text = "Справка по мероприятиям \"Программы качества\" на " + DateTime.Now.Date.ToShortDateString();
            }
            else
            {
                if (!maskedTextBox_end_reference.MaskCompleted)
                {
                    MessageBox.Show("Заполните все поля.", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    OleDbConnection connectionDB = new OleDbConnection(Service.ConnectionString);
                    try
                    {
                        connectionDB.Open();
                        String searchQuery = "SELECT data_rez, kol_per, fakt_isp FROM OKIDD5 WHERE vid_dok = 'Протокол ПДКК' and data_isp >= #01/01/2016# and data_isp <= #" + maskedTextBox_end_reference.Text.Replace(".", "/") + "#";
                        OleDbCommand searchCommand = new OleDbCommand(searchQuery, connectionDB);
                        OleDbDataReader dataReader = searchCommand.ExecuteReader();

                        while (dataReader.Read())
                        {
                            all++;
                            if (dataReader[0].ToString() == String.Empty || dataReader[1].ToString() == String.Empty)
                                incorrect++;
                            else
                            {
                                if (dataReader[2].ToString() == String.Empty)
                                    if (Convert.ToDateTime(dataReader[0]) > Convert.ToDateTime(maskedTextBox_end_reference.Text) && Convert.ToInt32(dataReader[1]) > 0)
                                        moved++;
                                    else
                                        notcomplete++;
                                else
                                    complete++;
                            }
                        }
                    }
                    catch(Exception Exp)
                    {
                        MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        connectionDB.Close();
                    }
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                label_head.Text = "Справка по мероприятиям \"Протокол ПДКК\" на " + maskedTextBox_end_reference.Text;
            }

            label_all.Text = "Запланировано: " + all;
            label_complete.Text = "Выполнено: " + complete;
            label_moved.Text = "Перенесено: " + moved;
            label_notcomplete.Text = "Не выполнено: " + notcomplete;
            label_treaded.Text = "Не обработанных: " + incorrect;
        }

        private void работаСАрхивомВыполненныхМероприятийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteInArchive writeInArchive = new WriteInArchive();
            writeInArchive.ShowDialog();
        }

        private void button_form_correct_Click(object sender, EventArgs e)
        {
            String formQuery = "SELECT nomer, vid_dok, nomer_dok, data_dok, punkt, ispoln, tema, data_isp, data_rez, kol_per, data_per, d_per2, d_per3, d_per4, d_sog_pp FROM OKIDD5 WHERE fakt_isp IS NULL and nomer = " + Convert.ToInt32(maskedTextBox_control_card_correct.Text);
            Service.Printing(formQuery, printDocument2, printDialog1);
        }

        private void руководствоПоПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Manual manual = new Manual();
            manual.Show();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void автозаполнениеНомераККToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoInsertNumber autoInsertNumber = new AutoInsertNumber();
            autoInsertNumber.ShowDialog();

            if (Properties.Settings.Default.checkBox_autoinsert)
                maskedTextBox_control_card.Text = Properties.Settings.Default.maskedTextBox_autoinsert.ToString();

            maskedTextBox_control_card.TabStop = (Properties.Settings.Default.checkBox_autoinsert) ? false : true;
        }
    }

    internal static class Service
    {
        private static string cardNumber;
        private static string[] moved = { "", "", "", "" };
        private static bool reference = false;
        private static String connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + Properties.Settings.Default.textBox_OKDD5;
        private static List<NotExecute> notExecutes;

        private static int emptyFields = 0;
        private static int complete = 0;

        public static string CardNumber { get => cardNumber; set => cardNumber = value; }

        public static string[] Moved { get => moved; set => moved = value; }

        public static String ConnectionString { get => connectionString; }

        public static bool Reference { get => reference; set => reference = value; }

        public struct NotExecute
        {
            public string Number;
            public string TypeDocument;
            public string NumberDocument;
            public string DateDocument;
            public string Punkt;
            public string nExecutor;
            public string sExecutor;
            public string Theme;
            public string DateBegin;
            public string DateEnd;
            public string CountMoved;
            public string DateMoved1;
            public string DateMoved2;
            public string DateMoved3;
            public string DateMoved4;
            public string DateLastMoved;

            public NotExecute(string Number, string TypeDocument, string NumberDocument, string DateDocument, string Punkt, string nExecutor, string sExecutor, string Theme, string DateBegin, string DateEnd, string CountMoved, string DateMoved1, string DateMoved2, string DateMoved3, string DateMoved4, string DateLastMoved)
            {
                this.Number = Number;
                this.TypeDocument = TypeDocument;
                this.NumberDocument = NumberDocument;
                this.DateDocument = DateDocument;
                this.Punkt = Punkt;
                this.nExecutor = nExecutor;
                this.sExecutor = sExecutor;
                this.Theme = Theme;
                this.DateBegin = DateBegin;
                this.DateEnd = DateEnd;
                this.CountMoved = CountMoved;
                this.DateMoved1 = DateMoved1;
                this.DateMoved2 = DateMoved2;
                this.DateMoved3 = DateMoved3;
                this.DateMoved4 = DateMoved4;
                this.DateLastMoved = DateLastMoved;
            }
        }

        public static bool GetNumberCard()
        {
            SearchCard searchCard = new SearchCard();
            return searchCard.ShowDialog() == DialogResult.OK;
        }

        public static void PrintCardV1(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 3), new Rectangle(new Point(50, 50), new Size(725, 510)));
            using (Font font = new Font("Courier", 12, FontStyle.Regular))
            {
                e.Graphics.DrawString("Форма МП0100", font, Brushes.Black, 645, 50);
                e.Graphics.DrawString("Контрольная карта N: " + notExecutes[notExecutes.Count - 1].Number, font, Brushes.Black, 60, 75);
                e.Graphics.DrawString("Вид документа: " + notExecutes[notExecutes.Count - 1].TypeDocument, font, Brushes.Black, 60, 100);
                e.Graphics.DrawString("Пункт: " + notExecutes[notExecutes.Count - 1].Punkt, font, Brushes.Black, 60, 125);
                e.Graphics.DrawString("от " + notExecutes[notExecutes.Count - 1].DateDocument, font, Brushes.Black, 625, 75);
                e.Graphics.DrawString("N: " + notExecutes[notExecutes.Count - 1].NumberDocument, font, Brushes.Black, 475, 75);
                e.Graphics.DrawString("Отв. исполнитель нач.: ", font, Brushes.Black, 475, 100);
                if (notExecutes[notExecutes.Count - 1].sExecutor != String.Empty)
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].nExecutor + " " + notExecutes[notExecutes.Count - 1].sExecutor, font, Brushes.Black, 315, 125);
                else
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].nExecutor, font, Brushes.Black, 710, 100);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 150), new Point(775, 150));
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(555, 150), new Point(555, 430));
                if (notExecutes[notExecutes.Count - 1].Theme.Length < 48)
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].Theme, font, Brushes.Black, 50, 155);
                else
                    if (notExecutes[notExecutes.Count - 1].Theme.Length > 48 && notExecutes[notExecutes.Count - 1].Theme.Length <= 96)
                {
                    String[] buffer = new String[2];
                    buffer[0] = notExecutes[notExecutes.Count - 1].Theme.Substring(0, 47);
                    buffer[1] = notExecutes[notExecutes.Count - 1].Theme.Substring(48);
                    e.Graphics.DrawString(buffer[0], font, Brushes.Black, 50, 155);
                    e.Graphics.DrawString(buffer[1], font, Brushes.Black, 50, 177.5F);
                }
                else
                    if (notExecutes[notExecutes.Count - 1].Theme.Length > 96)
                {
                    String[] buffer = new String[3];
                    buffer[0] = notExecutes[notExecutes.Count - 1].Theme.Substring(0, 47);
                    buffer[1] = notExecutes[notExecutes.Count - 1].Theme.Substring(48, 48);
                    buffer[2] = notExecutes[notExecutes.Count - 1].Theme.Substring(96);
                    e.Graphics.DrawString(buffer[0], font, Brushes.Black, 50, 155);
                    e.Graphics.DrawString(buffer[1], font, Brushes.Black, 50, 177.5F);
                    e.Graphics.DrawString(buffer[2], font, Brushes.Black, 50, 200);
                }
                e.Graphics.DrawString("Срок исполнения", font, Brushes.Black, 587, 155);
                e.Graphics.DrawString("Нач.  " + notExecutes[notExecutes.Count - 1].DateBegin, font, Brushes.Black, 567, 180);
                e.Graphics.DrawString("Окон. " + notExecutes[notExecutes.Count - 1].DateEnd, font, Brushes.Black, 567, 200);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 222), new Point(775, 222));
                e.Graphics.DrawString("Отчёт о выполнении", font, Brushes.Black, 206, 227);
                e.Graphics.DrawString("Срок перенесён до:", font, Brushes.Black, 565, 227);
                if (notExecutes[notExecutes.Count - 1].TypeDocument == "Протокол ПДКК" || notExecutes[notExecutes.Count - 1].TypeDocument == "Программа качества")
                {
                    e.Graphics.DrawString("Согласовано", new Font("Courier", 12, FontStyle.Underline), Brushes.Black, 605, 247);
                    e.Graphics.DrawString("Срок до", font, Brushes.Black, 558, 267);
                    if (notExecutes[notExecutes.Count - 1].TypeDocument == "Протокол ПДКК")
                        e.Graphics.DrawString("Тех.Дир", font, Brushes.Black, 645, 267);
                    else
                        e.Graphics.DrawString("Нач УК", font, Brushes.Black, 650, 267);
                    e.Graphics.DrawString("НачВп", font, Brushes.Black, 718, 267);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(645, 270), new Point(645, 407));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(721, 270), new Point(721, 407));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 317), new Point(775, 317));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved1, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 297);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 347), new Point(775, 347));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved2, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 327);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 377), new Point(775, 377));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved3, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 357);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 407), new Point(775, 407));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved4, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 387);
                }
                else
                {
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved1, font, Brushes.Black, 610, 272);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved2, font, Brushes.Black, 610, 302);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved3, font, Brushes.Black, 610, 332);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateMoved4, font, Brushes.Black, 610, 362);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 292), new Point(770, 292));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 322), new Point(770, 322));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 352), new Point(770, 352));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 382), new Point(770, 382));
                }
                e.Graphics.DrawString("Всего переносов: " + notExecutes[notExecutes.Count - 1].CountMoved, font, Brushes.Black, 555, 410);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 430), new Point(775, 430));
                e.Graphics.DrawString("Дата согласования последнего переноса: ", font, Brushes.Black, 100, 437);
                e.Graphics.DrawString(notExecutes[notExecutes.Count - 1].DateLastMoved, font, Brushes.Black, 505, 437);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 460), new Point(775, 460));
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(555, 460), new Point(555, 560));
                e.Graphics.DrawString("Вернуть в ОТК для снятия с контроля", font, Brushes.Black, 60, 495);
                e.Graphics.DrawString("Выполнение подтвержд.", font, Brushes.Black, 555, 465);
                e.Graphics.DrawString("/", font, Brushes.Black, 575, 525);
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(580, 540), new Point(750, 540));
                e.Graphics.DrawString("/", font, Brushes.Black, 746, 525);

            }
        }

        public static void PrintCardV2(System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 3), new Rectangle(new Point(50, 610), new Size(725, 510)));
            using (Font font = new Font("Courier", 12, FontStyle.Regular))
            {
                e.Graphics.DrawString("Форма МП0100", font, Brushes.Black, 645, 610);
                e.Graphics.DrawString("Контрольная карта N: " + notExecutes[notExecutes.Count - 2].Number, font, Brushes.Black, 60, 635);
                e.Graphics.DrawString("Вид документа: " + notExecutes[notExecutes.Count - 2].TypeDocument, font, Brushes.Black, 60, 660);
                e.Graphics.DrawString("Пункт: " + notExecutes[notExecutes.Count - 2].Punkt, font, Brushes.Black, 60, 685);
                e.Graphics.DrawString("от " + notExecutes[notExecutes.Count - 2].DateDocument, font, Brushes.Black, 625, 635);
                e.Graphics.DrawString("N: " + notExecutes[notExecutes.Count - 2].NumberDocument, font, Brushes.Black, 475, 635);
                e.Graphics.DrawString("Отв. исполнитель нач.: ", font, Brushes.Black, 475, 660);
                if (notExecutes[notExecutes.Count - 2].sExecutor != String.Empty)
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].nExecutor + " " + notExecutes[notExecutes.Count - 2].sExecutor, font, Brushes.Black, 315, 685);
                else
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].nExecutor, font, Brushes.Black, 710, 660);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 710), new Point(775, 710));
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(555, 710), new Point(555, 990));
                if (notExecutes[notExecutes.Count - 2].Theme.Length < 48)
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].Theme, font, Brushes.Black, 50, 715);
                else
                    if (notExecutes[notExecutes.Count - 2].Theme.Length > 48 && notExecutes[notExecutes.Count - 2].Theme.Length <= 96)
                {
                    String[] buffer = new String[2];
                    buffer[0] = notExecutes[notExecutes.Count - 2].Theme.Substring(0, 47);
                    buffer[1] = notExecutes[notExecutes.Count - 2].Theme.Substring(48);
                    e.Graphics.DrawString(buffer[0], font, Brushes.Black, 50, 715);
                    e.Graphics.DrawString(buffer[1], font, Brushes.Black, 50, 737.5F);
                }
                else
                    if (notExecutes[notExecutes.Count - 2].Theme.Length > 96)
                {
                    String[] buffer = new String[3];
                    buffer[0] = notExecutes[notExecutes.Count - 2].Theme.Substring(0, 47);
                    buffer[1] = notExecutes[notExecutes.Count - 2].Theme.Substring(48, 48);
                    buffer[2] = notExecutes[notExecutes.Count - 2].Theme.Substring(96);
                    e.Graphics.DrawString(buffer[0], font, Brushes.Black, 50, 715);
                    e.Graphics.DrawString(buffer[1], font, Brushes.Black, 50, 737.5F);
                    e.Graphics.DrawString(buffer[2], font, Brushes.Black, 50, 760);
                }
                e.Graphics.DrawString("Срок исполнения", font, Brushes.Black, 587, 715);
                e.Graphics.DrawString("Нач.  " + notExecutes[notExecutes.Count - 2].DateBegin, font, Brushes.Black, 567, 740);
                e.Graphics.DrawString("Окон. " + notExecutes[notExecutes.Count - 2].DateEnd, font, Brushes.Black, 567, 760);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 782), new Point(775, 782));
                e.Graphics.DrawString("Отчёт о выполнении", font, Brushes.Black, 206, 787);
                e.Graphics.DrawString("Срок перенесён до:", font, Brushes.Black, 565, 787);
                if (notExecutes[notExecutes.Count - 2].TypeDocument == "Протокол ПДКК" || notExecutes[notExecutes.Count - 2].TypeDocument == "Программа качества")
                {
                    e.Graphics.DrawString("Согласовано", new Font("Courier", 12, FontStyle.Underline), Brushes.Black, 605, 807);
                    e.Graphics.DrawString("Срок до", font, Brushes.Black, 558, 827);
                    if (notExecutes[notExecutes.Count - 2].TypeDocument == "Протокол ПДКК")
                        e.Graphics.DrawString("Тех.Дир", font, Brushes.Black, 645, 827);
                    else
                        e.Graphics.DrawString("Нач УК", font, Brushes.Black, 650, 827);
                    e.Graphics.DrawString("НачВп", font, Brushes.Black, 718, 827);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(645, 830), new Point(645, 967));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(721, 830), new Point(721, 967));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 877), new Point(775, 877));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved1, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 857);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 907), new Point(775, 907));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved2, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 887);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 937), new Point(775, 937));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved3, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 917);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(555, 967), new Point(775, 967));
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved4, new Font("Courier", 10, FontStyle.Regular), Brushes.Black, 555, 947);
                }
                else
                {
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved1, font, Brushes.Black, 610, 832);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved2, font, Brushes.Black, 610, 862);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved3, font, Brushes.Black, 610, 892);
                    e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateMoved4, font, Brushes.Black, 610, 922);
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 852), new Point(770, 852));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 882), new Point(770, 882));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 912), new Point(770, 912));
                    e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(560, 942), new Point(770, 942));
                }
                e.Graphics.DrawString("Всего переносов: " + notExecutes[notExecutes.Count - 2].CountMoved, font, Brushes.Black, 555, 970);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 990), new Point(775, 990));
                e.Graphics.DrawString("Дата согласования последнего переноса: ", font, Brushes.Black, 100, 997);
                e.Graphics.DrawString(notExecutes[notExecutes.Count - 2].DateLastMoved, font, Brushes.Black, 505, 997);
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(50, 1020), new Point(775, 1020));
                e.Graphics.DrawLine(new Pen(Color.Black, 2), new Point(555, 1020), new Point(555, 1120));
                e.Graphics.DrawString("Вернуть в ОТК для снятия с контроля", font, Brushes.Black, 60, 1055);
                e.Graphics.DrawString("Выполнение подтвержд.", font, Brushes.Black, 555, 1025);
                e.Graphics.DrawString("/", font, Brushes.Black, 575, 1085);
                e.Graphics.DrawLine(new Pen(Color.Black, 1), new Point(580, 1100), new Point(750, 1100));
                e.Graphics.DrawString("/", font, Brushes.Black, 746, 1085);

            }
        }

        public static void Printing(String formQuery, System.Drawing.Printing.PrintDocument printDocument, PrintDialog printDialog)
        {
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                List<NotExecute> notExecute = new List<NotExecute>();
                emptyFields = 0;
                complete = 0;

                try
                {
                    OleDbConnection connectionDB = new OleDbConnection(ConnectionString);
                    try
                    {
                        connectionDB.Open();
                        OleDbCommand formCommand = new OleDbCommand(formQuery, connectionDB);
                        OleDbDataReader dataReader = formCommand.ExecuteReader();

                        while (dataReader.Read())
                        {
                            String kspr = String.Empty;
                            OleDbConnection connectionDB_KSPR = new OleDbConnection(ConnectionString);
                            try
                            {
                                String ResponsibleExecutorQuery = "SELECT naim FROM KSPR WHERE kod_spr = 10 and kod_naim = " + dataReader[5].ToString();
                                connectionDB_KSPR.Open();
                                OleDbCommand popCommand_KSPR = new OleDbCommand(ResponsibleExecutorQuery, connectionDB_KSPR);
                                OleDbDataReader dataReaderKSPR = popCommand_KSPR.ExecuteReader();
                                dataReaderKSPR.Read();
                                kspr = dataReaderKSPR[0].ToString();
                            }
                            catch (Exception Exp)
                            {
                                if (kspr == String.Empty)
                                    emptyFields++;
                                else
                                {
                                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                            finally
                            {
                                connectionDB_KSPR.Close();
                            }

                            notExecute.Add(new NotExecute(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString(), (dataReader[3].ToString() != String.Empty) ? dataReader[3].ToString().Remove(10, dataReader[3].ToString().Length - 10) : String.Empty, dataReader[4].ToString(), dataReader[5].ToString(), kspr, dataReader[6].ToString(), (dataReader[7].ToString() != String.Empty) ? dataReader[7].ToString().Remove(10, dataReader[7].ToString().Length - 10) : String.Empty, (dataReader[8].ToString() != String.Empty) ? dataReader[8].ToString().Remove(10, dataReader[8].ToString().Length - 10) : String.Empty, dataReader[9].ToString(), (dataReader[10].ToString() != String.Empty) ? dataReader[10].ToString().Remove(10, dataReader[10].ToString().Length - 10) : String.Empty, (dataReader[11].ToString() != String.Empty) ? dataReader[11].ToString().Remove(10, dataReader[11].ToString().Length - 10) : String.Empty, (dataReader[12].ToString() != String.Empty) ? dataReader[12].ToString().Remove(10, dataReader[12].ToString().Length - 10) : String.Empty, (dataReader[13].ToString() != String.Empty) ? dataReader[13].ToString().Remove(10, dataReader[13].ToString().Length - 10) : String.Empty, (dataReader[14].ToString() != String.Empty) ? dataReader[14].ToString().Remove(10, dataReader[14].ToString().Length - 10) : String.Empty));
                        }
                    }
                    catch (Exception Exp)
                    {
                        MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    finally
                    {
                        connectionDB.Close();
                    }
                    notExecutes = notExecute;

                    if (Properties.Settings.Default.checkBox_sort_card)
                        notExecutes.Sort((first, second) => Convert.ToInt32(first.Number).CompareTo(Convert.ToInt32(second.Number)));

                    notExecutes.Reverse();

                    printDocument.Print();
                    MessageBox.Show("Карты успешно сформированы! Карты без ответственного исполнителя : " + emptyFields + ". Карт сформировано: " + complete, "Выполнено", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch(Exception Exp)
                {
                    MessageBox.Show(Exp.Message + Environment.NewLine + Environment.NewLine + Exp.TargetSite, Exp.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
            }
        }

        public static void PrintPageEvent(System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (notExecutes.Count > 2)
            {
                PrintCardV1(e);
                PrintCardV2(e);
                notExecutes.RemoveRange(notExecutes.Count - 2, 2);
                complete += 2;
                e.HasMorePages = true;
            }
            else
            {
                if (notExecutes.Count == 2)
                {
                    PrintCardV1(e);
                    PrintCardV2(e);
                    complete += 2;
                }

                if(notExecutes.Count == 1)
                {
                    PrintCardV1(e);
                    complete++;
                }
                e.HasMorePages = false;
            }
        }
    }
}
