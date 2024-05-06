using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace YanSimSaveFilemngr
{
    public partial class form : Form
    {
        private static bool _90smode = false;
        private static int savei;

        public form(int saveint)
        {
            savei = saveint;
            InitializeComponent();
        }

        public static string _90smodepatch()
        {
            if (_90smode)
            {
                switch (savei)
                {
                    case 1:
                        return "1";
                    case 2:
                        return "2";
                    case 3:
                        return "3";
                    case 4:
                        return "";
                    default:
                        return "";
                }
            }else
            {
                return "";
            }
        }

        private void form_Load(object sender, EventArgs e)
        {
            label2.Text = "current save slot int: " + savei.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            credits credits = new credits();
            credits.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("this tool works by overwriting the values in the registry. (basically modifing the yandere simulator savefile)\n\nThis tool is pretty simple as it basically just takes an advantage of playerprefs being unsecure.\n(not encoded or encrypted thoe tbh theres no need for that)", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                if (p.ProcessName.ToLower().Contains("yandere"))
                {
                    savemngr.injecttxt(p.Id, "YandereSimulator");
                    break;
                }
            }

            Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_MasksBanned");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_MasksBanned"), 0);
                listBox1.Items.Add("masksbanned value set to 0 (true)");
            }
            else
            {
                MessageBox.Show("err masksbanned value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listBox1.Items.Add("err masksbanned value not found or is null");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_NoJournalist");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_NoJournalist"), 1);
                listBox1.Items.Add("nojournalist value set to 1 (true)");
            }
            else
            {
                MessageBox.Show("err nojournalist value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listBox1.Items.Add("err nojournalist value not found or is null");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_CanBefriendCouncil");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_CanBefriendCouncil"), 1);
                listBox1.Items.Add("CanBefriendCouncil value set to 1 (true)");
            }
            else
            {
                MessageBox.Show("err canbefriendcouncil value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listBox1.Items.Add("err canbefriendcouncil value not found or is null");
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_HighSecurity");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_HighSecurity"), 0);
                listBox1.Items.Add("highsecurity value set to 0 (false)");
            }
            else
            {
                MessageBox.Show("err highsecurity value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listBox1.Items.Add("err highsecurity value not found or is null");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0x40 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_"), binaryData);
                    listBox1.Items.Add("reputation value set to: 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0x40");
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err reputation value not found or is null");
                }
            }
        }
        //infinite money
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                DialogResult dr = MessageBox.Show("are you sure you want to uncheck this checkbox?\nas if youl uncheck this checkbox your money value will be reseted to the default value", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    checkBox1.Checked = true;
                }else
                {
                    string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Money");
                    if (output != null)
                    {
                        try
                        {
                            byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x40 };
                            savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Money"), binaryData);
                            listBox1.Items.Add("money value set to: 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x40 aka 10.00 (default val)");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                            listBox1.Items.Add("An error occurred: " + ex.Message);
                        }

                    }
                }
            }
            else 
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Money");
                if (output != null)
                {
                    try
                    {
                        byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x97, 0x60 };
                        savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Money"), binaryData);
                        listBox1.Items.Add("money value set to: 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x97, 0x60 aka inf");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                        listBox1.Items.Add("An error occurred: " + ex.Message);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0xC0 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_"), binaryData);
                    listBox1.Items.Add("reputation value set to: 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0xC0");
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err reputation value not found or is null");
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Reputation_"), binaryData);
                    listBox1.Items.Add("reputation value set to: 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 aka null");
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err reputation value not found or is null");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != " ")
            {
                Int32.TryParse(textBox1.Text, out int a);
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_BonusStudyPoints_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_BonusStudyPoints_"), a);
                    listBox1.Items.Add($"err Bonusstudypoints value set to: {a}");
                }
                else
                {
                    MessageBox.Show("err Bonusstudypoints value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err Bonusstudypoints value not found or is null");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentDead");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentDead_" + i), 1);
                }
                else
                {
                    MessageBox.Show("err StudentDead value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err StudentDead value not found or is null");
                    break;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != null && textBox3.Text != " ")
            {
                Int32.TryParse(textBox3.Text, out int a);
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_PantyShots");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_PantyShots"), a);
                    listBox1.Items.Add($"PantyShots (infopoints) value set to {a}");
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err PantyShots (infopoints) value not found or is null");
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentDead");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentDead_" + i), 0);
                }
                else
                {
                    MessageBox.Show("err StudentDead value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err StudentDead value not found or is null");
                    break;
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentExpelled_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentExpelled_" + i), 1);
                }
                else
                {
                    MessageBox.Show("err StudentExpelled value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err StudentExpelled value not found or is null");
                    break;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentExpelled_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_StudentExpelled_" + i), 0);
                }
                else
                {
                    MessageBox.Show("err StudentExpelled value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    listBox1.Items.Add("err StudentExpelled value not found or is null");
                    break;
                }
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked)
            {
                timer2.Enabled = false;
            }
            else
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Numbness");
                if (output != null)
                {
                    try
                    {
                        timer2.Enabled = true;
                        listBox1.Items.Add("Numbness locked");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                        listBox1.Items.Add("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Numbness"), 0);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcesses();

            foreach (Process p in processes)
            {
                if (p.ProcessName.ToLower().Contains("yandere"))
                {
                    savemngr.injecttxt(p.Id,"YandereSimulator " + textBox2.Text.ToString());
                    listBox1.Items.Add($"Yandere Simulator title changed to: {"YandereSimulator " + textBox2.Text.ToString()}");
                    break;
                }
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (savei != 4)
            {
                _90smode = checkBox4.Checked;
                Console.WriteLine(_90smode);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Triggering the anticheat will wipe your save file do you want to backup your save file before you trigger it?", "question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                DateTime currentTime = DateTime.UtcNow;
                string formattedTime = currentTime.ToString("MMddyyyy");
                if (savemngr.ExportToRegistry($"output{formattedTime}.reg"))
                {
                    MessageBox.Show("save file was exported successfully into the current directory", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listBox1.Items.Add("save file was exported successfully into the current directory");
                }
                else
                {
                    MessageBox.Show("an error accured while trying to export the save files", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBox1.Items.Add("an error accured while trying to export the save files");
                }
            }
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Week_");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString() + _90smodepatch(), "_Week_"), 99);
            }
            else
            {
                MessageBox.Show("err Week value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listBox1.Items.Add("err Week value not found or is null");
            }
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("YandereSimulator.exe");

            if (processes.Length < 1)
            {
                MessageBox.Show($"Process YandereSimulator.exe was not found in memory", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listBox1.Items.Add("Process YandereSimulator.exe was not found in memory");
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Dynamic Link Libraries (*.dll)|*.dll|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (dllinject.InjectDll("YandereSimulator.exe", openFileDialog.FileName) != true)
                {
                    MessageBox.Show($"An exception accured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listBox1.Items.Add("DLL injection failed");
                } 
            }
            else
            {
                MessageBox.Show("DLL injection cancelled","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
                listBox1.Items.Add("DLL cancelled");
            }
        }
    }
}