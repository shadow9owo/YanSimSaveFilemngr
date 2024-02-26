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

namespace YanSimSaveFilemngr
{
    public partial class form : Form
    {
        private static int savei;
        public form(int saveint)
        {
            savei = saveint;
            InitializeComponent();
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
            Environment.Exit(0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_MasksBanned");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_MasksBanned"), 0);
            }
            else
            {
                MessageBox.Show("err masksbanned value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_NoJournalist");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_NoJournalist"), 1);
            }
            else
            {
                MessageBox.Show("err nojournalist value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_CanBefriendCouncil");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_CanBefriendCouncil"), 1);
            }
            else
            {
                MessageBox.Show("err canbefriendcouncil value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_HighSecurity");
            if (output != null)
            {
                savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_HighSecurity"), 0);
            }
            else
            {
                MessageBox.Show("err highsecurity value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0x40 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                    string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money");
                    if (output != null)
                    {
                        try
                        {
                            byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x40 };
                            savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money"), binaryData);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }

                    }
                }
            }
            else 
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money");
                if (output != null)
                {
                    try
                    {
                        byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x97, 0x60 };
                        savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Money"), binaryData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x59, 0xC0 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("you can only update the reputation after youve finished a school day\n\nif you didnt finish a school go and finish it and when youre in your room activate it\nif you wont do that then well an error will accure.\n\nso are you sure you want to update the reputation?", "warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_");
                if (output != null)
                {
                    byte[] binaryData = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    savemngr.SetBinary(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Reputation_"), binaryData);
                }
                else
                {
                    MessageBox.Show("err reputation value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox1.Text != " ")
            {
                Int32.TryParse(textBox1.Text, out int a);
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_BonusStudyPoints_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_BonusStudyPoints_"), a);
                }
                else
                {
                    MessageBox.Show("err Bonusstudypoints value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentDead");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentDead_" + i), 1);
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_PantyShots");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_PantyShots"), a);
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentDead");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentDead_" + i), 0);
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentExpelled_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentExpelled_" + i), 1);
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentExpelled_");
                if (output != null)
                {
                    savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_StudentExpelled_" + i), 0);
                }
                else
                {
                    MessageBox.Show("err PantyShots (infopoints) value not found or is null", "err", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                string output = savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Numbness");
                if (output != null)
                {
                    try
                    {
                        timer2.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            savemngr.SetInt(savemngr.FindKeysWithCustomStrings("Profile_" + savei.ToString(), "_Numbness"), 0);
        }
    }
}